using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Commands.DeleteServiceInfo;

public record DeleteServiceInfoCommand(
    long Id
    ) : ICommand<ServiceInfo>;