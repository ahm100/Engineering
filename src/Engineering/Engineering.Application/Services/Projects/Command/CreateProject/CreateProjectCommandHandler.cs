using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Organizations;
using Engineering.Application.WebServices.IdentityServices.Users.Queries.GetUsersByActionId;
using Engineering.Domain.Entities.Projects;
using IdentityServer.ClientSdk.Services;
using MessageSender.ClientSdk.Messaging;
using MessageSender.ClientSdk.Messaging.Targets;
using MessageSender.ClientSdk.Services;
using ProjectImplementationAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectImplementationAssistant;
using ProjectTechnicalAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectTechnicalAssistant;

namespace Engineering.Application.Services.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand, Project>
{
    private readonly ILogger<CreateProjectCommand> _logger;
    private readonly IProjectRepository _repository;
    private readonly IUserInfoProvider _userInfoService;
    private readonly IMessageRelay _relay;
    private readonly IMediator _mediator;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly IViewOrganizationRepository _orgRepo;

    public CreateProjectCommandHandler(
        ILogger<CreateProjectCommand> logger,
        IProjectRepository repository,
        IUserInfoProvider userInfoService,
        IMessageRelay relay,
        IMediator mediator,
        IViewThirdPartyRepository thirdPartyRepo,
        IViewOrganizationRepository orgRepo)
    {
        _logger = logger;
        _repository = repository;
        _userInfoService = userInfoService;
        _relay = relay;
        _mediator = mediator;
        _thirdPartyRepo = thirdPartyRepo;
        _orgRepo = orgRepo;
    }

    public async Task<Result<Project?>> Handle(CreateProjectCommand request, CT ct)
    {
        try
        {
            var userId = _userInfoService.UserId;

            if (request.IsOrganizationUnit == true &&
                (request.CostCenter is not null || request.CostCentersList?.Count > 0))
                return Result.Failure<Project?>(ProjectErrors.UnitOrgCantHaveCC);

            if (request.IsOrganizationUnit == true && request.ProjectType is not null)
                return Result.Failure<Project?>(ProjectErrors.UnitOrgCantHaveType);

            if (request.IsOrganizationUnit == true && request.OrganizationId is null)
                return Result.Failure<Project?>(ProjectErrors.OrganizationIdIsEmpty);

            if (request.IsOrganizationUnit == false && request.ProjectManager == null)
                return Result.Failure<Project?>(ProjectErrors.ProjectManagerIdIsEmpty);

            if (request.IsOrganizationUnit == true || request.OrganizationId != null)
            {
                var organization = await _orgRepo.GetById(request.OrganizationId!.Value, ct);
                if (organization is null)
                    return Result.Failure<Project>(ProjectErrors.OrganizationNotFound);
                if (organization!.ManagerId == null)
                    return Result.Failure<Project>(ProjectErrors.OrganizationManagerIsEmpty);
            }

            if (request.CostCenter is null &&
                (request.CostCentersList?.Count ?? 0) == 0 &&
                request.CityId is null)
                return Result.Failure<Project?>(ProjectErrors.CityIsNull);

            var newProject = new Project(
                request.Categories,
                request.CostCenter,
                request.ProjectType,
                request.ProjectName,
                request.ProjectEnName,
                request.ProjectCode,
                request.Prefix,
                request.EmployerId,
                request.SupervisorEngineer,
                request.Advisor,
                request.ProjectManager,
                request.PlanningAssistant,
                request.Status,
                request.Contractual,
                request.CollectiveService,
                request.IsActive,
                request.ApprovedBudget,
                request.CityId,
                request.Description,
                request.DescriptionEn,
                request.AddressDescription,
                request.CompanyId,
                request.HasProducts,
                request.OrganizationId,
                request.IsOrganizationUnit);

            if (request.CostCentersList?.Count > 0)
            {
                foreach (var costCenter in request.CostCentersList)
                {
                    newProject.SetCostCenter(costCenter);
                }
            }

            if (request.ImplementationAssistants?.Count > 0)
                foreach (var implementationAssistant in request.ImplementationAssistants)
                {
                    var newImplementationAssistant = new ProjectImplementationAssistant(newProject, implementationAssistant);
                    newProject.AddImplementationAssistant(newImplementationAssistant);
                }

            if (request.TechnicalAssistants?.Count > 0)
                foreach (var technicalAssistant in request.TechnicalAssistants)
                {
                    var newTechnicalAssistant = new ProjectTechnicalAssistant(newProject, technicalAssistant);
                    newProject.AddTechnicalAssistant(newTechnicalAssistant);

                }

            if (request.ProjectWarehouses.HasAny())
                foreach (var projectWarehouse in request.ProjectWarehouses!)
                    newProject.AddWarehouse(projectWarehouse.Id, projectWarehouse.IsDefault);

            var result = await _repository.Create(newProject, ct);


            try
            {
                if (request.CostCenter is null &&
                    (request.CostCentersList?.Count ?? 0) == 0)
                {
                    var ids = await _mediator.Send(new GetUsersByActionIdQuery([238]), ct);
                    if (!ids.IsBad() && ids.Value.Value is not null && ids.Value.Value.Data is not null)
                    {
                        var thirdParties = await _thirdPartyRepo.GetByUserIds(
                            ids.Value.Value.Data.Listed(x => x.UserId), ct);

                        if (thirdParties is not null && thirdParties.Count > 0)
                        {
                            foreach (var item in thirdParties)
                            {
                                await _relay.Send(new MessageEnvelope(
                                    MessageSender.ClientSdk.Enums.MessageChannels.Inbox,

                                    new TemplatedMessage(Guid.NewGuid().ToString(), "engineering-set-project", new Dictionary<string, object?>
                                    {
                                    { "FullName", item?.FirstName + " " + item?.LastName },
                                    { "ProjectName", request.ProjectName },
                                    })
                                    , new UserTarget(item.UserId.Value)), ct);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create project");
            return Result.Failure<Project>(SharedErrors.UnknownError);
        }
    }
}
