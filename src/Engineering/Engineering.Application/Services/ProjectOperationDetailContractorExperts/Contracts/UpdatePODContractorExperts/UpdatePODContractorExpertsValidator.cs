namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.UpdatePODContractorExperts;

public class UpdatePODContractorExpertsValidator : AbstractValidator<UpdatePODContractorExpertsRequest>
{
    public UpdatePODContractorExpertsValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}