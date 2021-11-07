using FastCore.Application;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FastCore.Api
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }      

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);
            services.AddScoped<IBookApplication, BookApplication>();
            
            services.AddRouting(options => options.LowercaseUrls = true); 
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_configuration.GetSection("AppConfiguration:Version").Value, new OpenApiInfo { Title = _configuration.GetSection("AppConfiguration:Name").Value, Version = _configuration.GetSection("AppConfiguration:Version").Value });
            });

            // POC - HealthChecks - Habilita o monitoramento das dependências da aplicação como banco de dados transacional e do cache distribuido.
            services.AddHealthChecks().AddSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            services.AddHealthChecks().AddRedis(_configuration.GetConnectionString("Cache"));
            services.AddHealthChecksUI(option => { option.MaximumHistoryEntriesPerEndpoint(50); }).AddInMemoryStorage();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FastCore.WebApi v1"));

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // POC - HealthChecks
                // Painel de Dashboard para exibir monitoramento da aplicação, acessar a url http://localhost:56929/health-ui.
                app.UseHealthChecksUI(options => { options.UIPath = "/health-ui"; });

                // POC - HealthChecks
                // Endpoint para o Dashboard que devolve o json, acessar a url http://localhost:56929/health.
                endpoints.MapHealthChecks("/health", new HealthCheckOptions() { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

                endpoints.MapControllers();
            });
        }
    }
}
