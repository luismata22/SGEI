using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using SGEI.Interfaces;
using SGEI.Repository;
using SGEI.Interfaces.mail;
using SGEI.Repository.mail;

namespace SGEI.ApiConfiguration
{
  public static class ServiceConfiguration
  {
    private const string AllowSpecificOrigins = "AllowAllOrigin";

    internal static void ConfigureDependencies(this IServiceCollection services)
    {
      //Authenticate
      services.AddTransient<ILoginRepository, LoginRepository>();

      //email
      services.AddTransient<IMailService, MailService>();

      //Role
      services.AddTransient<IRoleRepository, RoleRepository>();

      //User
      services.AddTransient<IUserRepository, UserRepository>();

      //Student
      services.AddTransient<IStudentRepository, StudentRepository>();

      //Person
      services.AddTransient<IPersonRepository, PersonRepository>();
    }

    internal static void ConfigureLocalization(this IServiceCollection services)
    {
      services.AddLocalization(opt => opt.ResourcesPath = "Resources");
      services.Configure<RequestLocalizationOptions>(options =>
      {
        List<CultureInfo> supportedCultures = new List<CultureInfo>();
        options.DefaultRequestCulture = new RequestCulture("es");
        options.SupportedCultures = supportedCultures;
      });
    }

    internal static void UseExceptionMiddleware(this IApplicationBuilder app) => app.UseMiddleware(typeof(ExceptionHandler));

    internal static void UseLocalization(this IApplicationBuilder app)
    {
      IOptions<RequestLocalizationOptions> localizedOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
      app.UseRequestLocalization(localizedOptions.Value);
    }

    internal static void ConfigureCors(this IServiceCollection services)
    {
      services.AddCors(options => options.AddPolicy(
          AllowSpecificOrigins,
          builder =>
          {
            builder.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
          }));
    }

    internal static void UseCorsDev(this IApplicationBuilder app) => app.UseCors(AllowSpecificOrigins);
  }
}
