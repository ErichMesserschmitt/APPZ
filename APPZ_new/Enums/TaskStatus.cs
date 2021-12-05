using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace APPZ_new.Enums
{
    public enum TaskStatus
    {
        [Description("Not Passed")]
        NotPassed = 0,

        [Description("Passed")]
        Passed = 1,

        [Description("New")]
        NotStarted = 2,

        [Description("Not Completed")]
        InProcess = 3
    }
}
