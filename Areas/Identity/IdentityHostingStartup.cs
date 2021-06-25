using System;
using LibraryAssistant.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(LibraryAssistant.Areas.Identity.IdentityHostingStartup))]
namespace LibraryAssistant.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<LibraryAssistantIdentityDbContext>(options =>
                    options.UseNpgsql(
                        context.Configuration.GetConnectionString("LibraryAssistantIdentityDbContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<LibraryAssistantIdentityDbContext>();
            });
        }
    }
}