using Engineering.Application.Services.ProjectRisks.Contracts.CreateProjectRisk;
using Engineering.Application.Services.ProjectRisks.Contracts.DeleteProjectRisk;
using Engineering.Application.Services.ProjectRisks.Contracts.GetFltrProjectRisk;
using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskById;
using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskByProjectId;
using Engineering.Application.Services.ProjectRisks.Contracts.UpdateProjectRisk;

namespace Engineering.Application.Services.ProjectRisks;

public interface IProjectRiskLogic
{
    Task<Result<CreateProjectRiskResponse?>> CreateProjectRisk(
        CreateProjectRiskRequest request, CT ct);

    Task<Result<UpdateProjectRiskResponse?>> UpdateProjectRisk(
        UpdateProjectRiskRequest request, CT ct);

    Task<Result<DeleteProjectRiskResponse?>> DeleteProjectRisk(
        DeleteProjectRiskRequest request, CT ct);

    Task<Result<GetFltrProjectRiskResponse?>> GetFltrProjectRisk(
        GetFltrProjectRiskRequest request, CT ct);

    Task<Result<GetProjectRiskByIdResponse?>> GetProjectRiskById(
        GetProjectRiskByIdRequest request, CT ct);

    Task<Result<GetProjectRiskByProjectIdResponse?>> GetProjectRiskByProjectId(
        GetProjectRiskByProjectIdRequest request, CT ct);
}
