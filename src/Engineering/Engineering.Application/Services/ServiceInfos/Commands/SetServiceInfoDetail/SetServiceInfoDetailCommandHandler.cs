using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Application.Services.ServiceInfos.Models.SetServiceInfoDetail;

namespace Engineering.Application.Services.ServiceInfos.Commands.SetServiceInfoDetail;

public class SetServiceInfoDetailCommandHandler : ICommandHandler<SetServiceInfoDetailCommand, SetServiceInfoDetailResponse?>
{
    private readonly ILogger<SetServiceInfoDetailCommandHandler> _logger;
    private readonly IServiceInfoRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public SetServiceInfoDetailCommandHandler(ILogger<SetServiceInfoDetailCommandHandler> logger,
        IUnitOfWork unitOfWork,
        IServiceInfoRepository repository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result<SetServiceInfoDetailResponse?>> Handle(SetServiceInfoDetailCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<SetServiceInfoDetailResponse>(ServiceInfoErrors.ServiceInfoWithIdNotFound);

            if (request.DescriptionEn != null)
                entity.SetDescriptionEn(request.DescriptionEn);

            if (request.DescriptionFa != null)
                entity.SetDescriptionFa(request.DescriptionFa);

            if (request.ServiceInfoEnName != null)
                entity.SetServiceInfoNameEn(request.ServiceInfoEnName);

            await _repository.Update(entity);
            await _unitOfWork.CommitAsync(ct);
            return new SetServiceInfoDetailResponse(entity.Id, true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<SetServiceInfoDetailResponse>(SharedErrors.UnknownError);
        }
    }
}