
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByFilter;

public class GetsContractorServiceByFilterValidator : AbstractValidator<GetsContractorServiceByFilterRequest>
{
    public GetsContractorServiceByFilterValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(ContractorServiceErrors.CostCenterIdIsEmpty);
        RuleFor(oo => oo.ProjectId).NotNull().WithError(ContractorServiceErrors.ProjectIdIsEmpty);
        RuleFor(oo => oo.ProjectOperationIds).NotEmpty().WithError(ContractorServiceErrors.ProjectOperationIdsIsEmpty);
        RuleFor(oo => oo.ServiceInfoIds).NotEmpty().WithError(ContractorServiceErrors.ServiceInfoIdsIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}
