namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationPrice;

public class UpdateProjectOperationPriceCommandValidator : AbstractValidator<UpdateProjectOperationPriceCommand>
{
    public UpdateProjectOperationPriceCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperation).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}