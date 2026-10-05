using Engineering.Application.IdentityServices.Users.Queries.GetsUserById;
using Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.CreateCCHVersion;
using Engineering.Application.Services.ContractorContracts.Queries.GetsDraftableContractorCostOver;
using Engineering.Application.Services.ContractorStatusStatements.Commands.ContractorStatusStatementStatusChanger;
using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementDraftCreator;
using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;
using Engineering.Application.Services.RequestRewards.Queries.GetsConfirmedContractorRequestReward;
using Engineering.Application.Services.Seasons.Queries.GetSeasonById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredUsers;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.DeletePaymentOrder;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;
using Engineering.Domain.Entities.RequestRewards.Enums;
using Gita.Backend.Shared.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetThirdPartyById;
using EngineeringCreatePaymentOrder = Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreatePaymentOrderWithAutoDetail.CreatePaymentOrderWithAutoDetailCommand;

namespace Engineering.Application.Services.ContractorStatusStatements;

public partial class ContractorStatusStatementLogic : IContractorStatusStatementLogic
{
    private async Task<Result<ContractorContractProductDraftModel?>> GoodsSupplyDraftCreator(
        ContractorStatusStatementDraftCreatorRequest request, CT ct)
    {
        ContractorContractProductDraftModel productDraftModel = new();
        var requestGoodsSupplyQuery = await GetsRequestGoodsSupplyProductByContractorIdExecute(
            request.StartDate,
            request.EndDate,
            request.ContractorId,
            request.ProjectId, ct);
        var requestGoodsSupplyProducts = requestGoodsSupplyQuery.Value!;
        if (requestGoodsSupplyProducts is not null && requestGoodsSupplyProducts.Any())
        {
            var productIds = requestGoodsSupplyProducts.Where(x => x.ProductId > 0).Select(oo => oo.ProductId).Distinct().ToList();
            var getProductsQuery = await _mediator.Send(new GetsProductByIdQuery(1, productIds.Count, null, productIds), ct);
            var products = getProductsQuery.Value?.Data;
            foreach (var requestGoodsSupplyProduct in requestGoodsSupplyProducts)
            {
                var product = products?.Where(oo => oo.Id.Equals(requestGoodsSupplyProduct.ProductId)).FirstOrDefault();
                productDraftModel.Products.Add(new ContractorContractProductsDraftModel()
                {
                    RequestGoodsSupplyDetailId = requestGoodsSupplyProduct.Id,
                    ProductId = requestGoodsSupplyProduct.ProductId,
                    ProductName = product?.Name,
                    MeasureUnit = product?.Group.Measure,
                    OperationInfoName = requestGoodsSupplyProduct?.RequestGoodsSupplyDetails.FirstOrDefault()?.ConsumableVolumeProduct.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                    ProductGroupId = product?.Group.Id,
                    Created = TimeCalculator.DatePiker(requestGoodsSupplyProduct?.Created),

                    SupplyCount = requestGoodsSupplyProduct?.RequestedCount,
                    OtherPrice = (requestGoodsSupplyProduct?.RequestGoodsSupply.OtherPrice) / (requestGoodsSupplyProduct?.RequestGoodsSupply.RequestGoodsSupplyProducts.Count) ?? 0,
                    TransferPrice = (requestGoodsSupplyProduct?.RequestGoodsSupply.TransferPrice) / (requestGoodsSupplyProduct?.RequestGoodsSupply.RequestGoodsSupplyProducts.Count) ?? 0,
                    PackingPrice = (requestGoodsSupplyProduct?.PackingPrice) ?? 0,
                    RequestCount = requestGoodsSupplyProduct is null ? 0 : requestGoodsSupplyProduct.RequestedCount,
                    UnitPrice = requestGoodsSupplyProduct is null ? 0 : requestGoodsSupplyProduct.UnitPrice,
                    TaxNumber = requestGoodsSupplyProduct is null ? 0 : requestGoodsSupplyProduct.TaxNumber,
                    DiscountByNum = requestGoodsSupplyProduct is null ? 0 : requestGoodsSupplyProduct.DiscountByNumber,
                    Urls = requestGoodsSupplyProduct is null ? null : requestGoodsSupplyProduct.RequestGoodsSupplyDetails.FirstOrDefault()?
                        .RequestGoodsSupplyDetailDocuments.Select(x => x.Url).ToList(),
                });
            }
        }

        return productDraftModel;
    }

