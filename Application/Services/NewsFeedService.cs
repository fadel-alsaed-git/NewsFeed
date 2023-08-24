using Application.IServices;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NewsFeedService : ApplicationService, INewsFeedService
    {

        private readonly INewsFeedRepository _repository;
        private readonly IMapper _mapper;

        public NewsFeedService(INewsFeedRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        /// <summary>
        /// GEt Last Synv
        /// </summary>
        /// <returns></returns>
        public async Task<DateTime> GetLastSync()
        {
            return (await _repository.MaxAsync(a => a.PublishedAt)) ?? DateTime.MinValue;
        }

        /// <summary>
        /// Add news
        /// </summary>
        /// <param name="feeds"></param>
        /// <returns></returns>
        public async Task AddFromWorker(List<FeedConvertDTO> feeds)
        {


            if (feeds?.Any() is true)
            {
                foreach (var feed in feeds)
                {
                    if (await _repository.NotExist(a => a.Title == feed.Title))
                    {
                        await _repository.Insert(_mapper.Map<Feed>(feed));
                    }
                }
                await _repository.SaveChanges();
            }
        }


        /// <summary>
        /// Filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<FeedResultDTO> Filter(FeedFilter filter)
        {
            var query = _repository.AsQueryable();




            if (!string.IsNullOrEmpty(filter.SearchQuery))
            {
                query = query.Where(a => (a.Title + a.Description).ToLower().Contains(filter.SearchQuery));
            }


            query = query.OrderByDescending(a => a.PublishedAt);


            return new FeedResultDTO()
            {
                Count = await query.CountAsync(),
                List = _mapper.Map<List<FeedDTO>>(await query.Skip(filter.ElementsToBeSkippedCount()).Take(filter.PerPage).ToListAsync())
            };
        }
        /// <summary>
        /// Get details using Dapper
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FeedDTO> GetDetails(int id)
        {
            return _mapper.Map<FeedDTO>(await _repository.GetDetails(id));
        }
    }
}
