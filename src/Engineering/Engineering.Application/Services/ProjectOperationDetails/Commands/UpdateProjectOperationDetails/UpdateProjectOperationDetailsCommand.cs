using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.UpdateProjectOperationDetails;

public record UpdateProjectOperationDetailsCommand(
    List<ProjectOperationDetail> ProjectOperationDetails,
    DateTime? StartDate,
    DateTime? EndDate,
    decimal? Length,
    bool? LengthChangeable,
    decimal? Width,
    bool? WidthChangeable,
    decimal? Height,
    bool? HeightChangeable,
    decimal? Weight,
    bool? WeightChangeable,
    decimal? Number,
    bool? NumberChangeable,
    decimal? FinalAmount,
    ProjectOperationDetailStatus? Status,
    int? Day,
    int? Hour,
    long? CreatedProductId,
    List<long>? PlannerRequests,
    List<long>? ImplementationAssistantRequests,
    List<long>? TechnicalAssistantRequests,
    string? StatusDescription
    ) : ICommand<List<ProjectOperationDetail>>;