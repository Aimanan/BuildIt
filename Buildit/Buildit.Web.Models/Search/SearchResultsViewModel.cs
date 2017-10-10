using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Web.Models.Search
{
    public class SearchResultsViewModel
    {
        public IEnumerable<PublicationViewModel> Publications { get; set; }

        public int PublicationsCount { get; set; }

        public int Pages { get; set; }

        public int Page { get; set; }

        public SearchViewResultModel SearchModel{ get; set; }
    }
}
