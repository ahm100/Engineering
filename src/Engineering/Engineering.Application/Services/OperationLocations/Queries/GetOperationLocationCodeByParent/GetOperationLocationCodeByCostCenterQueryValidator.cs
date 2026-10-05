namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationCodeByParent;

public class GetOperationLocationCodeByParentQueryValidator : AbstractValidator<GetOperationLocationCodeByParentQuery>
{
    public GetOperationLocationCodeByParentQueryValidator()
    {
        RuleFor(oo => oo.PrivateCode).NotEmpty().WithError(OperationLocationErrors.PrivateCodeIsEmpty);
        RuleFor(oo => oo.ParentId).NotNull().WithError(OperationLocationErrors.ParentIdIsEmpty);
    }
}
