using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace ItemsApi
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

			if (Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING").Length == 0)
			{
				Exception ex = new Exception("Undefined environment variable POSTGRES_CONNECTION_STRING");
				throw (ex);
			}

			String postgresConnectionString = String.Format("{0}; Pooling = true;", Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING"));
            
            
           
            services.AddDbContext<WebAPIDataContext>(options => {
				options.UseNpgsql(postgresConnectionString, b => b.MigrationsAssembly("WebAPISample"));
            });

            services.AddMvc();

			// Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Items API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

			// Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Items API V1");
            });


            app.UseStaticFiles();

			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

			app.Use(async (context, next) =>
            {
                Console.WriteLine($"[{context.Connection.RemoteIpAddress}] | {context.Request.Path}");
                await next.Invoke();
            });
        }
    }
}
