using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Abstractions.Data.RequestMachineries;

public interface IRequestMachineryProjectOperationDetailRepository : IBaseRepository<RequestMachineryProjectOperationDetail>
{
    Task<(List<RequestMachineryProjectOperationDetail> Data, int RowCount)> GetFilteredAsync(long projectOperationDetailId,
                                                                                        string? FilterData,
                                                                                         string[]? orderBy,
                                                                                        int pageIndex,
                                                                                        int pageSize,
                                                                                        CT ct);
    Task<(List<RequestMachineryProjectOperationDetail> Data, int RowCount)> GetFilteredProjectOperationDetail(long RequestMachineryId, string? filterData, string[]? orderBy, int pageIndex, int pageSize, CT ct);
    Task<(List<RequestMachineryProjectOperationDetail> Data, int RowCount)> GetFilteredProjectOperationDetailByProjectOperation(long RequestMachineryId, long ProjectOperationId, string? filterData, string[]? orderBy, int pageIndex, int pageSize, CT ct);

}
