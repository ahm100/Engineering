namespace Engineering.Application.Services.ProjectWbses.Contracts.ImportMppFile;

public class ImportMppFileValidator : AbstractValidator<ImportMppFileRequest>
{
    public ImportMppFileValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);

        RuleFor(v => v.DocumentFile)
            .NotEmpty().WithError(GlobalErrors.FileIsEmpty);

        RuleFor(v => v.DocumentFile.Length)
            .LessThanOrEqualTo(10000000).WithError(GlobalErrors.ImporteredCanNotBeMore10MG);

        RuleFor(v => v.DocumentFile.FileName)
            .Must(m => m.EndsWith(".mpp", StringComparison.OrdinalIgnoreCase)).WithError(GlobalErrors.ImporteredFileMustBeMpp);
    }
}
