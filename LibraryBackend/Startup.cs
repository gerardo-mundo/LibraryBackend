using LibraryBackend.context;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LibraryBackend.Utilities;
using System.IdentityModel.Tokens.Jwt;
using LibraryBackend.Filters;
using LibraryNetCoreAPI.Models;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace LibraryBackend
{
    public class Startup
    {
        public Startup(ConfigurationManager configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public OpenApiLicense MIT { get; private set; } = null!;

        public void ConfigureServices(IServiceCollection services)
        {
            var JwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
            var ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            var ClientUrl = Environment.GetEnvironmentVariable("CLIENT_URL");

            services
            .AddControllers(options => options.Filters.Add(typeof(FilterExceptions)))
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            })
            .AddNewtonsoftJson();

            services.AddEndpointsApiExplorer();

            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0, 0))));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey!)),
                    ClockSkew = TimeSpan.Zero
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Library WebApi",
                    Description = "WebApi for library management",
                    License = MIT,
                    Version = "v1",
                    Contact = new OpenApiContact() { Name = "Gerardo Mundo", Email = "gerardo.perez@udgvirtual.udg.mx" }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new String[]{}
                    }
                });
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddIdentity<ApplicationIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options => options.AddPolicy("IsAdmin", policy =>
            policy.RequireClaim("isAdmin")));

            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder
                .WithOrigins(ClientUrl!)
                .AllowAnyMethod()
                .AllowAnyHeader()
            ));

            services.AddApplicationInsightsTelemetry();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/api/v1/swagger.json", "Library API V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            PrepareDB.Population(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
