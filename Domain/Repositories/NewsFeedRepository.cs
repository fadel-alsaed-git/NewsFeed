using Dapper;
using Domain.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Domain.Repositories
{
    public class NewsFeedRepository : Repository<Feed>, INewsFeedRepository
    {

        private readonly DapperContext _dapperContext;

        public NewsFeedRepository(NewsFeedContext context, DapperContext dapperContext) : base(context)
        {
            _dapperContext = dapperContext;
        }

        public async Task<Feed> GetDetails(int id)
        {
            var query = "SELECT * FROM Feeds WHERE Id = @Id";
            using var connection = _dapperContext.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Feed>(query, new { id });
        }
    }
}
