using Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.CreateImplementationAssistants;
using Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.DeletemplementationAssistants;
using Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.ImplementationAssistantsGetsByProjectId;
using Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.CreateTechnicalAssistans;
using Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.DeleteTechnicalAssistans;
using Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.TechnicalAssistansGetsByProjectId;

namespace Engineering.Application.Services.ProjectAssistants;

public interface IProjectAssistantsLogic
{
    Task<Result<CreateImplementationAssistansResponse?>> CreateImplementationAssistant(
        CreateImplementationAssistansRequest request, CT ct);

    Task<Result<ImplementationAssistansGetsByProjectIdResponse?>> ImplementationAssistantGetsByProjectId(
        ImplementationAssistansGetsByProjectIdRequest request, CT ct);

    Task<Result<DeleteImplementationAssistansResponse?>> DeleteImplementationAssistant(
        DeleteImplementationAssistansRequest request, CT ct);

    Task<Result<CreateTechnicalAssistansResponse?>> CreateTechnicalAssistant(
        CreateTechnicalAssistansRequest request, CT ct);

    Task<Result<TechnicalAssistansGetsByProjectIdResponse?>> TechnicalAssistantGetsByProjectId(
        TechnicalAssistansGetsByProjectIdRequest request, CT ct);

    Task<Result<DeleteTechnicalAssistansResponse?>> DeleteTechnicalAssistant(
        DeleteTechnicalAssistansRequest request, CT ct);

}