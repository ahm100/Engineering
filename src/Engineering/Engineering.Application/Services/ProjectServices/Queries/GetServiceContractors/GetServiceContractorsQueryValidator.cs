namespace Engineering.Application.Services.ProjectServices.Queries.GetServiceContractors;

public class GetServiceContractorsQueryValidator : AbstractValidator<GetServiceContractorsQuery>
{
    public GetServiceContractorsQueryValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().WithError(ProjectServiceErrors.ProjectIsEmpty)
            .GreaterThan(0).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.ServiceInfoIds).NotNull().NotEmpty().WithError(ProjectServiceErrors.ServiceIsEmpty);
    }
}