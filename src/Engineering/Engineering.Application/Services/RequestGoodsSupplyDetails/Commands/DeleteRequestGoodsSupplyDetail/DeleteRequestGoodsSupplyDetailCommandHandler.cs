using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.DeleteRequestGoodsSupplyDetail;

public class DeleteRequestGoodsSupplyDetailCommandHandler : ICommandHandler<DeleteRequestGoodsSupplyDetailCommand, RequestGoodsSupplyDetail>
{
    private ILogger<DeleteRequestGoodsSupplyDetailCommandHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public DeleteRequestGoodsSupplyDetailCommandHandler(ILogger<DeleteRequestGoodsSupplyDetailCommandHandler> logger,
                                                        IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyDetail?>> Handle(DeleteRequestGoodsSupplyDetailCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity is null)
                return Result.Failure<RequestGoodsSupplyDetail>(RequestGoodsSupplyDetailErrors.RequestGoodsSupplyDetailWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<RequestGoodsSupplyDetail>(RequestGoodsSupplyDetailErrors.IsDeleted);

            entity.SetIsDeleted();
            entity.AddHistory();
            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupplyDetail>(SharedErrors.UnknownError);
        }
    }
}
