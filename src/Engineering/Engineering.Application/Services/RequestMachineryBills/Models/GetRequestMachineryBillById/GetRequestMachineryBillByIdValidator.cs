namespace Engineering.Application.Services.RequestMachineryBills.Models.GetRequestMachineryBillById;

public class GetRequestMachineryBillByIdValidator : AbstractValidator<GetRequestMachineryBillByIdRequest>
{
    public GetRequestMachineryBillByIdValidator()
    {
        RuleFor(oo => oo.RequestMachineryBillId).NotNull().WithError(RequestMachineryBillErrors.InValidRequestMachineryBill);
    }
}
