using BusinessLayer;
using BusinessLayer.Contracts;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LayersOnWeb
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
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp/dist";
            //});

            services.AddSwaggerGen();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IFineService, FineService>();
            services.AddScoped<IComplaintService, ComplaintService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IRepository, GenericRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddDbContext<FinesPaymentManagerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("connectionString")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                  .AddEntityFrameworkStores<FinesPaymentManagerDbContext>()
                  .AddDefaultTokenProviders();
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            
            CreateUserRoles(serviceProvider).Wait();
            CreateStartupUsers(serviceProvider);
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //Adding PostOfficeEmployee Role
            var roleCheck = await RoleManager.RoleExistsAsync("PostOfficeEmployee");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                await RoleManager.CreateAsync(new IdentityRole("PostOfficeEmployee"));
            }

            //Adding PoliceOfficer Role
            roleCheck = await RoleManager.RoleExistsAsync("PoliceOfficer");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                await RoleManager.CreateAsync(new IdentityRole("PoliceOfficer"));
            }

            //Adding Admin Role
            roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //Adding Driver Role
            roleCheck = await RoleManager.RoleExistsAsync("Driver");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                await RoleManager.CreateAsync(new IdentityRole("Driver"));
            }
        }

        private void CreateStartupUsers(IServiceProvider serviceProvider)
        {
            var userMgr = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var users = userMgr.Users;
            if (!users.Any(x=> x.UserName == "admin@webdotnet.com"))
            {
                var user = new IdentityUser { UserName = "admin@webdotnet.com" };
                userMgr.CreateAsync(user,  "P@ssw0rd").Wait();
                var registeredUser = userMgr.FindByNameAsync(user.UserName).Result;
                userMgr.AddToRoleAsync(registeredUser, "admin").Wait();
            }
        }
    }
}
