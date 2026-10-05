using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.DeleteRequestGoodsSupplyProduct;

public class DeleteRequestGoodsSupplyProductCommandHandler : ICommandHandler<DeleteRequestGoodsSupplyProductCommand, RequestGoodsSupplyProduct>
{
    private readonly ILogger<DeleteRequestGoodsSupplyProductCommandHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public DeleteRequestGoodsSupplyProductCommandHandler(
        ILogger<DeleteRequestGoodsSupplyProductCommandHandler> logger,
        IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyProduct?>> Handle(DeleteRequestGoodsSupplyProductCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity.IsDeleted)
                return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.IsDeleted);
            if (!GSDSRules.AllowStatusForDelete.Any(x => x.Equals(entity.Status)))
                return Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyStatusForDelete);

            entity.SetIsDeleted();

            var details = entity.RequestGoodsSupplyDetails.ToList();
            details.ForEach(oo =>
            {
                oo.SetIsDeleted();
                oo.AddHistory();
                var documents = oo.RequestGoodsSupplyDetailDocuments.ToList();
                documents.ForEach(oo =>
                {
                    oo.SetIsDeleted();
                });
            });

            entity.AddHistory();
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
