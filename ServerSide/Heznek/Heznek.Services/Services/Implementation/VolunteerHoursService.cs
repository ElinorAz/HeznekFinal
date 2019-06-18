using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heznek.DataAccess.Entities;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Models;
using Heznek.Services.Providers;

namespace Heznek.Services.Implementation
{
    public class VolunteerHoursService : AbstractService, IVolunteerHoursService
    {
        public VolunteerHoursService(IUnitOfWork unitOfWork, IAuthenticatedUser authUser) 
            : base(unitOfWork, authUser)
        {
        }

        public SeparatedVolunteerHoursModel GetMyHours()
        {
            return this.GetHours(_authUser.Id);   
        }

        public SeparatedVolunteerHoursModel GetHours(string userId)
        {
            var hoursAll = _unitOfWork.Repository<VolunteerHoursEntity>().Set
                .Where(x => x.Profile.UserId == userId)
                .Select(x => new VolunteerHoursModel
                {
                    Id = x.Id,
                    ActivityType = x.ActivityType,
                    Date = x.Date,
                    End = x.End,
                    Start = x.Start,
                    Semester = x.Semester
                })
                .ToList();
            var result = new SeparatedVolunteerHoursModel
            {
                VolunteerHoursA = hoursAll.Where(x => x.Semester == Common.Enums.SemesterEnum.A).ToList(),
                VolunteerHoursB = hoursAll.Where(x => x.Semester == Common.Enums.SemesterEnum.B).ToList(),
                VolunteerHoursSummer = hoursAll.Where(x => x.Semester == Common.Enums.SemesterEnum.Summer).ToList()
            };

            var hours = new TimeSpan();
            result.VolunteerHoursA.ForEach(v =>
            {
                var _hours = TimeSpan.Parse(v.End) - TimeSpan.Parse(v.Start);
                if (_hours.TotalMinutes > 0)
                {
                    hours += _hours;
                }
            });
            result.VolunteerHoursB.ForEach(v =>
            {
                var _hours = TimeSpan.Parse(v.End) - TimeSpan.Parse(v.Start);
                if (_hours.TotalMinutes > 0)
                {
                    hours += _hours;
                }
            });
            result.VolunteerHoursSummer.ForEach(v =>
            {
                var _hours = TimeSpan.Parse(v.End) - TimeSpan.Parse(v.Start);
                if (_hours.TotalMinutes > 0)
                {
                    hours += _hours;
                }
            });

            result.HoursSpent = (int)hours.TotalHours;
            return result;

        }

        public VolunteerHoursModel Insert(VolunteerHoursModel model)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                var profileId = _unitOfWork.Repository<ProfileEntity>().Set.FirstOrDefault(x => x.UserId == _authUser.Id).Id;
                var entity = new VolunteerHoursEntity
                {
                    ActivityType = model.ActivityType,
                    Date = model.Date,
                    Start = model.Start,
                    End = model.End,
                    ProfileId = profileId,
                    Semester = model.Semester
                };
                _unitOfWork.Repository<VolunteerHoursEntity>().Insert(entity);
                tran.Commit();
                return model;
            }
        }
    }
}
