namespace Engineering.Application.Services.ProjectWbses.Contracts.GetExportMppFile;

public class GetExportMppFileValidator
    : AbstractValidator<GetExportMppFileRequest>
{
    public GetExportMppFileValidator()
    {
        RuleFor(x => x.ProjectId)
            .IsPositive(GlobalCmts.ProjectId)
            .WithError(GlobalErrors.IdsLessThanOrEqualZero);
    }
}