namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.Create;

public class CreateOpAssignValidator
    : AbstractValidator<CreateOpAssignRequest>
{
    public CreateOpAssignValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(ProjectOperationDetailErrors.ProjectOperationIdIsEmpty);

        RuleFor(oo => oo.ContractorId)
            .IsPositive(ContractorServiceErrors.ContractorIdMustGreaterThanZero);

        RuleFor(oo => oo.Items)
            .NotEmpty()
            .WithError(ContractorServiceErrors.ContractorServiceRequestsIsEmpty);

        When(oo => oo.Items != null, () =>
        {
            RuleForEach(oo => oo.Items)
                .NotEmpty()
                .SetValidator(new CreateOperationBasedAssignmentItemValidator());
        });
    }
}

public class CreateOperationBasedAssignmentItemValidator
    : AbstractValidator<CreateOpAssignItem>
{
    public CreateOperationBasedAssignmentItemValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId)
            .IsPositive(ContractorServiceErrors.ProjectOperationDetailIdGreaterThanZero);

        When(oo => oo.Volume.HasValue, () =>
        {
            RuleFor(oo => oo.Volume)
                .GreaterThan(0)
                .WithError(ContractorServiceErrors.VolumeMustGreaterThan);
        });
    }
}