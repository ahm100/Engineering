namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.CreatePODContractorExperts;

public class CreatePODContractorExpertsCommandValidator : AbstractValidator<CreatePODContractorExpertsCommand>
{
    public CreatePODContractorExpertsCommandValidator()
    {
        RuleFor(oo => oo.ConsumableVolumeExpertId)
            .IsPositive(ProjectDetailCmts.ConsumableVolumeExpert);
        RuleFor(oo => oo.ProjectOperationDetailContractorServiceId)
            .IsPositive(ProjectDetailCmts.ProjectOperationDetailContractorService);
        RuleFor(oo => oo.Volume)
            .IsPositive(ProjectDetailCmts.Volume);
    }
}