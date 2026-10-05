using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSAdvertisementTypes.Create;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSAdvertisementTypes.Update;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProductTypes.Create;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProductTypes.Update;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProjectTypes.Create;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSProjectTypes.Update;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSServiceTypes.Create;
using Engineering.Application.Services.RequestGoodsSupplies.Commands.RGSServiceTypes.Update;
using Engineering.Application.Services.RequestGoodsSupplies.Models.UpdateRGSType;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.UpdateRGSType;

public class UpdateRGSTypeCommandHandler : ICommandHandler<UpdateRgsTypeCommand, UpdateRGSTypeResponse?>
{
    private readonly IRequestGoodsSupplyRepository _repository;
    private readonly ILogger<UpdateRGSTypeCommandHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IProjectProductRepository _projectProductRepo;
    private readonly IViewProductRepository _viewProductRepo;

    public UpdateRGSTypeCommandHandler(IRequestGoodsSupplyRepository repository,
        ILogger<UpdateRGSTypeCommandHandler> logger,
        IMediator mediator,
        IProjectProductRepository projectProductRepo,
        IViewProductRepository viewProductRepo)
    {
        _repository = repository;
        _mediator = mediator;
        _projectProductRepo = projectProductRepo;
        _viewProductRepo = viewProductRepo;
        _logger = logger;
    }

