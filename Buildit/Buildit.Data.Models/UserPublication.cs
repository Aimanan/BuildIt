using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Data.Models
{
    public class UserPublication
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public int PublicationId { get; set; }

        public virtual User User { get; set; }

        public virtual Publication Publication { get; set; }

        public PublicationRate PublicationRate { get; set; }
    }
}
