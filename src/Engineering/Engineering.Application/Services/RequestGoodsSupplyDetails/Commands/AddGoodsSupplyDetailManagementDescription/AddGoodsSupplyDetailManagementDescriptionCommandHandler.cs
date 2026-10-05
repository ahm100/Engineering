using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.AddGoodsSupplyDetailManagementDescription;

public class AddGoodsSupplyDetailManagementDescriptionCommandHandler : ICommandHandler<AddGoodsSupplyDetailManagementDescriptionCommand, RequestGoodsSupplyDetail>
{
    private readonly ILogger<AddGoodsSupplyDetailManagementDescriptionCommandHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public AddGoodsSupplyDetailManagementDescriptionCommandHandler(ILogger<AddGoodsSupplyDetailManagementDescriptionCommandHandler> logger,
                                                        IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyDetail?>> Handle(AddGoodsSupplyDetailManagementDescriptionCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetRequestGoodsSupplyDetailById(request.Id, ct);
            if (entity is null)
                return Result.Failure<RequestGoodsSupplyDetail>(RequestGoodsSupplyDetailErrors.RequestGoodsSupplyDetailWithIdNotFound);

            entity.SetManagementDescription(request.ManagementDescription);
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
