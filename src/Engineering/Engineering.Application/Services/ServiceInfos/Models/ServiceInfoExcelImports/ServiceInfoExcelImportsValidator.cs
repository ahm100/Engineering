namespace Engineering.Application.Services.ServiceInfos.Models.ServiceInfoExcelImports;

public class ServiceInfoExcelImportsValidator : AbstractValidator<ServiceInfoExcelImportsRequest>
{
    public ServiceInfoExcelImportsValidator()
    {
        RuleFor(oo => oo.DocumentFile).NotEmpty().WithError(GlobalErrors.FileIsEmpty);
        RuleFor(oo => oo.DocumentFile.Length).LessThanOrEqualTo(1000000).WithError(GlobalErrors.ExcelImporteredCanNotBeMore1MG);
        RuleFor(x => x.DocumentFile.FileName).Must(a => a.EndsWith(".xlsx")).WithError(GlobalErrors.ImporteredFileMustBeExcel);
    }
}
