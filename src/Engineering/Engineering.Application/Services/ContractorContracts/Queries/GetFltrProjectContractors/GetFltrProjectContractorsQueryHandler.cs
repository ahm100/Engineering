using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFltrProjectContractors;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFltrProjectContractors;

public class GetFltrProjectContractorsQueryHandler : IQueryHandler<GetFltrProjectContractorsQuery, GetFltrProjectContractorsResponse?>
{
    private readonly ILogger<GetFltrProjectContractorsQueryHandler> _logger;
    private readonly IContractorContractRepository _repo;
    private readonly IViewThirdPartyRepository _thirdPartyrepo;

    public GetFltrProjectContractorsQueryHandler(
        ILogger<GetFltrProjectContractorsQueryHandler> logger,
        IContractorContractRepository repo,
        IViewThirdPartyRepository thirdPartyrepo)
    {
        _logger = logger;
        _repo = repo;
        _thirdPartyrepo = thirdPartyrepo;
    }

    public async Task<Result<GetFltrProjectContractorsResponse?>> Handle(GetFltrProjectContractorsQuery request, CT ct)
    {
        try
        {
            var result = await _repo.GetFltrProjectContractors(request.CostCenterIds, request.ProjectIds, request.CompanyId, ct);
            if (result is null)
                return Result.Failure<GetFltrProjectContractorsResponse>(ContractorContractErrors.ContractorContractContractorsWithFilterNotFound);

            var contractorIds = result.Listed(x => x.ContractorId);
            var thirdParties = await _thirdPartyrepo.GetFltrThirdParties(contractorIds, request.FilterData, request.PageIndex, request.PageSize, ct);

            if (thirdParties.Data is null || thirdParties.RowCount < 1)
                return Result.Failure<GetFltrProjectContractorsResponse>(ContractorContractErrors.NoThirdPartyFound);

            var value = thirdParties.Data;
            List<GetFltrProjectContractorsModel> models = [];
            foreach (var item in value)
            {
                var contract = result.FirstOrDefault(x => x.ContractorId == item.Id);
                var model = new GetFltrProjectContractorsModel
                {
                    CostCenterId = contract?.CostCenterId,
                    CostCenterName = contract?.CostCenterName,
                    ProjectId = contract?.ProjectId,
                    ProjectName = contract?.ProjectName,
                    ContractorId = item.Id,
                    ContractorName = item.FirstName + " " + item.LastName,
                };
                models.Add(model);
            }

            return models.Any() ?
                new GetFltrProjectContractorsResponse(
                    models,
                    thirdParties.RowCount) :
                    Result.Failure<GetFltrProjectContractorsResponse>(ContractorContractErrors.NoThirdPartyFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFltrProjectContractorsResponse?>(SharedErrors.UnknownError);
        }
    }
}
