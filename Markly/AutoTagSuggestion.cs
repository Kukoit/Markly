using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class AutoTagSuggestion
    {
        public string TagName { get; set; }
        public double Confidence { get; set; }
        public bool? Accepted { get; set; } // null = pending review
    }
}
