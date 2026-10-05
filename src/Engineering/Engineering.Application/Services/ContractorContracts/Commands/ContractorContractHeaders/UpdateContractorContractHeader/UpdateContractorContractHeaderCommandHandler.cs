using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.UpdateContractorContractHeader;

public class UpdateContractorContractHeaderCommandHandler : ICommandHandler<UpdateContractorContractHeaderCommand, ContractorContractHeader>
{
    private readonly ILogger<UpdateContractorContractHeaderCommandHandler> _logger;
    private readonly IContractorContractHeaderRepository _repository;
    private readonly IUserProfileService _userProfileService;

    public UpdateContractorContractHeaderCommandHandler(
        ILogger<UpdateContractorContractHeaderCommandHandler> logger,
        IContractorContractHeaderRepository repository,
        IUserProfileService userProfileService)
    {
        _logger = logger;
        _repository = repository;
        _userProfileService = userProfileService;
    }

    public async Task<Result<ContractorContractHeader?>> Handle(UpdateContractorContractHeaderCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity is null)
                return Result.Failure<ContractorContractHeader>(ContractorContractErrors.InValidContractorContractId);

            var user = _userProfileService.GetProfileInfo();
            if (user.UserId != entity.CreatorId)
                return Result.Failure<ContractorContractHeader>(ContractorContractErrors.InValidUser);

            if (!ContractorContractStatusRules.AllowForUpdate.Any(x => x.Equals(entity.Status)))
                return Result.Failure<ContractorContractHeader>(ContractorContractErrors.InValidCCForUpdate);

            entity.SetCurrencyId(request.CurrencyId);
            entity.SetDescription(request.Description);

            if (request.Urls.HasAny() && ContractorContractStatusRules.JustAddUrl.Any(x => x.Equals(entity.Status)))
                entity.AddDocuments(request.Urls, true);
            else
                entity.AddDocuments(request.Urls, false);

            if (ContractorContractStatusRules.AllowForUpdate.Any(x => x.Equals(entity.Status)))
                if (entity.Status != ContractorContractStatus.New)
                    entity.SetStatus(ContractorContractStatus.ProjectManagerResend);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractHeader>(SharedErrors.UnknownError);
        }
    }
}
