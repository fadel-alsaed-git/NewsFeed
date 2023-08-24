using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface INewsFeedRepository : IRepository<Feed>
    {

        Task<Feed> GetDetails(int id);

    }
}
