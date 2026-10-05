namespace Engineering.Application.Services.RequestContractors.Queries.GetRequestContractorById;

public class GetRequestContractorByIdQueryValidator : AbstractValidator<GetRequestContractorByIdQuery>
{
    public GetRequestContractorByIdQueryValidator()
    {
        RuleFor(oo => oo.RequestContractorId).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidRequestContractor)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
