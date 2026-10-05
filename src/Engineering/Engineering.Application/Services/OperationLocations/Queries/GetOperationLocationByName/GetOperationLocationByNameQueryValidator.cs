namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationByName;

public class GetOperationLocationByNameQueryValidator : AbstractValidator<GetOperationLocationByNameQuery>
{
    public GetOperationLocationByNameQueryValidator()
    {
        RuleFor(oo => oo.PrivateName).NotEmpty().WithError(OperationLocationErrors.PrivateNameIsEmpty);
    }
}