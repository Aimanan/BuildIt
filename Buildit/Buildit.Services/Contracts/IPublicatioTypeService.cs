using Buildit.Data.Models;
using System.Collections.Generic;

namespace Buildit.Services
{
    public interface IPublicatioTypeService
    {
        IEnumerable<PublicationType> GetPublicationTypes();
    }
}