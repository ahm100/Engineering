namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationCodeByCostCenter;

public class GetOperationLocationCodeByCostCenterValidator : AbstractValidator<GetOperationLocationCodeByCostCenterRequest>
{
    public GetOperationLocationCodeByCostCenterValidator()
    {
        RuleFor(oo => oo.PrivateCode).NotEmpty().WithError(OperationLocationErrors.PrivateCodeIsEmpty);
    }
}
