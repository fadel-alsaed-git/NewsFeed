using Application.IServices;
using Application.ViewModels;
using Microsoft.Extensions.Options;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using System.Reflection.Metadata;

namespace Newsfeed.Workers
{
    /// <summary>
    /// Get News
    /// </summary>
    public class FeedWorker : BackgroundService
    {

        private readonly ILogger<FeedWorker> _logger;
        private readonly ApplicationSettings _applicationSettings;

        private readonly INewsFeedService _newsFeedService;


        public FeedWorker(ILogger<FeedWorker> logger, IOptions<ApplicationSettings> applicationSettings, INewsFeedService newsFeedService)
        {
            _logger = logger;
            _applicationSettings = applicationSettings?.Value;
            _newsFeedService = newsFeedService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {



                try
                {
                    //Check Last Sync
                    var lasySync = await _newsFeedService.GetLastSync();

                    //Get news for last day
                    var selectedFated = DateTime.Now.AddDays(-1);
                    //Check last sync and selected date
                    if (lasySync.Day < selectedFated.Day)
                    {
                        //Init Api
                        var newsApiClient = new NewsApiClient(_applicationSettings?.NewsApiKey);


                        //Call Action
                        var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                        {
                            Q = "Apple",
                            SortBy = SortBys.Popularity,
                            Language = Languages.EN,
                            From = selectedFated.Date,
                        });
                        //Chack response
                        if (articlesResponse.Status == Statuses.Ok)
                        {
                            // total results found
                            Console.WriteLine(articlesResponse.TotalResults);

                            // here's the first 20
                            var result = articlesResponse.Articles?.Select(a => new FeedConvertDTO()
                            {
                                Author = a.Author,
                                Content = a.Content,
                                Description = a.Description,
                                PublishedAt = a.PublishedAt,
                                Title = a.Title,
                                Url = a.Url,
                                UrlToImage = a.UrlToImage,
                            }).ToList();
                            //Add news
                            await _newsFeedService.AddFromWorker(result);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex?.Message?.ToString());
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
            }
        }
    }
}
