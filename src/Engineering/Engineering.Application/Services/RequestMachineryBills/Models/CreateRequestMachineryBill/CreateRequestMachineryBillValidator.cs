namespace Engineering.Application.Services.RequestMachineryBills.Models.CreateRequestMachineryBill;

public class CreateRequestMachineryBillValidator : AbstractValidator<CreateRequestMachineryBillRequest>
{
    public CreateRequestMachineryBillValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().NotEmpty().WithError(RequestMachineryBillErrors.InValidRequestMachinery)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.FromDate).NotNull().NotEmpty().WithError(RequestMachineryBillErrors.InValidFromDate);
        RuleFor(oo => oo.ToDate).NotNull().NotEmpty().WithError(RequestMachineryBillErrors.InValidTodate);
    }
}
