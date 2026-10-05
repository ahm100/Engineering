using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Domain.Entities.CostOvers;
using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Commands.UpdateCostOver;

public class UpdateCostOverCommandHandler : ICommandHandler<UpdateCostOverCommand, CostOver>
{
    private readonly ILogger<UpdateCostOverCommand> _logger;
    private readonly ICostOverRepository _repository;

    public UpdateCostOverCommandHandler(
        ILogger<UpdateCostOverCommand> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostOver?>> Handle(UpdateCostOverCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetCostOverById(request.Id, ct);
            if (entity is null)
                return Result.Failure<CostOver>(CostOverErrors.CostOverWithIdNotFound);

            //TOWork
            if (entity.ApprovalStatus != CostOverApprovalStatus.Draft)
                throw new InvalidOperationException(
                    "ویرایش اطلاعات فقط در وضعیت پیش نویس مجاز است.");

            entity.SetName(request.CostOverName);
            entity.SetCode(request.CostOverCode);
            entity.SetCompanyId(request.CompanyId);
            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive)
                    entity.SetActive();
                else
                    entity.SetInActive();
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<CostOver>(SharedErrors.UnknownError);
        }
    }
}