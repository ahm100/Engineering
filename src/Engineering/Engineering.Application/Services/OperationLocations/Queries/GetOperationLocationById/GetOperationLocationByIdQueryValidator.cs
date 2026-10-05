namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationById;

public class GetOperationLocationByIdQueryValidator : AbstractValidator<GetOperationLocationByIdQuery>
{
    public GetOperationLocationByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(OperationLocationErrors.IdIsEmpty);
    }
}