    private async Task<Result<ContractorCostOverDraftModel?>> CostOverDraftCreator(
        long contractorId, long projectId, CT ct)
    {
        ContractorCostOverDraftModel costOverDraftModel = new();
        var responseCostOvers = await _mediator.Send(new GetsDraftableContractorCostOverQuery(contractorId, projectId, 0, 0), ct);
        var values = responseCostOvers.Value?.Data;
        if (values is not null && values.Any())
        {
            var measureIds = values.Where(x => !x.IsContractorContractCostOver && x.ProjectOperationUnitOfMeasurementId.HasValue && x.ServiceInfoUnitOfMeasurementId.HasValue)
                .SelectMany(x => new[] { x.ProjectOperationUnitOfMeasurementId!.Value, x.ServiceInfoUnitOfMeasurementId!.Value }).Where(id => id > 0).Distinct().ToList();
            var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

            foreach (var costOver in values)
                if (!costOver.IsContractorContractCostOver)
                {
                    costOver.ProjectOperationUnitOfMeasurement = measures?.FirstOrDefault(x => x.Id == costOver.ProjectOperationUnitOfMeasurementId!.Value)?.Name;
                    costOver.ServiceInfoUnitOfMeasurement = measures?.FirstOrDefault(x => x.Id == costOver.ServiceInfoUnitOfMeasurementId!.Value)?.Name;
                }
            costOverDraftModel.CostOvers = values;
        }

        return costOverDraftModel;
    }

    private async Task<Result<Tuple<ContractorContractFineDraftModel, ContractorContractRewardDraftModel>?>> FineAndRewardDraftCreator(
        ContractorStatusStatementDraftCreatorRequest request, CT ct)
    {
        ContractorContractFineDraftModel fineDraftModel = new();
        ContractorContractRewardDraftModel rewardDraftModel = new();
        ContractorContractDiscountDraftModel discountDraftModel = new();
        var requestRewardQueries = await _mediator.Send(new GetsConfirmedContractorRequestRewardQuery(
            request.ProjectId, request.ContractorId, request.StartDate, request.EndDate, 0, 0), ct);
        var requestRewards = requestRewardQueries.Value?.Data?.ToList();
        if (requestRewards is not null && requestRewards.Any())
            foreach (var reward in requestRewards)
            {
                if (reward.Type == RequestRewardType.Fine || reward.Type == RequestRewardType.FiduciaryProductFine)
                    fineDraftModel.Fines.Add(new ContractorContractFinesDraftModel()
                    {
                        RequestRewardId = reward.Id,
                        Created = TimeCalculator.DatePiker(reward.Created),
                        Description = reward.Description,
                        Type = reward.Type,
                        ManagerDescription = reward.ManagerDescription,
                        Price = Math.Round(reward.ConfirmedPrice, 2),
                        Urls = reward.RequestRewardDocuments.Listed(x => x.Url),
                        OfferedPrice = reward.OfferedPrice is null ? 0 : Math.Round(reward.OfferedPrice!.Value, 2),
                    });

                if (reward.Type == RequestRewardType.Reward)
                    rewardDraftModel.Rewards.Add(new ContractorContractRewardsDraftModel()
                    {
                        RequestRewardId = reward.Id,
                        Created = TimeCalculator.DatePiker(reward.Created),
                        Description = reward.Description,
                        ManagerDescription = reward.ManagerDescription,
                        Price = Math.Round(reward.ConfirmedPrice, 2),
                        Urls = reward.RequestRewardDocuments.Listed(x => x.Url),
                        OfferedPrice = reward.OfferedPrice is null ? 0 : Math.Round(reward.OfferedPrice!.Value, 2),
                    });

                if (reward.Type == RequestRewardType.Discount)
                    discountDraftModel.Discounts.Add(new ContractorContractDiscountsDraftModel()
                    {
                        RequestRewardId = reward.Id,
                        Created = TimeCalculator.DatePiker(reward.Created),
                        Description = reward.Description,
                        ManagerDescription = reward.ManagerDescription,
                        Price = Math.Round(reward.ConfirmedPrice, 2),
                        Urls = reward.RequestRewardDocuments.Listed(x => x.Url),
                        OfferedPrice = reward.OfferedPrice is null ? 0 : Math.Round(reward.OfferedPrice!.Value, 2),
                    });
            }

        return System.Tuple.Create(fineDraftModel, rewardDraftModel);
    }

