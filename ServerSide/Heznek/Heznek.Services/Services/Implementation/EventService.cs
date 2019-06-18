using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heznek.Common.Enums;
using Heznek.DataAccess.Entities;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Models;
using Heznek.Services.Providers;
using Heznek.DataAccess.Extensions;

namespace Heznek.Services.Implementation
{
    public class EventService : AbstractService, IEventService
    {
        public EventService(IUnitOfWork unitOfWork, IAuthenticatedUser authUser)
            : base(unitOfWork, authUser)
        {
        }

        public List<EventModel> GetByAdmin()
        {
            return _unitOfWork.Repository<EventEntity>().Set
                .Select(x => new EventModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    Name = x.Name,
                    Expected = x.Expected,
                    Location = x.Location,
                    Subject = x.Subject,
                    Time = x.Time,
                    FinishTime = x.FinishTime,
                    ParticipantTypes = x.ParticipantTypes.Select(n =>n.UserType).ToList(),
    
                }).ToList();
        }
        public EventModel GetById(int id)
        {
            var notAttend = _unitOfWork.Repository<ProfileEntity>().Set
                .Where(x => !x.AttendingEvents.Any(n => n.EventId == id))
                .Select(x => new UserModel
                {
                    Id = x.UserId,
                    Email = x.User.Email,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Role = x.User.Role
                })
                .ToList();
            var result = _unitOfWork.Repository<EventEntity>().Set
                .IncludeEntity(x=>x.Attendees)
                    .ThenIncludeEntity(x=>x.Profile)
                        .ThenIncludeEntity(x=>x.User)
                .Where(x=>x.Id == id)
                .Select(x => new EventModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    Name = x.Name,
                    Expected = x.Expected,
                    Location = x.Location,
                    Subject = x.Subject,
                    Time = x.Time,
                    FinishTime = x.FinishTime,
                    ParticipantTypes = x.ParticipantTypes.Select(n => n.UserType).ToList(),
                    Attending = x.Attendees.Select(n=>new UserModel
                    {
                        Id = n.Profile.UserId,
                        Email = n.Profile.User.Email,
                        FirstName = n.Profile.User.FirstName,
                        LastName = n.Profile.User.LastName,
                        Role = n.Profile.User.Role
                    }).ToList()

                }).FirstOrDefault();
            if (result != null)
            {
                result.NotAttending = notAttend;
            }
            return result;
        }

        public async Task<EventModel> Create(EventModel model)
        {
            var entity = new EventEntity
            {
                Id = 0,
                Date = model.Date,
                Expected = model.Expected,
                Location = model.Location,
                Name = model.Name,
                Subject = model.Subject,
                Time = model.Time,
                FinishTime = model.FinishTime,
                ParticipantTypes = model.ParticipantTypes.Select(x => new EventParticipantTypeEntity
                {
                    UserType = x
                }).ToList()
            };

            await _unitOfWork.Repository<EventEntity>().InsertAsync(entity);
            model.Id = entity.Id;
            return model;
        }

        public async Task<EventModel> Update(EventModel model)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                var entity = _unitOfWork.Repository<EventEntity>()
                    .Include(x => x.ParticipantTypes)
                    .FirstOrDefault(x => x.Id == model.Id);
                if (entity != null)
                {
                    entity.Date = model.Date;
                    entity.Expected = model.Expected;
                    entity.Location = model.Location;
                    entity.Name = model.Name;
                    entity.Subject = model.Subject;
                    entity.Time = model.Time;
                    entity.FinishTime = model.FinishTime;

                    _unitOfWork.Repository<EventParticipantTypeEntity>().DeleteRange(entity.ParticipantTypes);
                    var participantTypes = model.ParticipantTypes.Select(x => new EventParticipantTypeEntity
                    {
                        EventId = entity.Id,
                        UserType = x
                    }).ToList();
                    await _unitOfWork.Repository<EventParticipantTypeEntity>().InsertRangeAsync(participantTypes);
                    await _unitOfWork.Repository<EventEntity>().UpdateAsync(entity);
                    tran.Commit();
                    return model;
                }

                return null;
            }
        }

        public void Delete(int Id)
        {
            using(var tran = _unitOfWork.BeginTransaction())
            {
                var entity = _unitOfWork.Repository<EventEntity>().Include(x => x.Attendees, x => x.ParticipantTypes)
                    .FirstOrDefault(x => x.Id == Id);

                _unitOfWork.Repository<EventAttendeeEntity>().DeleteRange(entity.Attendees);
                _unitOfWork.Repository<EventParticipantTypeEntity>().DeleteRange(entity.ParticipantTypes);
                _unitOfWork.Repository<EventEntity>().Delete(entity);

                tran.Commit();
            }
        }

        public async Task Attend(AttendEventModel model)
        {
            var entity = _unitOfWork.Repository<EventAttendeeEntity>().Set
                .FirstOrDefault(x => x.Profile.UserId == _authUser.Id && x.EventId == model.EventId);
            if (model.Attend)
            {
                if(entity == null)
                {
                    var profile = _unitOfWork.Repository<ProfileEntity>().Set.FirstOrDefault(x => x.UserId == _authUser.Id);
                    await _unitOfWork.Repository<EventAttendeeEntity>().InsertAsync(new EventAttendeeEntity
                    {
                        EventId = model.EventId,
                        ProfileId = profile.Id
                    });
                }
            }
            else
            {
                if(entity!= null)
                {
                    _unitOfWork.Repository<EventAttendeeEntity>().Delete(entity);
                }
            }
        }

        public List<EventModel> GetMyEvents()
        {
            var profile = _unitOfWork.Repository<ProfileEntity>().Set.FirstOrDefault(x => x.UserId == _authUser.Id);
            return _unitOfWork.Repository<EventEntity>().Set.Where(x => x.ParticipantTypes.Any(n => n.UserType == profile.Status))
                .Select(x => new EventModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    Name = x.Name,
                    Expected = x.Expected,
                    Location = x.Location,
                    Subject = x.Subject,
                    Time = x.Time,
                    FinishTime = x.FinishTime,
                    Attend = x.Attendees.Any(n=>n.ProfileId == profile.Id),
                    ParticipantTypes = x.ParticipantTypes.Select(n => n.UserType).ToList(),

                }).ToList();
        }
    }
}
