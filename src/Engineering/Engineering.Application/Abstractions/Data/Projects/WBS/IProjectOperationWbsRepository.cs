using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetDetailPOWbsByProjectWbsId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetFltrPOWbs;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByPOId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByProjectWbsId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetProjectOperationWbsById;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Abstractions.Data.Projects.WBS;

public interface IProjectOperationWbsRepository : IBaseRepository<ProjectOperationWbs>
{
    Task<ProjectOperationWbs?> GetById(
        long id, CT ct);

    Task<GetProjectOperationWbsByIdResponse?> GetProjectOperationWbsById(
        long id, CT ct);

    Task<(List<GetPOWbsByPOIdModel>? Data, int RowCount)> GetPOWbsByPOId(
        long projectOperationid,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetDetailPOWbsByProjectWbsIdModel>? Data, int RowCount)> GetDetailPOWbsByProjectWbsId(
        long projectWbsId,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetFltrPOWbsModel>? Data, int RowCount)> GetFltrPOWbs(
        long? projectId,
        List<long>? projectOperationIds,
        List<long>? projectWbsIds,
        List<long>? projectOperationWbsIds,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct);

    Task<(List<GetPOWbsByProjectWbsIdModel>? Data, int RowCount)> GetPOWbsByProjectWbsId(
        long projectWbsId,
        string? filterData,
        int pageIndex,
        int pageSize, CT ct);
}