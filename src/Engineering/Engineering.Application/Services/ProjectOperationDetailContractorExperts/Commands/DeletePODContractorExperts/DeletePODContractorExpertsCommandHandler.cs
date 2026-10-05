using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Errors.WbsTemplates;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.DeletePODContractorExperts;

public class DeletePODContractorExpertsCommandHandler : ICommandHandler<DeletePODContractorExpertsCommand, ProjectOperationDetailContractorExpert?>
{
    private readonly ILogger<DeletePODContractorExpertsCommandHandler> _logger;
    private readonly IProjectOperationDetailContractorExpertRepository _repository;

    public DeletePODContractorExpertsCommandHandler(ILogger<DeletePODContractorExpertsCommandHandler> logger,
        IProjectOperationDetailContractorExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailContractorExpert?>> Handle(DeletePODContractorExpertsCommand request, CT ct)
    {
        try
        {
            var delete = await _repository.GetById(request.Id, ct);
            if (delete is null)
                return Result.Failure<ProjectOperationDetailContractorExpert?>(WbsTemplateErrors.ProjectOperationWbsWithIdNotFound);

            delete.ProjectOperationDetailContractorService.AddRemainingVolume(delete.Volume);

            delete.SoftDelete();
            return delete;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetailContractorExpert?>(SharedErrors.UnknownError);
        }
    }
}