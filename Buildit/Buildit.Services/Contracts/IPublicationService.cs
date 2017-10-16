using Buildit.Data.Models;
using Buildit.Web.Models;
using Buildit.Web.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Services.Contracts
{
    public interface IPublicationService
    {
        bool PublicationFound(string title);

        int AddPublication(PublicationViewModelAdmin publModel, string fileName);

        IEnumerable<Publication> GetTopPublications(int count);

        int GetPublicationsCount(string searchWord, IEnumerable<int> publicationTypeIds);

        Publication GetById(int id);

        IEnumerable<Publication> SearchPublications(string searchWord, IEnumerable<int> publicationTypeIds, string orderProperty, int page = 1, int numberOfPages = 9);      
    }
}
