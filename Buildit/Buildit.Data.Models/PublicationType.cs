using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buildit.Data.Models
{
    public class PublicationType
    {
        private ICollection<Publication> publications;

        public PublicationType()
        {
            this.publications = new HashSet<Publication>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Publication> Publications
        {
            get { return this.publications; }
            set { this.publications = value; }
        }
    }
}
