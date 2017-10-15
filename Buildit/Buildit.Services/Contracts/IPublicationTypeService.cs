using Buildit.Data.Models;
using System.Collections.Generic;

namespace Buildit.Services
{
    public interface IPublicationTypeService
    {
        IEnumerable<PublicationType> GetPublicationTypes();
    }
}