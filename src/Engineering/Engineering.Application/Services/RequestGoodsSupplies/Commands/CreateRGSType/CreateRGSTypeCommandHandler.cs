using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSAdvertisementTypes.Create;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProductTypes.Create;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProjectTypes.Create;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSServiceTypes.Create;
using Engineering.Application.Services.RequestGoodsSupplies.Models.CreateRGSType;
using Engineering.Domain.Entities.EngineeringConfig;
using Engineering.Domain.Entities.EngineeringConfig.Enum;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.CreateRGSType;

public class CreateRGSTypeCommandHandler : ICommandHandler<CreateRGSTypeCommand, CreateRGSTypeResponse?>
{
    private readonly ILogger<CreateRGSTypeCommandHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;
    private readonly IMediator _mediator;
    private readonly IUserInfoProvider _userProvider;
    private readonly IProjectProductRepository _projectProductRepo;
    private readonly IViewProductRepository _viewProductRepo;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRGSTypeCommandHandler(
        IRequestGoodsSupplyRepository repository,
        IUserInfoProvider userProvider,
        IMediator mediator,
        IUnitOfWork unitOfWork,
        IProjectProductRepository projectProductRepo,
        ILogger<CreateRGSTypeCommandHandler> logger,
        IViewProductRepository viewProductRepo)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _userProvider = userProvider;
        _projectProductRepo = projectProductRepo;
        _viewProductRepo = viewProductRepo;
        _logger = logger;
    }

    public async Task<Result<CreateRGSTypeResponse?>> Handle(CreateRGSTypeCommand request, CT ct)
    {
        try
        {
            var req = request.Request;

            var company = _userProvider.CompanyId;

            var status = GoodsSupplyStatus.NotSend;
            if (req.IsDraft)
                status = GoodsSupplyStatus.Draft;

            var project = await _mediator.Send(new GetProjectByIdIncludelessQuery(req.ProjectId), ct);
            if (project.IsBad())
                return project.Failure<CreateRGSTypeResponse?>();

            if (!project.Value!.IsOrganizationUnit || project.Value.OrganizationId == null)
                return Result.Failure<CreateRGSTypeResponse?>(RequestGoodsSupplyErrors.ProjectIsNotOrganization);

            if(req.Type == GoodsSupplyType.Services && req.ServiceReasonType == null)
                return Result.Failure<CreateRGSTypeResponse?>(RequestGoodsSupplyErrors.ServiceReasonTypeIsNull);

            string? configCode = null;
            var engConfig = await _mediator.Send(new GetActiveConfigQuery(), ct);
            if (!engConfig.IsBad() && engConfig.Value is not null && engConfig.Value!.HaveCodingAlgorithm)
            {
                EngineeringCodingConfig? codingConfig = null;
                switch (req.Type)
                {
                    case GoodsSupplyType.Products:
                        codingConfig = engConfig.Value!.EngineeringCodingConfigs.FirstOrDefault(x => x.Type == CodingAlgorithmType.ProductRGS);
                        break;
                    case GoodsSupplyType.Services:
                        codingConfig = engConfig.Value!.EngineeringCodingConfigs.FirstOrDefault(x => x.Type == CodingAlgorithmType.ServiceRGS);
                        break;
                    case GoodsSupplyType.Advertisements:
                        codingConfig = engConfig.Value!.EngineeringCodingConfigs.FirstOrDefault(x => x.Type == CodingAlgorithmType.AdsRGS);
                        break;
                    case GoodsSupplyType.ProjectItems:
                        codingConfig = engConfig.Value!.EngineeringCodingConfigs.FirstOrDefault(x => x.Type == CodingAlgorithmType.ProjectRGS);
                        break;
                    default:
                        break;
                }
                if (codingConfig is not null)
                {
                    var lastSerial = await _repository.GetLastCodeSerialByPrefix(
                        codingConfig.Prefix,
                        ct);

                    configCode = $"{codingConfig.Prefix}{lastSerial + 1}";
                }
            }

            RequestGoodsSupply? parent = null;
            if (req.ParentRGSId != null)
            {
                parent = await _repository.GetById(req.ParentRGSId.Value, ct);
                if (parent is null)
                    return Result.Failure<CreateRGSTypeResponse>(RequestGoodsSupplyErrors.ParentNotFound);
                parent.SetIsArchived(true);
                parent.SetStatus(GoodsSupplyStatus.Archive);
            }

            var entity = RequestGoodsSupply.Create(new CreateRGSParameters
            {
                Project = project.Value,
                Type = req.Type,
                Status = status,
                Parent = parent,
                IsProjectSupply = true,
                SupplierId = req.SupplyerId,
                BuyerId = req.BuyerId,
                CompanyId = company,
                ConfigCode = configCode,
                Importance = req.Importance ?? GoodsSupplyDetailImportance.Lowest,
                CurrencyId = req.CurrencyId,
                TransferPrice = req.TransferPrice,
                OtherPrice = req.OtherPrice,
                DeviceCode = req.DeviceCode,
                DeviceName = req.DeviceName,
                DeviceEnName = req.DeviceEnName,
                DeviceNumber = req.DeviceNumber,
                UnitCode = req.UnitCode,
                ServiceReasonType = req.ServiceReasonType,
                DiscountOnInvoicePercentage = req.DiscountOnInvoicePercentage,
                DiscountOnInvoiceNumber = req.DiscountOnInvoiceNumber,
                DiscountedPriceOnInvoice = req.DiscountedPriceOnInvoice,
                TaxOnInvoicePercentage = req.TaxOnInvoicePercentage,
                TaxOnInvoiceNumber = req.TaxOnInvoiceNumber,
                FinalInvoiceAmount = null,
                RequestedDate = req.RequestedDate,
                IsPettyCash = false,
                Description = req.Description,
                ConsumptionRateAndInventoryUrl = req.ConsumptionRateAndInventoryUrl,
                ConsumptionAddress = req.ConsumptionAddress,
                PurchaseLocation = req.PurchaseLocation,
                PurchaseReason = req.PurchaseReason,
                DeliveryDeadline = req.DeliveryDeadline,
                RegistrationNumber = req.RegistrationNumber,
                RequestingOrganizationId = project.Value.OrganizationId,
                DescriptionEn = req.DescriptionEn
            });

            await _repository.Create(entity, ct);

            if (req.ProductModels is not null && req.ProductModels.Any())
            {
                var create = await _mediator.Send(new CreateRGSProductTypesCommand(entity, req.ProductModels, project.Value.Id), ct);
                if (create.IsBad())
                    return create.Failure<CreateRGSTypeResponse?>();
            }

            if (req.ServiceModels is not null && req.ServiceModels.Any())
            {
                var create = await _mediator.Send(new CreateRGSServiceTypesCommand(entity, req.ServiceModels), ct);
                if (create.IsBad())
                    return create.Failure<CreateRGSTypeResponse?>();
            }

            if (req.AdvertisementModels is not null && req.AdvertisementModels.Any())
            {
                var create = await _mediator.Send(new CreateRGSAdvertisementTypesCommand(entity, req.AdvertisementModels), ct);
                if (create.IsBad())
                    return create.Failure<CreateRGSTypeResponse?>();
            }

            if (req.ProjectModels is not null && req.ProjectModels.Any())
            {
                var create = await _mediator.Send(new CreateRGSProjectTypesCommand(entity, req.ProjectModels), ct);
                if (create.IsBad())
                    return create.Failure<CreateRGSTypeResponse?>();
            }

            await _unitOfWork.CommitAsync(ct);
            return new CreateRGSTypeResponse(entity.Id, true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<CreateRGSTypeResponse?>(SharedErrors.UnknownError);
        }

    }
}