using Buildit.Web.Models.Admin;
using Buildit.Web.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Web.Models.Admin
{    
    public class PublicationViewModelAdmin : IMap<AddPublicationViewModel>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }

        public int PublicationTypeId { get; set; }
    }
}
