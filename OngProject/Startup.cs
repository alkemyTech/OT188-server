using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OngProject.DataAccess;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.Text;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using Amazon.S3;
using OngProject.Core.Helper;
using OngProject.Core.Mapper;
using OngProject.Middleware;

namespace OngProject
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
            services.AddDbContext<OngProjectDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //AWS
            services.AddSingleton<IAWSConfiguration,AWSConfiguration> ();
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
            services.AddTransient<IAmazonS3Helper,AmazonS3Helper>();


            services.AddControllers();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IEntityMapper, EntityMapper>();
            services.AddHttpContextAccessor();

            //Business
            services.AddScoped<IActivitiesBusiness, ActivitiesBusiness>();
            services.AddScoped<IMembersBusiness, MembersBusiness>();
            services.AddScoped<IOrganizationsBusiness, OrganizationsBusiness>();
            services.AddScoped<ICommentsBusiness, CommentsBusiness>();
            services.AddScoped<ITestimonialsBusiness, TestimonialsBusiness>();
            services.AddScoped<IRolesBusiness, RolesBusiness>();
            services.AddScoped<IUsersBusiness, UsersBusiness>();
            services.AddScoped<ICategoriesBusiness, CategoriesBusiness>();
            services.AddScoped<INewsBusiness, NewsBusiness>();
            services.AddScoped<IAuthBusiness, AuthBusiness>();
            services.AddScoped<ISlidesBusiness, SlidesBusiness>();
            services.AddScoped<IContactsBusiness, ContactsBusiness>();


            //Email
            services.AddTransient<IEmailServices, SendgridEmailServices>();
            //HttpContextAccessor
            services.AddHttpContextAccessor();
            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OngProject", Version = "v1" });
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
                        new string[]{}
                    }
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = "https://localhost:5001",
                        ValidAudience = "https://localhost:5001",
                        IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:DevelopmentJwtApiKey").Value))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OngProject v1"));
            }          

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<OwnershipMiddleware>();

            app.UseMiddleware<AdminMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });                
        }
    }
}
