using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JobPlatform.Models;

[assembly: HostingStartup(typeof(JobPlatform.Areas.Identity.IdentityHostingStartup))]
namespace JobPlatform.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<OnlineClassContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("JobPlatformContextConnection")));

            //    services.AddDefaultIdentity<IdentityUser2>()
            //        .AddEntityFrameworkStores<OnlineClassContext>();
            //});
        }
    }
}