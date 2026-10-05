using Engineering.Application.Abstractions.Data.Advertisements;
using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Application.Services.Advertisements.Contracts.SetAdsDetails;
using Engineering.Application.Services.ServiceInfos.Commands.SetServiceInfoDetail;
using Engineering.Application.Services.ServiceInfos.Models.SetServiceInfoDetail;

namespace Engineering.Application.Services.Advertisements.Commands.SetAdsDetails;

public class SetAdsDetailsCommandHandler : ICommandHandler<SetAdsDetailsCommand, SetAdsDetailsResponse?>
{
    private readonly ILogger<SetAdsDetailsCommandHandler> _logger;
    private readonly IAdvertisementRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public SetAdsDetailsCommandHandler(ILogger<SetAdsDetailsCommandHandler> logger,
        IUnitOfWork unitOfWork,
        IAdvertisementRepository repository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<Result<SetAdsDetailsResponse?>> Handle(SetAdsDetailsCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<SetAdsDetailsResponse>(ServiceInfoErrors.ServiceInfoWithIdNotFound)!;

            if (request.DescriptionEn != null)
                entity.SetDescriptionEn(request.DescriptionEn);

            if (request.DescriptionFa != null)
                entity.SetDescriptionFa(request.DescriptionFa);

            if (request.AdEnName != null)
                entity.SetTitleEn(request.AdEnName);

            await _repository.Update(entity);
            await _unitOfWork.CommitAsync(ct);
            return new SetAdsDetailsResponse(entity.Id, true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<SetAdsDetailsResponse>(SharedErrors.UnknownError)!;
        }
    }
}