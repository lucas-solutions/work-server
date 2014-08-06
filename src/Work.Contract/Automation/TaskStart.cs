using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Automation
{
    public class TaskStart
    {
        public static implicit operator TaskStart(string expression)
        {
            var parts = (expression ?? string.Empty).Split(':');

            return null;
        }

        public static implicit operator string(TaskStart cron)
        {
            return cron != null ? cron.ToString() : null;
        }

        public static implicit operator TimeSpan(TaskStart cron)
        {
            return TimeSpan.FromDays(1);
        }

        public int? DayOfMonth
        {
            get;
            set;
        }

        public DayOfWeek? DayOfWeek
        {
            get;
            set;
        }

        public int Hour
        {
            get;
            set;
        }

        public int Minute
        {
            get;
            set;
        }

        public int? Month
        {
            get;
            set;
        }

        public int Second
        {
            get;
            set;
        }

        public int? Year
        {
            get;
            set;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
