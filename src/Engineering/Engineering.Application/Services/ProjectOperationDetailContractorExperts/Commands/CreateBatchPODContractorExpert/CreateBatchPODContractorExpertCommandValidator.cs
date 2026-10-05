namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.CreateBatchPODContractorExpert;

public class CreateBatchPODContractorExpertCommandValidator : AbstractValidator<CreateBatchPODContractorExpertModel>
{
    public CreateBatchPODContractorExpertCommandValidator()
    {
        RuleFor(oo => oo.Volume)
            .IsPositive(GlobalCmts.Volume);
        RuleFor(oo => oo.IsActive)
            .IsPositive(GlobalCmts.IsActive);
    }
}