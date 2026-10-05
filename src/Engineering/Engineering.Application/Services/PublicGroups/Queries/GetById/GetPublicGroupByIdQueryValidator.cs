namespace Engineering.Application.Services.PublicGroups.Queries.GetById;

public class GetPublicGroupByIdQueryValidator : AbstractValidator<GetPublicGroupByIdQuery>
{
    public GetPublicGroupByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(OperationInfoErrors.NonStandardIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
