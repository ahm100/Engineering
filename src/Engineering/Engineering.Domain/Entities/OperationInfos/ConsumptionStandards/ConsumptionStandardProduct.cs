using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Domain.Entities.OperationInfos.ConsumptionStandards;

/// <summary>
/// استاندارد های ابزار
/// </summary>
public class ConsumptionStandardProduct : AuditableEntity<ConsumptionStandardProduct>
{
    public long ProductUnitId { get; private set; }
    public decimal Number { get; private set; }
    public decimal? UnusedPercentage { get; private set; } = 0;
    public StandardProductType StandardProductType { get; private set; } = StandardProductType.ProductGroup;
    public ProductAllowedType ProductAllowedType { get; private set; } = ProductAllowedType.IsStandard;

    public OperationInfo OperationInfo { get; set; }

    public ConsumptionStandardProduct(
        OperationInfo operationInfo,
        long productUnitId,
        decimal number,
        decimal? unusedPercentage,
        StandardProductType standardProductType,
        ProductAllowedType productAllowedType)
    {
        OperationInfo = Guard.Against.Null(operationInfo, nameof(operationInfo));
        StandardProductType = Guard.Against.Null(standardProductType, nameof(standardProductType));
        ProductAllowedType = Guard.Against.Null(productAllowedType, nameof(productAllowedType));
        ProductUnitId = Guard.Against.NegativeOrZero(productUnitId, nameof(productUnitId));
        Number = Guard.Against.Null(number, nameof(number));
        UnusedPercentage = unusedPercentage;
    }

    #region Set data

    public void SetProductUnitId(long productUnitId)
    {
        ProductUnitId = Guard.Against.NegativeOrZero(productUnitId, nameof(productUnitId));
    }
    public void SetNumber(decimal number)
    {
        Number = Guard.Against.Null(number, nameof(number));
    }
    public void SetUnusedPercentage(decimal? unusedPercentage)
    {
        UnusedPercentage = unusedPercentage;
    }
    public void SetStandardProductType(StandardProductType value)
    {
        StandardProductType = Guard.Against.Null(value, nameof(value));
    }
    public void SetProductAllowedType(ProductAllowedType value)
    {
        ProductAllowedType = Guard.Against.Null(value, nameof(value));
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Description(EContractCmts.EmployerOperationProduct)]
    private List<EmployerOperationProduct> _employerOperationProducts;
    public IReadOnlyList<EmployerOperationProduct> EmployerOperationProduct => _employerOperationProducts;
    private ConsumptionStandardProduct()
    {
        _employerOperationProducts = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
