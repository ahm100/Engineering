namespace Engineering.Application.Services.OperationInfoGroups.Queries.GetByCode;

public class GetOperationInfoGroupByCodeQueryValidator : AbstractValidator<GetOperationInfoGroupByCodeQuery>
{
    public GetOperationInfoGroupByCodeQueryValidator()
    {
        RuleFor(oo => oo.OperationInfoGroupCode).NotEmpty().WithError(OperationInfoGroupErrors.OperationInfoGroupCodeIsEmpty);
    }
}
