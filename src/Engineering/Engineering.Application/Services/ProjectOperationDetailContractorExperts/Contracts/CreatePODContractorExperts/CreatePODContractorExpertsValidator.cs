namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.CreatePODContractorExperts;

public class CreatePODContractorExpertsValidator : AbstractValidator<CreatePODContractorExpertsRequest>
{
    public CreatePODContractorExpertsValidator()
    {
        RuleFor(oo => oo.ConsumableVolumeExpertId)
            .IsPositive(ProjectDetailCmts.ConsumableVolumeExpert);
        RuleFor(oo => oo.ProjectOperationDetailContractorServiceId)
            .IsPositive(ProjectDetailCmts.ProjectOperationDetailContractorService);
        RuleFor(oo => oo.Volume)
            .IsPositive(ProjectDetailCmts.Volume);
    }
}