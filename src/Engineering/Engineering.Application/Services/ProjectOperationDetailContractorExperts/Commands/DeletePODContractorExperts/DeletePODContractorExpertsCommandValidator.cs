namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.DeletePODContractorExperts;

public class DeletePODContractorExpertsCommandValidator : AbstractValidator<DeletePODContractorExpertsCommand>
{
    public DeletePODContractorExpertsCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}