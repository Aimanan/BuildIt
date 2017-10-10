using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Web.Models.AdditionalInfo
{
    public class RatingViewModel
    {
        public int Id { get; set; }

        public double RatingCalculated { get; set; }

        public int UserRating { get; set; }
    }
}
