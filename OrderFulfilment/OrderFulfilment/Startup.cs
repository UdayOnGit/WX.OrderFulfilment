using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WX.OrderFulfilment.Mapping;
using WX.OrderFulfilment.Middleware;
using WX.OrderFulfilment.Repository;
using WX.OrderFulfilment.Services;

namespace WX.OrderFulfilment
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WX.OrderFulfilment", Version = "v1" });
            });
            services.AddScoped<IRepository, OrderDetailsRepository>();
            services.AddScoped<IUserDetailsService, UserDetailsService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IGetWooliesProducts, GetWooliesProducts>();
            
            services.AddScoped<GetProductsSortedLowToHigh>();
            services.AddScoped<GetProductsSortedHighToLowPrice>();
            services.AddScoped<GetProductsWithAscendingName>();
            services.AddScoped<GetProductsWithDescendingName>();
            services.AddScoped<GetRecommendedProducts>();

            services.AddScoped<Func<SortOptionEnum, IGetProducts>>
            (x => key =>
             {
                 return key switch
                 {
                     SortOptionEnum.Low => x.GetService<GetProductsSortedLowToHigh>(),
                     SortOptionEnum.High => x.GetService<GetProductsSortedHighToLowPrice>(),
                     SortOptionEnum.Ascending => x.GetService<GetProductsWithAscendingName>(),
                     SortOptionEnum.Descending => x.GetService<GetProductsWithDescendingName>(),
                     SortOptionEnum.Recommended => x.GetService<GetRecommendedProducts>(),
                     _ => throw new InvalidOperationException(),
                 };
             });

            services.AddAutoMapper(typeof(ModelToResourceProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WX.OrderFulfilment v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
