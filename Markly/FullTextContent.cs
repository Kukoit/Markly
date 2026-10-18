using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class FullTextContent
    {
        public string ExtractedText { get; set; }
        public DateTime ExtractedAt { get; set; }
        public int WordCount { get; set; }
    }
}
