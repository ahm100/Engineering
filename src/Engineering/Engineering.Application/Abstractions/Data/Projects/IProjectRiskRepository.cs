using Engineering.Application.Services.ProjectRisks.Contracts.GetFltrProjectRisk;
using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskById;
using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskByProjectId;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Abstractions.Data.Projects;

public interface IProjectRiskRepository : IBaseRepository<ProjectRisk>
{
    Task<ProjectRisk?> GetById(
        long id, CT ct);

    Task<(List<GetProjectRiskByProjectIdModel>? Data, int RowCount)> GetProjectRiskByProjectId(
        long id,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetFltrProjectRiskModel>? Data, int RowCount)> GetFltrProjectRisk(
        List<long>? projectIds,
        string? filterData,
        RiskProbability? riskProbability,
        RiskImpact? riskImpact,
        RiskStatus? riskStatus,
        int pageIndex,
        int pageSize, CT ct);

    Task<GetProjectRiskByIdResponse?> GetProjectRiskById(
        long id, CT ct);
}