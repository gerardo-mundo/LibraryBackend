using LibraryBackend.context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace LibraryBackend
{
    public class Startup
    {
        public Startup(ConfigurationManager configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public OpenApiLicense MIT { get; private set; } = null!;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            }).AddNewtonsoftJson();

            services.AddEndpointsApiExplorer();

            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            services.AddSwaggerGen(c => 
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Library WebApi", Description = "WebApi for library management", License = MIT, Version = "v1", 
                Contact = new OpenApiContact() { Name = "Gerardo Mundo", Email = "gerardo.perez@udgvirtual.udg.mx" }
            }));

            services.AddAutoMapper(typeof(Startup));

            services.AddDataProtection();

            services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyMethod()
            .WithOrigins("")));
        }

       public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
