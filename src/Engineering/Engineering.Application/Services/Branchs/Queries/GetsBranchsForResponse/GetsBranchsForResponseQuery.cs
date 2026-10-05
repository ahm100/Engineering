using Engineering.Application.Services.Branchs.Models.GetsBranchExcelEnum;
using Engineering.Application.Services.Branchs.Models.GetsBranchExcelExporter;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchsForResponse;

public record GetsBranchsForResponseQuery(
    List<long>? Ids,
    string? FilterData,
    long? CategoryId,
    string? BranchName,
    string? BranchCode,
    bool? IsActive,
    string[]? OrderBy,
    long? CompanyId,
    int PageIndex,
    int PageSize) : IQuery<List<GetsBranchExcelExporterModel>>; 