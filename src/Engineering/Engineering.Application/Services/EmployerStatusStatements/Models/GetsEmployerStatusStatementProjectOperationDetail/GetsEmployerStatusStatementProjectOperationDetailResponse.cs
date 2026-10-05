
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationDetail;

public record GetsEmployerStatusStatementProjectOperationDetailResponse(
    List<GetsEmployerStatusStatementProjectOperationDetailResponseModel> Data,
    int RowCount
    );

public record GetsEmployerStatusStatementProjectOperationDetailResponseModel
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public ProjectOperationDetailStatus ProjectOperationDetailStatus { get; set; }
    public string ProjectOperationDetailStatusTitle { get; set; } = string.Empty;
    public long OperationLocationId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public decimal TotalWorkVolume { get; set; }
    public decimal DoneWorkVolume { get; set; }
    public decimal StatusStatementWorkVolume { get; set; }
    public decimal DonePercentage { get; set; }
    public decimal TotalPercentage { get; set; }
    public decimal CalculatedAmount { get; set; }
    public decimal TotalDailyWorkVolume { get; set; }
    public decimal ContractorWorkVolume { get; set; }
    public decimal SupervisorWorkVolume { get; set; }
    public decimal ConsultantWorkVolume { get; set; }
    public decimal EmployerRepresentativeWorkVolume { get; set; }
    public string? Description { get; set; } = string.Empty;
}
