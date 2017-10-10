using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Data.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int Value { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int PublicationId { get; set; }

        public virtual Publication Publication { get; set; }
    }
}
