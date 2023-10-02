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
using my_books.Data;
using my_books.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books
{
    public class Startup
    {
        public string ConnectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DefaultConnectionString"); //appsettings.json dosyasýndaki "defaultconnectionstring"
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //confgure DB context with SQL
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString)); //appdbcontext servisi ekleyip içine yukarda oluþturduðumuz connection stringi verdik.
         
            //configure the services

            services.AddTransient<BooksService>(); //BooksService kullanabilmek için burada ekleme yaptýk.
            services.AddTransient<AuthorsService>(); //AuthorsService kullanabilmek için burada ekleme yaptýk.
            services.AddTransient<PublishersService>();  //PublishersService kullanabilmek için burada ekleme yaptýk.


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "my_books_updated_title", Version = "v2" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "my_books_ui_updated v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //AppDbInitializer den seed methodunu çaðýrmýþtýk daha önce hiç verimiz yoksa bu veri ekliyordu bunu iptal edicez.
            //Bunu Book servisi dýþýnda diðer Servis ve Controlleri oluþturduktan sonra iptal ettik çünkü AppDbInitializer de context.savechanges(); methodu hata verdi.
           
            //AppDbInitializer.Seed(app);
        }
    }
}
