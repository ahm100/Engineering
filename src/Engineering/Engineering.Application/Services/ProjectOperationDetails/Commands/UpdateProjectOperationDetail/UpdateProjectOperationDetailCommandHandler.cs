using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Users;
using ProjectOperationDetailStatus = Engineering.Domain.Entities.ProjectOperationDetails.Enums.ProjectOperationDetailStatus;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.UpdateProjectOperationDetail;

public class UpdateProjectOperationDetailCommandHandler : ICommandHandler<UpdateProjectOperationDetailCommand, ProjectOperationDetail>
{
    private readonly ILogger<UpdateProjectOperationDetailCommand> _logger;
    private readonly IProjectOperationDetailRepository _repository;
    private readonly IUserImplementationsRepository _implementationRepository;
    private readonly IUserTechnicalsRepository _technicalRepository;
    private readonly IUserPlanersRepository _planerRepository;

    public UpdateProjectOperationDetailCommandHandler(ILogger<UpdateProjectOperationDetailCommand> logger, IProjectOperationDetailRepository repository,
        IUserImplementationsRepository implementationRepository, IUserTechnicalsRepository technicalRepository,
        IUserPlanersRepository planerRepository)
    {
        _logger = logger;
        _repository = repository;
        _implementationRepository = implementationRepository;
        _technicalRepository = technicalRepository;
        _planerRepository = planerRepository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(UpdateProjectOperationDetailCommand request, CT ct)
    {
        try
        {
            var entity = request.ProjectOperationDetail;
            entity.SetCode(request.Code);
            entity.SetStartDate(request.StartDate);
            entity.SetEndDate(request.EndDate);
            entity.SetLength(request.Length);
            entity.SetLengthChangeable(request.LengthChangeable);
            entity.SetWidth(request.Width);
            entity.SetWidthChangeable(request.WidthChangeable);
            entity.SetHeight(request.Height);
            entity.SetHeightChangeable(request.HeightChangeable);
            entity.SetWeight(request.Weight);
            entity.SetWeightChangeable(request.WeightChangeable);
            entity.SetNumber(request.Number);
            entity.SetNumberChangeable(request.NumberChangeable);
            entity.SetPriority(request.Priority);
            entity.SetDay(request.Day ?? 0);
            entity.SetHour(request.Hour ?? 0);
            entity.SetStatus((ProjectOperationDetailStatus)request.Status!);
            entity.SetDescription(request.Description);
            entity.SetOperationLocation(request.OperationLocation);
            entity.SetCompanyId(request.CompanyId);
            entity.AddDocuments(request.Urls);
            entity.SetCreatedProductId(request.CreatedProductId);

            if (entity.UserPlaners is not null)
                if (entity.UserPlaners.Count > 0)
                    foreach (var planer in entity.UserPlaners)
                        await _planerRepository.Remove(planer);

            if (entity.UserTechnicals is not null)
                if (entity.UserTechnicals.Count > 0)
                    foreach (var technical in entity.UserTechnicals)
                        await _technicalRepository.Remove(technical);

            if (entity.UserImplementations.Count > 0)
                if (entity.UserImplementations is not null)
                    foreach (var implementation in entity.UserImplementations)
                        await _implementationRepository.Remove(implementation);

            if (request.PlannerRequests is not null)
                if (request.PlannerRequests?.Count > 0)
                    foreach (var item in request.PlannerRequests)
                        entity.AddPlaner(new UserPlaner(entity, item!));

            if (request.ImplementationAssistantRequests is not null)
                if (request.ImplementationAssistantRequests?.Count > 0)
                    foreach (var item in request.ImplementationAssistantRequests)
                        entity.AddImplementationAssistant(new UserImplementation(entity, item!));

            if (request.TechnicalAssistantRequests is not null)
                if (request.TechnicalAssistantRequests?.Count > 0)
                    foreach (var item in request.TechnicalAssistantRequests)
                        entity.AddTechnicalAssistant(new UserTechnical(entity, item!));

            entity.SetFinalAmount();
            entity.AddHistory(request.StatusDescription);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}