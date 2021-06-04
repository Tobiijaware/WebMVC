using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SQOO7FirstWebApp.Data;
using SQOO7FirstWebApp.Models;
using SQOO7FirstWebApp.Services;

namespace SQOO7FirstWebApp
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
            services.AddControllersWithViews(option => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                            .Build();
                option.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddDbContextPool<SQOO7DbContext>(
                option => option.UseSqlite(Configuration.GetConnectionString("default"))
            );
            services.AddIdentity<Employee, IdentityRole>(
                options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    //options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 5;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;

                    //options.Lockout.MaxFailedAccessAttempts = 3;
                    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);


                }).AddEntityFrameworkStores<SQOO7DbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();

            services.AddAuthorization(option =>
            {
                option.AddPolicy("CanEditPolicy", policy => policy.RequireClaim("CanEdit")); // add role policy
                option.AddPolicy("AdminRolePolicy", policy => policy.RequireRole("Admin")); // add claims policy
                
                // when you have a complex combination
                option.AddPolicy("AdminOrManagerRoleAndCanEdit", policy =>
                                policy.RequireAssertion(
                                    c => (c.User.IsInRole("Admin") || c.User.IsInRole("Manager")) 
                                                                  && c.User.HasClaim("CanEdit", "true")
                                ));
            });

            //services.Configure<IdentityUser>(options =>             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            SQOO7DbContext context, UserManager<Employee> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            else
            {
                // ensure launchSetting is set to Production before testing for this functions
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication(); // this comes first
            app.UseAuthorization();

            Preseeder.SeedIt(context, userManager, roleManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
