namespace Engineering.Application.Services.ProjectWbses.Contracts.EditProjectTasksPredecessors;

public record EditProjectTasksPredecessorsRequest(
    long Id,
    string Predecessors) : IHttpRequest;