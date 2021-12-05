using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ_new.Enums
{
    public enum TaskSeverity
    {
        [Description("Low")]
        Low = 0,

        [Description("Medium")]
        Medium = 1,

        [Description("Hard")]
        Hard = 2
    }
}
