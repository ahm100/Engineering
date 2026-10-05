namespace Engineering.Application.Services.ProjectOperations.Models.UpdateProjectOperationPrice;

public class UpdateProjectOperationPriceValidator : AbstractValidator<UpdateProjectOperationPriceRequest>
{
    public UpdateProjectOperationPriceValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationErrors.IdIsEmpty);
    }
}
