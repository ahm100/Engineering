using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.CreateCCHVersion;

public class CreateCCHVersionCommandHandler : ICommandHandler<CreateCCHVersionCommand, List<ContractorContractHeaderVersion>>
{
    private readonly ILogger<CreateCCHVersionCommandHandler> _logger;
    private readonly IContractorContractHeaderRepository _cCHRepository;
    private readonly IContractorStatusStatementRepository _cSSRepository;
    private readonly IContractorContractHeaderVersionRepository _cCHVersionRepository;

    public CreateCCHVersionCommandHandler(
        ILogger<CreateCCHVersionCommandHandler> logger,
        IContractorStatusStatementRepository cSSRepository,
        IContractorContractHeaderRepository cHHRepository,
        IContractorContractHeaderVersionRepository cCHVersionRepository)
    {
        _logger = logger;
        _cSSRepository = cSSRepository;
        _cCHRepository = cHHRepository;
        _cCHVersionRepository = cCHVersionRepository;
    }

    public async Task<Result<List<ContractorContractHeaderVersion>?>> Handle(CreateCCHVersionCommand request, CT ct)
    {
        try
        {
            List<ContractorContractHeaderVersion> entities = [];
            foreach (var item in request.CCHs)
            {
                if (!item.CompanyId.HasValue || item.CompanyId.Value <= 0)
                    return Result.Failure<List<ContractorContractHeaderVersion>>(GlobalErrors.InvalidCompany);

                var model = await _cCHRepository.GetContractorContractHeaderByIdNew(
                    item.Id,
                    item.CompanyId.Value,
                    ct);
                string serializedModel = JsonConvert.SerializeObject(model);
                var entity = new ContractorContractHeaderVersion(
                    item,
                    request.CSS,
                    serializedModel,
                    model!.Version,
                    DateTime.UtcNow);

                await _cCHVersionRepository.Create(entity, ct);
                entities.Add(entity);
            }
            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ContractorContractHeaderVersion>>(SharedErrors.UnknownError);
        }
    }
}
