using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSPayments;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetMonthlyPayment;
using Engineering.Domain.Entities.ContractorStatusStatements;
using System.Globalization;

namespace Engineering.Persistence.Repositories.ContractorStatusStatements;

public class ContractorStatusStatementPaymentRepository :
    BaseRepository<EngineeringDBContext, ContractorStatusStatementPayment>,
    IContractorStatusStatementPaymentRepository
{
    public ContractorStatusStatementPaymentRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetCSSPaymentsModel> Data, int RowCount)> GetCSSPayments(
        long id,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => x.ContractorStatusStatement.Id == id)
            .Select(item => new GetCSSPaymentsModel()
            {
                Id = item.Id,
                CalculatedAmount = item.CalculatedAmount,
                PayableAmount = item.PayableAmount,
                UserAmount = item.UserAmount,
                UserDescription = item.UserDescription,
                Status = item.Status,
                ProjectManagerAmount = item.ProjectManagerAmount,
                ProjectManagerDescription = item.ProjectManagerDescription,
                ManagementAmount = item.ManagementAmount,
                ManagementDescription = item.ManagementDescription,
                PrimaryManagerAmount = item.PrimaryManagerAmount,
                PrimaryManagerDescription = item.PrimaryManagerDescription,
                FinalManagerAmount = item.FinalManagerAmount,
                FinalManagerDescription = item.FinalManagerDescription,
                PaymentAmount = item.PaymentAmount,
                PaymentDescription = item.PaymentDescription,
                PaymentOrderId = item.PaymentOrderId,
                PaymentDate = item.PaymentDate,
                TreasuryPaid = item.TreasuryPaid,
                Created = item.Created,
                Urls = item.ContractorStatusStatementDocuments.Select(x => x.Url).ToList(),
            });

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<decimal> GetTodaysPayment(
        CT ct)
    {
        var today = DateTime.Today;

        return await DbSet
            .Where(x =>
                !x.IsDeleted &&
                x.PaymentDate.HasValue &&
                x.PaymentDate.Value.Date == today &&
                x.TreasuryPaid != null &&
                x.TreasuryPaid.Value != 0)
            .SumAsync(x => x.TreasuryPaid!.Value, ct);
    }

    public async Task<List<GetMonthlyPaymentModel>> GetMonthlyPayment(
        int monthCount,
        CT ct)
    {
        var fromDate = DateTime.Today.AddMonths(-monthCount);

        var data = await DbSet
            .Where(x =>
                !x.IsDeleted &&
                x.PaymentDate.HasValue &&
                x.PaymentDate.Value >= fromDate &&
                x.TreasuryPaid.HasValue &&
                x.TreasuryPaid.Value != 0)
            .Select(x => new
            {
                PaymentDate = x.PaymentDate!.Value,
                Paid = x.TreasuryPaid!.Value
            })
            .ToListAsync(ct);

        var pc = new PersianCalendar();

        var fa = new CultureInfo("fa-IR");
        fa.DateTimeFormat.Calendar = pc;

        var grouped = data
            .GroupBy(x => new
            {
                Year = pc.GetYear(x.PaymentDate),
                Month = pc.GetMonth(x.PaymentDate)
            })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                Total = g.Sum(x => x.Paid)
            })
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToList();

        return grouped
            .Select(x => new GetMonthlyPaymentModel
            {
                MonthLabel = $"{fa.DateTimeFormat.GetMonthName(x.Month)} {x.Year}",
                TotalPaid = x.Total
            })
            .ToList();
    }
}