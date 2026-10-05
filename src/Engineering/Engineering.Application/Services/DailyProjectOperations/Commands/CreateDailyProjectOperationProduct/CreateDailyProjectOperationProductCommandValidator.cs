namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationProduct;

public class CreateDailyProjectOperationProductCommandValidator : AbstractValidator<CreateDailyProjectOperationProductCommand>
{
    public CreateDailyProjectOperationProductCommandValidator()
    {
        RuleFor(oo => oo.ProductId).NotNull().WithError(DailyProjectOperationProductErrors.InValidProductId);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(DailyProjectOperationProductErrors.InValidFinalValue);
        RuleFor(oo => oo.DailyProjectOperation).NotNull().WithError(DailyProjectOperationProductErrors.InValidDailyProjectOperation);
        RuleFor(oo => oo.ConsumableVolumeProduct).NotNull().WithError(DailyProjectOperationProductErrors.InValidConsumableVolumeProduct);
    }
}
