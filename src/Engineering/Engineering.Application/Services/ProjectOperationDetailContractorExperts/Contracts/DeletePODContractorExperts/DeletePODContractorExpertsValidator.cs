namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.DeletePODContractorExperts;

public class DeletePODContractorExpertsValidator : AbstractValidator<DeletePODContractorExpertsRequest>
{
    public DeletePODContractorExpertsValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}