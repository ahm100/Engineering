using Engineering.Application.Abstractions.Data.Branchs;
using Branch = Engineering.Domain.Entities.Branchs.Branch;

namespace Engineering.Application.Services.Branchs.Commands.UpdateBranch;

public class UpdateBranchCommandHandler : ICommandHandler<UpdateBranchCommand, Branch>
{
    private readonly ILogger<UpdateBranchCommand> _logger;
    private readonly IBranchRepository _repository;

    public UpdateBranchCommandHandler(
        ILogger<UpdateBranchCommand> logger,
        IBranchRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Branch?>> Handle(UpdateBranchCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetBranchWithoutInclude(request.Id, ct);
            if (entity is null)
                return Result.Failure<Branch>(BranchErrors.BranchWithIdNotFound);

            entity.SetCategory(request.Category);
            entity.SetName(request.BranchName);
            entity.SetCode(request.BranchCode);
            entity.SetCompanyId(request.CompanyId);
            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive)
                    entity.SetActive();
                else
                    entity.SetDeactivate();
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<Branch>(SharedErrors.UnknownError);
        }
    }
}