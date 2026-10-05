namespace Engineering.Application.Services.RequestContractors.Models.GetRequestContractorHistories;

public class GetRequestContractorHistoriesValidator : AbstractValidator<GetRequestContractorHistoriesRequest>
{
    public GetRequestContractorHistoriesValidator()
    {
        RuleFor(oo => oo.RequestContractorId).NotNull().WithError(RequestContractorErrors.InValidRequestContractor);
    }
}
