using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.ComparisonrequestGoodSupplyProductScale;

public class ComparisonrequestGoodSupplyProductScaleCommandHandler : ICommandHandler<ComparisonrequestGoodSupplyProductScaleCommand, RequestGoodsSupplyDetail>
{
    private readonly ILogger<ComparisonrequestGoodSupplyProductScaleCommandHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public ComparisonrequestGoodSupplyProductScaleCommandHandler(ILogger<ComparisonrequestGoodSupplyProductScaleCommandHandler> logger,
                                                        IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<RequestGoodsSupplyDetail?>> Handle(ComparisonrequestGoodSupplyProductScaleCommand request, CT ct)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        try
        {
            RequestGoodsSupplyDetail? entity = null;
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyDetail>(SharedErrors.UnknownError);
        }
    }
}
