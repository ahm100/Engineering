using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetail;

public record UpdateProjectOperationDetailRequest : IHttpRequest
{
    public long Id { get; set; }
    public string? Code { get; set; }
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
    public string? StatusDescription { get; set; }
    public List<long>? PlannerRequests { get; set; }
    public List<long>? ImplementationAssistantRequests { get; set; }
    public List<long>? TechnicalAssistantRequests { get; set; }
    public List<UpdateContractorServiceRequestModel>? ContractorServiceRequests { get; set; }
    public List<UpdateExpertRequestModel>? ExpertRequests { get; set; }
    public List<UpdateContractorExpertLinkModel>? ContractorExpertLinks { get; set; }
    public List<UpdateMachineryRequestModel>? MachineryRequests { get; set; }
    public List<UpdateProductRequestModel>? ProductRequests { get; set; }
    public List<UpdateDeductionRequestModel>? DeductionRequests { get; set; }
    public List<string>? Urls { get; set; }
}

public class UpdateContractorExpertLinkModel
{
    public long? Id { get; set; }
    public long? ContractorServiceId { get; set; }
    public string? ContractorServiceTempId { get; set; }
    public long? ExpertId { get; set; }
    public string? ExpertTempId { get; set; }
    public decimal? Volume { get; set; }
    public bool? IsActive { get; set; }
    public bool IsDelete { get; set; }
}