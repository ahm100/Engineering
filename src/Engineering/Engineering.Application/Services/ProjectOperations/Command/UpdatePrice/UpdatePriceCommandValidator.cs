namespace Engineering.Application.Services.ProjectOperations.Command.UpdatePrice;

public class UpdatePriceValidator : AbstractValidator<UpdatePriceCommand>
{
    public UpdatePriceValidator()
    {
        RuleFor(oo => oo.ChangedPrice)
            .IsPositiveWithNullableInput(ProjectOperationCmts.ChangedPrice);
        RuleFor(oo => oo.IncreaseRate)
            .IsPositiveWithNullableInput(ProjectOperationCmts.IncreaseRate);
    }
}
