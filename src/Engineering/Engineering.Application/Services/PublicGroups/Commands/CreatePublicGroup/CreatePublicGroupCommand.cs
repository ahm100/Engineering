using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.PublicGroups.Commands.CreatePublicGroup;

public record CreatePublicGroupCommand(
    List<long> ProductGroupIds
    ) : ICommand<List<PublicGroup>?>;
