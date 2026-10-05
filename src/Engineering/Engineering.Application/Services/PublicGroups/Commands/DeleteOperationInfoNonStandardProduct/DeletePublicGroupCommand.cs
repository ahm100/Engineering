using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.PublicGroups.Commands.DeletePublicGroup;

public record DeletePublicGroupCommand(
    long Id
    ) : ICommand<PublicGroup>;
