namespace Engineering.Application.Services.Projects.Models.GetProjectForPdf;

public record GetProjectForPdfRequest(
    long Id) : IHttpRequest;