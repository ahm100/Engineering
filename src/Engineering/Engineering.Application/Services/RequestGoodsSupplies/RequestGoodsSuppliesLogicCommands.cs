using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Application.Services.RequestGoodsSupplies.Contracts.CreatePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Contracts.DeletePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Contracts.UpdatePRequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSupplyDetailsStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyById;
using Engineering.Application.WebServices.IdentityServices.Users.Queries.GetUsersByActionId;
using Engineering.Domain.Entities.EngineeringConfig;
using Engineering.Domain.Entities.EngineeringConfig.Enum;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using MessageSender.ClientSdk.Messaging;
using MessageSender.ClientSdk.Messaging.Targets;

namespace Engineering.Application.Services.RequestGoodsSupplies;

partial class RequestGoodsSupplyLogic
{
    public async Task<Result<RequestGoodsSupply?>> CreatePRequestGoodsSuppliesHandler(CreateProjectRequestGoodsSuppliesRequest request, Project project, long? companyId, long? requestingOrganizationId, CT ct)
    {
        try
        {
            var status = GoodsSupplyStatus.Created;
            if (request.IsDraft)
                status = GoodsSupplyStatus.Draft;

            string? configCode = null;
            var engConfig = await _mediator.Send(new GetActiveConfigQuery(), ct);
            if (!engConfig.IsBad() && engConfig.Value is not null && engConfig.Value!.HaveCodingAlgorithm)
            {
                EngineeringCodingConfig? codingConfig = null;
                switch (request.Type)
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
                    var lastSerial = await _requestGoodsSupplyRepository.GetLastCodeSerialByPrefix(
                        codingConfig.Prefix,
                        ct);

                    configCode = $"{codingConfig.Prefix}{lastSerial + 1}";
                }
            }

            if (request.Type == GoodsSupplyType.PurchaseForContractor && request.Details.Any(x => x.ContractorId is null))
                return Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyErrors.ContractorIdCantBeNull);

            var entity = RequestGoodsSupply.Create(new CreateRGSParameters
            {
                Project = project,
                ProjectOperation = null,
                ProjectOperationDetail = null,
                OperationInfoSeason = null,

                ConfigCode = configCode,
                Type = request.Type,
                Status = status,
                IsProjectSupply = true,

                SupplierId = request.SupplyerId,
                BuyerId = request.BuyerId,
                CompanyId = companyId,
                RequestingOrganizationId = requestingOrganizationId,

                CurrencyId = request.CurrencyId,
                TransferPrice = request.TransferPrice,
                OtherPrice = request.OtherPrice,
                DiscountOnInvoicePercentage = request.DiscountOnInvoicePercentage,
                DiscountOnInvoiceNumber = request.DiscountOnInvoiceNumber,
                DiscountedPriceOnInvoice = request.DiscountedPriceOnInvoice,
                TaxOnInvoicePercentage = request.TaxOnInvoicePercentage,
                TaxOnInvoiceNumber = request.TaxOnInvoiceNumber,
                FinalInvoiceAmount = null,

                RequestedDate = request.RequestedDate,

                IsPettyCash = request.IsPettyCash,
                Description = request.Description,

                ConsumptionRateAndInventoryUrl = request.ConsumptionRateAndInventoryUrl,
                ConsumptionAddress = request.ConsumptionAddress,

                PurchaseLocation = request.PurchaseLocation,
                PurchaseReason = request.PurchaseReason
            });

