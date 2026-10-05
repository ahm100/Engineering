using Engineering.Application.Services.Branchs.Models.GetsByCategoryId;

namespace Engineering.Application.Services.Branchs.Queries.GetsBranchByCategoryId;

public record GetsBranchByCategoryIdQuery(
    long CategoryId,
    int PageIndex,
    int PageSize)
    : IQuery<DataResult<List<GetsBranchByCategoryIdModel>>>;