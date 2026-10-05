namespace Engineering.Application.Services.BillOfLadings.Contracts.BillOfLadingExcelImports;

public class BillOfLadingExcelImportsValidator : AbstractValidator<BillOfLadingExcelImportsRequest>
{
    public BillOfLadingExcelImportsValidator()
    {
        RuleFor(v => v.DocumentFile)
            .NotEmpty().WithError(GlobalErrors.FileIsEmpty);

        RuleFor(v => v.DocumentFile.Length)
            .LessThanOrEqualTo(1000000).WithError(GlobalErrors.ExcelImporteredCanNotBeMore1MG);

        RuleFor(v => v.DocumentFile.FileName)
            .Must(m => m.EndsWith(".xlsx")).WithError(GlobalErrors.ImporteredFileMustBeExcel);
    }
}