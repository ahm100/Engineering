using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.Projects.Queries.GetProjectByIds;
using Engineering.Domain.Entities.CostCenters;
using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Application.Services.CostCenters.Commands.CreateCostCenter;

public class CreateCostCenterCommandHandler : ICommandHandler<CreateCostCenterCommand, CostCenter>
{
    private readonly ILogger<CreateCostCenterCommand> _logger;
    private readonly ICostCenterRepository _costCenterRepository;
    private readonly IMediator _mediator;
    private readonly ICostCenterInformedUserRepository _costCenterInformedUserRepository;
    private readonly ICostCenterAuthorizedRoleRepository _costCenterAuthorizedRoleRepository;
    private readonly ICostCenterAuthorizedUserRepository _costCenterAuthorizedUserRepository;

    public CreateCostCenterCommandHandler(
        ILogger<CreateCostCenterCommand> logger,
        ICostCenterRepository costCenterRepository,
        ICostCenterInformedUserRepository costCenterInformedUserRepository,
        ICostCenterAuthorizedRoleRepository costCenterAuthorizedRoleRepository,
        ICostCenterAuthorizedUserRepository costCenterAuthorizedUserRepository,
        IMediator mediator)
    {
        _logger = logger;
        _costCenterRepository = costCenterRepository;
        _costCenterInformedUserRepository = costCenterInformedUserRepository;
        _costCenterAuthorizedRoleRepository = costCenterAuthorizedRoleRepository;
        _costCenterAuthorizedUserRepository = costCenterAuthorizedUserRepository;
        _mediator = mediator;
    }

    public async Task<Result<CostCenter?>> Handle(CreateCostCenterCommand request, CT ct)
    {
        try
        {
            var newCostCenter = new CostCenter(
                request.CostCenterType,
                request.CostCenterName,
                request.CostCenterEnName,
                request.CostCenterCode,
                request.NoOperationDays,
                request.CityId,
                request.Address,
                request.PostalCode,
                request.Latitude,
                request.Longitude,
                request.Description,
                request.DescriptionEn,
                request.WeatherState,
                request.IsActive,
                request.IsDefault ?? false,
                request.CompanyId,
                request.PCR);

            var result = await _costCenterRepository.Create(newCostCenter, ct);

            var defaultCC = await _costCenterRepository.GetIsDefaultCostCenterByCompanyId(request.CompanyId.Value, ct);
            if (defaultCC is not null)
                defaultCC.SetIsDefault(false);

            if (request.InformedUsers is not null)
                if (request.InformedUsers.Count > 0)
                    foreach (var informedUser in request.InformedUsers)
                        await _costCenterInformedUserRepository.Create(new CostCenterInformedUser(newCostCenter, (long)informedUser!), ct);

            if (request.AuthorizedRoles is not null)
                if (request.AuthorizedRoles.Count > 0)
                    foreach (var authorizedRole in request.AuthorizedRoles)
                        await _costCenterAuthorizedRoleRepository.Create(new CostCenterAuthorizedRole(newCostCenter, (long)authorizedRole!), ct);

            if (request.AuthorizedUsers is not null)
                if (request.AuthorizedUsers.Count > 0)
                    foreach (var authorizedUser in request.AuthorizedUsers)
                        await _costCenterAuthorizedUserRepository.Create(new CostCenterAuthorizedUser(newCostCenter, (long)authorizedUser!), ct);

            if (request.CostCenterWarehouses is not null)
                if (request.CostCenterWarehouses.Count == 1)
                    foreach (var warehouse in request.CostCenterWarehouses)
                        newCostCenter.AddCostCenterWarehouse(new CostCenterWarehouse(newCostCenter, warehouse!.Id, true));
                else
                    foreach (var warehouse in request.CostCenterWarehouses)
                        newCostCenter.AddCostCenterWarehouse(new CostCenterWarehouse(newCostCenter, warehouse!.Id, warehouse.IsDefault));

            if (request.ProjectIds is not null && request.ProjectIds.Count > 0)
            {
                var projects = await _mediator.Send(new GetProjectByIdsQuery(request.ProjectIds, false, false), ct);
                if (projects.IsBad())
                    return projects.Failure<CostCenter?>();

                foreach (var item in projects.Value!)
                    item.SetCostCenter(newCostCenter);
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}