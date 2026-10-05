namespace Engineering.Application.Services.RequestMachineryBills.Models.UpdateRequestMachineryBill;

public class UpdateRequestMachineryBillValidator : AbstractValidator<UpdateRequestMachineryBillRequest>
{
    public UpdateRequestMachineryBillValidator()
    {

        RuleFor(oo => oo.Id).NotNull().WithError(RequestMachineryBillErrors.InValidRequestMachineryBill)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.FromDate).NotNull().NotEmpty().WithError(RequestMachineryBillErrors.InValidFromDate);
        RuleFor(oo => oo.ToDate).NotNull().NotEmpty().WithError(RequestMachineryBillErrors.InValidTodate);
    }
}
