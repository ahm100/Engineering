namespace Engineering.Domain.Entities.Adjustment.Enums;

public enum AdjustmentIndexType
{
    [Description(AdjustmentCmts.Temporary)]
    Temporary = 1,

    [Description(AdjustmentCmts.Final)]
    Final = 2,

    [Description(AdjustmentCmts.Average)]
    Average = 3
}