using Domain.Context;
using Domain.Repositories;

namespace Newsfeed.Extensions
{
    public static class DomainCollection
    {

        public static IServiceCollection AddDomainCollection(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton(typeof(INewsFeedRepository), typeof(NewsFeedRepository));

            return services;
        }
    }
}
