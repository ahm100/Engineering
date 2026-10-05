namespace Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryHistories;

public class GetRequestMachineryHistoriesValidator : AbstractValidator<GetRequestMachineryHistoriesRequest>
{
    public GetRequestMachineryHistoriesValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
    }
}
