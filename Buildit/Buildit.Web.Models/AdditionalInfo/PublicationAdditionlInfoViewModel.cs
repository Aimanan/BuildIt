using Buildit.Data.Models;
using Buildit.Web.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Web.Models.AdditionalInfo
{ 
    public class PublicationAdditionlInfoViewModel : IMap<Publication>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }

        public string Picture { get; set; }

        public string PublicationType { get; set; }
    }
}
