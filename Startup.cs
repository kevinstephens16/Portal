using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Portal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Portal.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Portal
{

    public class Startup
    {

        public Startup(IConfiguration config) => Configuration = config;

        private IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<ProductDbContext>(opts =>
            {
                opts.UseSqlServer(
                    Configuration["ConnectionStrings:DefaultConnection"]);
            });

            services.AddHttpsRedirection(opts =>
            {
                opts.HttpsPort = 44350;
            });

            services.AddDbContext<IdentityDbContext>(opts =>
            {
                opts.UseSqlServer(
                    Configuration["ConnectionStrings:IdentityConnection"],
                    opts => opts.MigrationsAssembly("Portal")
                );
            });

            //Code need to be written to send email
            services.AddScoped<IEmailSender, ConsoleEmailSender>();

            //Enable this to use Authorization when register
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<IdentityDbContext>();

            //Disable if or reconfigure to use Authorization when register new account
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 8;
                opts.Password.RequireDigit = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<IdentityDbContext>()
              .AddDefaultTokenProviders();

            //services.AddDefaultIdentity<ApplicationUser>(opts =>
            //{
            //    opts.Password.RequiredLength = 8;
            //    opts.Password.RequireDigit = false;
            //    opts.Password.RequireLowercase = false;
            //    opts.Password.RequireUppercase = false;
            //    opts.Password.RequireNonAlphanumeric = false;
            //    opts.SignIn.RequireConfirmedAccount = false;
            //})
            //  .AddEntityFrameworkStores<ApplicationDbContext>()
            // .AddDefaultTokenProviders();

            services.Configure<SecurityStampValidatorOptions>(opts =>
            {
                opts.ValidationInterval = System.TimeSpan.FromMinutes(1);
            });

            services.AddScoped<TokenUrlEncoderService>();
            //Code need to be written to send email
            services.AddScoped<IdentityEmailService>();

            ////*****************************************************************************************
            //services.AddAuthentication()
            //.AddFacebook(opts =>
            //{
            //    opts.AppId = Configuration["Facebook:AppId"];
            //    opts.AppSecret = Configuration["Facebook:AppSecret"];
            //})
            //.AddGoogle(opts =>
            //{
            //    opts.ClientId = Configuration["Google:ClientId"];
            //    opts.ClientSecret = Configuration["Google:ClientSecret"];
            //})
            //.AddTwitter(opts =>
            //{
            //    opts.ConsumerKey = Configuration["Twitter:ApiKey"];
            //    opts.ConsumerSecret = Configuration["Twitter:ApiSecret"];
            //    opts.RetrieveUserDetails = true;
            //}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            //{
            //    opts.TokenValidationParameters.ValidateAudience = false;
            //    opts.TokenValidationParameters.ValidateIssuer = false;
            //    opts.TokenValidationParameters.IssuerSigningKey
            //        = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            //            Configuration["BearerTokens:Key"]));
            //});

            //This part is used for Google, Facebook, Twitter auth Login
            services.ConfigureApplicationCookie(opts =>
            {
                //opts.LoginPath = "/Identity/SignIn";
                opts.LoginPath = "/Identity/Account/Login";
                opts.LoginPath = "/Identity/Account/Logout";
                //opts.LogoutPath = "/Identity/SignOut";
                opts.AccessDeniedPath = "/Identity/Forbidden";
                opts.Events.DisableRedirectionForApiClients();
            });
            //*****************************************************************************************

            services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:5100")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });

            app.SeedUserStoreForDashboard();
        }
    }
}