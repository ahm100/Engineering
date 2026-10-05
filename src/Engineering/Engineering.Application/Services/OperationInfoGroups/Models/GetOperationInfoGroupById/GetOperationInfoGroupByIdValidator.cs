namespace Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupById;

public class GetOperationInfoGroupByIdValidator : AbstractValidator<GetOperationInfoGroupByIdRequest>
{
    public GetOperationInfoGroupByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationInfoGroupErrors.IdIsEmpty);
    }
}