    public async Task<Result<UpdateRGSTypeResponse?>> Handle(UpdateRgsTypeCommand request, CT ct)
    {
        try
        {
            var req = request.Request;
            var rgs = await _repository.GetById(req.RequestGoodsSupplyId, ct);
            if (rgs is null)
                return Result.Failure<UpdateRGSTypeResponse?>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);

            rgs.Update(new UpdateRGSParameters
            {
                SupplierId = req.SupplyerId,
                BuyerId = req.BuyerId,
                ServiceReasonType = req.ServiceReasonType,
                CurrencyId = req.CurrencyId,
                TransferPrice = req.TransferPrice,
                OtherPrice = req.OtherPrice,
                DiscountOnInvoicePercentage = req.DiscountOnInvoicePercentage,
                DiscountOnInvoiceNumber = req.DiscountOnInvoiceNumber,
                DiscountedPriceOnInvoice = req.DiscountedPriceOnInvoice,
                TaxOnInvoicePercentage = req.TaxOnInvoicePercentage,
                TaxOnInvoiceNumber = req.TaxOnInvoiceNumber,
                RequestedDate = req.RequestedDate,
                IsPettyCash = false,
                Description = req.Description,
                DescriptionEn = req.DescriptionEn,
                Importance = req.Importance,
                RegistrationNumber = req.RegistrationNumber,
                DeviceCode = req.DeviceCode,
                DeviceName = req.DeviceName,
                DeviceEnName = req.DeviceEnName,
                DeviceNumber = req.DeviceNumber,
                UnitCode = req.UnitCode,
                RequestingOrganizationId = req.RequestingOrganizationId,
                DeliveryDeadline = req.DeliveryDeadline,
                ConsumptionRateAndInventoryUrl = req.ConsumptionRateAndInventoryUrl,
                ConsumptionAddress = req.ConsumptionAddress,
                PurchaseLocation = req.PurchaseLocation,
                PurchaseReason = req.PurchaseReason,
                IsDraft = req.IsDraft
            });

            var rgsTypes = rgs.RequestGoodsSupplyTypes.ToList();
            //delete
            var deleteTypes =
                (req.DeleteProductTypes ?? Enumerable.Empty<long>())
                .Concat(req.DeleteServiceTypes ?? Enumerable.Empty<long>())
                .Concat(req.DeleteAdvertisementTypes ?? Enumerable.Empty<long>())
                .Concat(req.DeleteProjectTypes ?? Enumerable.Empty<long>())
                .ToList();

            foreach (var item in deleteTypes)
            {
                var delete = rgs.RequestGoodsSupplyTypes.FirstOrDefault(x => x.Id == item);
                if (delete.RequestGoodsSupplyTypeDetails.Any())
                    foreach (var detail in delete.RequestGoodsSupplyTypeDetails)
                        detail.SoftDelete();

                delete.SoftDelete();
            }

            var deleteDetails =
                (req.UpdateProductTypes?
                .SelectMany(x => x.DeleteIds ?? Enumerable.Empty<long>()) ?? Enumerable.Empty<long>())
                .Concat(
                    req.UpdateServiceTypes?
                    .SelectMany(x => x.DeleteIds ?? Enumerable.Empty<long>()) ?? Enumerable.Empty<long>())
                .Concat(
                    req.UpdateAdvertisementTypes?
                    .SelectMany(x => x.DeleteIds ?? Enumerable.Empty<long>()) ?? Enumerable.Empty<long>())
                .Concat(
                    req.UpdateProjectTypes?
                    .SelectMany(x => x.DeleteIds ?? Enumerable.Empty<long>()) ?? Enumerable.Empty<long>())
                .ToList();
            foreach (var item in deleteDetails)
            {
                var delete = rgs.RequestGoodsSupplyTypeDetails.FirstOrDefault(x => x.Id == item);
                if (delete is not null)
                {
                    delete.SoftDelete();
                    var type = delete.RequestGoodsSupplyType;
                    if (type.RequestGoodsSupplyTypeDetails.All(x => x.IsDeleted))
                        type.SoftDelete();
                    else
                        type.SetRequestedCount(type.RequestedCount - delete.RequestedCount);
                }
            }

            if (req.UpdateProductTypes is not null && req.UpdateProductTypes.HasAny())
            {
                var update = await _mediator.Send(new UpdateRGSProductTypesCommand(rgs, req.UpdateProductTypes));
                if (update.IsBad())
                    return update.Failure<UpdateRGSTypeResponse?>();
            }

            if (req.UpdateServiceTypes is not null && req.UpdateServiceTypes.HasAny())
            {
                var update = await _mediator.Send(new UpdateRGSServiceTypesCommand(rgs, req.UpdateServiceTypes));
                if (update.IsBad())
                    return update.Failure<UpdateRGSTypeResponse?>();
            }

            if (req.UpdateAdvertisementTypes is not null && req.UpdateAdvertisementTypes.HasAny())
            {
                var update = await _mediator.Send(new UpdateRGSAdvertisementTypesCommand(rgs, req.UpdateAdvertisementTypes));
                if (update.IsBad())
                    return update.Failure<UpdateRGSTypeResponse?>();
            }

            if (req.UpdateProjectTypes is not null && req.UpdateProjectTypes.HasAny())
            {
                var update = await _mediator.Send(new UpdateRGSProjectTypesCommand(rgs, req.UpdateProjectTypes));
                if (update.IsBad())
                    return update.Failure<UpdateRGSTypeResponse?>();
            }

            if (req.CreateProductTypes is not null && req.CreateProductTypes.Count > 0)
            {
                var create = await _mediator.Send(new CreateRGSProductTypesCommand(rgs, req.CreateProductTypes, req.ProjectId));
                if (create.IsBad())
                    return create.Failure<UpdateRGSTypeResponse?>();
            }

            if (req.CreateServiceTypes is not null && req.CreateServiceTypes.Count > 0)
            {
                var create = await _mediator.Send(new CreateRGSServiceTypesCommand(rgs, req.CreateServiceTypes));
                if (create.IsBad())
                    return create.Failure<UpdateRGSTypeResponse?>();
            }

            if (req.CreateAdvertisementTypes is not null && req.CreateAdvertisementTypes.Count > 0)
            {
                var create = await _mediator.Send(new CreateRGSAdvertisementTypesCommand(rgs, req.CreateAdvertisementTypes));
                if (create.IsBad())
                    return create.Failure<UpdateRGSTypeResponse?>();
            }

            if (req.CreateProjectTypes is not null && req.CreateProjectTypes.Count > 0)
            {
                var create = await _mediator.Send(new CreateRGSProjectTypesCommand(rgs, req.CreateProjectTypes));
                if (create.IsBad())
                    return create.Failure<UpdateRGSTypeResponse?>();
            }

            return new UpdateRGSTypeResponse(rgs.Id, true);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<UpdateRGSTypeResponse?>(SharedErrors.UnknownError);
        }
    }
}