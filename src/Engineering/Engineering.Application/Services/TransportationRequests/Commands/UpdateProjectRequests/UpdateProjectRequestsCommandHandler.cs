using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateProjectRequests;

public class UpdateProjectRequestsCommandHandler : ICommandHandler<UpdateProjectRequestsCommand, TransportationRequest>
{
    private readonly ILogger<UpdateProjectRequestsCommand> _logger;
    private readonly ITransportationRequestRepository _repository;
    private readonly ITransportationRequestProjectRepository _projectRepository;

    public UpdateProjectRequestsCommandHandler(
        ILogger<UpdateProjectRequestsCommand> logger,
        ITransportationRequestRepository repository,
        ITransportationRequestProjectRepository projectRepository)
    {
        _logger = logger;
        _repository = repository;
        _projectRepository = projectRepository;
    }

    public async Task<Result<TransportationRequest?>> Handle(UpdateProjectRequestsCommand request, CT ct)
    {
        try
        {
            var entities = request.Projects;
            if (entities is null)
                return Result.Failure<TransportationRequest>(TransportationRequestErrors.UnValidProjects);

            foreach (var item in entities)
            {
                item.SetTransportRequest(request.TransportationRequest);
                await _projectRepository.Update(item);
            }

            return request.TransportationRequest;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TransportationRequest>(SharedErrors.UnknownError);
        }
    }
}