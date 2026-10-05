namespace Engineering.Application.Services.RequestMachineryBills.Models.DeleteRequestMachineryBill;

public class DeleteRequestMachineryBillValidator : AbstractValidator<DeleteRequestMachineryBillRequest>
{
    public DeleteRequestMachineryBillValidator()
    {
        RuleFor(oo => oo.RequestMachineryBillId).NotNull().WithError(RequestMachineryBillErrors.InValidRequestMachineryBill)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
