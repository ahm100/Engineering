using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Users;
using ProjectOperationDetailStatus = Engineering.Domain.Entities.ProjectOperationDetails.Enums.ProjectOperationDetailStatus;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.UpdateProjectOperationDetails;

public class UpdateProjectOperationDetailsCommandHandler : ICommandHandler<UpdateProjectOperationDetailsCommand, List<ProjectOperationDetail>>
{
    private readonly ILogger<UpdateProjectOperationDetailsCommand> _logger;
    private readonly IProjectOperationDetailRepository _repository;
    private readonly IUserImplementationsRepository _implementationRepository;
    private readonly IUserTechnicalsRepository _technicalRepository;
    private readonly IUserPlanersRepository _planerRepository;

    public UpdateProjectOperationDetailsCommandHandler(ILogger<UpdateProjectOperationDetailsCommand> logger, IProjectOperationDetailRepository repository,
        IUserImplementationsRepository implementationRepository, IUserTechnicalsRepository technicalRepository,
        IUserPlanersRepository planerRepository)
    {
        _logger = logger;
        _repository = repository;
        _implementationRepository = implementationRepository;
        _technicalRepository = technicalRepository;
        _planerRepository = planerRepository;
    }

    public async Task<Result<List<ProjectOperationDetail>?>> Handle(UpdateProjectOperationDetailsCommand request, CT ct)
    {
        try
        {
            var entities = request.ProjectOperationDetails;
            foreach (var entity in entities)
            {
                if (request.StartDate != null)
                    entity.SetStartDate(request.StartDate);

                if (request.EndDate != null)
                    entity.SetEndDate(request.EndDate);

                if (request.Length != null)
                    entity.SetLength(request.Length.Value);

                if (request.LengthChangeable != null)
                    entity.SetLengthChangeable(request.LengthChangeable.Value);

                if (request.Width != null)
                    entity.SetWidth(request.Width.Value);

                if (request.WidthChangeable != null)
                    entity.SetWidthChangeable(request.WidthChangeable.Value);

                if (request.Height != null)
                    entity.SetHeight(request.Height.Value);

                if (request.HeightChangeable != null)
                    entity.SetHeightChangeable(request.HeightChangeable.Value);

                if (request.Weight != null)
                    entity.SetWeight(request.Weight.Value);

                if (request.WeightChangeable != null)
                    entity.SetWeightChangeable(request.WeightChangeable.Value);

                if (request.Number != null)
                    entity.SetNumber(request.Number.Value);

                if (request.NumberChangeable != null)
                    entity.SetNumberChangeable(request.NumberChangeable.Value);

                if (request.Day != null)
                    entity.SetDay(request.Day ?? 0);

                if (request.Hour != null)
                    entity.SetHour(request.Hour ?? 0);

                if (request.Status != null)
                    entity.SetStatus((ProjectOperationDetailStatus)request.Status!);

                entity.SetFinalAmount();

                if (entity.UserPlaners is not null)
                    if (entity.UserPlaners.Count > 0)
                        foreach (var planer in entity.UserPlaners)
                            await _planerRepository.Remove(planer);

                if (entity.UserTechnicals is not null)
                    if (entity.UserTechnicals.Count > 0)
                        foreach (var technical in entity.UserTechnicals)
                            await _technicalRepository.Remove(technical);

                if (entity.UserImplementations?.Count > 0)
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

                entity.AddHistory(request.StatusDescription);
                entity.SetFinalAmount();
                await _repository.Update(entity);
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperationDetail>>(SharedErrors.UnknownError);
        }
    }
}