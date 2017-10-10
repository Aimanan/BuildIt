using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Buildit.Web.Models.Admin
{
    public class AddPublicationViewModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(80)]
        public string Author { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(10000)]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublishedOn { get; set; }

        [Required]
        [Display(Name = "Picture")]
        public HttpPostedFileBase Picture { get; set; }

        [Required]
        [Display(Name = "PublicationType")]
        public int PublicationTypeId { get; set; }

        public IEnumerable<SelectListItem> PublicationTypes { get; set; }
    }
}
