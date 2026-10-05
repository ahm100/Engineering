namespace Engineering.Application.Services.CostOvers.Commands.SubmitCostOverForApproval;

public sealed class SubmitCostOverForApprovalCommandValidator : AbstractValidator<SubmitCostOverForApprovalCommand>
{
    /// <summary>شناسه هزینه بالاسری، شرکت و کاربر درخواست کننده را اعتبارسنجی می کند.</summary>
    public SubmitCostOverForApprovalCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithError(CostOverErrors.UnValidId);

        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithError(GlobalErrors.InvalidCompany);

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithError(GlobalErrors.InvalidCompany);
    }
}
