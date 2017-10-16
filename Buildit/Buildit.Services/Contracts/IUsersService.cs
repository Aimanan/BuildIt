using Buildit.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Services.Contracts
{
    public interface IUsersService
    {
        bool CheckIfUserExists(string username);

        User GetById(string id);

        //void BanUser(string userId);

        //void UnbanUser(string userId);

    }
}
