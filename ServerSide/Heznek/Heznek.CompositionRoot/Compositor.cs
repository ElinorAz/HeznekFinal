using System;
using Microsoft.Extensions.DependencyInjection;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services;
using Heznek.Services.Implementation;
using Heznek.Common.Email;
using Heznek.Services.Helpers;

namespace Heznek.CompositionRoot
{
    /// <summary>
    /// Register all services from here
    /// </summary>
    public static class Compositor
    {
        public static void Compose(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IForgotPaswordService, ForgotPaswordService>();
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IVolunteerHoursService, VolunteerHoursService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IScholarshipService, ScholarshipService>();
            services.AddScoped<IStudentScholarshipService, StudentScholarshipService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ITelephonyService, TelephonyService>();
            services.AddScoped<IStatisticService, StatisticService>();

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddSingleton<IFileHelper, FileHelper>();
        }
    }
}