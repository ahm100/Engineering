using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Abstractions.Data.EmployerContracts;

public interface IEmployerDocRepository : IBaseRepository<EmployerDoc>
{
    Task<EmployerDoc?> GetById(long id, CT ct);

    Task<(List<EmployerDoc> Data, int RowCount)> GetActiveEmployerDocsByEmployerContractId(
        long employerContractId,
        EDocumentType? documentTypes,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<EmployerDoc> Data, int RowCount)> GetEmployerDocsByEmployerContractId(
        long employerContractId,
        EDocumentType? documentTypes,
        int pageIndex,
        int pageSize,
        CT ct);
}