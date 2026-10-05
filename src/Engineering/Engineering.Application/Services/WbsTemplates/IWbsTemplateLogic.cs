using Engineering.Application.Services.WbsTemplates.Contracts.CreateWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Contracts.DeleteWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Contracts.GetFltrWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateById;
using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateForProject;
using Engineering.Application.Services.WbsTemplates.Contracts.UpdateWbsTemplate;

namespace Engineering.Application.Services.WbsTemplates;

public interface IWbsTemplateLogic
{
    Task<Result<CreateWbsTemplateResponse?>> CreateWbsTemplate(
        CreateWbsTemplateRequest request, CT ct);

    Task<Result<UpdateWbsTemplateResponse?>> UpdateWbsTemplate(
        UpdateWbsTemplateRequest request, CT ct);

    Task<Result<DeleteWbsTemplateResponse?>> DeleteWbsTemplate(
        DeleteWbsTemplateRequest request, CT ct);

    Task<Result<GetFltrWbsTemplateResponse?>> GetFltrWbsTemplate(
        GetFltrWbsTemplateRequest request, CT ct);

    Task<Result<GetWbsTemplateByIdResponse?>> GetWbsTemplateById(
        GetWbsTemplateByIdRequest request, CT ct);

    Task<Result<GetWbsTemplateForProjectResponse?>> GetWbsTemplateForProject(
        GetWbsTemplateForProjectRequest request, CT ct);
}
