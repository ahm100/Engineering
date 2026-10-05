
namespace Engineering.Domain.Entities.Messengers.Enums;

public enum MessengerTargetType
{
    [Description("انبار")]
    Warehouse = 1,

    [Description("مرکزهزینه")]
    CostCenter = 2,

    [Description("پروژه")]
    Project = 3,

}
