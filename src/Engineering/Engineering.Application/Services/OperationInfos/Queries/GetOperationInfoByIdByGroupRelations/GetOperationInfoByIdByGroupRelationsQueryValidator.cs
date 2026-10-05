namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByGroupRelations;

public class GetOperationInfoByIdByGroupRelationsQueryValidator : AbstractValidator<GetOperationInfoByIdByGroupRelationsQuery>
{
    public GetOperationInfoByIdByGroupRelationsQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}
