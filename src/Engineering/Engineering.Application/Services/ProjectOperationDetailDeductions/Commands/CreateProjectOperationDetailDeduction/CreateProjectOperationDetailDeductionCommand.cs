using Engineering.Domain.Entities.ProjectOperationDetails;
using ProjectOperationDetailDeduction = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailDeduction;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.CreateProjectOperationDetailDeduction;

public record CreateProjectOperationDetailDeductionCommand(
    ProjectOperationDetail ProjectOperationDetail,
    decimal Length,
    decimal Width,
    decimal Height,
    decimal Weight,
    decimal Number
    ) : ICommand<ProjectOperationDetailDeduction>;