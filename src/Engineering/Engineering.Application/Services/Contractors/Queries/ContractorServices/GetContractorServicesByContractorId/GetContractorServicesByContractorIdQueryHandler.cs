using Engineering.Application.Abstractions.Data;
using Engineering.Domain.Entities.ContractorServices;

namespace Engineering.Application.Services.Contractors.Queries.ContractorServices.GetContractorServicesByContractorId;

public class GetContractorServicesByContractorIdQueryHandler : IQueryHandler<GetContractorServicesByContractorIdQuery, DataResult<List<ContractorService>>>
{
    private readonly IContractorServicesRepository _repository;
    private readonly ILogger<GetContractorServicesByContractorIdQueryHandler> _logger;

    public GetContractorServicesByContractorIdQueryHandler(IContractorServicesRepository ContractorServiceRepository, ILogger<GetContractorServicesByContractorIdQueryHandler> logger)
    {
        _repository = ContractorServiceRepository;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<ContractorService>>?>> Handle(GetContractorServicesByContractorIdQuery request, CT ct)
    {
        try
        {
            var ContractorServices = await _repository.GetContractorServicesByContractorId(request.ContractorId, request.FilterData, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

            var response = new DataResult<List<ContractorService>>
            {
                Data = ContractorServices.Data,
                RowCount = ContractorServices.RowCount
            };

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<ContractorService>>>(SharedErrors.UnknownError);
        }
    }
}
