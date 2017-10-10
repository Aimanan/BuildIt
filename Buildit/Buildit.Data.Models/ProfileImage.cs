using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buildit.Data.Models
{
    public class ProfileImage
    {
        public ProfileImage(byte[] content)
        {
            this.Content = content;
        }

        public int Id { get; set; }

        public byte[] Content { get; set; }

        public bool IsDeleted { get; set; }

    }
}
