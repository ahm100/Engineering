using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.SetRequestGoodsSupplyProductStatusClose;

public class SetRequestGoodsSupplyProductStatusCloseCommandHandler : ICommandHandler<SetRequestGoodsSupplyProductStatusCloseCommand, RequestGoodsSupplyProduct>
{
    private readonly ILogger<SetRequestGoodsSupplyProductStatusCloseCommandHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public SetRequestGoodsSupplyProductStatusCloseCommandHandler(ILogger<SetRequestGoodsSupplyProductStatusCloseCommandHandler> logger,
                                                              IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyProduct?>> Handle(SetRequestGoodsSupplyProductStatusCloseCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;


            entity.SetStatus(GoodsSupplyDetailStatus.Closed, "این درخواست به دلیل تغییر کالا ها بسته شده است.");

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyProduct>(SharedErrors.UnknownError);
        }
    }
}
