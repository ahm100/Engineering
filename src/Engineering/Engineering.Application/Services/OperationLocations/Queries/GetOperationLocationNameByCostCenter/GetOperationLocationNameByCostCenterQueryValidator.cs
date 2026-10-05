namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationNameByCostCenter;

public class GetOperationLocationNameByCostCenterQueryValidator : AbstractValidator<GetOperationLocationNameByCostCenterQuery>
{
    public GetOperationLocationNameByCostCenterQueryValidator()
    {
        RuleFor(oo => oo.PrivateName).NotEmpty().WithError(OperationLocationErrors.PrivateNameIsEmpty);
    }
}
