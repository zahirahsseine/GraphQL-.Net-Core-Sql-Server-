using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using GraphqlIt.DataAccess;
using GraphqlIt.DataBase;
using GraphqlIt.DataBase.Model;
using GraphqlIt.Mutations;
using GraphqlIt.Queries;
using GraphqlIt.Schema;
using GraphqlIt.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GraphqlIt
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
            services.AddMvc(MvcOptions => MvcOptions.EnableEndpointRouting = false);
            services.AddDbContext<GrapgQlContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:dbstring"]));
            services.AddTransient<IRepositoryProperty, RepositoryProperty>();
            services.AddTransient<IRepositoryPayment, RepositoryPayment>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddScoped<PropertyQuery>();
            services.AddScoped<PropertyMutation>();
            services.AddScoped<PorpertyType>();
            services.AddScoped<PropertyInputType>();
            services.AddScoped<PaymentType>();
            services.AddSingleton<ISchema>(new GraphQlSchema(new FuncServiceProvider(Type => getservice(services).GetService(Type))));
           


        }
        public ServiceProvider getservice(IServiceCollection sp)
        {
            return sp.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env, GrapgQlContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseGraphiQl();
            app.UseRouting();
            app.UseMvc();
            db.EnsureSeedData();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
