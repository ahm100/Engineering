namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts.Exporter;

public class GetCStatementSContractsExporterValidator : AbstractValidator<GetCStatementSContractsExporterRequest>
{
    public GetCStatementSContractsExporterValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(oo => oo.PageIndex)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);

        RuleFor(oo => oo.PageSize)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);

        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex)
                .GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}

