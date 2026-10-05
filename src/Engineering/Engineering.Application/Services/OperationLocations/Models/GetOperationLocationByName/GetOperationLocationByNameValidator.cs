namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByName;

public class GetOperationLocationByNameValidator : AbstractValidator<GetOperationLocationByNameRequest>
{
    public GetOperationLocationByNameValidator()
    {
        RuleFor(oo => oo.PrivateName).NotEmpty().WithError(OperationLocationErrors.PrivateNameIsEmpty);
    }
}
