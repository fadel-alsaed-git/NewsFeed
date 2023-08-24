using Application.IServices;
using Application.Services;

namespace Newsfeed.Extensions
{
    public static class AppicationCollection
    {

        public static IServiceCollection AddAppicationCollection(this IServiceCollection services)
        {

            services.AddSingleton(typeof(INewsFeedService), typeof(NewsFeedService));
            return services;
        }
    }
}
