using System.ComponentModel;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;

public enum ImportanceDegree
{
    [Description("کم")] Low = 0,
    [Description("متوسط")] Medium = 1,
    [Description("زیاد")] High = 2,
    [Description("خیلی زیاد")] VeryHigh = 3
}
