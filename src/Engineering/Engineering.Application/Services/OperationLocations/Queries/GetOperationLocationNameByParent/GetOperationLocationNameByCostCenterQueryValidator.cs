namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationNameByParent;

public class GetOperationLocationNameByParentQueryValidator : AbstractValidator<GetOperationLocationNameByParentQuery>
{
    public GetOperationLocationNameByParentQueryValidator()
    {
        RuleFor(oo => oo.PrivateName).NotEmpty().WithError(OperationLocationErrors.PrivateNameIsEmpty);
        RuleFor(oo => oo.ParentId).NotNull().WithError(OperationLocationErrors.ParentIdIsEmpty);
    }
}
