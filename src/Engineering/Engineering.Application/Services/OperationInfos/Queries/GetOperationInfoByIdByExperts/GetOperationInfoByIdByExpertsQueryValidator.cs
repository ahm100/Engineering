namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByExperts;

public class GetOperationInfoByIdByExpertsQueryValidator : AbstractValidator<GetOperationInfoByIdByExpertsQuery>
{
    public GetOperationInfoByIdByExpertsQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}
