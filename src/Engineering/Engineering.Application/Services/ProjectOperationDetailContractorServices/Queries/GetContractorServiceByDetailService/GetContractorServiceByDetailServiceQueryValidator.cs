namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetContractorServiceByDetailService;

public class GetContractorServiceByDetailServiceQueryValidator : AbstractValidator<GetContractorServiceByDetailServiceQuery>
{
    public GetContractorServiceByDetailServiceQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().NotEmpty().WithError(ContractorServiceErrors.ProjectOperationDetailIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.ServiceInfoId).NotNull().NotEmpty().WithError(ContractorServiceErrors.ServiceIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}