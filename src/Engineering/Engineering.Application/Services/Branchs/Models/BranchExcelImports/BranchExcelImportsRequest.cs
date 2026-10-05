namespace Engineering.Application.Services.Branchs.Models.BranchExcelImports;

public record BranchExcelImportsRequest(
    IFormFile DocumentFile)
    : IHttpRequest;