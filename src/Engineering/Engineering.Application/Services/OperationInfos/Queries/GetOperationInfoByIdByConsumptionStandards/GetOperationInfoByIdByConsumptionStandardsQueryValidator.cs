namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByConsumptionStandards;

public class GetOperationInfoByIdByConsumptionStandardsQueryValidator : AbstractValidator<GetOperationInfoByIdByConsumptionStandardsQuery>
{
    public GetOperationInfoByIdByConsumptionStandardsQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}
