
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Entities;
using VotingApp.Helpers;
using VotingApp.Interfaces;
using VotingApp.Services;
using VotingApp.Services.Linq;

namespace VotingApp
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
            services.AddCors();
            services.AddDbContext<MasterContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddAutoMapper(typeof(Startup));
           


            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IRepository<User>>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user =userService.GetByID(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IUsersInterface, UsersService>();
            services.AddScoped<IRepository<User>, BaseRepository<User>>();
            services.AddScoped<IRepository<Electoral_Room>, BaseRepository<Electoral_Room>>();
            services.AddScoped<IRepository<Role>, BaseRepository<Role>>();
            services.AddScoped<IRepository<College>, BaseRepository<College>>();
            services.AddScoped<IRepository<Department>, BaseRepository<Department>>();
            services.AddScoped<IRepository<Election_User>, BaseRepository<Election_User>>();
            services.AddScoped<IElectionInterface, ElectionService>();
            services.AddScoped<ICandidateInterface, CandidatesService>();
            services.AddScoped<IRepository<Candidate>, BaseRepository<Candidate>>();
            services.AddScoped<IRepository<Activation_Code>, BaseRepository<Activation_Code>>();
            services.AddScoped<IRepository<PasswordToken>, BaseRepository<PasswordToken>>();
            services.AddScoped<IRepository<ElectionTypes>, BaseRepository<ElectionTypes>>();
            services.AddScoped<ICollegeInterface, CollegeService>();
            services.AddScoped<IRepository<Role>, BaseRepository<Role>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();   
        }
    }
}
