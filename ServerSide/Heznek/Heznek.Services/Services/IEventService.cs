using Heznek.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Services
{
    public interface IEventService
    {
        List<EventModel> GetByAdmin();
        EventModel GetById(int Id);
        Task<EventModel> Create(EventModel model);
        Task<EventModel> Update(EventModel model);
        void Delete(int Id);
        Task Attend(AttendEventModel model);
        List<EventModel> GetMyEvents();
    }
}
