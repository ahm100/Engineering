namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperationProduct;

public class DeleteDailyProjectOperationProductCommandValidator : AbstractValidator<DeleteDailyProjectOperationProductCommand>
{
    public DeleteDailyProjectOperationProductCommandValidator()
    {
        RuleFor(oo => oo.DailyProjectOperationProductId).NotNull().WithError(DailyProjectOperationProductErrors.DailyProjectOperationProductWithIdNotFound);
    }
}
