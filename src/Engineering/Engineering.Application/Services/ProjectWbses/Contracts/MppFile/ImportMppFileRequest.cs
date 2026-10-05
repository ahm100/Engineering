namespace Engineering.Application.Services.ProjectWbses.Contracts.ImportMppFile;

public record ImportMppFileRequest(
    long ProjectId,
    Guid FileId,
    string FileName,
    IFormFile DocumentFile
    ) : IHttpRequest;