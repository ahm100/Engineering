using Engineering.Application.Services.Projects.Models.GetProjectForPdf;

namespace Engineering.Application.Services.Projects.Queries.GetProjectForPdf;

public record GetProjectForPdfQuery(
    long Id) : IQuery<GetProjectForPdfResponse>;