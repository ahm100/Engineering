using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.DeleteRGSType;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.DeleteRGSType;

public class DeleteRGSTypeCommandHandler : ICommandHandler<DeleteRGSTypeCommand, DeleteRGSTypeResponse?>
{
    private readonly ILogger<DeleteRGSTypeCommandHandler> _logger;
    private readonly IRequestGoodsSupplyTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRGSTypeCommandHandler(ILogger<DeleteRGSTypeCommandHandler> logger,
        IRequestGoodsSupplyTypeRepository repository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DeleteRGSTypeResponse?>> Handle(DeleteRGSTypeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<DeleteRGSTypeResponse?>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<DeleteRGSTypeResponse?>(RequestGoodsSupplyErrors.IsDeleted);

            if(entity.RequestGoodsSupplyTypeDetails is not null && entity.RequestGoodsSupplyTypeDetails.Count > 0)
                foreach (var item in entity.RequestGoodsSupplyTypeDetails)
                {
                    item.SoftDelete();
                }

            entity.SoftDelete();
            await _unitOfWork.CommitAsync(ct);

            return new DeleteRGSTypeResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DeleteRGSTypeResponse>(SharedErrors.UnknownError);
        }
     }
}