    private async Task<Result<ContractorStatusStatement?>> StatusChanger(
        ContractorStatusStatementStatusChangerRequest request, CT ct)
    {
        ContractorStatusStatement? value = null;
        if (CSSStatusRules.AllowForNoInclude.Any(x => x.Equals(request.Status)))
        {
            var result = await GetContractorStatusStatementExecute(request.Id, IncludeType.Non, ct);
            if (result.IsBad()) return result.Failure<ContractorStatusStatement>()!;
            value = result.Value!;
        }
        else if (CSSStatusRules.AllowForIncludeLess.Any(x => x.Equals(request.Status)))
        {
            var result = await GetContractorStatusStatementExecute(request.Id, IncludeType.Less, ct);
            if (result.IsBad()) return result.Failure<ContractorStatusStatement>()!;
            value = result.Value!;
        }
        else if (CSSStatusRules.AllowForFullInclude.Any(x => x.Equals(request.Status)))
        {
            var result = await GetContractorStatusStatementExecute(request.Id, IncludeType.Full, ct);
            if (result.IsBad()) return result.Failure<ContractorStatusStatement>()!;
            value = result.Value!;
        }
        else
        {
            var result = await GetContractorStatusStatementExecute(request.Id, IncludeType.Update, ct);
            if (result.IsBad()) return result.Failure<ContractorStatusStatement>()!;
            value = result.Value!;
        }

        var services = value.ContractorStatusStatementDetails.SelectMany(x => x.ContractorStatusStatementServices).ToList();
        var dailyServices = services.SelectMany(x => x.ContractorStatusStatementServiceDailies).ToList();

        decimal projectFix = 0;
        decimal projectService = 0;
        if (request.Status == CSSStatus.ProjectManagerConfirmed)
        {
            var docs = value.ContractorStatusStatementDocuments;

            if (docs.Any() && docs.Count > 0 && request.SelectedUrls != null && request.SelectedUrls.Count > 0)
            {
                var removes = docs.Where(x => !request.SelectedUrls.Contains(x.Url));
                foreach (var item in removes)
                    item.SoftDelete();

                var creates = request.SelectedUrls.Where(x => !docs.Select(x => x.Url).Contains(x));
                foreach (var item in creates)
                    value.AddDocuments([item], true);
            }

            foreach (var item in value.ContractorStatusStatementDetails
                .Where(x => x.ContractorContract.ContractorContractType == ContractorContractType.Fixed))
            {
                if (request.FixedContractPct is not null && request.FixedContractPct.Count > 0)
                {
                    var fix = request.FixedContractPct.FirstOrDefault(x => x.Id == item.Id);
                    if (fix is not null)
                        value.ContractorStatusStatementDetails!.FirstOrDefault(x => x.Id == item.Id)!
                            .SetProjectContractPct(fix.ApprovalPercentage, fix.ApprovedDescription);
                }
                else
                    value.ContractorStatusStatementDetails!.FirstOrDefault(x => x.Id == item.Id)!
                        .SetProjectContractPct(100, "");

                projectFix = value.ContractorStatusStatementDetails.Sum(x => x.ProjectFixedContractPctAmount ?? 0);
            }

            if (request.DailyServices is not null && request.DailyServices.Count > 0)
            {
                foreach (var item in request.DailyServices)
                {
                    var dailyService = dailyServices.Where(x => x.Id == item.Id).FirstOrDefault();
                    if (dailyService is not null)
                    {
                        dailyService.SetProjectManagementApprovalPercentage(item.ManagementApprovalPercentage);
                        dailyService.SetProjectManagementApprovedDescription(item.ApprovedDescription);
                        dailyService.SetProjectManagementApprovedPrice();
                    }
                }

                foreach (var service in services)
                    service.SetProjectManagerApprovalAmount();

                projectService = services.Sum(x => x.ThirdPartiesAmount) ?? 0;
            }

            value.SetProjectManagmentDescription(request.Description);
            value.SetProjectManagerApprovalAmount(projectFix + projectService);
            value.SetRemainingAmount(value.ProjectManagerApprovalAmount ?? 0);
        }

        decimal managmentFix = 0;
        decimal managmentService = 0;
        if (request.Status == CSSStatus.ManagementConfirmed)
        {
            foreach (var item in value.ContractorStatusStatementDetails
                .Where(x => x.ContractorContract.ContractorContractType == ContractorContractType.Fixed))
            {
                if (request.FixedContractPct is not null && request.FixedContractPct.Count > 0)
                {
                    var fix = request.FixedContractPct.FirstOrDefault(x => x.Id == item.Id);
                    if (fix is not null)
                        value.ContractorStatusStatementDetails!.FirstOrDefault(x => x.Id == item.Id)!
                            .SetProjectContractPct(fix.ApprovalPercentage, fix.ApprovedDescription);
                }
                else
                    value.ContractorStatusStatementDetails!.FirstOrDefault(x => x.Id == item.Id)!
                        .SetProjectContractPct(100, "");

                managmentFix = value.ContractorStatusStatementDetails.Sum(x => x.ManagerFixedContractPctAmount ?? 0);
            }

            var docs = value.ContractorStatusStatementDocuments;

            if (docs.Any() && docs.Count > 0 && request.SelectedUrls != null && request.SelectedUrls.Count > 0)
            {
                var removes = docs.Where(x => !request.SelectedUrls.Contains(x.Url));
                foreach (var item in removes)
                    item.SoftDelete();

                var creates = request.SelectedUrls.Where(x => !docs.Select(x => x.Url).Contains(x));
                foreach (var item in creates)
                    value.AddDocuments([item], true);
            }

            if (request.DailyServices is not null && request.DailyServices.Count > 0)
            {
                foreach (var item in request.DailyServices)
                {
                    var dailyService = dailyServices.Where(x => x.Id == item.Id).FirstOrDefault();
                    if (dailyService is not null)
                    {
                        dailyService.SetManagementApprovalPercentage(item.ManagementApprovalPercentage);
                        dailyService.SetApprovedDescription(item.ApprovedDescription);
                        dailyService.SetApprovedPrice();
                    }
                }

                foreach (var service in services)
                    service.SetManagementApprovalAmount();

                managmentService = services.Sum(x => x.ThirdPartiesAmount) ?? 0;
            }
            value.SetManagementApprovalAmount(managmentService + managmentFix);
            value.SetRemainingAmount(value.ManagementApprovalAmount ?? 0);
        }

        long? orderId = null;
        if (request.Status == CSSStatus.PaymentConfirmation)
        {
            if (value.FinalManagerConfirmed != true || value.PrimaryManagerConfirmed != true)
                return Result.Failure<ContractorStatusStatement>(CSSErrors.InValidStatusForConfirmed);
            if (request.ConfirmedPaymentDate is null)
                if (request.ConfirmedBankAccountId is null || request.ConfirmedBankAccountId <= 0)
                    return Result.Failure<ContractorStatusStatement>(CSSErrors.InValidConfirmedBankAccountId);
            if (request.ConfirmedPrice is null || request.ConfirmedPrice <= 0)
                return Result.Failure<ContractorStatusStatement>(CSSErrors.InValidConfirmedPrice);
            if (string.IsNullOrEmpty(request.Description))
                return Result.Failure<ContractorStatusStatement>(CSSErrors.InValidDescription);
            if (request.SeasonId is null || request.SeasonId <= 0)
                return Result.Failure<ContractorStatusStatement>(CSSErrors.InValidSeasonId);
            if (request.ConfirmedPaymentDate is not null)
                if (request.ConfirmedPaymentDate!.Value.Date < DateTime.Now.Date)
                    return Result.Failure<ContractorStatusStatement>(CSSErrors.InValidPaymentDate);

            var responseSeason = await _mediator.Send(new GetSeasonByIdQuery(request.SeasonId!.Value), ct);
            if (responseSeason.IsFailure)
                return Result.Failure<ContractorStatusStatement>(responseSeason.Error!);
            var seasonValue = responseSeason.Value!;

            var getThirdPartyByIdQuery = await _mediator.Send(new GetThirdPartyByIdQuery(value!.ContractorId!.Value), ct);
            var thirdParty = getThirdPartyByIdQuery.Value;

            var primaryManager = value.ContractorStatusStatementHistories.LastOrDefault(x => x.Status == CSSStatus.PrimaryManagerConfirmed);
            var finalManager = value.ContractorStatusStatementHistories.LastOrDefault(x => x.Status == CSSStatus.FinalManagerConfirmed);

            string? primaryUser = string.Empty;
            string? finalUser = string.Empty;
            var userIds = new List<long?> { primaryManager?.CreatorId, finalManager?.CreatorId }
                .Where(id => id.HasValue && id.Value > 0).Select(id => id!.Value).Distinct().ToList();
            if (userIds is not null && userIds.Count > 0)
            {
                userIds = userIds.Where(x => x > 0).Distinct().ToList();
                var response = await _mediator.Send(new GetFilteredUsersQuery(userIds, null, null, null, null, 1, userIds.Count), ct);
                if (response.IsFailure)
                    return Result.Failure<ContractorStatusStatement>(response.Error!);
                primaryUser = response.Value?.Data?.FirstOrDefault(x => x.UserId == primaryManager?.CreatorId)?.FullName;
                finalUser = response.Value?.Data?.FirstOrDefault(x => x.UserId == finalManager?.CreatorId)?.FullName;
            }

            value.SetRemainingAmount(request.ConfirmedPrice ?? 0);

            var createPaymentOrder = await _mediator.Send(new EngineeringCreatePaymentOrder(
                value,
                seasonValue,
                thirdParty,
                request.ConfirmedPrice,
                request.ConfirmedBankAccountId,
                request.ConfirmedPaymentDate,
                request.SelectedUrls,
                request.Description,
                request.CostCategoryId,
                request.CostGroupId,
                request.DocumentTypeId,
                request.PreferentialTypeId,
                new(primaryManager?.CreatorId, primaryUser, CSSStatus.PrimaryManagerConfirmed.GetEnumDescription(), value?.PrimaryManagerDescription),
                new(finalManager?.CreatorId, finalUser, CSSStatus.FinalManagerConfirmed.GetEnumDescription(), value?.FinalManagerDescription)), ct);
            if (createPaymentOrder.IsFailure)
                return Result.Failure<ContractorStatusStatement>(createPaymentOrder.Error!);

            value!.SetConfirmedPaymentDate(request.ConfirmedPaymentDate);
            value!.SetConfirmedBankAccountId(request.ConfirmedBankAccountId);
            value!.SetConfirmedPrice(request.ConfirmedPrice);
            value!.SetSeason(seasonValue);
            value!.SetConfirmedDescription(request.Description);

            orderId = createPaymentOrder.Value!.PaymentOrderId;
        }

        var lastDescription = await DescriptionMacker(value, request.Description, request.Status, ct);
        var changeRes = await _mediator.Send(new ContractorStatusStatementStatusChangerCommand(
            value,
            request.Status,
            request.ConfirmedPrice,
            request.Description,
            request.MultiPayment,
            lastDescription,
            orderId,
            request.SelectedUrls), ct);
        if (changeRes.IsBad())
        {
            var managment = await RollBackManagment(orderId, ct);
            return changeRes.Failure<ContractorStatusStatement>()!;
        }

        if (request.Status == CSSStatus.PaymentConfirmation)
        {
            var headers = value.ContractorStatusStatementDetails
                .Listed(x => x.ContractorContract.ContractorContractHeader);
            var versionRes = await _mediator.Send(new CreateCCHVersionCommand(value, headers), ct);
            if (versionRes.IsBad())
            {
                var managment = await RollBackManagment(orderId, ct);
                return versionRes.Failure<ContractorStatusStatement>()!;
            }
        }

        return value;
    }

    private async Task<string> DescriptionMacker(
        ContractorStatusStatement? value, string? description, CSSStatus status, CT ct)
    {
        var desc = FirstNonEmpty(description, value?.ManagmentDescription);

        var getUsers = await _mediator.Send(new GetsUserByIdQuery([_currenctUserId]), ct);
        var user = getUsers.Value?.Data?.FirstOrDefault();
        var subSystem = "مهندسی";
        return $"{subSystem} - {user?.FullName} - {status.GetEnumDescription()} - {desc}";
    }

    private async Task<bool> RollBackManagment(long? orderId, CT ct)
    {
        var pipeline = _resilience.GetPipeline<Result>("RetryPipeline");
        if (orderId is not null && orderId.HasValue)
        {
            var result = await pipeline.ExecuteAsync(async cancellation =>
                await _mediator.Send(new DeletePaymentOrderCommand(orderId!.Value), ct), ct);
            if (result.IsFailure)
                return false;
        }

        return true;
    }

    string FirstNonEmpty(params string?[] values) =>
        values.FirstOrDefault(s => !string.IsNullOrEmpty(s))!;

}
