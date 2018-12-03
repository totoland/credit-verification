using CardVerifyCore.Models;
using CreditCardVerification.Interfaces.IRepositories;
using CreditCardVerification.Interfaces.IServices;
using CreditCardVerification.Interfaces.Repositories;
using CreditCardVerification.Interfaces.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CardVerifyCore
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
            services.AddDbContext<CreditCardContext>();
            services.AddMvc();

            services.AddTransient(typeof(ICreditCardService), typeof(CreditCardService));
            services.AddTransient(typeof(ICreditCardRepository), typeof(CreditCardRepository));

            services.Configure<AppSettings>(Configuration.GetSection("appsettings"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Credit card API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Credit card API V1");
            });

            app.UseMvc();
        }
    }
}
