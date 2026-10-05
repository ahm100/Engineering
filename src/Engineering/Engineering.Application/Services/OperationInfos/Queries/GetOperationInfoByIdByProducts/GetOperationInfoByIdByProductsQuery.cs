using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByProducts;

public record GetOperationInfoByIdByProductsQuery(
    long Id
    ) : IQuery<OperationInfo>;