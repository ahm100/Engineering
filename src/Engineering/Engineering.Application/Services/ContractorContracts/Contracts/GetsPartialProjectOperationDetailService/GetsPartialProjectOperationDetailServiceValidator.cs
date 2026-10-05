namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsPartialProjectOperationDetailService;

public class GetsPartialProjectOperationDetailServiceValidator
    : AbstractValidator<GetsPartialProjectOperationDetailServiceRequest>
{
    public GetsPartialProjectOperationDetailServiceValidator()
    {
        RuleFor(x => x.ServiceInfoId)
            .IsPositive(GlobalCmts.ServiceId);
    }
}