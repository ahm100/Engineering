using Engineering.Application.Services.SubProjects.Contracts.ChangeSubProjectStatus;
using Engineering.Application.Services.SubProjects.Contracts.CreateSubProject;
using Engineering.Application.Services.SubProjects.Contracts.DeleteSubProject;
using Engineering.Application.Services.SubProjects.Contracts.GetAllowedSubProjectManagers;
using Engineering.Application.Services.SubProjects.Contracts.GetSubProjectByCode;
using Engineering.Application.Services.SubProjects.Contracts.GetSubProjectById;
using Engineering.Application.Services.SubProjects.Contracts.GetSubProjects;
using Engineering.Application.Services.SubProjects.Contracts.UpdateSubProject;

namespace Engineering.Application.Services.SubProjects;

public interface ISubProjectLogic
{
    Task<Result<CreateSubProjectResponse?>> CreateSubProject(
        CreateSubProjectRequest request, CT ct);

    Task<Result<UpdateSubProjectResponse?>> UpdateSubProject(
        UpdateSubProjectRequest request, CT ct);

    Task<Result<DeleteSubProjectResponse?>> DeleteSubProject(
        DeleteSubProjectRequest request, CT ct);

    Task<Result<ChangeSubProjectStatusResponse?>> ChangeSubProjectStatus(
        ChangeSubProjectStatusRequest request, CT ct);

    Task<Result<GetSubProjectByIdResponse?>> GetSubProjectById(
        GetSubProjectByIdRequest request, CT ct);

    Task<Result<GetSubProjectByCodeResponse?>> GetSubProjectByCode(
        GetSubProjectByCodeRequest request, CT ct);

    Task<Result<GetSubProjectsResponse?>> GetSubProjects(
        GetSubProjectsRequest request, CT ct);

    Task<Result<GetAllowedSubProjectManagersResponse?>> GetAllowedSubProjectManagers(
        GetAllowedSubProjectManagersRequest request, CT ct);
}
