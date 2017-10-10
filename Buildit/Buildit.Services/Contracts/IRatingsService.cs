using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Services.Contracts
{
    public interface IRatingsService
    {
        void RatePublication(int publId, string userId, int rate);

        int GetRating(int publId, string userId);
    }
}
