using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProcesVerbal;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

public class ConsumableVolumeProduct : AuditableEntity<ConsumableVolumeProduct>
{

    public long ProductGroupId { get; private set; }
    public decimal? UnusedPercentage { get; private set; } = 0;
    public bool IsStandard { get; private set; }
    public decimal? StandardValue { get; private set; }
    public decimal FinalValue { get; private set; }
    public VolumeProductType VolumeProductType { get; private set; } = VolumeProductType.ProductGroup;

    public ProjectOperationDetail ProjectOperationDetail { get; set; }

    public ConsumableVolumeProduct(ProjectOperationDetail projectOperationDetail, long productGroupId, decimal? unusedPercentage,
        bool isStandard, decimal? standardValue, decimal finalValue, VolumeProductType volumeProductType)
    {
        ProjectOperationDetail = Guard.Against.Null(projectOperationDetail, nameof(projectOperationDetail));
        ProductGroupId = Guard.Against.Null(productGroupId, nameof(productGroupId));
        FinalValue = Guard.Against.Null(finalValue, nameof(finalValue));
        VolumeProductType = Guard.Against.Null(volumeProductType, nameof(volumeProductType));
        UnusedPercentage = unusedPercentage;
        IsStandard = isStandard;
        StandardValue = standardValue;

        _dailyOperationProducts = [];
        _requestGoodsSupplyDetail = [];
    }

    #region Set Date

    public void SetProjectOperationDetail(ProjectOperationDetail value)
    {
        ProjectOperationDetail = Guard.Against.Null(value, nameof(value));
    }
    public void SetProductGroupId(long value)
    {
        ProductGroupId = Guard.Against.Null(value, nameof(value));
    }
    public void SetFinalValue(decimal value)
    {
        FinalValue = Guard.Against.Null(value, nameof(value));
    }
    public void SetUnusedPercentage(decimal? value)
    {
        UnusedPercentage = value;
    }
    public void SetIsStandard(bool value)
    {
        IsStandard = value;
    }
    public void SetStandardValue(decimal? value)
    {
        StandardValue = value;
    }
    public void SetType(VolumeProductType value)
    {
        VolumeProductType = Guard.Against.Null(value, nameof(value)); ;
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
    private List<DailyProjectOperationProduct> _dailyOperationProducts;
    public IReadOnlyList<DailyProjectOperationProduct> DailyOperationProducts => _dailyOperationProducts;
    private List<RequestGoodsSupplyDetail> _requestGoodsSupplyDetail;
    public IReadOnlyList<RequestGoodsSupplyDetail> RequestGoodsSupplyDetails => _requestGoodsSupplyDetail;
    private List<ProcesVerbalProduct> _procesVerbalProduct;
    public IReadOnlyList<ProcesVerbalProduct> ProcesVerbalProduct => _procesVerbalProduct;
    private ConsumableVolumeProduct()
    {
        _dailyOperationProducts = [];
        _requestGoodsSupplyDetail = [];
        _procesVerbalProduct = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
