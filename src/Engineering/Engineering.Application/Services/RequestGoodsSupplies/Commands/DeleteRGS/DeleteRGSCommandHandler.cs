using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRGS;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRGS;

public class DeleteRGSCommandHandler : ICommandHandler<DeleteRGSCommand, DeleteRGSResponse?>
{
    private readonly ILogger<DeleteRGSCommandHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public DeleteRGSCommandHandler(ILogger<DeleteRGSCommandHandler> logger,
        IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DeleteRGSResponse?>> Handle(DeleteRGSCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetRequestGoodsSupplyById(request.Id, ct);
            if (entity is null)
                return Result.Failure<DeleteRGSResponse?>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<DeleteRGSResponse?>(RequestGoodsSupplyErrors.IsDeleted);

            entity.SoftDelete();

            var details = entity.RequestGoodsSupplyTypeDetails.ToList();
            var types = entity.RequestGoodsSupplyTypes.ToList();
            types.ForEach(oo =>
            {
                oo.SoftDelete();
                //_.AddHistory();
            });

            details.ForEach(oo =>
            {
                oo.SoftDelete();
                //_.AddHistory();
                //var documents = oo.RequestGoodsSupplyDetailDocuments.ToList();
                //documents.ForEach(oo =>
                //{
                //    oo.SetIsDeleted();
                //});
            });

            entity.AddHistory();
            await _repository.Update(entity);
            return new DeleteRGSResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DeleteRGSResponse>(SharedErrors.UnknownError);
        }
    }
}
