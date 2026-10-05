using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationService;

public record CreateDailyProjectOperationServiceCommand(
    DailyProjectOperation DailyProjectOperation,
    ProjectOperationDetailContractorService ContractorService,
    decimal Volume,
    decimal? ProjectServiceVolume,
    long? ThirdPartyId,
    long? TimeSpant,
    bool IsActive
    ) : ICommand<DailyProjectOperationService>;