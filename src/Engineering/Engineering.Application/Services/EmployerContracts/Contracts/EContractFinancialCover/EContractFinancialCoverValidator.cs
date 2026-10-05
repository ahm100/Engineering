namespace Engineering.Application.Services.EmployerContracts.Contracts.EContractFinancialCover;

public class EContractFinancialCoverRequestValidator : AbstractValidator<EContractFinancialCoverRequest>
{
    public EContractFinancialCoverRequestValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(c => c.CurrencyId)
            .IsPositive(EContractCmts.CurrencyId);

        RuleForEach(oo => oo.EContracts)
            .NotEmpty().SetValidator(new EContractFinancialCoverModelValidator());

    }
}

public class EContractFinancialCoverModelValidator : AbstractValidator<EContractFinancialCoverModel>
{
    public EContractFinancialCoverModelValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(c => c.AdvancePayment)
            .IsRequiredDecimal(EContractCmts.AdvancePayment);

        When(oo => oo.ProjectOperations != null && oo.ProjectOperations.Any(), () =>
        {
            RuleForEach(oo => oo.ProjectOperations)
                .NotEmpty().SetValidator(new ProjectOperationFinancialModelValidator());
        });

        When(oo => oo.CostOvers != null && oo.CostOvers.Any(), () =>
        {
            RuleForEach(oo => oo.CostOvers)
                .NotEmpty().SetValidator(new CreateEmployerCostOverFinancialModelValidator());
        });
    }
}

public class CreateEmployerCostOverFinancialModelValidator : AbstractValidator<CreateEmployerCostOverFinancialModel>
{
    public CreateEmployerCostOverFinancialModelValidator()
    {
        RuleFor(c => c.CostOverId)
            .IsPositive(EContractCmts.CostOver);

        RuleFor(c => c.Percent)
            .IsPositive(EContractCmts.Percent);

        When(oo => oo.CostOverImpacts != null && oo.CostOverImpacts.Any(), () =>
        {
            RuleForEach(oo => oo.CostOverImpacts)
                .NotEmpty().SetValidator(new CreateCostOverImpactFinancialModelValidator());
        });
    }
}

public class ProjectOperationFinancialModelValidator : AbstractValidator<ProjectOperationFinancialModel>
{
    public ProjectOperationFinancialModelValidator()
    {
        RuleFor(c => c.EOperationId)
            .IsPositive(EContractCmts.EmployerOperation);

        RuleFor(c => c.Price)
            .IsPositive(ProjectOperationCmts.Price);

        RuleFor(c => c.IncreaseRate)
            .IsPositive(EContractCmts.IncreaseRate);
    }
}

public class CreateCostOverImpactFinancialModelValidator : AbstractValidator<CreateCostOverImpactFinancialModel>
{
    public CreateCostOverImpactFinancialModelValidator()
    {
        RuleFor(c => c.CostOverId)
            .IsPositive(EContractCmts.CostOver);

        RuleFor(c => c.Percent)
            .IsPositive(EContractCmts.Percent);
    }
}

public class AssignEOServiceModelValidator : AbstractValidator<AssignEOServiceModel>
{
    public AssignEOServiceModelValidator()
    {
        RuleFor(c => c.ServiceId)
            .IsPositive(EContractCmts.Id);

        RuleFor(c => c.MaxPrice)
            .IsPositive(EContractCmts.Percent);

        RuleFor(c => c.Tax)
            .IsPositive(EContractCmts.EmployerOperation);

        RuleFor(c => c.TaxPercent)
            .IsPositive(ProjectOperationCmts.TolerancePercentage);

        RuleFor(c => c.TransportationCost)
            .IsPositive(ProjectOperationCmts.Price);

        RuleFor(c => c.TransportationCostPercent)
            .IsPositive(ProjectOperationCmts.Price);

        RuleFor(c => c.IncreaseRate)
            .IsPositive(EContractCmts.IncreaseRate);
    }
}
public class UpdateAssignEOServiceModelValidator : AbstractValidator<UpdateAssignEOServiceModel>
{
    public UpdateAssignEOServiceModelValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(EContractCmts.Id);

        RuleFor(c => c.MaxPrice)
            .IsPositive(EContractCmts.Percent);

        RuleFor(c => c.Tax)
            .IsPositive(EContractCmts.EmployerOperation);

        RuleFor(c => c.TaxPercent)
            .IsPositive(ProjectOperationCmts.TolerancePercentage);

        RuleFor(c => c.TransportationCost)
            .IsPositive(ProjectOperationCmts.Price);

        RuleFor(c => c.TransportationCostPercent)
            .IsPositive(ProjectOperationCmts.Price);

        RuleFor(c => c.IncreaseRate)
            .IsPositive(EContractCmts.IncreaseRate);
    }
}