
using Engineering.Api.Controllers.ImportTemplateController;

public class TemplateService
{
    private readonly IWebHostEnvironment _environment;

    public TemplateService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<Result<GlobalTemplateResponse>> GetTemplateFileBase64(string fileName)
    {
        var filePath = Path.Combine(
            _environment.ContentRootPath,
            "Reports",
            "ExcelTemplateFiles",
            fileName + ".xlsx");

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File {fileName} not found");

        var fileBytes = await File.ReadAllBytesAsync(filePath);

        return new GlobalTemplateResponse(new FileContentResult(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"Template-{fileName}.xlsx",
            LastModified = DateTime.UtcNow
        });
    }
}