namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationCodeByCostCenter;

public class GetOperationLocationCodeByCostCenterQueryValidator : AbstractValidator<GetOperationLocationCodeByCostCenterQuery>
{
    public GetOperationLocationCodeByCostCenterQueryValidator()
    {
        RuleFor(oo => oo.PrivateCode).NotEmpty().WithError(OperationLocationErrors.PrivateCodeIsEmpty);
    }
}
