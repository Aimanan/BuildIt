using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace Buildit.Data.Models
{
    public class User : IdentityUser
    {
        private ICollection<Rating> ratings;
        private ICollection<UserPublication> userPublication;

        public User()
        {
            this.ratings = new HashSet<Rating>();
            this.userPublication = new HashSet<UserPublication>();
        }

        public virtual ICollection<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }

        public virtual ICollection<UserPublication> UserPublication
        {
            get { return this.userPublication; }
            set { this.userPublication = value; }
        }

        public bool IsBanned { get; set; }

        public virtual ProfileImage ProfileImage { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
