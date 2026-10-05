namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.Delete;

public class DeleteOpAssignValidator
    : AbstractValidator<DeleteOpAssignRequest>
{
    public DeleteOpAssignValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ContractorServiceErrors.IdIsEmpty);
    }
}