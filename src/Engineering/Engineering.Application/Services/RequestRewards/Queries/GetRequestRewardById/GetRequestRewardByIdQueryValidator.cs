namespace Engineering.Application.Services.RequestRewards.Queries.GetRequestRewardById;

public class GetRequestRewardByIdQueryValidator : AbstractValidator<GetRequestRewardByIdQuery>
{
    public GetRequestRewardByIdQueryValidator()
    {
        RuleFor(c => c.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestRewardErrors.InValidRequestRewardId);
    }
}
