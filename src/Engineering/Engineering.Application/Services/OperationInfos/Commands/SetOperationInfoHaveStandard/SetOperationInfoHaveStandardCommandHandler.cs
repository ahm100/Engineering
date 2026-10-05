using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Commands.SetOperationInfoHaveStandard;

public class SetOperationInfoHaveStandardCommandHandler : ICommandHandler<SetOperationInfoHaveStandardCommand, OperationInfo>
{
    private readonly ILogger<SetOperationInfoHaveStandardCommand> _logger;
    private readonly IOperationInfoRepository _repository;

    public SetOperationInfoHaveStandardCommandHandler(ILogger<SetOperationInfoHaveStandardCommand> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationInfo?>> Handle(SetOperationInfoHaveStandardCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.HaveOperationInfoChild(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationInfo>(OperationInfoErrors.OperationInfoWithIdNotFound);

            entity.SetStandard();
            entity.AddHistory();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<OperationInfo>(SharedErrors.UnknownError);
        }
    }
}