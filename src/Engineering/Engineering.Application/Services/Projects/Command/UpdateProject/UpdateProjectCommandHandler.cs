using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Organizations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.ProjectUsers;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand, Project>
{
    private readonly ILogger<UpdateProjectCommand> _logger;
    private readonly IProjectRepository _repository;
    private readonly IViewOrganizationRepository _orgRepo;
    private readonly IProjectTechnicalAssistantRepository _technicalAssistantRepository;
    private readonly IProjectImplementationAssistantRepository _implementationAssistantRepository;

    public UpdateProjectCommandHandler(ILogger<UpdateProjectCommand> logger,
        IProjectRepository repository,
       IProjectTechnicalAssistantRepository technicalAssistantRepository,
       IProjectImplementationAssistantRepository implementationAssistantRepository,
       IViewOrganizationRepository orgRepo)
    {
        _logger = logger;
        _repository = repository;
        _technicalAssistantRepository = technicalAssistantRepository;
        _implementationAssistantRepository = implementationAssistantRepository;
        _orgRepo = orgRepo;

    }

    public async Task<Result<Project?>> Handle(UpdateProjectCommand request, CT ct)
    {
        try
        {
            var entity = request.Project;
            
            entity.SetEmployerId(request.EmployerId);
            entity.SetStatus(request.Status);

            if (!string.IsNullOrEmpty(request.ProjectName))
                entity.SetName(request.ProjectName);

            if (!string.IsNullOrEmpty(request.NewCode))
                entity.SetCode(request.NewCode);

            entity.SetPrefix(request.Prefix);

            if (!string.IsNullOrEmpty(request.ProjectEnName))
                entity.SetEnName(request.ProjectEnName);

            if (request.IsOrganizationUnit == false && request.ProjectManager == null)
                return Result.Failure<Project>(ProjectErrors.ProjectManagerIdIsEmpty);

            if(request.IsOrganizationUnit == true || request.OrganizationId != null)
            {
                var organization = await _orgRepo.GetById(request.OrganizationId!.Value, ct);
                if (organization is null)
                    return Result.Failure<Project>(ProjectErrors.OrganizationNotFound);
                if (organization!.ManagerId == null)
                    return Result.Failure<Project>(ProjectErrors.OrganizationManagerIsEmpty);
            }

            entity.SetDescriptionEn(request.DescriptionEn);

            entity.SetAdvisor(request.Advisor);
            entity.SetSupervisorEngineer(request.SupervisorEngineer);
            entity.SetProjectManager(request.ProjectManager);
            entity.SetPlanningAssistant(request.PlanningAssistant);
            entity.SetCompanyId(request.CompanyId);
            entity.SetCollectiveService(request.CollectiveService);
            entity.SetApprovedBudget(request.ApprovedBudget);
            entity.SetCityId(request.CityId);
            entity.SetDescription(request.Description);
            entity.SetAddressDescription(request.AddressDescription);
            entity.SetIsOrganizationUnit(request.IsOrganizationUnit);
            entity.SetOrganizationId(request.OrganizationId);

            if (request.IsOrganizationUnit == true && request.EmployerId != null)
                return Result.Failure<Project?>(ProjectErrors.UnitOrgCantHaveEmployer);

            if (entity.IsOrganizationUnit == true && entity.ProjectCostCenters.Any())
                return Result.Failure<Project?>(ProjectErrors.UnitOrgCantHaveCC);

            if (!entity.EmployerContracts.Any())
                entity.SetContractual(request.Contractual);
            else
            {
                if (request.Contractual)
                {
                    if (entity.EmployerContracts.Any())
                        return Result.Failure<Project>(ProjectErrors.HaveContract);
                    else
                        entity.SetContractual(request.Contractual);
                }
                if (!entity.Contractual && request.Contractual)
                    entity.SetContractual(request.Contractual);
            }

            if (request.IsActive != entity.IsActive)
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetInActive();

            if (entity.ProjectTechnicalAssistants.Count > 0)
                foreach (var assistant in entity.ProjectTechnicalAssistants)
                    await _technicalAssistantRepository.Remove(assistant);
            if (request.TechnicalAssistants?.Count > 0)
                foreach (var assistant in request.TechnicalAssistants)
                    entity.AddTechnicalAssistant(new ProjectTechnicalAssistant(entity, assistant));

            if (entity.ProjectImplementationAssistants.Count > 0)
                foreach (var assistant in entity.ProjectImplementationAssistants)
                    await _implementationAssistantRepository.Remove(assistant);
            if (request.ImplementationAssistants?.Count > 0)
                foreach (var assistant in request.ImplementationAssistants)
                    entity.AddImplementationAssistant(new ProjectImplementationAssistant(entity, assistant));

            if (request.HasProduct.HasValue)
            {
                if (request.HasProduct.Value == false && entity.HasProduct && entity.ProjectProducts.Any(x => x.CompletedQuantity != 0 || x.CompletedQuantity != 0))
                    return Result.Failure<Project>(ProjectErrors.HasProduct);
                entity.SetHasProduct(request.HasProduct.Value);
            }

            entity.AddHistory();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<Project>(SharedErrors.UnknownError);
        }
    }
}
