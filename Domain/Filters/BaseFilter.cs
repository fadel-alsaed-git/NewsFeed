using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Filters
{
    public abstract class BaseFilter
    {
        /// <summary>
        /// For paginatio
        /// </summary>
        public int Page { get; set; } = 1;

        public int RequestedPage() => Page > 0 ? Page : 1;

        private int _perPage;
        /// <summary>
        /// For paginattion, items per page
        /// </summary>
        public int PerPage
        {
            get => _perPage <= 0 ? 10 : _perPage;
            set => _perPage = value <= 0 ? 10 : value;
        }

        public int ElementsToBeSkippedCount() => (RequestedPage() - 1) * PerPage;

        private string _searchQuery;

        /// <summary>
        /// For public search
        /// </summary>
        public string SearchQuery
        {
            set => _searchQuery = value;
            get => string.IsNullOrEmpty(_searchQuery) ? _searchQuery : _searchQuery.ToLower().Trim();
        }

    }
}
