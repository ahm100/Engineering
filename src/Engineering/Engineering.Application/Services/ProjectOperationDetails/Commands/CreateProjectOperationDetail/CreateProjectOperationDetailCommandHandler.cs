using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.CreateBatchPODContractorExpert;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails.Users;
using ProjectOperationDetailStatus = Engineering.Domain.Entities.ProjectOperationDetails.Enums.ProjectOperationDetailStatus;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.CreateProjectOperationDetail;

public class CreateProjectOperationDetailCommandHandler : ICommandHandler<CreateProjectOperationDetailCommand, ProjectOperationDetail>
{
    private readonly ILogger<CreateProjectOperationDetailCommand> _logger;
    private readonly IProjectOperationDetailRepository _repository;
    private readonly IProjectOperationDetailContractorExpertRepository _detailRepo;
    private readonly IMediator _mediator;

    public CreateProjectOperationDetailCommandHandler(ILogger<CreateProjectOperationDetailCommand> logger,
        IProjectOperationDetailRepository repository,
        IProjectOperationDetailContractorExpertRepository detailRepo,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _detailRepo = detailRepo;
        _mediator = mediator;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(CreateProjectOperationDetailCommand request, CT ct)
    {
        try
        {
            var entity = ProjectOperationDetail.Create(
                request.ProjectOperation,
                request.OperationLocation,
                request.Code,
                request.StartDate,
                request.EndDate,
                request.Length,
                request.LengthChangeable,
                request.Width,
                request.WidthChangeable,
                request.Height,
                request.HeightChangeable,
                request.Weight,
                request.WeightChangeable,
                request.Number,
                request.NumberChangeable,
                (ProjectOperationDetailStatus)request.Status!,
                request.Priority,
                request.Day ?? 0,
                request.Hour ?? 0,
                request.CreatedProductId,
                request.Description,
                request.Urls,
                request.CompanyId);

            if (request.PlannerRequests is not null)
                if (request.PlannerRequests?.Count > 0)
                    foreach (var item in request.PlannerRequests)
                        entity.AddPlaner(new UserPlaner(entity, item!));

            if (request.ImplementationAssistantRequests is not null)
                if (request.ImplementationAssistantRequests?.Count > 0)
                    foreach (var item in request.ImplementationAssistantRequests)
                        entity.AddImplementationAssistant(new UserImplementation(entity, item!));

            if (request.TechnicalAssistantRequests is not null)
                if (request.TechnicalAssistantRequests?.Count > 0)
                    foreach (var item in request.TechnicalAssistantRequests)
                        entity.AddTechnicalAssistant(new UserTechnical(entity, item!));


            var contractorMap = new Dictionary<string, ProjectOperationDetailContractorService>();
            var expertMap = new Dictionary<string, ConsumableVolumeExpert>();

            if (request.ContractorServiceRequests is not null)
                if (request.ContractorServiceRequests is { Count: > 0 })
                {
                    var hasServiceBased =
                        request.ContractorServiceRequests.Any(x =>
                            x is not null &&
                            x.Type == PODContractorServiceType.ServiceBased);

                    var hasOperationBased =
                        request.ContractorServiceRequests.Any(x =>
                            x is not null &&
                            x.Type == PODContractorServiceType.OperationBased);

                    if (hasServiceBased && hasOperationBased)
                    {
                        return Result.Failure<ProjectOperationDetail>(
                            ContractorServiceErrors.MixedContractorServiceTypeNotAllowed);
                    }
                }
            foreach (var item in request.ContractorServiceRequests)
            {
                var contractorService = new ProjectOperationDetailContractorService(entity, item!.OperationInfoService,
                    null, item.ContractorId, item.Volume, item.TimeSpant, item.IsActive, item.Type);
                entity.AddServiceInfo(contractorService);
                if (!String.IsNullOrEmpty(item.TempId))
                    contractorMap[item.TempId] = contractorService;
            }

            if (request.ExpertRequests is not null)
                if (request.ExpertRequests?.Count > 0)
                    foreach (var item in request.ExpertRequests)
                    {
                        var expert = new ConsumableVolumeExpert(entity, item!.ExpertId, item.Number ?? 0, item.UnusedPercentage,
                            item.IsStandard, item.StandardValue, item.FinalValue);
                        entity.AddExpert(expert);
                        if (!String.IsNullOrEmpty(item.TempId))
                            expertMap[item.TempId] = expert;
                    }

            if (request.MachineryRequests is not null)
                if (request.MachineryRequests?.Count > 0)
                    foreach (var item in request.MachineryRequests)
                        entity.AddMachinery(new ConsumableVolumeMachinery(entity, item!.Machinery, item.Number ?? 0, item.UnusedPercentage,
                            item.IsStandard, item.StandardValue, Convert.ToDecimal(item.FinalValue), item.Unit));

            if (request.ProductRequests is not null)
                if (request.ProductRequests?.Count > 0)
                    foreach (var item in request.ProductRequests)
                        entity.AddProduct(new ConsumableVolumeProduct(entity, item!.ProductGroupId, item.UnusedPercentage, item.IsStandard,
                            item.StandardValue, item.FinalValue, item.VolumeProductType));

            if (request.DeductionRequests is not null)
                if (request.DeductionRequests?.Count > 0)
                    foreach (var item in request.DeductionRequests)
                        entity.AddDeduction(new ProjectOperationDetailDeduction(entity, item!.Length, item.Width, item.Height, item.Weight, item.Number));

            List<CreateBatchPODContractorExpertModel>? createContractorExperts = [];
            if (request.ContractorExpertLinks is not null && request.ContractorExpertLinks.Count > 0)
                foreach (var item in request.ContractorExpertLinks)
                {
                    if (!contractorMap.TryGetValue(item.ContractorServiceTempId, out var contractor))
                        continue;

                    if (!expertMap.TryGetValue(item.ExpertTempId, out var expert))
                        continue;

                    var create = new CreateBatchPODContractorExpertModel
                    {
                        ProjectOperationDetailContractorService = contractor,
                        ConsumableVolumeExpert = expert,
                        Volume = item.Volume,
                        IsActive = item.IsActive
                    };

                    createContractorExperts.Add(create);
                }

            var createBatch = await _mediator.Send(new CreateBatchPODContractorExpertCommand(createContractorExperts), ct);
            if (createBatch.IsBad())
                return createBatch.Failure<ProjectOperationDetail?>();

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}