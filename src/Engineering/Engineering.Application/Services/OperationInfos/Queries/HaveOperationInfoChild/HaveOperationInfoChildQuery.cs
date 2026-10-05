using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.HaveOperationInfoChild;

public record HaveOperationInfoChildQuery(
    long Id
    ) : IQuery<OperationInfo>;