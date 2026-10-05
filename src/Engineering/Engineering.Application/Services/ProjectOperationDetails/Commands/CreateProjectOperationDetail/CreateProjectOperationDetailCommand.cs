using Engineering.Application.Services.ProjectOperationDetails.Commands.CreateVizardProjectOperationDetail;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Services;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.CreateProjectOperationDetail;

public record CreateProjectOperationDetailCommand(
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
    ProjectOperationDetailStatus? Status,
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