using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class Tag
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
