
namespace Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupExcelImports;

public class MachineriesGroupExcelImportsValidator : AbstractValidator<MachineriesGroupExcelImportsRequest>
{
    public MachineriesGroupExcelImportsValidator()
    {
        RuleFor(oo => oo.DocumentFile).NotEmpty().WithError(GlobalErrors.FileIsEmpty);
        RuleFor(oo => oo.DocumentFile.Length).LessThanOrEqualTo(1000000).WithError(GlobalErrors.ExcelImporteredCanNotBeMore1MG);
        RuleFor(x => x.DocumentFile.FileName).Must(a => a.EndsWith(".xlsx")).WithError(GlobalErrors.ImporteredFileMustBeExcel);
    }
}
