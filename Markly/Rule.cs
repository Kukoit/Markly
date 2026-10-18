using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class Rule
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; } = true;
        public List<RuleCondition> Conditions { get; set; } = new List<RuleCondition>();
        public List<RuleAction> Actions { get; set; } = new List<RuleAction>();
        public DateTime CreatedAt { get; set; }
    }
}
