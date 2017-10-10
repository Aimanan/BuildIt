using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Web.Models.Search
{
    public class SearchViewModel
    {
        public IEnumerable<PublicationTypeViewModel> PublicationTypes { get; set; }
    }
}
