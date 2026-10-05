
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Domain.Entities.OperationInfos;

[Description(OperationInfoCmts.OperationInfoSeason)]
public class OperationInfoSeason : AuditableEntity<OperationInfo>
{
    [Description(GlobalCmts.OperationInfo)]
    public long OperationInfoId { get; set; }
    public OperationInfo OperationInfo { get; set; }
    [Description(GlobalCmts.Season)]
    public long SeasonId { get; set; }
    public Season Season { get; set; }


    public OperationInfoSeason(OperationInfo operationInfo,
        Season season) : this()
    {
        SetOperationInfo(operationInfo);
        SetSeason(season);
    }

    #region Set data 

    public void SetOperationInfo(OperationInfo value)
    {
        OperationInfo = Guard.Against.Null(value, nameof(value));
        OperationInfoId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetSeason(Season value)
    {
        Season = Guard.Against.Null(value, nameof(value));
        SeasonId = Guard.Against.Null(value.Id, nameof(value.Id));
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
    private List<RequestGoodsSupply> _requestGoodsSupplies;
    public IReadOnlyList<RequestGoodsSupply> RequestGoodsSupplies => _requestGoodsSupplies;
    private OperationInfoSeason()
    {
        _requestGoodsSupplies = new List<RequestGoodsSupply>();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
