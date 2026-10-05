using Engineering.Application.Abstractions.Data.ReviewReports;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceItems;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.ReviewReports;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Persistence.Repositories.ReviewReports;

public class ReviewReportRepository : EngineeringDBContext, IReviewReportRepository
{

    public ReviewReportRepository(
        DbContextOptions<EngineeringDBContext> options,
        IUserInfoProvider userInfoProvider) : base(options, userInfoProvider)
    {
    }

    public async Task<(List<GetReferenceItemsModel>? Data, int RowCount)> GetReferenceItemReport(
        string? faFilterData,
        string? enFilterData,
        List<SupplyType> supplyTypes,
        long companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = Set<ReferenceItemReport>()
            .FromSqlInterpolated($@"SELECT * FROM engineer.vwReferenceItemReport")
            .Where(x => supplyTypes.Contains(x.SupplyType) &&
                companyId == x.CompanyId &&
                (string.IsNullOrWhiteSpace(faFilterData) || EF.Functions.Like(x.Name, faFilterData.MakeLikePattern()) &&
                (string.IsNullOrWhiteSpace(enFilterData) || EF.Functions.Like(x.NameEn, enFilterData.MakeLikePattern()))))
            .Select(x => new GetReferenceItemsModel
            {
                Id = x.Id,
                SupplyType = x.SupplyType,
                Code = x.Code,
                Name = x.Name,
                NameEn = x.NameEn,
                TechnicalCode = x.TechnicalCode,
                Created = x.Created,
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);

        return (newQuery, count);
    }
}