using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;

public interface IRequestMachineryStatusStatementDetailRepository : IBaseRepository<RequestMachineryStatusStatementDetail>
{
    Task<RequestMachineryStatusStatementDetail?> GetRequestMachineryStatusStatementDetailById(
        long id,
        CT ct);
}
