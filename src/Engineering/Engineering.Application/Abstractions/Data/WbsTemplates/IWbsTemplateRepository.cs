using Engineering.Application.Services.WbsTemplates.Contracts.GetFltrWbsTemplate;
using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateById;
using Engineering.Application.Services.WbsTemplates.Contracts.GetWbsTemplateForProject;
using Engineering.Domain.Entities.WbsTemplates;

namespace Engineering.Application.Abstractions.Data.WbsTemplates;

public interface IWbsTemplateRepository : IBaseRepository<WbsTemplate>
{
    Task<WbsTemplate?> GetById(
        long id, CT ct);

    Task<GetWbsTemplateByIdResponse?> GetWbsTemplateById(
        long id, CT ct);

    Task<(List<GetFltrWbsTemplateModel>? Data, int RowCount)> GetFltrWbsTemplate(
        string? filterData,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetWbsTemplateForProjectModel>? Data, int RowCount)> GetWbsTemplateForProject(
        string? filterData,
        List<long>? notShow,
        int pageIndex,
        int pageSize, CT ct);

    Task<WbsTemplate?> IsDuplicateTitle(
        string title,
        CT ct);

    Task<WbsTemplate?> IsDuplicateCode(
        string code,
        CT ct);
}
