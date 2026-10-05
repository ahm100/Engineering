using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectWbses.Queries.GetProjectByImportId;

public record GetProjectByImportIdQuery(
    long ImportId) : IQuery<Project?>;