using Engineering.Application.Services.ProjectTypes.Models.ActiveProjectType;
using Engineering.Application.Services.ProjectTypes.Models.CodeCreator;
using Engineering.Application.Services.ProjectTypes.Models.CreateProjectType;
using Engineering.Application.Services.ProjectTypes.Models.DisableProjectType;
using Engineering.Application.Services.ProjectTypes.Models.GetActiveProjectTypes;
using Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeByCode;
using Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeById;
using Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeByName;
using Engineering.Application.Services.ProjectTypes.Models.GetsProjectType;
using Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelEnum;
using Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelExporter;
using Engineering.Application.Services.ProjectTypes.Models.InactiveProjectType;
using Engineering.Application.Services.ProjectTypes.Models.ProjectTypeExcelImports;
using Engineering.Application.Services.ProjectTypes.Models.ProjectTypeGroupDelete;
using Engineering.Application.Services.ProjectTypes.Models.StateChangerProjectTypes;
using Engineering.Application.Services.ProjectTypes.Models.UpdateProjectType;

namespace Engineering.Application.Services.ProjectTypes;

public interface IProjectTypeLogic
{
    ///Commands
    Task<Result<CreateProjectTypeResponse?>> CreateProjectType(CreateProjectTypeRequest request, CT ct);
    Task<Result<ProjectTypeExcelImportsResponse?>> ProjectTypeExcelImports(ProjectTypeExcelImportsRequest request, CT ct);
    Task<Result<ProjectTypeCodeCreatorResponse?>> ProjectTypeCodeCreator(ProjectTypeCodeCreatorRequest request, CT ct);
    Task<Result<UpdateProjectTypeResponse?>> UpdateProjectType(UpdateProjectTypeRequest request, CT ct);
    Task<Result<DisableProjectTypeResponse?>> DisableProjectType(DisableProjectTypeRequest request, CT ct);
    Task<Result<InactiveProjectTypeResponse?>> InactiveProjectType(InactiveProjectTypeRequest request, CT ct);
    Task<Result<ActiveProjectTypeResponse?>> ActiveProjectType(ActiveProjectTypeRequest request, CT ct);
    Task<Result<StateChangerProjectTypesResponse?>> StateChangerProjectTypes(StateChangerProjectTypesRequest request, CT ct);
    Task<Result<ProjectTypeGroupDeleteResponse?>> ProjectTypeGroupDelete(ProjectTypeGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetProjectTypeByIdResponse?>> GetProjectTypeById(GetProjectTypeByIdRequest request, CT ct);
    Task<Result<GetsProjectTypeResponse?>> GetsProjectType(GetsProjectTypeRequest request, CT ct);
    Task<Result<GetProjectTypeByNameResponse?>> GetProjectTypeByName(GetProjectTypeByNameRequest request, CT ct);
    Task<Result<GetProjectTypeByCodeResponse?>> GetProjectTypeByCode(GetProjectTypeByCodeRequest request, CT ct);
    Task<Result<GetActiveProjectTypesResponse?>> GetActiveProjectTypes(GetActiveProjectTypesRequest request, CT ct);
    Task<Result<GetsProjectTypeExcelExporterResponse?>> GetsProjectTypeExcelExporter(GetsProjectTypeExcelExporterRequest request, CT ct);
    Task<Result<GetsProjectTypeExcelEnumResponse?>> GetsProjectTypeExcelEnum(GetsProjectTypeExcelEnumRequest request, CT ct);

}