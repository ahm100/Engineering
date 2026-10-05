using ProjectOperationDetailDeduction = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailDeduction;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.UpdateProjectOperationDetailDeduction;

public record UpdateProjectOperationDetailDeductionCommand(
    long Id,
    decimal Length,
    decimal Width,
    decimal Height,
    decimal Weight,
    decimal Number
    ) : ICommand<ProjectOperationDetailDeduction>;