namespace Engineering.Application.Services.EmployerContracts.Contracts.UpdateEContractFinancial;

public class UpdateHeaderFinancialModelValidator : AbstractValidator<UpdateEContractFinancialRequest>
{
    public UpdateHeaderFinancialModelValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(EContractCmts.Id);

        RuleFor(c => c.CurrencyId)
            .IsPositive(EContractCmts.CurrencyId);

        When(oo => oo.UpdateEContractFinancials != null && oo.UpdateEContractFinancials.Any(), () =>
        {
            RuleForEach(oo => oo.UpdateEContractFinancials)
                .NotEmpty().SetValidator(new UpdateEContractFinancialModelValidator());
        });
    }
}

public class UpdateEContractFinancialModelValidator : AbstractValidator<UpdateEContractFinancialModel>
{
    public UpdateEContractFinancialModelValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(c => c.AdvancePayment)
            .IsRequiredDecimal(EContractCmts.AdvancePayment);

        When(oo => oo.CostOvers != null && oo.CostOvers.Any(), () =>
        {
            RuleForEach(oo => oo.CostOvers)
                .NotEmpty().SetValidator(new UpdateEmployerCostOverFinancialModelValidator());
        });

        When(oo => oo.ProjectOperations != null && oo.ProjectOperations.Any(), () =>
        {
            RuleForEach(oo => oo.ProjectOperations)
                .NotEmpty().SetValidator(new UpdateProjectOperationFinancialModelValidator());
        });
    }
}

public class UpdateProjectOperationFinancialModelValidator : AbstractValidator<UpdateProjectOperationFinancialModel>
{
    public UpdateProjectOperationFinancialModelValidator()
    {
        RuleFor(c => c.EOperationId)
            .IsPositive(EContractCmts.Id);

        RuleFor(c => c.IncreaseRate)
            .IsPositive(EContractCmts.IncreaseRate);

        RuleFor(c => c.Price)
            .IsPositive(ProjectOperationCmts.Price);
    }
}

public class UpdateEmployerCostOverFinancialModelValidator : AbstractValidator<UpdateEmployerCostOverFinancialModel>
{
    public UpdateEmployerCostOverFinancialModelValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(EContractCmts.CostOver);

        RuleFor(c => c.CostOverId)
            .IsPositive(EContractCmts.CostOver);

        RuleFor(c => c.Percent)
            .IsPositive(EContractCmts.Percent);

        When(oo => oo.CostOverImpacts != null && oo.CostOverImpacts.Any(), () =>
        {
            RuleForEach(oo => oo.CostOverImpacts)
                .NotEmpty().SetValidator(new UpdateCostOverImpactFinancialModelValidator());
        });
    }
}

public class UpdateCostOverImpactFinancialModelValidator : AbstractValidator<UpdateCostOverImpactFinancialModel>
{
    public UpdateCostOverImpactFinancialModelValidator()
    {
        RuleFor(c => c.CostOverId)
            .IsPositive(EContractCmts.CostOver);

        RuleFor(c => c.Percent)
            .IsPositive(EContractCmts.Percent);
    }
}