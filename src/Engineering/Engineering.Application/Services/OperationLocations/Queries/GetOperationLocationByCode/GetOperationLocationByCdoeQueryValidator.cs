namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationByCode;

public class GetOperationLocationByCodeQueryValidator : AbstractValidator<GetOperationLocationByCodeQuery>
{
    public GetOperationLocationByCodeQueryValidator()
    {
        RuleFor(oo => oo.PrivateCode).NotEmpty().WithError(OperationLocationErrors.PrivateCodeIsEmpty);
    }
}