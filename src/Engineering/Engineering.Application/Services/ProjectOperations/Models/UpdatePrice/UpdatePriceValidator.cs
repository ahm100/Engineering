namespace Engineering.Application.Services.ProjectOperations.Models.UpdatePrice;

public class UpdatePriceValidator : AbstractValidator<UpdatePriceRequest>
{
    public UpdatePriceValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectOperationErrors.IdIsEmpty);
        RuleFor(oo => oo.ChangePrice)
            .IsPositiveWithNullableInput(ProjectOperationCmts.ValidChangedPrice);
        RuleFor(oo => oo.IncreaseRate)
            .IsPositiveWithNullableInput(ProjectOperationCmts.IncreaseRate);
    }
}
