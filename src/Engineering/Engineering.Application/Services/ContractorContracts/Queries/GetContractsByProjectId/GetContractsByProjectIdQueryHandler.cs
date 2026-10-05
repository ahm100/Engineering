using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractsByProjectId;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetContractsByProjectId;

public class GetContractsByProjectIdQueryHandler : IQueryHandler<GetContractsByProjectIdQuery, GetContractsByProjectIdResponse>
{
    private readonly ILogger<GetContractsByProjectIdQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetContractsByProjectIdQueryHandler(
        ILogger<GetContractsByProjectIdQueryHandler> logger,
        IContractorContractRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetContractsByProjectIdResponse?>> Handle(GetContractsByProjectIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetContractsByProjectId(request.ProjectId, request.PageIndex, request.PageSize, request.CompanyId, ct);

            var userIds = result.Data.Any() ? result.Data?.NullListed(x => x.ContractorId) : null;
            if (userIds is not null && userIds.Count > 0)
            {
                var users = await _thirdPartyRepo.GetByIds(userIds, ct);
                foreach (var item in result.Data!)
                {
                    var contractor = users.FirstOrDefault(x => x.Id == item.ContractorId);
                    item.Contractor = contractor?.FirstName + " " + contractor?.LastName;
                }
            }

            return result.Data.Any() ?
                new GetContractsByProjectIdResponse
                (
                    result.Data,
                    result.RowCount
                ) : Result.Failure<GetContractsByProjectIdResponse>(ContractorContractErrors.NoContractFoundForPdf);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetContractsByProjectIdResponse?>(SharedErrors.UnknownError);
        }
    }
}
