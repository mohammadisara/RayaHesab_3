using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RayaHesab.Models;

[assembly: HostingStartup(typeof(RayaHesab.Areas.Identity.IdentityHostingStartup))]
namespace RayaHesab.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {


            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ShahrsaatContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MyConnection")));

                //services.AddDefaultIdentity<IdentityUser>()
                //    .AddEntityFrameworkStores<ShahrsaatContext>();
            });
        }
    }
}