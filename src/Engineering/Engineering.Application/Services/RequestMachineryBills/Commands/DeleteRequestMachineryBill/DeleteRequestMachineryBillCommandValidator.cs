namespace Engineering.Application.Services.RequestMachineryBills.Commands.DeleteRequestMachineryBill;

public class DeleteRequestMachineryBillCommandValidator : AbstractValidator<DeleteRequestMachineryBillCommand>
{
    public DeleteRequestMachineryBillCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryBillId).NotNull().WithError(RequestMachineryBillErrors.InValidRequestMachineryBill);
    }
}
