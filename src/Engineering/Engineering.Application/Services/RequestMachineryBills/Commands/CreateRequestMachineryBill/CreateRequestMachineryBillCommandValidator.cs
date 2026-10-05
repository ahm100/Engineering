namespace Engineering.Application.Services.RequestMachineryBills.Commands.CreateRequestMachineryBill;

public class CreateRequestMachineryBillCommandValidator : AbstractValidator<CreateRequestMachineryBillCommand>
{
    public CreateRequestMachineryBillCommandValidator()
    {
        RuleFor(oo => oo.RequestMachinery).NotNull().NotEmpty().WithError(RequestMachineryBillErrors.InValidRequestMachinery);
        RuleFor(oo => oo.BillDate).NotNull().NotEmpty().WithError(RequestMachineryBillErrors.InValidBillDate);
        RuleFor(oo => oo.FromDate).NotNull().NotEmpty().WithError(RequestMachineryBillErrors.InValidFromDate);
        RuleFor(oo => oo.ToDate).NotNull().NotEmpty().WithError(RequestMachineryBillErrors.InValidTodate);
    }
}
