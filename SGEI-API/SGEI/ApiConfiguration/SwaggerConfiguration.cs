using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SGEI.ApiConfiguration
{
    public static class SwaggerConfiguration
    {
        /// <summary>
        ///     <para>OSMMaster API v2.</para>
        /// </summary>
        private const string EndpointName = "Sistema de gestión educativa integral Api v2";

        /// <summary>
        ///     <para>/swagger/v1/swagger.json.</para>
        /// </summary>
        private const string EndpointUrl = "/swagger/v2/swagger.json";

        /// <summary>
        ///     <para>v1.</para>
        /// </summary>
        private const string DocNameV2 = "v2";

        /// <summary>
        ///     <para>Foo API.</para>
        /// </summary>
        private const string DocInfoTitle = "Sistema de gestión educativa integral API";

        /// <summary>
        ///     <para>v1.</para>
        /// </summary>
        private const string DocInfoVersion = "v2";

        /// <summary>
        ///     <para>Authorization Api - OSMMaster API in ASP.NET Core 3.1.</para>
        /// </summary>
        private const string DocInfoDescription = "CentralIT Api";

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc(
                    DocNameV2,
                    new OpenApiInfo
                    {
                        Title = DocInfoTitle,
                        Version = DocInfoVersion,
                        Description = DocInfoDescription
                    });
            });
        }

        public static void UseSwaggerUi(this IApplicationBuilder app)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(config => { config.SwaggerEndpoint(EndpointUrl, EndpointName); });
        }
    }
}
