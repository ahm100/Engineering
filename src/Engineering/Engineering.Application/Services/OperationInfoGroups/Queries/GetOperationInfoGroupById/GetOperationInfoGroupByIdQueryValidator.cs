namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetOperationInfoGroupById;

public class GetOperationInfoGroupByIdQueryValidator : AbstractValidator<GetOperationInfoGroupByIdQuery>
{
    public GetOperationInfoGroupByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoGroupErrors.IdIsEmpty);
    }
}