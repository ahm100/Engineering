using Engineering.Application.Abstractions.Data.Advertisements;
using Engineering.Application.Services.Advertisements.Contracts.DeleteAdvertisement;
using Engineering.Domain.Errors.Advertisements;

namespace Engineering.Application.Services.Advertisements.Commands.DeleteAdvertisement;


public class DeleteAdvertisementCommandHandler : ICommandHandler<DeleteAdvertisementCommand, DeleteAdvertisementResponse?>
{
    private readonly ILogger<DeleteAdvertisementCommandHandler> _logger;
    private readonly IAdvertisementRepository _repository;

    public DeleteAdvertisementCommandHandler(ILogger<DeleteAdvertisementCommandHandler> logger,
        IAdvertisementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DeleteAdvertisementResponse?>> Handle(DeleteAdvertisementCommand request, CT ct)
    {
        try
        {
            var ad = await _repository.GetById(request.Id, ct);
            if (ad is null)
                return Result.Failure<DeleteAdvertisementResponse?>(AdvertisementErrors.AdWithIdNotFound);

            ad.SoftDelete();
            return new DeleteAdvertisementResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DeleteAdvertisementResponse?>(SharedErrors.UnknownError);
        }
    }
}