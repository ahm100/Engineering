namespace Engineering.Application.Services.RequestMachineryManagements.Models.AssignMachineryForRequestMachinery;

public class AssignMachineryForRequestMachineryValidator : AbstractValidator<AssignMachineryForRequestMachineryRequest>
{
    public AssignMachineryForRequestMachineryValidator()
    {
        RuleFor(oo => oo.ConfirmFromDate).NotNull().WithError(RequestMachineryInquiryErrors.InValidFromDate);
        RuleFor(oo => oo.ConfirmToDate).NotNull().WithError(RequestMachineryInquiryErrors.InValidToDate);
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryInquiryErrors.InValidRequestMachinery)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
        RuleFor(oo => oo.ContractorMachineryId).NotNull().WithError(ContractorMachineryErrors.InValidContractorMachineryId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
