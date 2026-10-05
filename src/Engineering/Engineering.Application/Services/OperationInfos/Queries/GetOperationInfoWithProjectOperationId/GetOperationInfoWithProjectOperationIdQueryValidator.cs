namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoWithProjectOperationId;

public class GetOperationInfoWithProjectOperationIdQueryValidator : AbstractValidator<GetOperationInfoWithProjectOperationIdQuery>
{
    public GetOperationInfoWithProjectOperationIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoErrors.IdIsEmpty);
    }
}
