using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.RequestRewards.Commands.CreateRequestReward;

public record CreateRequestRewardCommand(
    decimal? OfferedPrice,
    string Description,
    DateTime RegistrationDate,
    RequestRewardType Type,
    long? CurrencyId,
    CostCenter? CostCenter,
    Project? Project,
    ProjectOperation? ProjectOperation,
    ProjectOperationDetail? ProjectOperationDetail,
    FiduciaryProductDetailReturn? FiduciaryProductDetailReturn,
    long? CompanyId
    ) : ICommand<RequestReward>;

