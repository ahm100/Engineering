namespace Engineering.Application.Services.CostCenterTypes.Models.CostCenterTypeExcelImports;

public class CostCenterTypeExcelImportsValidator : AbstractValidator<CostCenterTypeExcelImportsRequest>
{
    public CostCenterTypeExcelImportsValidator()
    {
        RuleFor(v => v.DocumentFile)
            .NotEmpty().WithError(GlobalErrors.FileIsEmpty);

        RuleFor(v => v.DocumentFile.Length)
            .LessThanOrEqualTo(1000000).WithError(GlobalErrors.ExcelImporteredCanNotBeMore1MG);

        RuleFor(v => v.DocumentFile.FileName)
            .Must(m => m.EndsWith(".xlsx")).WithError(GlobalErrors.ImporteredFileMustBeExcel);
    }
}