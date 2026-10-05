namespace Engineering.Application.Services.PublicGroups.Models.GetById;

public class GetPublicGroupByIdValidator : AbstractValidator<GetPublicGroupByIdRequest>
{
    public GetPublicGroupByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(OperationInfoErrors.NonStandardIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
