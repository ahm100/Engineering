using System.ComponentModel;

namespace Engineering.Application.Services.ProjectWbses.ImportMPP;

public enum MppDependencyType
{
    [Description("پایان به آغاز")]
    FinishToStart = 1,

    [Description("آغاز به آغاز")]
    StartToStart = 2,

    [Description("پایان به پایان")]
    FinishToFinish = 3,

    [Description("آغاز به پایان")]
    StartToFinish = 4
}
