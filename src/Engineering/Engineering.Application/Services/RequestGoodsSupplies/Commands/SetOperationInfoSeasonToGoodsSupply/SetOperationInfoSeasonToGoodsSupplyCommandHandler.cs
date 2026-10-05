using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.SetOperationInfoSeasonToGoodsSupply;

public class SetOperationInfoSeasonToGoodsSupplyCommandHandler : ICommandHandler<SetOperationInfoSeasonToGoodsSupplyCommand, RequestGoodsSupply>
{
    private readonly ILogger<SetOperationInfoSeasonToGoodsSupplyCommandHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public SetOperationInfoSeasonToGoodsSupplyCommandHandler(
        ILogger<SetOperationInfoSeasonToGoodsSupplyCommandHandler> logger,
        IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupply?>> Handle(SetOperationInfoSeasonToGoodsSupplyCommand request, CT ct)
    {
        try
        {
            var entity = request.GoodsSupply;

            entity.SetOperationInfoSeason(request.OperationInfoSeason);

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
