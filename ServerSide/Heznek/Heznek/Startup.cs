using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using Heznek.API.Crypto;
using Heznek.API.Extensions;
using Heznek.API.Policies;
using Heznek.API.Providers;
using Heznek.Common.DomainTaskStatus;
using Heznek.CompositionRoot;
using Heznek.DataAccess;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Crypto;
using Heznek.Services.Providers;
using Heznek.Services.Options;
using Swashbuckle.AspNetCore.Swagger;
using Heznek.Common.Email;
using Heznek.API.ViewRender;
using Heznek.Common.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http.Internal;

namespace Heznek
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Compositor.Compose(services);

            services.ConfigureFromSection<CryptoOptions>(Configuration);
            services.ConfigureFromSection<JwtOptions>(Configuration);
            services.ConfigureFromSection<EmailOptions>(Configuration);
            services.ConfigureFromSection<ResetPasswordOptions>(Configuration);
            services.ConfigureFromSection<AllowedExtensionsOptions>(Configuration);

            services.AddSingleton<ICryptoContext, AspNetCryptoContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthenticatedUser, AuthenticatedUserProvider>();
            services.AddScoped<IRazorViewRenderer, RazorViewRenderer>();
            services.AddScoped<IAuthTokenProvider, AuthTokenProvider>();

            services.AddScoped(typeof(DomainTaskStatus));

            services.AddAuthorization(options => options.AddPolicy(AuthPolicies.AuthenticatedUser, AuthenticatedUserPolicy.Policy));

            services.AddDbContext<HeznekDbContext>(o =>
            {
                string connStr = Configuration.GetConnectionString("Development");
                if (String.IsNullOrWhiteSpace(connStr))
                {
                    throw new Exception($"No connection string defined for {_hostingEnvironment.EnvironmentName}");
                }
                o.UseSqlServer(connStr);
            }, ServiceLifetime.Scoped);

            services.AddCors(o => o.AddPolicy("CORS", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMvc().AddJsonOptions(options =>
             {
                 //options.SerializerSettings.Converters.Add(new StringEnumConverter
                 //{
                 //    AllowIntegerValues = true,
                 //    CamelCaseText = false
                 //});
                 options.SerializerSettings.DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTimeOffset;
                 options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                 options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
             })
           .AddFluentValidation(o =>
           {
               o.RegisterValidatorsFromAssemblyContaining<Startup>();
           });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration.GetSection("Jwt")["ValidIssuer"],
                    ValidAudience = Configuration.GetSection("Jwt")["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt")["Key"]))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Heznek", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            //if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CORS");

            app.UseAuthentication();

            app.UseSwagger();

            app.UseStaticFiles();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Heznek V1");
                c.EnableFilter();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}"
                    );

                routes.MapRoute(
                    name: "catch-all",
                    template: "{*url}",
                    defaults: new { controller = "Home", action = "Index" }
                    );
            });

            serviceProvider.GetService<HeznekDbContext>().EnsureSeeded();
        }
    }
    
}