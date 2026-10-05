namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoById;

public class GetOperationInfoByIdQueryValidator : AbstractValidator<GetOperationInfoByIdQuery>
{
    public GetOperationInfoByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}