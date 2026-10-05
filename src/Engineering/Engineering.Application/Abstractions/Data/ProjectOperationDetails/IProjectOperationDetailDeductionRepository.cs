using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Abstractions.Data.ProjectOperationDetails;

public interface IProjectOperationDetailDeductionRepository : IBaseRepository<ProjectOperationDetailDeduction>
{
    Task<ProjectOperationDetailDeduction?> GetById(long id, CT ct);
    Task<ProjectOperationDetailDeduction?> GetForDelete(long id, CT ct);
    Task<(List<ProjectOperationDetailDeduction> Data, int RowCount)> GetsByProjectOperationDetailId(long projectOperationDetailId, int pageIndex, int pageSize, CT ct);
    Task<List<ProjectOperationDetailDeduction>> GetsProjectOperationDetailDeductionByIds(List<long> ids, CT ct);
    Task<List<decimal>?> GetDeductionAmountsByDetailId(long projectOperationDetailId, CT ct);

}