            var result = await _requestGoodsSupplyRepository.Create(entity, ct);
            return result;
        }
        catch
        {
            return Result.Failure<RequestGoodsSupply>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<RequestGoodsSupply?>> DeletePRequestGoodsSupplyHandler(DeleteProjectRequestGoodsSuppliesRequest request, CT ct)
    {
        try
        {
            var response = await _mediator.Send(new GetRequestGoodsSupplyByIdQuery(request.Id));
            if (response.IsBad())
                return Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
            var entity = response.Value;
            entity!.SetIsDelete();
            await _requestGoodsSupplyRepository.Update(entity);
            return entity;
        }
        catch
        {
            return Result.Failure<RequestGoodsSupply>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<RequestGoodsSupply?>> Update(UpdateProjectRequestGoodsSuppliesRequest request, CT ct)
    {
        try
        {
            var response = await _mediator.Send(new GetRequestGoodsSupplyByIdQuery(request.RequestGoodsSupplyId));
            if (response.IsBad())
                return Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
            var entity = response.Value;
            entity!.Update(new UpdateRGSParameters
            {
                SupplierId = request.SupplyerId,
                BuyerId = request.BuyerId,
                CurrencyId = request.CurrencyId,
                TransferPrice = request.TransferPrice,
                OtherPrice = request.OtherPrice,
                DiscountOnInvoicePercentage = request.DiscountOnInvoicePercentage,
                DiscountOnInvoiceNumber = request.DiscountOnInvoiceNumber,
                DiscountedPriceOnInvoice = request.DiscountedPriceOnInvoice,
                TaxOnInvoicePercentage = request.TaxOnInvoicePercentage,
                TaxOnInvoiceNumber = request.TaxOnInvoiceNumber,
                RequestedDate = request.RequestedDate,
                IsPettyCash = request.IsPettyCash,
                Description = request.Description,
                ConsumptionRateAndInventoryUrl = request.ConsumptionRateAndInventoryUrl,
                ConsumptionAddress = request.ConsumptionAddress,
                PurchaseLocation = request.PurchaseLocation,
                PurchaseReason = request.PurchaseReason,
                IsDraft = request.IsDraft
            });
            await _requestGoodsSupplyRepository.Update(entity);
            return entity;
        }
        catch
        {
            return Result.Failure<RequestGoodsSupply>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<RequestGoodsSupply?>> RGSupplyDetailsStatusChangerCommand(RGSupplyDetailsStatusChangerRequest request, CT ct)
    {
        try
        {
            var RGS = await _requestGoodsSupplyRepository.GetRequestProducts(request.RequestGoodsSupplyId, ct);

            if (RGS is null)
                return Result.Failure<RequestGoodsSupply?>(RequestGoodsSupplyErrors.RequestGoodsProductNotFound);

            foreach (var item in RGS.RequestGoodsSupplyProducts)
            {
                if (GSDSRules.PMStatuses.Contains(request.Status) && !GSDSRules.AllowForPM.Contains(item.Status))
                    return Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyDetailErrors.InValidPMChangeStatus);
                else if (GSDSRules.ManagementStatuses.Contains(request.Status) && !GSDSRules.AllowForManagement.Contains(item.Status))
                    return Result.Failure<RequestGoodsSupply>(RequestGoodsSupplyDetailErrors.InValidManagementChangeStatus);

                item.SetStatus(request.Status, request.Description);
                await _requestGoodsSupplyProductRepository.Update(item);
            }

            if (GSDSRules.AllowForManagement.Contains(request.Status))
            {
                var ids = await _mediator.Send(new GetUsersByActionIdQuery([10812]), ct);
                if (!ids.IsBad() && ids.Value!.Value is not null && ids.Value.Value.Data is not null)
                {
                    var thirdParties = await _thirdPartyRepo.GetByUserIds(
                        ids.Value.Value.Data.Listed(x => x.UserId), ct);

                    if (thirdParties is not null && thirdParties.Count > 0)
                    {
                        foreach (var item in thirdParties)
                        {
                            try
                            {
                                await _relay.Send(new MessageEnvelope(
                                    MessageSender.ClientSdk.Enums.MessageChannels.Inbox,

                                    new TemplatedMessage(Guid.NewGuid().ToString(), "engineering-set-operator", new Dictionary<string, object?>
                                    {
                                    { "FullName", item?.FirstName + " " + item?.LastName },
                                    { "RequestNumber", RGS.RequestSerialNumber },
                                    })

                                    , new UserTarget(item!.UserId!.Value)), ct);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex,
                                    "Failed to send notification for request {RequestNumber}",
                                    RGS.RequestSerialNumber);
                            }
                        }
                    }
                }
            }
            return RGS;
        }
        catch
        {
            return Result.Failure<RequestGoodsSupply>(SharedErrors.UnknownError);
        }
    }
}