using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.GetDetailPOWbsByProjectWbsId;

public record GetDetailPOWbsByProjectWbsIdResponse(
    List<GetDetailPOWbsByProjectWbsIdModel> Data,
    int RowCount);

public class GetDetailPOWbsByProjectWbsIdModel
{
    public long Id { get; set; }
    public long ProjectWbsId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string TitleFa { get; set; } = string.Empty;
    public string? TitleEn { get; set; } = string.Empty;
    public string? DescriptionFa { get; set; } = string.Empty;
    public string? DescriptionEn { get; set; } = string.Empty;
    public long ProjectOperationId { get; set; }
    public string? ProjectOperationDescription { get; set; }
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public DateTime? PlannedStartDate { get; set; }
    public string? PlannedStartDateShamsi => PlannedStartDate.ToShamsi();
    public DateTime? PlannedFinishDate { get; set; }
    public string? PlannedFinishDateShamsi => PlannedFinishDate.ToShamsi();
    public int? PlannedDays =>
    PlannedStartDate.HasValue && PlannedFinishDate.HasValue
        ? (PlannedFinishDate.Value.Date - PlannedStartDate.Value.Date).Days + 1
        : null;
    public decimal? TotalDailyVolume { get; set; }
    public decimal? TotalWorkload { get; set; }
    public decimal? ProgressPercent => TotalWorkload == 0
    ? 0
    : TotalDailyVolume / TotalWorkload * 100m;
    public ProjectOperationWbsStatus Status { get; set; }
    public string? StatusDescription => Status.GetEnumDescription();
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public bool IsActive { get; set; }
    public List<POWbsPredecessorDependencyModel>? pOWbsPredecessors { get; set; }
    public List<POWbsSuccessorDependencyModel>? pOWbsSuccessors { get; set; }
}

public class POWbsPredecessorDependencyModel
{
    public long SuccessorId { get; set; }
    public string Successor { get; set; } = string.Empty;
    public ProjectOperationDependencyType DependencyType { get; set; }
    public string? DependencyTypeDescription => DependencyType.GetEnumDescription();
}

public class POWbsSuccessorDependencyModel
{
    public long PredecessorId { get; set; }
    public string Predecessor { get; set; } = string.Empty;
    public ProjectOperationDependencyType DependencyType { get; set; }
    public string? DependencyTypeDescription => DependencyType.GetEnumDescription();
}