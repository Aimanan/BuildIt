using Buildit.Data.Contracts;
using Buildit.Data.Models;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Services
{
    public class PublicationTypeService: IPublicatioTypeService
    {
        private readonly IBuilditData data;

        public PublicationTypeService(IBuilditData data)
        {
            Guard.WhenArgument(data, "Data").IsNull().Throw();
            this.data = data;
        }

        public IEnumerable<PublicationType> GetPublicationTypes()
        {
            return this.data.PublicationTypes.All.ToList();
        }
    }
}
