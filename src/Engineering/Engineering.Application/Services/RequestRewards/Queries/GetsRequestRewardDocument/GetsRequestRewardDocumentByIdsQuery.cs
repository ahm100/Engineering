using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.RequestRewards.Queries.GetsRequestRewardDocument;

public record GetsRequestRewardDocumentByIdsQuery(List<long> Ids) : IQuery<DataResult<List<RequestRewardDocument>>>;
