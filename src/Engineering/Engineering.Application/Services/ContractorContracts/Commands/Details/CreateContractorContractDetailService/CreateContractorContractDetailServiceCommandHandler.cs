using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailService;

public class CreateContractorContractDetailServiceCommandHandler : ICommandHandler<CreateContractorContractDetailServiceCommand, ContractorContractDetailService>
{
    private readonly ILogger<CreateContractorContractDetailServiceCommandHandler> _logger;
    private readonly IContractorContractDetailServiceRepository _repository;
    private readonly IProjectOperationDetailContractorExpertRepository _detailRepo;

    public CreateContractorContractDetailServiceCommandHandler(
        ILogger<CreateContractorContractDetailServiceCommandHandler> logger,
        IContractorContractDetailServiceRepository repository,
        IProjectOperationDetailContractorExpertRepository detailRepo)
    {
        _logger = logger;
        _repository = repository;
        _detailRepo = detailRepo;
    }

    public async Task<Result<ContractorContractDetailService?>> Handle(CreateContractorContractDetailServiceCommand request, CT ct)
    {
        try
        {
            var result = ContractorContractDetailService.Create(
                request.ContractorContractDetail,
                request.ContractorContractDetailService
                );

            var detailExperts = await _detailRepo.GetByProjectOperationDetailContractorServiceId(request.ContractorContractDetailService.Id, ct);
            if (detailExperts is not null && detailExperts.Count > 0)
                foreach (var item in detailExperts)
                {
                    item.SetHaveContract(true);
                    await _detailRepo.Update(item);
                }

            await _repository.Create(result, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractDetailService>(SharedErrors.UnknownError);
        }
    }
}
