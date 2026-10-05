using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Commands.InactiveService;

public record InactiveServiceInfoCommand(
    long Id
    ) : ICommand<ServiceInfo>;