using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenters.Models.GetCompaniesWork;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Application.Services.CostCenters.Queries.GetCompaniesWork;

public class GetCompaniesWorkQueryHandler : IQueryHandler<GetCompaniesWorkQuery, GetCompaniesWorkResponse?>
{
    private readonly ICostCenterRepository _repository;
    private readonly IUserInfoProvider _userInfo;
    private readonly ILogger<GetCompaniesWorkQueryHandler> _logger;

    public GetCompaniesWorkQueryHandler(ILogger<GetCompaniesWorkQueryHandler> logger,
        ICostCenterRepository repository,
        IUserInfoProvider userInfo)
    {
        _logger = logger;
        _repository = repository;
        _userInfo = userInfo;
    }

    public async Task<Result<GetCompaniesWorkResponse?>> Handle(GetCompaniesWorkQuery request, CT ct)
    {
        try
        {
            var companyId = _userInfo.CompanyId;
            var result = await _repository.GetCompaniesWork(companyId, ct);

            return result.Data.Any() ?
                new GetCompaniesWorkResponse(result.Data, result.RowCount) :
                Result.Failure<GetCompaniesWorkResponse?>(CostCenterErrors.FilteredCostCenterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCompaniesWorkResponse?>(SharedErrors.UnknownError);
        }
    }
}