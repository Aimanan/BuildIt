using Buildit.Data.Contracts;
using Buildit.Data.Models;
using Buildit.Services.Contracts;
using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Services
{
    public class RatingsService : IRatingsService
    {
        private readonly IBuilditData data;

        public RatingsService(IBuilditData data)
        {
            Guard.WhenArgument(data, "Data").IsNull().Throw();

            this.data = data;
        }

        public int GetRating(int publId, string userId)
        {
            var rating = this.data.Ratings.All
                .FirstOrDefault(x => x.UserId == userId && x.PublicationId == publId);
            if (rating != null)
            {
                return rating.Value;
            }
            else
            {
                return 0;
            }
        }

        public void RatePublication(int publId, string userId, int rate)
        {
            var rating = this.data.Ratings.All
                .Where(x => x.PublicationId == publId && x.UserId == userId).FirstOrDefault();
            if (rating != null)
            {
                rating.Value = rate;
            }
            else
            {
                rating = new Rating()
                {
                    PublicationId = publId,
                    Value = rate,
                    UserId = userId
                };
                this.data.Ratings.Add(rating);
            }

            this.data.SaveChanges();
        }
    }
}
