using Engineering.Application.Services.Projects.Models.GetProjectPOTimelines;

namespace Engineering.Application.Services.Projects.Queries.GetProjectPOTimelines;

public record GetProjectPOTimelinesQuery(
    long Id
     ) : IQuery<GetProjectPOTimelinesResponse?>;