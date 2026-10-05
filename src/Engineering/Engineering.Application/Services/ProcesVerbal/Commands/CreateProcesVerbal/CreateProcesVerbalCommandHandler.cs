using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Abstractions.Data.ProcesVerbals;
using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProcesVerbal.Contracts.CreateProcesVerbal;
using Engineering.Domain.Entities.ProcesVerbal;
using Engineering.Domain.Entities.ProcesVerbal.Enums;

namespace Engineering.Application.Services.ProcesVerbal.Commands.CreateProcesVerbal;

public class CreateProcesVerbalCommandHandler : ICommandHandler<CreateProcesVerbalCommand, CreateProcesVerbalResponse?>
{
    private readonly ILogger<CreateProcesVerbalCommandHandler> _logger;
    private readonly IProcesVerbalsRepository _repository;
    private readonly IProcesVerbalProductRepository _productRepository;
    private readonly IProcesVerbalItemRepository _itemRepository;
    private readonly IConsumableVolumeProductRepository _consumableVolumeProductRepository;
    private readonly IProjectOperationDetailRepository _projectODRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IContractRepository _contractRepository;
    private readonly IProjectRepository _projectRepository;

    public CreateProcesVerbalCommandHandler(
        ILogger<CreateProcesVerbalCommandHandler> logger,
        IProcesVerbalsRepository repository,
        IProcesVerbalProductRepository productRepository,
        IConsumableVolumeProductRepository ConsumableVolumeProductRepository,
        IUnitOfWork unitOfWork,
        IProcesVerbalItemRepository itemRepository,
        IProjectOperationDetailRepository projectODRepository,
        IContractRepository contractRepository,
        IProjectRepository projectRepository)
    {
        _logger = logger;
        _repository = repository;
        _productRepository = productRepository;
        _consumableVolumeProductRepository = ConsumableVolumeProductRepository;
        _unitOfWork = unitOfWork;
        _itemRepository = itemRepository;
        _projectODRepository = projectODRepository;
        _contractRepository = contractRepository;
        _projectRepository = projectRepository;
    }

    public async Task<Result<CreateProcesVerbalResponse?>> Handle(
        CreateProcesVerbalCommand request, CT ct)
    {
        var products = request.Products ?? [];
        var pods = request.PODs ?? [];

        var productIds = products.Select(p => p.ProductId).Distinct().ToList();
        var productMap = (await _consumableVolumeProductRepository.GetByIds(productIds, ct)!)!
            .ToDictionary(e => e.Id);

        if (productMap.Count != productIds.Count)
            return Result.Failure<CreateProcesVerbalResponse>(SharedErrors.ItemNotFound)!;

        var podIds = pods.Select(p => p.PODId).Distinct().ToList();
        var podMap = (await _projectODRepository.GetsByIds(podIds, ct))
            .Data.ToDictionary(e => e.Id);

        if (podMap.Count != podIds.Count)
            return Result.Failure<CreateProcesVerbalResponse>(SharedErrors.ItemNotFound)!;

        var contract = await _contractRepository.GetContractById(request.ContractId, ct);
        if (contract is null)
            return Result.Failure<CreateProcesVerbalResponse>(SharedErrors.ItemNotFound)!;

        var project = await _projectRepository.GetById(request.ProjectId, ct);
        if (project is null)
            return Result.Failure<CreateProcesVerbalResponse>(SharedErrors.ItemNotFound)!;

        await _unitOfWork.BeginTransactionAsync(ct);
        try
        {
            var procesVerbal = Domain.Entities.ProcesVerbal.ProcesVerbal.Create(
                request.TitleFa, request.TitleEn, request.Type, request.RecordDateTime,
                request.Location, request.ContractId, request.ProjectId,
                request.DeliveryStatus, request.Limitations, ComputeProductStatus(products));

            procesVerbal.SetWorkStatus(request.WorkStatus);
            procesVerbal.SetLimitationStatus(request.LimitationStatus);
            procesVerbal.SetWorkStartStatus(request.WorkStartStatus);
            procesVerbal.SetWorkStopReason(request.WorkStopReason);
            procesVerbal.SetWorkStopStatus(request.WorkStopStatus);

            foreach (var titleFa in request.Items ?? [])
                procesVerbal.AddItem(ProcesVerbalItem.Create(procesVerbal, titleFa));

            foreach (var url in request.Docs ?? [])
                procesVerbal.AddDocument(new ProcesVerbalDoc(url, procesVerbal));

            foreach (var pod in pods)
                procesVerbal.AddPOD(ProcesVerbalPOD.Create(
                    procesVerbal, podMap[pod.PODId], pod.NewFinalAmount));

            foreach (var p in products)
                procesVerbal.AddProduct(ProcesVerbalProduct.Create(
                    procesVerbal, productMap[p.ProductId], p.NewFinalAmount, p.Status, p.Description));

            await _repository.Create(procesVerbal, ct);
            await _unitOfWork.CommitAsync(ct);
            await _unitOfWork.CommitTransactionAsync(ct);

            return Result.Success(new CreateProcesVerbalResponse(procesVerbal.Id))!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error CreateProcesVerbal: {TitleFa}", request.TitleFa);
            await _unitOfWork.RollbackTransactionAsync(ct);
            return Result.Failure<CreateProcesVerbalResponse>(SharedErrors.UnknownError)!;
        }
    }

    private static ProcesVerbalProductStatus? ComputeProductStatus(
        IReadOnlyCollection<ProcesVerbalProductModel> products)
    {
        if (products.Count == 0) return null;

        if (products.Any(e => e.Status == ProcesVerbalProductItemStatus.Parted))
            return ProcesVerbalProductStatus.Parted;

        if (products.All(e => e.Status == ProcesVerbalProductItemStatus.Full))
            return ProcesVerbalProductStatus.Full;

        if (products.All(e => e.Status == ProcesVerbalProductItemStatus.Processing))
            return ProcesVerbalProductStatus.Processing;

        return ProcesVerbalProductStatus.Parted;
    }
}