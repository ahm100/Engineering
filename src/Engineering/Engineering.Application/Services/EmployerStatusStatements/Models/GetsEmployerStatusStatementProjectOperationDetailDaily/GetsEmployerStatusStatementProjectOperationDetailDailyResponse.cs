
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationDetailDaily;

public record GetsEmployerStatusStatementProjectOperationDetailDailyResponse(
    List<GetsEmployerStatusStatementProjectOperationDetailDailyResponseModel> Data,
    int RowCount
    );

public record GetsEmployerStatusStatementProjectOperationDetailDailyResponseModel
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
    public decimal ContractorLength { get; set; }
    public decimal ContractorWidth { get; set; }
    public decimal ContractorHeight { get; set; }
    public decimal ContractorWeight { get; set; }
    public decimal ContractorNumber { get; set; }
    public decimal ContractorVolume { get; set; }
    public string? ContractorDescription { get; set; }
    public decimal SupervisorLength { get; set; }
    public decimal SupervisorWidth { get; set; }
    public decimal SupervisorHeight { get; set; }
    public decimal SupervisorWeight { get; set; }
    public decimal SupervisorNumber { get; set; }
    public decimal SupervisorVolume { get; set; }
    public string? SupervisorDescription { get; set; }
    public decimal ConsultantLength { get; set; }
    public decimal ConsultantWidth { get; set; }
    public decimal ConsultantHeight { get; set; }
    public decimal ConsultantWeight { get; set; }
    public decimal ConsultantNumber { get; set; }
    public decimal ConsultantVolume { get; set; }
    public string? ConsultantDescription { get; set; }
    public decimal EmployerRepresentativeLength { get; set; }
    public decimal EmployerRepresentativeWidth { get; set; }
    public decimal EmployerRepresentativeHeight { get; set; }
    public decimal EmployerRepresentativeWeight { get; set; }
    public decimal EmployerRepresentativeNumber { get; set; }
    public decimal EmployerRepresentativeVolume { get; set; }
    public string? EmployerRepresentativeDescription { get; set; } = string.Empty;
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}
