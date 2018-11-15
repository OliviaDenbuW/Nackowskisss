﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nackowskisss.Data;
using Nackowskisss.Models;
using Nackowskisss.Services;
using Nackowskisss.BusinessLayer;
using Nackowskisss.DataLayer;
using Nackowskisss.Services.Identity;

namespace Nackowskisss
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            //Registrera klasserna som används i de olika skikten
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuctionBusinessService, AuctionBusinessService>();
            services.AddScoped<IAuctionRepository, AuctionRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            Seeder.SeedData(userManager, roleManager);
            Seeder.SeedAdminRoleToUser(userManager);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
