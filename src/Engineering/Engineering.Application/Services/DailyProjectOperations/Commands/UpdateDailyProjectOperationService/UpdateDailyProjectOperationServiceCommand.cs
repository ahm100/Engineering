using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.UpdateDailyProjectOperationService;

public record UpdateDailyProjectOperationServiceCommand(
    DailyProjectOperationService Entity,
    ProjectOperationDetailContractorService? ContractorService,
    decimal Volume,
    decimal? ProjectServiceVolume,
    long? TimeSpant,
    bool IsActive
    ) : ICommand<DailyProjectOperationService>;
