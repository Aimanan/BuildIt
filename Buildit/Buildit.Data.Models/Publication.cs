using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Data.Models
{
    public class Publication
    {
        private ICollection<Rating> ratings;
        private ICollection<UserPublication> uerPublications;

        public Publication()
        {
            this.ratings = new HashSet<Rating>();
            this.uerPublications = new HashSet<UserPublication>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(1)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(80)]
        public string Author { get; set; }

        [MinLength(3)]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [MinLength(50)]
        [MaxLength(10000)]
        public string Content { get; set; }

        [Required]
        public DateTime PublishedOn { get; set; }

        [Required]
        public string Picture { get; set; }

        public int PublicationTypeId { get; set; }

        public virtual PublicationType PublicationType { get; set; }

        public virtual ICollection<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }

        public virtual ICollection<UserPublication> UserPublications
        {
            get { return this.uerPublications; }
            set { this.uerPublications = value; }
        }
    }
}
