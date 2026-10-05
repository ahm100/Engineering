using Engineering.Application.Abstractions.Data.ReviewReports;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceItems;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetReferenceItems;

public class GetReferenceItemsQueryHandler : IQueryHandler<GetReferenceItemsQuery, GetReferenceItemsResponse?>
{
    private readonly ILogger<GetReferenceItemsQueryHandler> _logger;
    private readonly IReviewReportRepository _repository;
    private readonly IUserInfoProvider _userProvider;

    public GetReferenceItemsQueryHandler(ILogger<GetReferenceItemsQueryHandler> logger,
        IReviewReportRepository repository,
        IUserInfoProvider userProvider)
    {
        _logger = logger;
        _repository = repository;
        _userProvider = userProvider;
    }

    public async Task<Result<GetReferenceItemsResponse?>> Handle(GetReferenceItemsQuery request, CT ct)
    {
        try
        {
            var companyId = _userProvider.CompanyId;
            var result = await _repository.GetReferenceItemReport(request.FaFilter, request.EnFilter, request.SupplyTypes, companyId, request.PageIndex, request.PageSize, ct);

            return result.Data is not null && result.RowCount > 0 ?
                new GetReferenceItemsResponse(result.Data, result.RowCount) :
                Result.Failure<GetReferenceItemsResponse>(RequestGoodsSupplyErrors.NoItemFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetReferenceItemsResponse>(SharedErrors.UnknownError);
        }
    }
}
