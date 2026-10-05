namespace Engineering.Application.Services.RequestContractors.Queries.GetRequestContractorModelById;

public class GetRequestContractorModelByIdQueryValidator : AbstractValidator<GetRequestContractorModelByIdQuery>
{
    public GetRequestContractorModelByIdQueryValidator()
    {
        RuleFor(oo => oo.RequestContractorId).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidRequestContractor)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
