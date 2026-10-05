
namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

public class GetOperationInfosModel : IUserAuditable
{
    public long Id { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public string? OperationLatinName { get; set; }
    public int? Priority { get; set; }
    public decimal BasePrice { get; set; }
    public bool IsActive { get; set; }
    public long UnitOfMeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;

    public long? OperationInfoDependencyId { get; set; }
    public long? RelationId { get; set; }
    public string? DependencyName { get; set; }
    public string? DependencyCode { get; set; }
    public int? DependencyPriority { get; set; }
    public int? WorkingDays { get; set; }
    public string? DependencyType { get; set; }

    public List<string>? CategoryItems { get; set; }
    public string? Categories => CategoryItems.JoinListDisc();
    public List<string>? BranchItems { get; set; }
    public string? Branchs => BranchItems.JoinListDisc();
    public List<string>? SeasonItems { get; set; }
    public string? Seasons => SeasonItems.JoinListDisc();
    public List<string>? GroupItems { get; set; }
    public string? Groups => GroupItems.JoinListDisc();
    public bool IsPriceList { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public bool HaveStandard { get; set; }
    public bool HaveExpertStandard { get; set; }
    public bool HaveMachineryStandard { get; set; }
    public bool HaveProductStandard { get; set; }
    public bool HaveServices { get; set; }
    public bool HasChanged { get; set; }
    public DateTime Created { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
    public long? UpdaterId { get; set; }
    public string? Updater { get; set; } = string.Empty;
}
