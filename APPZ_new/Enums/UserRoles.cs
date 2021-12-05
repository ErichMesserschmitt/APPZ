using System.ComponentModel;

namespace APPZ_new.Enums
{
    public enum UserRole
    {
        [Description("super_admin")]
        SuperAdmin = -1,

        [Description("Administrator")]
        Admin = 0,

        [Description("Learner")]
        User = 1
    }
}
