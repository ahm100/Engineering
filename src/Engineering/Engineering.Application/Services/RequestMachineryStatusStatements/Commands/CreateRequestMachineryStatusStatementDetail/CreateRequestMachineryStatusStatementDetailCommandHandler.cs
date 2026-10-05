using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.CreateRequestMachineryStatusStatementDetail;

public class CreateRequestMachineryStatusStatementDetailCommandHandler : ICommandHandler<CreateRequestMachineryStatusStatementDetailCommand, RequestMachineryStatusStatementDetail>
{
    private readonly ILogger<CreateRequestMachineryStatusStatementDetailCommandHandler> _logger;
    private readonly IRequestMachineryStatusStatementDetailRepository _repository;
    private readonly IProjectOperationRepository _projectOperationRepository;
    private readonly IProjectOperationDetailRepository _projectOperationDetailRepository;

    public CreateRequestMachineryStatusStatementDetailCommandHandler(
        ILogger<CreateRequestMachineryStatusStatementDetailCommandHandler> logger,
        IRequestMachineryStatusStatementDetailRepository repository,
        IProjectOperationRepository projectOperationRepository,
        IProjectOperationDetailRepository projectOperationDetailRepository)
    {
        _logger = logger;
        _repository = repository;
        _projectOperationRepository = projectOperationRepository;
        _projectOperationDetailRepository = projectOperationDetailRepository;
    }

    public async Task<Result<RequestMachineryStatusStatementDetail?>> Handle(CreateRequestMachineryStatusStatementDetailCommand request, CT ct)
    {
        try
        {
            var entity = new RequestMachineryStatusStatementDetail(request.Project,
                request.Machinery, request.RequestMachinery, request.RequestMachineryStatusStatement, request.ContractorId, request.FromDate, request.ToDate,
                request.RequestedCount, request.FinalPrice, request.CurrencyId, request.Unit, request.OperatorId, request.TimeRequired);

            var result = await _repository.Create(entity, ct);

            if (request.ProjectOperationIds is not null && request.ProjectOperationIds.Count > 0)
            {
                var projectOperations = await _projectOperationRepository.GetWithoutIncludeByIds(request.ProjectOperationIds, ct);
                if (projectOperations is not null && projectOperations.Count > 0)
                    foreach (var item in projectOperations)
                        entity.AddProjectOepration(item);
            }

            if (request.ProjectOperationDetailIds is not null && request.ProjectOperationDetailIds.Count > 0)
            {
                var projectOperationDetails = await _projectOperationDetailRepository.GetWithoutIncludeByIds(request.ProjectOperationDetailIds, ct);
                if (projectOperationDetails is not null && projectOperationDetails.Count > 0)
                    foreach (var item in projectOperationDetails)
                        entity.AddProjectOeprationDetail(item);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryStatusStatementDetail>(SharedErrors.UnknownError);
        }
    }
}
