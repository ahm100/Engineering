using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Commands.DeleteOperationInfoService;

public record DeleteOperationInfoServiceCommand(
    long OperationInfoId,
    long ServiceInfoId
    ) : ICommand<OperationInfoService>;
