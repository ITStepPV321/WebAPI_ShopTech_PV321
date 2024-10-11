using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPI_ShopTech_PV321.Infrastructure.Data;

namespace WebAPI_ShopTech_PV321.Infrastructure
{
    public static class ServiceExctensions
    {
        public static void AddDbContextCustom(this IServiceCollection serviceCollection, string connection)
        {
            serviceCollection.AddDbContext<ShopTechAPI_PV321>(options => {
                options.UseSqlServer(connection);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
        //ServiceExctensions using =>FluentValidation.AspNetCore
        public static void AddIdentityCustom(this IServiceCollection serviceCollectionis)
        {
            serviceCollectionis.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ShopTechAPI_PV321>();

            ////        serviceCollectionis.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            ////.AddRoles<IdentityRole>()
            ////.AddEntityFrameworkStores<ShopTechAPI_PV321>();
        }

    }


    }
