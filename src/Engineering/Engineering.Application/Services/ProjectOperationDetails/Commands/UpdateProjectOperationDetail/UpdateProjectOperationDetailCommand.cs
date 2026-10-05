using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.UpdateProjectOperationDetail;

public record UpdateProjectOperationDetailCommand(
    string Code,
    ProjectOperationDetail ProjectOperationDetail,
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
    decimal FinalAmount,
    ProjectOperationDetailStatus? Status,
    int Priority,
    int? Day,
    int? Hour,
    long? CreatedProductId,
    string? Description,
    List<long>? PlannerRequests,
    List<long>? ImplementationAssistantRequests,
    List<long>? TechnicalAssistantRequests,
    List<string>? Urls,
    long? CompanyId,
    string? StatusDescription
    ) : ICommand<ProjectOperationDetail>;