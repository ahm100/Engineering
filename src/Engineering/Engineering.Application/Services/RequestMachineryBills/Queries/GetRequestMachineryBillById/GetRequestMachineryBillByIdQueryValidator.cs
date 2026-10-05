namespace Engineering.Application.Services.RequestMachineryBills.Queries.GetRequestMachineryBillById;

public class GetRequestMachineryBillByIdQueryValidator : AbstractValidator<GetRequestMachineryBillByIdQuery>
{
    public GetRequestMachineryBillByIdQueryValidator()
    {
        RuleFor(oo => oo.RequestMachineryBillId).NotNull().WithError(RequestMachineryBillErrors.InValidRequestMachineryBill)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
