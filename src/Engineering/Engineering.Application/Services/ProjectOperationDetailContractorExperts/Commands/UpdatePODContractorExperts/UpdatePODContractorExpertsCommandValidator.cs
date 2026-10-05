namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.UpdatePODContractorExperts;

public class UpdatePODContractorExpertsCommandValidator : AbstractValidator<UpdatePODContractorExpertsCommand>
{
    public UpdatePODContractorExpertsCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}