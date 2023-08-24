using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class FeedDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Author { get; set; }


        public DateTime? PublishedAt { get; set; }
    }

    public class FeedConvertDTO
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string UrlToImage { get; set; }

        public DateTime? PublishedAt { get; set; }

        public string Content { get; set; }
    }

    public class FeedResultDTO
    {
        public int Count { get; set; }
        public List<FeedDTO> List { get; set; }
    }
}
