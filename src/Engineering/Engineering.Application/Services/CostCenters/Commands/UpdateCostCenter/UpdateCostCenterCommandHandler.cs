using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.Projects.Queries.GetProjectByIds;
using Engineering.Domain.Entities.CostCenters;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;
using CostCenterInformedUser = Engineering.Domain.Entities.CostCenters.CostCenterInformedUser;

namespace Engineering.Application.Services.CostCenters.Commands.UpdateCostCenter;

public class UpdateCostCenterCommandHandler : ICommandHandler<UpdateCostCenterCommand, CostCenter>
{
    private readonly ILogger<UpdateCostCenterCommand> _logger;
    private readonly ICostCenterRepository _costCenterRepository;
    private readonly ICostCenterInformedUserRepository _informedUserRepository;
    private readonly ICostCenterAuthorizedUserRepository _authorizedUserRepository;
    private readonly ICostCenterAuthorizedRoleRepository _authorizedRoleRepository;
    private readonly IMediator _mediator;

    public UpdateCostCenterCommandHandler(ILogger<UpdateCostCenterCommand> logger, ICostCenterRepository costCenterRepository,
        ICostCenterInformedUserRepository informedUserRepository, ICostCenterAuthorizedUserRepository authorizedUserRepository,
        ICostCenterAuthorizedRoleRepository authorizedRoleRepository,
        IMediator mediator)
    {
        _logger = logger;
        _costCenterRepository = costCenterRepository;
        _informedUserRepository = informedUserRepository;
        _authorizedRoleRepository = authorizedRoleRepository;
        _authorizedUserRepository = authorizedUserRepository;
        _mediator = mediator;
    }

    public async Task<Result<CostCenter?>> Handle(UpdateCostCenterCommand request, CT ct)
    {
        try
        {
            var costCenterEntity = await _costCenterRepository.FindByIdAndChild(request.Id, ct);
            if (costCenterEntity is null)
                return Result.Failure<CostCenter>(CostCenterErrors.CostCenterWithIdNotFound);
            if (costCenterEntity!.IsDeleted == true)
                return Result.Failure<CostCenter>(CostCenterErrors.IsDeleted);

            if (costCenterEntity!.InformedUsers.Count > 0)
                foreach (var informedUser in costCenterEntity.InformedUsers)
                    await _informedUserRepository.Remove(informedUser);

            if (request.InformedUsers is not null)
                if (request.InformedUsers.Count > 0)
                    foreach (var informedUser in request.InformedUsers)
                        await _informedUserRepository.Create(new CostCenterInformedUser(costCenterEntity, (long)informedUser!), ct);

            if (costCenterEntity!.CostCenterAuthorizedRoles.Count > 0)
                foreach (var authorizedRole in costCenterEntity.CostCenterAuthorizedRoles)
                    await _authorizedRoleRepository.Remove(authorizedRole);

            if (request.AuthorizedRoles is not null)
                if (request.AuthorizedRoles.Count > 0)
                    foreach (var authorizedRole in request.AuthorizedRoles)
                        await _authorizedRoleRepository.Create(new CostCenterAuthorizedRole(costCenterEntity, (long)authorizedRole!), ct);

            if (costCenterEntity!.CostCenterAuthorizedUsers.Count > 0)
                foreach (var authorizedUser in costCenterEntity.CostCenterAuthorizedUsers)
                    await _authorizedUserRepository.Remove(authorizedUser);

            if (request.AuthorizedUsers is not null)
                if (request.AuthorizedUsers.Count > 0)
                    foreach (var authorizedUser in request.AuthorizedUsers)
                        await _authorizedUserRepository.Create(new CostCenterAuthorizedUser(costCenterEntity, (long)authorizedUser!), ct);

            if (request.IsDefault is not null && request.IsDefault == true)
            {
                var defaultCC = await _costCenterRepository.GetIsDefaultCostCenterByCompanyId(request.CompanyId.Value, ct);
                defaultCC.SetIsDefault(false);
            }

            costCenterEntity.SetCostCenterType(request.CostCenterType);
            costCenterEntity.SetName(request.CostCenterName);
            costCenterEntity.SetEnName(request.CostCenterEnName);
            costCenterEntity.SetCode(request.CostCenterCode);
            costCenterEntity.SetNoOperationDays(request.NoOperationDays);
            costCenterEntity.SetCityId(request.CityId);
            costCenterEntity.SetAddress(request.Address);
            costCenterEntity.SetPostalCode(request.PostalCode);
            costCenterEntity.SetLatitude(request.Latitude);
            costCenterEntity.SetLongitude(request.Longitude);
            costCenterEntity.SetDescription(request.Description);
            costCenterEntity.SetWeatherState(request.WeatherState);
            costCenterEntity.SetDescriptionEn(request.DescriptionEn);
            costCenterEntity.SetCompanyId(request.CompanyId);
            costCenterEntity.SetIsDefault(request.IsDefault ?? costCenterEntity.IsDefault);
            if (request.IsActive != costCenterEntity.IsActive)
            {
                if (request.IsActive == true)
                    costCenterEntity.SetActive();
                else
                    costCenterEntity.SetDeactivate();
            }


            if (request.ProjectIds is not null && request.ProjectIds.Count > 0)
            {
                var projects = await _mediator.Send(new GetProjectByIdsQuery(request.ProjectIds, false, false), ct);
                if (projects.IsBad())
                    return projects.Failure<CostCenter?>();

                foreach (var item in projects.Value!)
                    item.SetCostCenter(costCenterEntity);
            }

            costCenterEntity.AddHistory();
            await _costCenterRepository.Update(costCenterEntity);
            return costCenterEntity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostCenter>(SharedErrors.UnknownError);
        }
    }
}