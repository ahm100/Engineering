using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRequestGoodsSupply;

public class DeleteRequestGoodsSupplyCommandHandler : ICommandHandler<DeleteRequestGoodsSupplyCommand, RequestGoodsSupply>
{
    private readonly ILogger<DeleteRequestGoodsSupplyCommandHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public DeleteRequestGoodsSupplyCommandHandler(ILogger<DeleteRequestGoodsSupplyCommandHandler> logger,
                                                  IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupply?>> Handle(DeleteRequestGoodsSupplyCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetRequestGoodsSupplyById(request.RequestGoodsSupplyId, ct);
            if (entity is null)
                return Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyErrors.IsDeleted);
            //if (!RequestGoodsSupply.AllowStatusForDelete.Any(x => x.Equals(entity.Status)))
            //    return Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyStatusForDelete);

            entity.SetIsDelete();

            var details = entity.RequestGoodsSupplyDetails.ToList();
            var products = entity.RequestGoodsSupplyProducts.ToList();
            products.ForEach(oo =>
            {
                oo.SetIsDeleted();
                oo.AddHistory();
            });

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
            return Result.Failure<RequestGoodsSupply>(SharedErrors.UnknownError);
        }
    }
}
