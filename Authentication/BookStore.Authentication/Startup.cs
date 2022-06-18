using Authentication.Application.Interface;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Repository;
using Authentication.Infrastructure.Service;
using Bookstore.Service;
using Bookstore.Service.Interface;
using Bookstore.Service.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BookStore.Authentication
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
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var jwtSettings = Configuration.GetSection("JwtSettings");

            services.AddScoped<IFaculty, FacultyRepository>();
            services.AddScoped<IBook, BookRepository>();
            services.AddScoped<IAuthor, AuthorRepository>();
            services.AddScoped<IBookAuthor, BookAuthorRepository>();
            services.AddScoped<EmailService>();
            services.AddControllers();
            services.AddScoped<Credentials>();
            services.AddDbContext<DbService>(s =>
            {
                s.UseSqlServer(Configuration["ConnectionStrings:Authentication"]).EnableSensitiveDataLogging();
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    AuthenticationType = "Bearer",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'authenticationv'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
           

            services.AddDbContext<AuthDbService>(s => {
                s.UseSqlServer(Configuration["ConnectionStrings:Authentication"],s=> s.MigrationsAssembly(migrationsAssembly)).EnableSensitiveDataLogging();
            });
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AuthDbService>().AddDefaultTokenProviders();
            
            


            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequiredLength = 6;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.LoginPath = PathString.Empty;
                options.AccessDeniedPath = PathString.Empty;

            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("authenticationv1", new OpenApiInfo()
                {
                    Title = "BookStore API",
                    Version = "1.0",
                    Description = "Bookstore",
                    Contact = new OpenApiContact()
                    {
                        Email = "adeolaaderibigbe09@gmail.com",
                        Name = "Adeola Aderibigbe",
                        Url = new Uri("https://github.com/Adexandria")

                    }

                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {

                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                    BearerFormat = "bearer",
                    Description = "Enter Token Only"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                         new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearer"
                                }
                            },
                          new string[] {}
                    }

                });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                //... and tell Swagger to use those XML comments.
                c.IncludeXmlComments(xmlPath);
            });
            
        }

      
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

          
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    setupAction.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",description.GroupName);
                }
                setupAction.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
