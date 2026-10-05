namespace Engineering.Application.Services.Transportations.Models.TransportationExcelImports;

public class TransportationExcelImportsValidator : AbstractValidator<TransportationExcelImportsRequest>
{
    public TransportationExcelImportsValidator()
    {
        RuleFor(oo => oo.DocumentFile).NotEmpty().WithError(GlobalErrors.FileIsEmpty);
        RuleFor(oo => oo.DocumentFile.Length).LessThanOrEqualTo(1000000).WithError(GlobalErrors.ExcelImporteredCanNotBeMore1MG);
        RuleFor(x => x.DocumentFile.FileName).Must(a => a.EndsWith(".xlsx")).WithError(GlobalErrors.ImporteredFileMustBeExcel);
    }
}
