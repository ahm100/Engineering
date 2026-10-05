namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.UpdateBatchPODContractorExpert;

public class UpdateBatchPODContractorExpertCommandValidator : AbstractValidator<UpdateBatchPODContractorExpertModel>
{
    public UpdateBatchPODContractorExpertCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
        RuleFor(oo => oo.Volume)
            .IsPositive(GlobalCmts.Volume);
        RuleFor(oo => oo.IsActive)
            .IsPositive(GlobalCmts.IsActive);
    }
}