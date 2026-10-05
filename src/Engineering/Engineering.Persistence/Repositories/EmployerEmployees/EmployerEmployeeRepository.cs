using Engineering.Application.Abstractions.Data.EmployerEmployees;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetEmployeesByEmployerId;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetFltrEmployees;
using Engineering.Domain.Entities.EmployerEmployees;

namespace Engineering.Persistence.Repositories.EmployerEmployees;

public class EmployerEmployeeRepository : BaseRepository<EngineeringDBContext, EmployerEmployee>, IEmployerEmployeeRepository
{
    public EmployerEmployeeRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<EmployerEmployee?> GetById(
        long id, CT ct)
    {
        var query = DbSet
            .Where(x => x.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<(List<GetEmployeesByEmployerIdModel>? Data, int RowCount)> GetEmployeesByEmployerId(
        long employerId,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => x.EmployerId == employerId)
            .Select(x => new GetEmployeesByEmployerIdModel
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                EmployerId = x.EmployerId,
            });

        var rowCount = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var result = await query.ToListAsync(ct);
        return (result, rowCount);
    }

    public async Task<(List<GetFltrEmployeesModel>? Data, int RowCount)> GetFltrEmployees(
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Select(x => new GetFltrEmployeesModel
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                EmployerId = x.EmployerId,
            });

        var rowCount = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var result = await query.ToListAsync(ct);
        return (result, rowCount);
    }

    public async Task<List<EmployerEmployee>> GetThirdPartiesByEmployerId(
        List<long> employerIds, CT ct)
    {
        return await DbSet
            .Where(c => employerIds.Contains(c.EmployerId) && !c.IsDeleted).ToListAsync(ct);
    }
}
