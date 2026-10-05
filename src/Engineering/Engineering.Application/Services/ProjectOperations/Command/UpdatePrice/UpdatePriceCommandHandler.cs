using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Command.UpdatePrice;
using Engineering.Domain.Entities.ProjectOperations;

public class UpdatePriceCommandHandler : ICommandHandler<UpdatePriceCommand, List<ProjectOperation?>?>
{
    private readonly ILogger<UpdatePriceCommand> _logger;
    private readonly IProjectOperationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePriceCommandHandler(
        ILogger<UpdatePriceCommand> logger,
        IProjectOperationRepository repository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<ProjectOperation?>?>> Handle(UpdatePriceCommand request, CT ct)
    {
        try
        {
            var result = new List<ProjectOperation>();

            var pO = request.ProjectOperation;
            pO.UpdatePrice(request.ChangedPrice, request.IncreaseRate);
            result.Add(pO);

            await _unitOfWork.CommitAsync(ct);
            return result!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperation?>>(SharedErrors.UnknownError)!;
        }
    }
}
