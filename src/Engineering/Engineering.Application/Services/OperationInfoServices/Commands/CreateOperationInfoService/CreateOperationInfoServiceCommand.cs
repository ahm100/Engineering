using Engineering.Application.Services.OperationInfos.Commands.CreateOperationInfo;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Commands.CreateOperationInfoService;

public record CreateOperationInfoServiceCommand(
    OperationInfo OperationInfo,
    List<OperationInfoServiceModel> ServiceInfos
    ) : ICommand<List<OperationInfoService>?>;