using System.ComponentModel;

namespace Lucas.Solutions.Diagnostics
{
    [DefaultValue(LogCategories.Info)]
    public enum LogCategories
    {
        Critical,

        Debug,

        Error,

        FailureAudit,

        Fatal,

        Info,

        Resume,

        Start,

        Stop,

        SuccessAudit,

        Suspend,

        Transfer,

        Verbose,

        Warning
    }
}