using Engineering.Application.Services.Branchs.Models.GetsBranchs;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchsResponseModel;

public record GetsBranchsResponseModelQuery(
    string? FilterData,
    long? CategoryId,
    string? BranchName,
    string? BranchCode,
    bool? IsActive,
    int PageIndex,
    int PageSize) : IQuery<GetsBranchsResponse?>;