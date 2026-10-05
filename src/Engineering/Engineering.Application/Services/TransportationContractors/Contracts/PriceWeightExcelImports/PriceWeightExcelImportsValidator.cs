namespace Engineering.Application.Services.TransportationContractors.Models.PriceWeightExcelImports;

public class PriceWeightExcelImportsValidator : AbstractValidator<PriceWeightExcelImportsRequest>
{
    public PriceWeightExcelImportsValidator()
    {
        RuleFor(v => v.TransportationContractorId)
            .IsPositive(TransportationContractorCmts.TransportationContractorId);

        RuleFor(v => v.DocumentFile)
            .NotEmpty().WithError(GlobalErrors.FileIsEmpty);

        RuleFor(v => v.DocumentFile.Length)
            .LessThanOrEqualTo(1000000).WithError(GlobalErrors.ExcelImporteredCanNotBeMore1MG);

        RuleFor(v => v.DocumentFile.FileName)
            .Must(m => m.EndsWith(".xlsx")).WithError(GlobalErrors.ImporteredFileMustBeExcel);
    }
}