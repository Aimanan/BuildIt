using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Web.Models.Search
{
    public class SearchViewResultModel
    {
        public string SearchWord { get; set; }

        public IEnumerable<int> ChosenPublicationTypesIds { get; set; }

        public string SortBy { get; set; }
    }
}
