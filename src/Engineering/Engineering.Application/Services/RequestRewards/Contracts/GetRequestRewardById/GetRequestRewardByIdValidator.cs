
namespace Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;

public class GetRequestRewardByIdValidator : AbstractValidator<GetRequestRewardByIdRequest>
{
    public GetRequestRewardByIdValidator()
    {
        RuleFor(c => c.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestRewardErrors.InValidRequestRewardId);
    }
}
