using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Abstractions.Data.RequestRewards;

public interface IRequestRewardDocumentRepository : IBaseRepository<RequestRewardDocument>
{
    Task<(List<RequestRewardDocument> Data, int RowCount)> GetsRequestRewardDocumentsByIds(List<long> documentIds, CT ct);
}
