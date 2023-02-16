using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SGEI.ApiConfiguration;
using SGEI.Context;
using SGEI.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGEI
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.ConfigureDependencies();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "SGEI", Version = "v1" });
        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
      });
      //services.ConfigureSwagger();

      var connectionString = Configuration.GetConnectionString("PostgreSqlConnection");

      services.AddDbContext<SGEIContext>(options =>
      {
        options.UseNpgsql(connectionString);
      });

      services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

      services.AddCors();
      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

      
      

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SGEI v1"));
      }

      // CORS - Allow calling the API from WebBrowsers
      app.UseCors(x => x
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials()
          //.WithOrigins("http://localhost:34337")); // Allow only this origin can also have multiple origins seperated with comma
          .SetIsOriginAllowed(origin => true));// Allow any origin  

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      //app.UseSwaggerUI();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
