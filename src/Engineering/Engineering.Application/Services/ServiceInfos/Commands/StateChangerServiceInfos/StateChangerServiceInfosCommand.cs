using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Commands.StateChangerServiceInfos;

public record StateChangerServiceInfosCommand(
    List<ServiceInfo> Items,
    bool State
    ) : ICommand<bool?>;
