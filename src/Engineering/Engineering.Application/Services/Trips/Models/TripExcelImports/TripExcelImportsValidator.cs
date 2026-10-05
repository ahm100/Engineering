namespace Engineering.Application.Services.Trips.Models.TripExcelImports;

public class TripExcelImportsValidator : AbstractValidator<TripExcelImportsRequest>
{
    public TripExcelImportsValidator()
    {
        RuleFor(oo => oo.DocumentFile).NotEmpty().WithError(GlobalErrors.FileIsEmpty);
        RuleFor(oo => oo.DocumentFile.Length).LessThanOrEqualTo(1000000).WithError(GlobalErrors.ExcelImporteredCanNotBeMore1MG);
        RuleFor(x => x.DocumentFile.FileName).Must(a => a.EndsWith(".xlsx")).WithError(GlobalErrors.ImporteredFileMustBeExcel);
    }
}
