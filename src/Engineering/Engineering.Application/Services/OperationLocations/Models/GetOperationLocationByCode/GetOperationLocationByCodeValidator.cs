namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByCode;

public class GetOperationLocationByCodeValidator : AbstractValidator<GetOperationLocationByCodeRequest>
{
    public GetOperationLocationByCodeValidator()
    {
        RuleFor(oo => oo.PrivateCode).NotEmpty().WithError(OperationLocationErrors.PrivateCodeIsEmpty);
    }
}
