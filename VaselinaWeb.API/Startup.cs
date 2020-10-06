using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Data;
using Framework.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using VaselinaWeb.API.CustomExceptionMiddleware;
using VaselinaWeb.DataModel.Repositories;

namespace VaselinaWeb.API
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
            //AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnetion"), 
                b => b.MigrationsAssembly("VaselinaWeb.API")));
            services.AddControllers();
            services.AddCors();

            // Inyección de Repositorios
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ICambioPasswordRepository, CambioPasswordRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            // Definimos autenticación del aplicativo
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes("Miclavedecontraseña")),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Agregamos Swagger
            //services.AddSwaggerGen(options =>
            //{
            //    var groupName = "v1";

            //    options.SwaggerDoc(groupName, new OpenApiInfo
            //    {
            //        Title = $"Vaselina API {groupName}",
            //        Version = groupName,
            //        Description = "Vaselina API",
            //        Contact = new OpenApiContact
            //        {
            //            Name = "Vaselina",
            //            Email = string.Empty,
            //            Url = new Uri("https://github.com/AlejandroGilVelez")
            //        }
            //    });
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();               
            }

            // Para agregar el Middleware
            app.UseMiddleware<ExceptionMiddleware>();

            // Allow all request
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            // Agregamos Swagger
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vaselina API V1");
            //});

            app.UseRouting();

            // Se agrega al momento de definir la autenticación del aplicativo.
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
