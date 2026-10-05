namespace Engineering.Application.Services.RequestMachineryBills.Commands.UpdateRequestMachineryBill;

public class UpdateRequestMachineryBillCommandValidator : AbstractValidator<UpdateRequestMachineryBillCommand>
{
    public UpdateRequestMachineryBillCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryBill).NotNull().NotEmpty().WithError(RequestMachineryBillErrors.InValidRequestMachinery);
        RuleFor(oo => oo.FromDate).NotNull().NotEmpty().WithError(RequestMachineryBillErrors.InValidFromDate);
        RuleFor(oo => oo.ToDate).NotNull().NotEmpty().WithError(RequestMachineryBillErrors.InValidTodate);
    }
}
