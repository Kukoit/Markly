using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class ExportJob
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Format { get; set; } // "html", "json", "csv"
        public string Scope { get; set; }  // "all" or "collection"
        public Guid? CollectionId { get; set; }
        public string Status { get; set; } = "pending";
        public string FileUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
