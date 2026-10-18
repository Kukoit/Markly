using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class RuleAction
    {
        public string ActionType { get; set; }  // "addTag", "moveToCollection", "addToCollection", "flagForReview"
        public string ActionValue { get; set; } // meaning depends on ActionType: tag name, collection id, etc.
    }
}
