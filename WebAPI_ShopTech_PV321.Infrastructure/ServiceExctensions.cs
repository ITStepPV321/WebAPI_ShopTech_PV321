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

    }
}
