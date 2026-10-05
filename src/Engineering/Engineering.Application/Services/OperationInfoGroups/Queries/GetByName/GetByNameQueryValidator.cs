namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetByName;

public class GetOperationInfoGroupByNameQueryValidator : AbstractValidator<GetOperationInfoGroupByNameQuery>
{
    public GetOperationInfoGroupByNameQueryValidator()
    {
        RuleFor(oo => oo.OperationInfoGroupName).NotEmpty().WithError(OperationInfoGroupErrors.OperationInfoGroupNameIsEmpty);
    }
}
