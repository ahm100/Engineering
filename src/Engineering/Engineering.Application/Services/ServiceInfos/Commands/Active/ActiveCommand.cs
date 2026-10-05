using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Commands.Active;

public record ActiveServiceInfoCommand(
    long Id
    ) : ICommand<ServiceInfo>;