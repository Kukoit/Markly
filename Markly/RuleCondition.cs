using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class RuleCondition
    {
        public string Field { get; set; }          // "url", "title", "tag", "domain", "daysSinceSaved"
        public string Operator { get; set; }       // "contains", "equals", "startsWith", "greaterThan"
        public string ConditionValue { get; set; }
    }
}
