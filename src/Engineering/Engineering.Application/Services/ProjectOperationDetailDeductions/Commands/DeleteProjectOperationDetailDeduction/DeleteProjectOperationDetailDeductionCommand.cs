using ProjectOperationDetailDeduction = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailDeduction;

namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Commands.DeleteProjectOperationDetailDeduction;

public record DeleteProjectOperationDetailDeductionCommand(
    long Id
    ) : ICommand<ProjectOperationDetailDeduction>;