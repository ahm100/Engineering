namespace Engineering.Application.Services.Projects.Queries.GetLastProjectCode;

public record GetLastProjectCodeQuery(
    string EmployerCode,
    string? CostCenterCode) : IQuery<long>;
