using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Services;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.CreateVizardProjectOperationDetail;

public record CreateVizardProjectOperationDetailCommand(
    string Code,
    ProjectOperation ProjectOperation,
    OperationLocation OperationLocation,
    DateTime? StartDate,
    DateTime? EndDate,
    decimal Length,
    bool LengthChangeable,
    decimal Width,
    bool WidthChangeable,
    decimal Height,
    bool HeightChangeable,
    decimal Weight,
    bool WeightChangeable,
    decimal Number,
    bool NumberChangeable,
    int Status,
    int Priority,
    int? Day,
    int? Hour,
    long? CreatedProductId,
    string? Description,
    List<long>? PlannerRequests,
    List<long>? ImplementationAssistantRequests,
    List<long>? TechnicalAssistantRequests,
    List<ContractorServiceModel>? ContractorServiceRequests,
    List<ExpertServiceModel>? ExpertRequests,
    List<ContractorExpertLinkModel>? ContractorExpertLinks,
    List<MachineryServiceModel>? MachineryRequests,
    List<ProductServiceModel>? ProductRequests,
    List<DeductionServiceModel>? DeductionRequests,
    List<string>? Urls,
    long? CompanyId
    ) : ICommand<ProjectOperationDetail>;

public class ContractorExpertLinkModel
{
    public string ContractorServiceTempId { get; set; } = string.Empty;
    public string ExpertTempId { get; set; } = string.Empty;
    public decimal Volume { get; set; }
    public bool IsActive { get; set; }
}