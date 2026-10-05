using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Queries.GetOperationInfoServiceForValidation;

public record GetOperationInfoServiceForValidationQuery(
    long OperationInfoId,
    long ServiceInfoId
    ) : IQuery<OperationInfoService>;