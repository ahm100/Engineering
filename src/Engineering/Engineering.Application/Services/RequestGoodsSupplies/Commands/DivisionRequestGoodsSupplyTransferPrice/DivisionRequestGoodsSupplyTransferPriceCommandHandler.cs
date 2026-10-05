using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.DivisionRequestGoodsSupplyTransferPrice;

public class DivisionRequestGoodsSupplyTransferPriceCommandHandler : ICommandHandler<DivisionRequestGoodsSupplyTransferPriceCommand, RequestGoodsSupply>
{
    private readonly ILogger<DivisionRequestGoodsSupplyTransferPriceCommandHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public DivisionRequestGoodsSupplyTransferPriceCommandHandler(
        ILogger<DivisionRequestGoodsSupplyTransferPriceCommandHandler> logger,
        IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupply?>> Handle(DivisionRequestGoodsSupplyTransferPriceCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;

            var transferByCount = entity.TransferPrice / entity.RequestGoodsSupplyProducts.Count();
            foreach (var product in entity.RequestGoodsSupplyProducts)
                if (entity.TransferPrice is not null && entity.TransferPrice > 0)
                {
                    product.SetTransferPrice(transferByCount);
                    product.SetFinalPrice(product.FinalPrice + transferByCount);

                }
                else
                    product.SetTransferPrice(0);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupply>(SharedErrors.UnknownError);
        }
    }
}
