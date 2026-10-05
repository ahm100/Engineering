namespace Engineering.Application.Services.RequestContractors.Models.GetRequestContractorById;

public class GetRequestContractorByIdValidator : AbstractValidator<GetRequestContractorByIdRequest>
{
    public GetRequestContractorByIdValidator()
    {
        RuleFor(oo => oo.RequestContractorId).NotNull().NotEmpty().WithError(RequestContractorErrors.InValidRequestContractor)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
