using Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDoc;
using Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDocHistory;
using Engineering.Application.Services.EngineeringDocs.Contracts.DeleteProjectDoc;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDiscipline;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDoc;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDocType;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocById;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocs;
using Engineering.Application.Services.EngineeringDocs.Contracts.ProjectDocCodeGenerator;
using Engineering.Application.Services.EngineeringDocs.Contracts.SeedeDocTypes;
using Engineering.Application.Services.EngineeringDocs.Contracts.UpdateProjectDoc;

namespace Engineering.Application.Services.EngineeringDocs;

public interface IEngineeringDocLogic
{
    Task<Result<SeedDocTypeResponse?>> DocTypeSeeder(
        CT ct);

    Task<Result<GetDisciplineResponse?>> GetDiscipline(
        GetDisciplineRequest request, CT ct);

    Task<Result<GetDisciplineDocResponse?>> GetDisciplineDoc(
        GetDisciplineDocRequest request, CT ct);

    Task<Result<GetDisciplineDocTypeResponse?>> GetDisciplineDocType(
        GetDisciplineDocTypeRequest request, CT ct);

    Task<Result<UpdateProjectDocResponse?>> UpdateProjectDoc(
        UpdateProjectDocRequest request, CT ct);

    Task<Result<ProjectDocCodeGeneratorResponse?>> ProjectDocCodeGenerator(
        ProjectDocCodeGeneratorRequest request, CT ct);

    Task<Result<CreateProjectDocResponse?>> CreateProjectDoc(
        CreateProjectDocRequest request, CT ct);

    Task<Result<DeleteProjectDocResponse?>> DeleteProjectDoc(
        DeleteProjectDocRequest request, CT ct);

    Task<Result<GetProjectDocByIdResponse?>> GetProjectDocById(
        GetProjectDocByIdRequest request, CT ct);

    Task<Result<GetProjectDocsResponse?>> GetProjectDocs(
        GetProjectDocsRequest request, CT ct);

    Task<Result<GetProjectDocHistoriesResponse>> GetProjectDocHistories(
    GetProjectDocHistoriesRequest request, CT ct);

}
