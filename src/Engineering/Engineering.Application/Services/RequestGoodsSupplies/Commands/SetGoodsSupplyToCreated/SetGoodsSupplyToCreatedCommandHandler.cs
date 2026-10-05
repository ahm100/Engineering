using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.SetGoodsSupplyToCreated;

public class SetGoodsSupplyToCreatedCommandHandler : ICommandHandler<SetGoodsSupplyToCreatedCommand, RequestGoodsSupply>
{
    private readonly ILogger<SetGoodsSupplyToCreatedCommandHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public SetGoodsSupplyToCreatedCommandHandler(ILogger<SetGoodsSupplyToCreatedCommandHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupply?>> Handle(SetGoodsSupplyToCreatedCommand request, CT ct)
    {
        try
        {
            var entity = request.GoodsSupply;
            if (entity.RequestGoodsSupplyDetails.Count <= 0)
                return Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyErrors.NoHaveDetails);

            entity.SetStatusToCreated();
            entity.RequestGoodsSupplyProducts.ToList().ForEach(product =>
            {
                product.SetStatusToNew();
                product.RequestGoodsSupplyDetails.ToList().ForEach(detail =>
                {
                    detail.SetStatusToNew();
                });
            });

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
