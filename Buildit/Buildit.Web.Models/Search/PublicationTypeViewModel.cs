using Buildit.Data.Models;
using Buildit.Web.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Web.Models.Search
{
    public class PublicationTypeViewModel : IMap<PublicationType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
