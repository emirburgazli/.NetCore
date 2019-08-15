 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstProject.CustomMiddlewares;
using FirstProject.DataAccess;
using FirstProject.Formatters;
using FirstProject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FirstProject
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
            services.AddScoped<IProductDal, EfProductDal>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            var connection = @"Server='DESKTOP - 0TDUATN\SQLEXPRESS'; Database=SchoolDb; Trusted_Connection=true; ";
            services.AddDbContext<SchoolContext>(options => options.UseSqlServer(connection));



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<AuthenticationMiddleware>();
            //app.UseHttpsRedirection();
            app.UseMvc(config =>
            {
                //config.MapRoute("DefaultRounte", "api/{controller}/{action}");
            });
        }
    }
}
