using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markly
{
    public class Reminder
    {
        public Guid Id { get; set; }
        public DateTime RemindAt { get; set; }
        public bool IsNotified { get; set; } = false;
        public string Note { get; set; }
    }
}
