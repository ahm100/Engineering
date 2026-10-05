using Engineering.Application.Services.Branchs.Models.GetBranchById;

namespace Engineering.Application.Services.Branchs.Queries.GetBranchByIdForResponse;

public record GetBranchByIdForResponseQuery(
    long Id) : IQuery<GetBranchByIdResponse?>;