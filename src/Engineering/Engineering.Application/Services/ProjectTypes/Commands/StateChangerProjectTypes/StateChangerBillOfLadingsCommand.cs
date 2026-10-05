using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Commands.StateChangerProjectTypes;

public record StateChangerProjectTypesCommand(
    List<ProjectType> Items,
    bool State
    ) : ICommand<bool?>;
