namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationNameByCostCenter;

public class GetOperationLocationNameByCostCenterValidator : AbstractValidator<GetOperationLocationNameByCostCenterRequest>
{
    public GetOperationLocationNameByCostCenterValidator()
    {
        RuleFor(oo => oo.PrivateName).NotEmpty().WithError(OperationLocationErrors.PrivateNameIsEmpty);
    }
}
