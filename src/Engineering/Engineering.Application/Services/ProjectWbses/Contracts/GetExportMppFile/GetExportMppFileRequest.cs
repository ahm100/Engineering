namespace Engineering.Application.Services.ProjectWbses.Contracts.GetExportMppFile;

public record GetExportMppFileRequest(
    long ProjectId) : IHttpRequest;
