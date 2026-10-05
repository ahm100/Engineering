namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.Update;

public class UpdateOpAssignValidator
    : AbstractValidator<UpdateOpAssignRequest>
{
    public UpdateOpAssignValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ContractorServiceErrors.IdIsEmpty);

        RuleFor(oo => oo.ContractorId)
            .IsPositive(ContractorServiceErrors.ContractorIdMustGreaterThanZero);

        RuleFor(oo => oo.Volume)
            .GreaterThan(0)
            .WithError(ContractorServiceErrors.VolumeMustGreaterThan);
    }
}