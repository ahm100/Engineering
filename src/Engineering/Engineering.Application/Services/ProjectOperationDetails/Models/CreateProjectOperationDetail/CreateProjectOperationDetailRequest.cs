using Engineering.Application.Services.ProjectOperationDetails.Commands.CreateVizardProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.CreateProjectOperationDetail;

public record CreateProjectOperationDetailRequest : IHttpRequest
{
    public string? Code { get; set; }
    public long ProjectOperationId { get; set; }
    public long OperationLocationId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Length { get; set; }
    public bool LengthChangeable { get; set; }
    public decimal Width { get; set; }
    public bool WidthChangeable { get; set; }
    public decimal Height { get; set; }
    public bool HeightChangeable { get; set; }
    public decimal Weight { get; set; }
    public bool WeightChangeable { get; set; }
    public decimal Number { get; set; }
    public bool NumberChangeable { get; set; }
    public ProjectOperationDetailStatus? Status { get; set; } = ProjectOperationDetailStatus.NotStarted;
    public int Priority { get; set; }
    public int? Day { get; set; } = 0;
    public int? Hour { get; set; } = 0;
    public long? CreatedProductId { get; set; }
    public string? Description { get; set; }
    public List<long>? PlannerRequests { get; set; }
    public List<long>? ImplementationAssistantRequests { get; set; }
    public List<long>? TechnicalAssistantRequests { get; set; }
    public List<CreateContractorServiceRequestModel>? ContractorServiceRequests { get; set; }
    public List<CreateExpertRequestModel>? ExpertRequests { get; set; }
    public List<ContractorExpertLinkModel>? ContractorExpertLinks { get; set; }
    public List<CreateMachineryRequestModel>? MachineryRequests { get; set; }
    public List<CreateProductRequestModel>? ProductRequests { get; set; }
    public List<CreateDeductionRequestModel>? DeductionRequests { get; set; }
    public List<string>? Urls { get; set; }
}
