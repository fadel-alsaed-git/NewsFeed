using Application.ViewModels;
using Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface INewsFeedService : IApplicationService
    {
        Task<FeedResultDTO> Filter(FeedFilter filter);
        Task<DateTime> GetLastSync();
        Task AddFromWorker(List<FeedConvertDTO> feeds);

        Task<FeedDTO> GetDetails(int id);


    }
}
