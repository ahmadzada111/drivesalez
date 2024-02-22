using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveSalez.Core.Domain.Entities
{
    public class ImageUrl
    {
        public int Id { get; set; }

        public Uri? Url { get; set; }
    }
}
