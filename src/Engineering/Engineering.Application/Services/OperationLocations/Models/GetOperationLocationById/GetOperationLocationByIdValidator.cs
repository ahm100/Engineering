namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationById;

public class GetOperationLocationByIdValidator : AbstractValidator<GetOperationLocationByIdRequest>
{
    public GetOperationLocationByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationLocationErrors.IdIsEmpty);
    }
}
