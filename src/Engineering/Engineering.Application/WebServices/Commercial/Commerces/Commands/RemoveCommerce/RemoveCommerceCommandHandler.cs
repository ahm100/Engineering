using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.Commercial.Commerces.Models.RemoveCommerce;

namespace Engineering.Application.WebServices.Commercial.Commerces.Commands.RemoveCommerce;

public class RemoveCommerceCommandHandler : ICommandHandler<RemoveCommerceCommand, bool?>
{
    private readonly ILogger<RemoveCommerceCommandHandler> _logger;
    private readonly ICommercialService _commercialService;

    public RemoveCommerceCommandHandler(ILogger<RemoveCommerceCommandHandler> logger, ICommercialService commercialService)
    {
        _logger = logger;
        _commercialService = commercialService;
    }

    public async Task<Result<bool?>> Handle(RemoveCommerceCommand request, CT ct)
    {
        try
        {
            var result = await _commercialService.RemoveCommerce(new RemoveCommerceRequest(request.Id), ct);
            if (result is null || result.IsFailure)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}
