using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.DeleteProjectOperationWbs;
using Engineering.Domain.Errors.WbsTemplates;

namespace Engineering.Application.Services.ProjectOperationWbses.Commands.DeleteProjectOperationWbs;

public class DeleteProjectOperationWbsCommandHandler : ICommandHandler<DeleteProjectOperationWbsCommand, DeleteProjectOperationWbsResponse?>
{
    private readonly ILogger<DeleteProjectOperationWbsCommandHandler> _logger;
    private readonly IProjectOperationWbsRepository _repository;

    public DeleteProjectOperationWbsCommandHandler(ILogger<DeleteProjectOperationWbsCommandHandler> logger,
        IProjectOperationWbsRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DeleteProjectOperationWbsResponse?>> Handle(DeleteProjectOperationWbsCommand request, CT ct)
    {
        try
        {
            var pOWbs = await _repository.GetById(request.Id, ct);
            if (pOWbs is null)
                return Result.Failure<DeleteProjectOperationWbsResponse?>(WbsTemplateErrors.ProjectOperationWbsWithIdNotFound);

            pOWbs.SoftDelete();

            return new DeleteProjectOperationWbsResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DeleteProjectOperationWbsResponse?>(SharedErrors.UnknownError);
        }
    }
}