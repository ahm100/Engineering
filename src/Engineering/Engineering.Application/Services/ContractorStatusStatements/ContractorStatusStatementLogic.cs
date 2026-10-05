using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsDraftableContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractCostOverForCSS;
using Engineering.Application.Services.ContractorContracts.Queries.GetsDraftableContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Queries.GetsHeaderForContractorStatusStatement;
using Engineering.Application.Services.ContractorStatusStatements.Commands.ContractorStatusStatementCodeCreator;
using Engineering.Application.Services.ContractorStatusStatements.Commands.ContractorStatusStatementStatusChanger;
using Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatement;
using Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementCostOver;
using Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementDetail;
using Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementDiscount;
using Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementFine;
using Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementProduct;
using Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementReward;
using Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementService;
using Engineering.Application.Services.ContractorStatusStatements.Commands.DeleteContractorStatusStatement;
using Engineering.Application.Services.ContractorStatusStatements.Commands.DeleteContractorStatusStatementDiscount;
using Engineering.Application.Services.ContractorStatusStatements.Commands.UpdateContractorStatusStatement;
using Engineering.Application.Services.ContractorStatusStatements.Commands.UpdateContractorStatusStatementDiscount;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSCreators;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSDailyServiceUrls;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSPayments;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts.Enum;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts.Exporter;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts.Enum;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts.Exporter;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSS.ExcelEnum;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSS.Service;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSSByProjectId;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetsContractorStatusStatementDailyExcel.Enum;
using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementCodeCreator;
using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementDiscountOperations;
using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementDraftCreator;
using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;
using Engineering.Application.Services.ContractorStatusStatements.Models.CreateContractorStatusStatement;
using Engineering.Application.Services.ContractorStatusStatements.Models.CSSGroupStatusChanger;
using Engineering.Application.Services.ContractorStatusStatements.Models.DeleteContractorStatusStatement;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementById;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementStatus;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetFilteredContractorStatusStatement;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementDiscountById;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementExcelEnum;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementExcelExporter;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementHistory;
using Engineering.Application.Services.ContractorStatusStatements.Models.UpdateContractorStatusStatement;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetsTotalPriceDailyContractorService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetProjectOperationDetailByIds;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;
using Engineering.Application.Services.Projects.Queries.GetProjectModelById;
using Engineering.Application.Services.RequestRewards.Queries.GetRequestRewardById;
using Engineering.Application.Services.RequestRewards.Queries.GetsConfirmedContractorRequestReward;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetPaymentOrdersTotalSummary;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Queries.GetFilteredPaymentOrdersSummary;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Queries.GetPaymentOrdersTotalSummary;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestRewards.Enums;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.Preferential.Models;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.Preferential.Queries.GetActiveFilteredPreferentials;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.Preferential.Queries.GetFilteredPreferentials;
using Gita.Backend.Shared.Persistence.Extensions;
using Polly.Registry;
using ThirdPartyAlias = Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.ThirdParty;

namespace Engineering.Application.Services.ContractorStatusStatements;

public partial class ContractorStatusStatementLogic : IContractorStatusStatementLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ContractorStatusStatementLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoService _userInfoService;
    private readonly IUserProfileService _userProfileService;
    private readonly IContractorStatusStatementRepository _cssRepository;
    private readonly IContractorStatusStatementDetailRepository _cssDetailRepository;
    private readonly IContractorStatusStatementServiceRepository _cssServiceRepository;
    private readonly IContractorStatusStatementServiceDailyRepository _cssServiceDailyRepository;
    private readonly IContractorStatusStatementDiscountRepository _cssDiscountRepository;
    private readonly IContractorStatusStatementHistoryRepository _cssHistoryRepository;
    private readonly IRequestGoodsSupplyProductRepository _supplyProductRepository;
    private readonly IContractorStatusStatementPaymentRepository _cssPaymentRepository;
    private readonly IRequestRewardRepository _requestRewardRepository;
    private readonly IViewThirdPartyRepository _tPRRepo;
    private readonly IViewProductRepository _productRepo;
    private readonly long _currenctUserId;
    private readonly ResiliencePipelineProvider<string> _resilience;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public ContractorStatusStatementLogic(
        IMediator mediator,
        ILogger<ContractorStatusStatementLogic> logger,
        IUnitOfWork unitOfWork,
        IUserProfileService userProfileService,
        IUserInfoService userInfoService,
        IContractorStatusStatementRepository cssRepository,
        IContractorStatusStatementDetailRepository cssDetailRepository,
        IContractorStatusStatementServiceRepository cssServiceRepository,
        IContractorStatusStatementServiceDailyRepository cssServiceDailyRepository,
        IContractorStatusStatementDiscountRepository cssDiscountRepository,
        IContractorStatusStatementHistoryRepository cssHistoryRepository,
        IViewThirdPartyRepository tPRRepo,
        IViewProductRepository productRepo,
        IRequestGoodsSupplyProductRepository supplyProductRepository,
        IContractorStatusStatementPaymentRepository cssPaymentRepository,
        IRequestRewardRepository requestRewardRepository,
        ResiliencePipelineProvider<string> resilience,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userInfoService = userInfoService;
        _userProfileService = userProfileService;
        _currenctUserId = _userProfileService.GetProfileInfo().UserId;
        _cssRepository = cssRepository;
        _cssDetailRepository = cssDetailRepository;
        _cssServiceRepository = cssServiceRepository;
        _cssServiceDailyRepository = cssServiceDailyRepository;
        _cssDiscountRepository = cssDiscountRepository;
        _cssHistoryRepository = cssHistoryRepository;
        _supplyProductRepository = supplyProductRepository;
        _cssPaymentRepository = cssPaymentRepository;
        _productRepo = productRepo;
        _tPRRepo = tPRRepo;
        _requestRewardRepository = requestRewardRepository;
        _resilience = resilience;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<CreateContractorStatusStatementResponse?>> CreateContractorStatusStatement(
        CreateContractorStatusStatementRequest request, CT ct)
    {
        var queryContractor = await _thirdPartyRepo.GetById(request.ContractorId, ct);
        if (queryContractor is null)
            return Result.Failure<CreateContractorStatusStatementResponse>(CSSErrors.InValidContractor);
        var contractor = queryContractor;

        var queryProject = await _mediator.Send(new GetProjectByIdIncludelessQuery(request.ProjectId), ct);
        if (queryProject.IsFailure || queryProject.Value is null)
            return Result.Failure<CreateContractorStatusStatementResponse>(CSSErrors.InValidProject);
        var project = queryProject.Value;

        if (ProjectStatusRules.NotAllowed.Contains(project.Status))
            return Result.Failure<CreateContractorStatusStatementResponse>(ProjectErrors.NotAllowed);

        var contractHeaderValue = await _mediator.Send(new GetsHeaderForContractorStatusStatementQuery(request.ContractorId, request.ProjectId, 0, 0), ct);
        if (contractHeaderValue.IsFailure || contractHeaderValue.Value is null || contractHeaderValue.Value.Data is null)
            return Result.Failure<CreateContractorStatusStatementResponse>(CSSErrors.InValidContractorContract);
        var contractHeaders = contractHeaderValue.Value.Data.Where(x => x.Status == ContractorContractStatus.ManagementConfirmed).ToList();

        var contractorContracts = contractHeaders.Where(x => x.ContractorContracts is not null && x.ContractorContracts.Count > 0)
             .SelectMany(x => x.ContractorContracts).ToList();

        long? companyId = _userInfoService.UserCompanyId <= 0 || _userInfoService.UserCompanyId == null ? null : _userInfoService.UserCompanyId;
        if (companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            if (companyResponse.IsFailure)
                return Result.Failure<CreateContractorStatusStatementResponse>(companyResponse.Error!);
        }

        var newCode = request.Code;
        if (string.IsNullOrEmpty(newCode))
        {
            var codeResponse = await _mediator.Send(new ContractorStatusStatementCodeCreatorCommand(companyId), ct);
            if (codeResponse.IsFailure)
                return Result.Failure<CreateContractorStatusStatementResponse>(codeResponse.Error!);
            newCode = codeResponse.Value ?? "";
        }

        var invalidatedList = contractorContracts
            .Where(x => x.ContractorStatusStatementDetails is not null && x.ContractorStatusStatementDetails.Count > 0)
            .SelectMany(x => x.ContractorStatusStatementDetails).Select(x => x.ContractorStatusStatement)
            .Where(x => x.Status != CSSStatus.Paid &&
                        x.Status != CSSStatus.PaymentConfirmation &&
                        x.Status != CSSStatus.IncompletelyPaid &&
                        x.Status != CSSStatus.ProjectManagerConfirmed &&
                        x.Status != CSSStatus.ManagementConfirmed &&
                        x.Status != CSSStatus.Invalidated).Distinct().ToList();

        if (request.AccsseToInvalidated)
        {
            foreach (var invalidated in invalidatedList)
            {
                var invalidatedDescription = "به دلیل ثبت صورت وضعیت جدید این صورت وضعیت باطل شده است.";
                if (!string.IsNullOrEmpty(request.InvalidatedDescription))
                    invalidatedDescription = request.InvalidatedDescription;
                var responseChanges = await _mediator.Send(new ContractorStatusStatementStatusChangerCommand(
                    invalidated, CSSStatus.Invalidated, null, request.InvalidatedDescription, false, invalidatedDescription, null, null), ct);
                if (responseChanges.IsFailure)
                    return Result.Failure<CreateContractorStatusStatementResponse>(responseChanges.Error!);
            }
        }

        var lastDescription = await DescriptionMacker(null, request.Description, CSSStatus.New, ct);
        ContractorStatusStatement? entity = null;
        if (contractHeaders is not null && contractHeaders.Count > 0)
            entity = new ContractorStatusStatement(project, CSSType.System, newCode, request.StartDate, request.EndDate,
                request.Description, lastDescription, request.ContractorId, request.CreatorConfirmedAmount, contractHeaders.FirstOrDefault()!.CurrencyId, request.Urls, companyId);

        decimal oldServicedAmount = 0;
        decimal servicedAmount = 0;
        decimal oldFixedAmount = 0;
        decimal fixedAmount = 0;
        decimal advanceAmount = 0;
        var contractorStatusStatementDetails = new List<ContractorStatusStatementDetail>();
        var statusStatementServices = new List<ContractorStatusStatementService>();
        if (contractorContracts is not null && contractorContracts.Count > 0)
            foreach (var contract in contractorContracts)
            {
                advanceAmount = advanceAmount + contract.AdvancePaymentAmount ?? 0;

                var typeCode = contract.ContractorContractType;

                var fixedContractPct = request.FixContracts?.FirstOrDefault(x => x.Id == contract.Id)?.FixedContractPct;
                var fixedDesc = request.FixContracts?.FirstOrDefault(x => x.Id == contract.Id)?.Description;

                var detailEntity = new ContractorStatusStatementDetail(
                    entity!, contract, contract.StartDate, contract.EndDate, contract.TotalAmount,
                    fixedContractPct, fixedDesc, contract.PercentageDoingJobWell, contract.DoingJobWellAmount,
                    contract.PercentageAdvancePayment, contract.AdvancePaymentAmount, contract.DailyLatenessPenalty,
                    contract.WorkDonePercent, contract.WorkDeliveryPercent, contract.WorkCompletionPercent, contract.Description);

                if (typeCode == ContractorContractType.Fixed)
                {
                    var contractorContractsDetails = contract.Details.ToList();
                    if (contractorContractsDetails.Any())
                        foreach (var contractorContractsDetail in contractorContractsDetails)
                        {
                            var detailServices = contractorContractsDetail.ContractorContractDetailServices.ToList();
                            foreach (var detailService in detailServices)
                                if (detailService.ProjectOperationDetailContractorService is not null)
                                {
                                    var dailyServices = detailService.ProjectOperationDetailContractorService.DailyOperationServices
                                        .Where(x => x.DailyProjectOperation.ProjectOperationDetail.Status != ProjectOperationDetailStatus.Stopped).ToList();
                                    if (dailyServices.Any())
                                    {
                                        var serviceEntity = new ContractorStatusStatementService(detailEntity, contractorContractsDetail, null, 0);
                                        foreach (var dailyService in dailyServices)
                                            serviceEntity.AddContractorStatusStatementServiceDailies(dailyService, dailyService.Volume, dailyService.TimeSpant, 0, 100, null);

                                        serviceEntity.SetThirdPartiesAmount(serviceEntity.ContractorStatusStatementServiceDailies.Sum(x => x.AcceptableAmount));
                                        statusStatementServices.Add(serviceEntity);
                                    }
                                }
                        }
                }

                if (typeCode == ContractorContractType.Service)
                {
                    var contractorContractsDetails = contract.Details.ToList();
                    if (contractorContractsDetails.Any())
                        foreach (var contractorContractsDetail in contractorContractsDetails)
                        {
                            var detailServices = contractorContractsDetail.ContractorContractDetailServices.ToList();
                            var detailPrices = contractorContractsDetail.ContractorContractDetailPrices.ToList();
                            foreach (var detailService in detailServices)
                                if (detailService.ProjectOperationDetailContractorService is not null)
                                {
                                    var dailyServices = detailService.ProjectOperationDetailContractorService.DailyOperationServices
                                        .ToList();    /// be darkhast pod owner arzhin Amini va mohandes taghipur validation stopped rizmetre bardashte shod 3/5
                                    if (dailyServices.Any())
                                    {
                                        var serviceEntity = new ContractorStatusStatementService(detailEntity, contractorContractsDetail, null, 0);
                                        foreach (var dailyService in dailyServices)
                                        {
                                            decimal unitPrice = 0;
                                            if (detailPrices is not null)
                                            {
                                                var date = dailyService.DailyProjectOperation.StartDate.Date;
                                                if (!detailPrices.Any(d => d.StartDate.Date <= date && d.EndDate.Date >= date))
                                                {
                                                    if (detailPrices.Any(d => d.IsActive))
                                                        unitPrice = detailPrices.FirstOrDefault(d => d.IsActive)!.Price;
                                                    else
                                                        unitPrice = detailPrices.FirstOrDefault()!.Price;
                                                }
                                                else
                                                    unitPrice = detailPrices.FirstOrDefault(d => d.StartDate.Date <= date && d.EndDate.Date >= date)!.Price;
                                            }

                                            if (request.DailyServices is not null && request.DailyServices.Count > 0 && request.DailyServices.Any(x => x.DailyServiceId == dailyService.Id))
                                            {
                                                var serviceValue = request.DailyServices.Where(x => x.DailyServiceId.Equals(dailyService.Id)).FirstOrDefault();
                                                if (serviceValue is not null)
                                                    serviceEntity.AddContractorStatusStatementServiceDailies(dailyService, dailyService.Volume,
                                                        dailyService.TimeSpant, unitPrice, serviceValue.AcceptablePercentage, serviceValue.AcceptableDescription);
                                            }
                                            else
                                                serviceEntity.AddContractorStatusStatementServiceDailies(dailyService, dailyService.Volume, dailyService.TimeSpant, unitPrice, 100, null);
                                        }

                                        serviceEntity.SetThirdPartiesAmount(serviceEntity.ContractorStatusStatementServiceDailies.Sum(x => x.AcceptableAmount));
                                        statusStatementServices.Add(serviceEntity);
                                    }
                                }
                        }
                }

                if (statusStatementServices.Any())
                    foreach (var statusStatementService in statusStatementServices)
                    {
                        var serviceResponse = await _mediator.Send(new CreateContractorStatusStatementServiceCommand(statusStatementService), ct);
                        if (serviceResponse.IsFailure)
                            return Result.Failure<CreateContractorStatusStatementResponse>(serviceResponse.Error!);
                    }

                var detailResponse = await _mediator.Send(new CreateContractorStatusStatementDetailCommand(detailEntity), ct);
                if (detailResponse.IsFailure)
                    return Result.Failure<CreateContractorStatusStatementResponse>(detailResponse.Error!);
                contractorStatusStatementDetails.Add(detailResponse.Value!);

                if (typeCode == ContractorContractType.Fixed)
                {
                    fixedAmount = fixedAmount + (detailEntity.FixedContractPctAmount ?? 0);
                    oldFixedAmount = oldFixedAmount + (detailEntity.TotalAmount ?? 0);
                }

                if (typeCode == ContractorContractType.Service)
                {
                    servicedAmount = servicedAmount + (detailEntity.ContractorStatusStatementServices.Sum(x => x.ThirdPartiesAmount) ?? 0);
                    var workedDaily = detailEntity.ContractorStatusStatementServices.SelectMany(x => x.ContractorStatusStatementServiceDailies).ToList();
                    oldServicedAmount = oldServicedAmount + workedDaily.Sum(x => x.TotalPrice);
                }
            }

        entity!.SetServicedAmount(servicedAmount);
        entity!.SetFixedAmount(fixedAmount);

        ///CostOvers
        var costOversQueries = await _mediator.Send(new GetsContractorContractCostOverForCSSQuery(
            request.ProjectId, request.ContractorId, 0, 0), ct);
        var costOvers = costOversQueries.Value?.Data?.ToList();
        if (costOvers is not null && costOvers.Any())
        {
            foreach (var costOver in costOvers)
            {
                var costOverEntity = new ContractorStatusStatementCostOver(entity!, costOver, costOver.Amount, costOver.Description);
                var costOverResponce = await _mediator.Send(new CreateContractorStatusStatementCostOverCommand(costOverEntity), ct);
                if (costOverResponce.IsFailure)
                    return Result.Failure<CreateContractorStatusStatementResponse>(costOverResponce.Error!);
            }

            var costOversAmount = entity!.ContractorStatusStatementCostOvers.Sum(x => x.Amount);
            entity!.SetCostOversAmount(costOversAmount);
        }

        ///RequestRewards
        var requestRewardQueries = await _mediator.Send(new GetsConfirmedContractorRequestRewardQuery(
            request.ProjectId, request.ContractorId, request.StartDate, request.EndDate, 0, 0), ct);
        var requestRewards = requestRewardQueries.Value?.Data?.ToList();
        if (requestRewards is not null && requestRewards.Any())
        {
            var fines = requestRewards.Where(x => x.Type == RequestRewardType.Fine).ToList();
            if (fines.Any())
            {
                foreach (var fine in fines)
                {
                    var fineEntity = new ContractorStatusStatementFine(entity!, fine, fine.RegistrationDate, fine.ConfirmedPrice);
                    var fineResponce = await _mediator.Send(new CreateContractorStatusStatementFineCommand(fineEntity), ct);
                    if (fineResponce.IsFailure)
                        return Result.Failure<CreateContractorStatusStatementResponse>(fineResponce.Error!);
                }
                var finesAmount = entity!.ContractorStatusStatementFines.Sum(x => x.ConfirmedPrice);
                entity!.SetFinesAmount(finesAmount);
            }

            var rewards = requestRewards.Where(x => x.Type == RequestRewardType.Reward).ToList();
            if (rewards.Any())
            {
                foreach (var reward in rewards)
                {
                    var rewardEntity = new ContractorStatusStatementReward(entity!, reward, reward.RegistrationDate, reward.ConfirmedPrice);
                    var rewardResponce = await _mediator.Send(new CreateContractorStatusStatementRewardCommand(rewardEntity), ct);
                    if (rewardResponce.IsFailure)
                        return Result.Failure<CreateContractorStatusStatementResponse>(rewardResponce.Error!);
                }
                var rewardsAmount = entity!.ContractorStatusStatementRewards.Sum(x => x.ConfirmedPrice);
                entity!.SetRewardsAmount(rewardsAmount);
            }

            var discounts = requestRewards.Where(x => x.Type == RequestRewardType.Discount).ToList();
            if (discounts.Any())
            {
                foreach (var discount in discounts)
                {
                    var discountEntity = new ContractorStatusStatementDiscount(entity!, discount, discount.RegistrationDate, discount.ConfirmedPrice, discount.Description);
                    var createDiscount = await _mediator.Send(new CreateContractorStatusStatementDiscountCommand(
                    entity, discount, discount.ConfirmedPrice, discount.RegistrationDate, discount.Description), ct);
                    if (createDiscount.IsFailure)
                        return Result.Failure<CreateContractorStatusStatementResponse>(createDiscount.Error!);
                }
                var discountPrice = entity!.ContractorStatusStatementDiscounts.Sum(x => x.DiscountPrice);
                entity!.SetDiscountPrice(discountPrice);
            }
        }

        ///Products
        var requestGoodsSupplyQuery = await GetsRequestGoodsSupplyProductByContractorIdExecute(
            request.StartDate, request.EndDate, request.ContractorId, request.ProjectId, ct);
        var requestGoodsSupplyDetails = requestGoodsSupplyQuery.Value?.SelectMany(oo => oo.RequestGoodsSupplyDetails).ToList();
        if (requestGoodsSupplyDetails is not null && requestGoodsSupplyDetails.Count > 0)
        {
            var productDetails = new List<ContractorStatusStatementProduct>();
            foreach (var detail in requestGoodsSupplyDetails)
            {
                var supplyCount = detail.RequestGoodsSupply.RequestGoodsSupplyDetails.Count;
                var price = detail.RequestGoodsSupplyProduct!.TotalPrice ?? 0;
                var otherPrice = (detail.RequestGoodsSupply!.OtherPrice / supplyCount) ?? 0;
                var transferPrice = (detail.RequestGoodsSupply!.TransferPrice / supplyCount) ?? 0;
                var discountByNum = detail.DiscountByNumber ?? 0;
                var taxNumber = detail.TaxNumber ?? 0;
                var totalPrice = ((price + otherPrice + transferPrice + taxNumber) - discountByNum);

                productDetails.Add(new ContractorStatusStatementProduct(
                    entity!,
                    detail,
                    detail.ProductId,
                    detail.RequestedCount,
                    detail.RequestedCount,
                    detail.Created,
                    detail.RequestGoodsSupply.Type == GoodsSupplyType.PurchaseForContractor ? true : false,
                    price,
                    otherPrice,
                    transferPrice,
                    discountByNum,
                    taxNumber,
                    totalPrice,
                    detail.CustomerInvoiceNumber));
            }

            if (productDetails.Any())
                foreach (var detail in productDetails)
                {
                    var product = await _mediator.Send(new CreateContractorStatusStatementProductCommand(detail), ct);
                    if (product.IsFailure)
                        return Result.Failure<CreateContractorStatusStatementResponse>(product.Error!);
                }

            var productsAmount = productDetails.Where(x => !x.IsPurchaseForContractor).Sum(x => x.TotalPrice) ?? 0;
            entity!.SetProductsAmount(productsAmount);

            var forContractorProductsAmount = productDetails.Where(x => x.IsPurchaseForContractor).Sum(x => x.TotalPrice) ?? 0;
            entity.SetForContractorProductsAmount(forContractorProductsAmount);
        }

        if (request.Discounts is not null && request.Discounts.Count > 0)
        {
            foreach (var discount in request.Discounts)
            {
                var requestReward = await _mediator.Send(new GetRequestRewardByIdQuery(discount.RequestRewardId), ct);
                var createDiscount = await _mediator.Send(new CreateContractorStatusStatementDiscountCommand(
                    entity, requestReward.Value, discount.DiscountPrice, discount.RegistrationDate, discount.Description), ct);
                if (createDiscount.IsFailure)
                    return Result.Failure<CreateContractorStatusStatementResponse>(createDiscount.Error!);
            }

            var discountPrice = entity.ContractorStatusStatementDiscounts.Sum(x => x.DiscountPrice);
            entity!.SetDiscountPrice(discountPrice);
        }

        if (contractor!.PreferentialReferenceCode is not null)
        {
            var preferentialProject = await _mediator.Send(new GetFilteredPreferentialsQuery(
                null, null, null, null, null, [project.PreferentialReferenceCode, contractor.PreferentialReferenceCode!.Value], 1, 10), ct);
            if (preferentialProject is not null && preferentialProject.Value is not null && preferentialProject.Value.Data is not null && preferentialProject.Value!.Data.HasAny())
            {
                var proId = preferentialProject.Value.Data!.FirstOrDefault(x => x.ReferenceCode == project.PreferentialReferenceCode)!.Id;
                var therId = contractor.Id;
                var ressss = await _mediator.Send(new GetFilteredPaymentOrdersSummaryQuery(therId, proId, 0, 0), ct);
                if (ressss.Value is not null && ressss.Value.Data is not null)
                    entity.SetPaymentedAmount(ressss.Value!.Data?.Sum(x => x.FilledAmount));
            }
        }

        //entity!.SetTotalAdvancePaymentAmount(contractorContracts?.Sum(x => x.AdvancePaymentAmount) ?? 0);
        //entity.SetTotalPercentageAdvancePayment((entity.TotalAdvancePaymentAmount / entity.FinalTotalAmount) * 100);

        entity!.SetFinalTotalAmount(oldFixedAmount, oldServicedAmount);

        if (contractorContracts is not null && contractorContracts.Count > 0)
        {
            entity.SetTotalPercentageDoingJobWell((contractorContracts?.Sum(x => x.PercentageDoingJobWell) / contractorContracts?.Count) ?? 0);
            entity.SetTotalDoingJobWellAmount((contractorContracts?.Sum(x => x.DoingJobWellAmount) / contractorContracts?.Count) ?? 0);
            entity.SetTotalDailyLatenessPenalty((contractorContracts?.Sum(x => x.DailyLatenessPenalty) / contractorContracts?.Count) ?? 0);
            entity.SetTotalWorkDonePercent((contractorContracts?.Sum(x => x.WorkDonePercent) / contractorContracts?.Count) ?? 0);
            entity.SetTotalWorkDeliveryPercent((contractorContracts?.Sum(x => x.WorkDeliveryPercent) / contractorContracts?.Count) ?? 0);
            entity.SetTotalWorkCompletionPercent((contractorContracts?.Sum(x => x.WorkCompletionPercent) / contractorContracts?.Count) ?? 0);
        }
        entity.SetThirdPartiesAmount(statusStatementServices.Sum(x => x.ThirdPartiesAmount) ?? 0);

        //لطیفی بهم گفت با مسئولیت من پیش پرداخت رو بزارین اینجا باز باشه تاریخ 25 ام آذر ماه
        var pishPardakht = contractHeaders.SelectMany(x => x.ContractorContracts).ToList().Sum(x => x.AdvancePaymentAmount ?? 0);

        if (entity.FinalTotalAmount == 0 && entity.TotalPercentageDoingJobWell == 0 &&
            entity.TotalDoingJobWellAmount == 0 && entity.TotalAdvancePaymentAmount == 0 &&
            entity.TotalPercentageAdvancePayment == 0 && entity.TotalDailyLatenessPenalty == 0 &&
            entity.TotalWorkDonePercent == 0 && entity.TotalWorkDeliveryPercent == 0 &&
            entity.TotalWorkCompletionPercent == 0 && entity.ProductsAmount == 0 &&
            entity.FinesAmount == 0 && entity.RewardsAmount == 0 && entity.ThirdPartiesAmount == 0 && pishPardakht == 0)
            return Result.Failure<CreateContractorStatusStatementResponse>(CSSErrors.AllIsZiro);

        entity!.SetCanPayableAmount();
        entity!.SetPayableAmount(request.CreatorConfirmedAmount ?? 0);

        if (entity.CanPayableAmount + pishPardakht < entity.PayableAmount)
            return Result.Failure<CreateContractorStatusStatementResponse>(CSSErrors.CanPayableAmount);
        entity!.SetRemainingAmount();

        var response = await _mediator.Send(new CreateContractorStatusStatementCommand(entity), ct);
        if (response.IsFailure)
            return Result.Failure<CreateContractorStatusStatementResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateContractorStatusStatementResponse(response.Value!.Id);
    }

    public async Task<Result<UpdateContractorStatusStatementResponse?>> UpdateContractorStatusStatement(
        UpdateContractorStatusStatementRequest request, CT ct)
    {
        var result = await GetContractorStatusStatementExecute(request.Id, IncludeType.Update, ct);
        if (result.IsBad()) return result.Failure<UpdateContractorStatusStatementResponse>()!;
        var value = result.Value!;

        if (ProjectStatusRules.NotAllowed.Contains(value.Project!.Status))
            return Result.Failure<UpdateContractorStatusStatementResponse>(ProjectErrors.NotAllowed);

        if (!CSSStatusRules.AllowForUpdate.Any(x => x.Equals(value.Status)))
            return Result.Failure<UpdateContractorStatusStatementResponse>(CSSErrors.InValidStatusForUpdate);

        decimal oldServicedAmount = 0;
        decimal servicedAmount = 0;
        decimal oldFixedAmount = 0;
        decimal fixedAmount = 0;


        var fixIds = request.FixContracts?.Select(x => x.Id).ToList();
        var fixContracts = fixIds is null || fixIds.Count < 1 ? null : value.ContractorStatusStatementDetails.Where(x => fixIds.Contains(x.Id)).ToList();
        fixedAmount = value.ContractorStatusStatementDetails.Where(x => x.FixedContractPctAmount > 0)
            .Sum(x => x.FixedContractPctAmount ?? 0);
        oldFixedAmount = value.ContractorStatusStatementDetails.Where(x => x.FixedContractPctAmount > 0)
            .Sum(x => x.TotalAmount ?? 0);

        fixedAmount = value.ContractorStatusStatementDetails.Where(x => x.FixedContractPctAmount > 0)
            .Sum(x => x.FixedContractPctAmount ?? 0);
        if (request.FixContracts is not null && request.FixContracts.Count > 0)
        {
            foreach (var contract in fixContracts)
            {
                var fixContract = request.FixContracts.Where(x => x.Id == contract.Id).FirstOrDefault();
                if (fixContract is not null)
                {
                    contract.SetFixedContractPct(fixContract.FixedContractPct ?? 100, fixContract.FixedContractPctDesc);
                    await _cssDetailRepository.Update(contract);
                }
            }
        }
        var services = value.ContractorStatusStatementDetails.SelectMany(x => x.ContractorStatusStatementServices).ToList();
        var dailyServices = services.SelectMany(x => x.ContractorStatusStatementServiceDailies).ToList();

        servicedAmount = (services.Sum(x => x.ThirdPartiesAmount) ?? 0);
        var workedDaily = services.SelectMany(x => x.ContractorStatusStatementServiceDailies).ToList();
        oldServicedAmount = workedDaily.Sum(x => x.TotalPrice);
        if (request.DailyServices is not null && request.DailyServices.Count > 0)
        {
            foreach (var item in request.DailyServices)
            {
                var dailyService = dailyServices.Where(x => x.Id == item.Id).FirstOrDefault();
                if (dailyService is not null)
                {
                    dailyService.SetAcceptablePercentage(item.AcceptablePercentage);
                    dailyService.SetAcceptableDescription(item.AcceptableDescription);
                    dailyService.SetAcceptableAmount();
                    dailyService.ResetManagement();
                    dailyService.ResetProjectManagement();
                    await _cssServiceDailyRepository.Update(dailyService);
                }
            }

            foreach (var service in services)
            {
                service.SetThirdPartiesAmount(service.ContractorStatusStatementServiceDailies.Sum(x => x.AcceptableAmount));
                await _cssServiceRepository.Update(service);
            }

        }

        value!.SetServicedAmount(servicedAmount);
        value!.SetFixedAmount(fixedAmount);

        value!.SetFinalTotalAmount(oldFixedAmount, oldServicedAmount);
        value!.SetCanPayableAmount();
        value!.SetPayableAmount(request.UpdatorConfirmedAmount ?? 0);
        if (value.CanPayableAmount < value.PayableAmount)
            return Result.Failure<UpdateContractorStatusStatementResponse>(CSSErrors.CanPayableAmount);
        value!.SetRemainingAmount();

        if (value.ContractorStatusStatementDiscounts != null &&
            request.Discounts != null &&
            request.Discounts.Count <= value.ContractorStatusStatementDiscounts.Count)
        {
            var entities = await GetsContractorStatusStatementDiscountByIdExecute(new GetsContractorStatusStatementDiscountByIdRequest(request.Id,
                    0,
                    0), ct);
            if (entities.IsBad())
                return entities.Failure<UpdateContractorStatusStatementResponse>()!;

            foreach (var entity in entities.Value!.Data!)
            {
                var discountPrice = request.Discounts.FirstOrDefault(x => x.Id == entity.Id)!.DiscountPrice;
                entity.SetDiscountPrice(discountPrice);
            }
        }

        var changesRes = await _mediator.Send(new UpdateContractorStatusStatementCommand(value,
            request.UpdatorConfirmedAmount, request.Description, request.Urls), ct);
        if (changesRes.IsBad()) return changesRes.Failure<UpdateContractorStatusStatementResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateContractorStatusStatementResponse(changesRes.Value!.Id);
    }

    public async Task<Result<ContractorStatusStatementDraftCreatorResponse?>> ContractorStatusStatementDraftCreator(
        ContractorStatusStatementDraftCreatorRequest request, CT ct)
    {
        var response = new ContractorStatusStatementDraftCreatorResponse();
        var headersRes = await GetsDraftableContractorContractHeader(new(request.ContractorId, request.ProjectId, 0, 0), ct);
        if (headersRes.IsBad())
            return headersRes.Failure<ContractorStatusStatementDraftCreatorResponse>()!;
        response.ContractorContractHeaders = headersRes.Value!.Data!;
        var firstHeader = response.ContractorContractHeaders.FirstOrDefault()!;

        var getServcesTotalPrice = await _mediator.Send(new GetsTotalPriceDailyContractorServiceQuery(
            request.ProjectId, request.ContractorId, request.StartDate, request.EndDate), ct);
        if (getServcesTotalPrice.IsFailure)
            return Result.Failure<ContractorStatusStatementDraftCreatorResponse>(getServcesTotalPrice.Error!);
        response.WorkedContractorContractPrice = getServcesTotalPrice.Value;

        var productDraftCreator = await GoodsSupplyDraftCreator(request, ct);
        if (productDraftCreator.Value is not null)
            response.Product = productDraftCreator.Value!;

        var costOverDraftCreator = await CostOverDraftCreator(request.ContractorId, request.ProjectId, ct);
        if (costOverDraftCreator.Value is not null)
            response.CostOver = costOverDraftCreator.Value!;

        var fineAndRewardDraftCreator = await FineAndRewardDraftCreator(request, ct);
        if (fineAndRewardDraftCreator.Value is not null)
        {
            if (fineAndRewardDraftCreator.Value.Item1 is not null)
                response.Fine = fineAndRewardDraftCreator.Value.Item1;
            if (fineAndRewardDraftCreator.Value.Item2 is not null)
                response.Reward = fineAndRewardDraftCreator.Value.Item2;
        }

        if (firstHeader.ProjectPreferentialCode is not null && firstHeader.ContractorPreferentialCode is not null)
        {
            var preferentialProject = await _mediator.Send(new GetFilteredPreferentialsQuery(
                null, null, null, null, null, [firstHeader.ProjectPreferentialCode!.Value, firstHeader.ContractorPreferentialCode!.Value], 1, 10), ct);
            if (preferentialProject.Value != null)
            {
                if (preferentialProject.Value!.Data.HasAny())
                {
                    var proId = preferentialProject.Value.Data!.FirstOrDefault(x => x.ReferenceCode == firstHeader.ProjectPreferentialCode)!.Id;
                    var therId = firstHeader.ContractorId;
                    var ressss = await _mediator.Send(new GetFilteredPaymentOrdersSummaryQuery(therId, proId, 0, 0), ct);
                    if (ressss.Value is not null && ressss.Value.Data is not null)
                        response.PaymentOrdersSummaries = ressss.Value!.Data.Adapt<List<PaymentOrdersSummaryModel>>();
                    response.Paymented = response.PaymentOrdersSummaries?.Sum(x => x.FilledAmount);
                }
            }
        }

        var responseStatusStatements = await GetsDraftableContractorStatusStatementExecute(
            request.ContractorId, request.ProjectId, 0, 0, ct);
        var statusStatements = responseStatusStatements.Value?.Data;
        if (statusStatements is not null)
        {
            var haveConfirmed = statusStatements.Where(y => y.Status == CSSStatus.ProjectManagerConfirmed || y.Status == CSSStatus.ManagementConfirmed).ToList();
            var sendToPayment = statusStatements.Where(y => y.Status == CSSStatus.PaymentConfirmation).ToList();
            var paymented = statusStatements.Where(x => x.Status == CSSStatus.Paid).ToList();

            response.HaveConfirmed = haveConfirmed.Any();
            response.PendToPayment = sendToPayment.Sum(x => x.ConfirmedPrice);

            response.PaidContractorStatusStatements = paymented.Adapt<List<PaidContractorStatusStatementModel>>();
            response.PaidContractorStatus = response.PaidContractorStatusStatements.Sum(x => x.ConfirmedPrice);
        }

        var discounts = await _requestRewardRepository.GetDiscounts(request.ProjectId, request.ContractorId, ct);
        List<ContractorContractDiscountsDraftModel> discountModels = [];
        foreach (var item in discounts)
        {
            discountModels.Add(new ContractorContractDiscountsDraftModel
            {
                RequestRewardId = item.Id,
                Created = item.Created.ToString(),
                Description = item.Description,
                ManagerDescription = item.ManagerDescription,
                OfferedPrice = item.OfferedPrice,
                Price = item.ConfirmedPrice,
                Urls = item.RequestRewardDocuments.Select(x => x.Url).ToList(),
            });
        }

        ContractorContractDiscountDraftModel discountModel = new ContractorContractDiscountDraftModel
        {
            Discounts = discountModels
        };
        var contractorContracts = response.ContractorContractHeaders.SelectMany(x => x.ContractorContracts).Where(x => x.ContractorContractTypeId == ContractorContractType.Fixed).ToList();
        response.FixContractorContractAmounts = contractorContracts.Sum(x => x.TotalAmount);
        response.Discount = discountModel;
        response.StartDate = request.StartDate;
        response.EndDate = request.EndDate;
        response.ContractorId = request.ContractorId;
        response.Contractor = firstHeader.Contractor;
        response.CostCenterId = firstHeader.CostCenterId!.Value;
        response.CostCenterName = firstHeader.CostCenterName;
        response.CostCenterCode = firstHeader.CostCenterCode;
        response.ProjectId = firstHeader.ProjectId!.Value;
        response.ProjectName = firstHeader.ProjectName;
        response.ProjectCode = firstHeader.ProjectCode;
        response.CurrencyId = firstHeader.CurrencyId;
        response.Currency = firstHeader.Currency;
        response.ProjectManagerId = firstHeader.ProjectManagerId;
        response.ProjectManager = firstHeader.ProjectManager;
        return response;
    }

    public async Task<Result<GetsDraftableContractorContractHeaderResponse?>> GetsDraftableContractorContractHeader(
        GetsDraftableContractorContractHeaderRequest request, CT ct)
    {
        var getProject = await _mediator.Send(new GetProjectModelByIdQuery(request.ProjectId), ct);
        if (getProject.IsFailure)
            return Result.Failure<GetsDraftableContractorContractHeaderResponse>(getProject.Error!);
        var project = getProject.Value!;

        if (ProjectStatusRules.NotAllowed.Contains(project.Status))
            return Result.Failure<GetsDraftableContractorContractHeaderResponse>(ProjectErrors.NotAllowed);

        var responses = await _mediator.Send(new GetsDraftableContractorContractHeaderQuery(
            request.ContractorId,
            request.ProjectId,
            request.PageIndex,
            request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsDraftableContractorContractHeaderResponse>(responses.Error!);
        var values = responses.Value!.Data!;

        var currencyId = values!.Where(x => x.CurrencyId is not null && x.CurrencyId > 0).FirstOrDefault()!.CurrencyId!.Value;
        var currencies = await WebServicesLogic.CurrenciesDataReceiver([currencyId], _mediator, ct);

        var creatorIds = values?.Where(x => x.CreatorId is not null && x.CreatorId > 0).Select(c => c.CreatorId!.Value).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var contractorIds = values!.Select(oo => oo.ContractorId).Distinct().ToList();
        if(project.ProjectManagerId.HasValue)
            contractorIds.Add(project.ProjectManagerId.Value);

        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);
        values!.ForEach(header =>
        {
            header.CostCenterId = project?.CostCenterId;
            header.CostCenterName = project?.CostCenterName;
            header.CostCenterCode = project?.CostCenterCode;
            header.ProjectId = project?.Id;
            header.ProjectManagerId = project?.ProjectManagerId;
            header.ProjectName = project?.ProjectName;
            header.ProjectCode = project?.ProjectCode;
            header.ProjectPreferentialCode = project?.PreferentialReferenceCode;

            if (header.CurrencyId is not null)
                header.Currency = currencies?.FirstOrDefault()?.Name;

            if (header.CreatorId is not null)
                header.Creator = creators?.Where(c => c.UserId == header.CreatorId).FirstOrDefault()?.FullName;

            header.Contractor = contractors?.Where(c => c?.Id == header.ContractorId).FirstOrDefault()?.FullName!;
            header.ContractorPreferentialCode = contractors?.Where(c => c?.Id == header.ContractorId).FirstOrDefault()?.PreferentialReferenceCode;
            header.ProjectManager = contractors?.Where(c => c?.Id == header.ProjectManagerId).FirstOrDefault()?.FullName!;
        });

        return new GetsDraftableContractorContractHeaderResponse(values, responses.Value!.RowCount!);
    }

    public async Task<Result<ContractorStatusStatementDiscountOperationsResponse?>> ContractorStatusStatementDiscountOperations(
        ContractorStatusStatementDiscountOperationsRequest request, CT ct)
    {
        var result = await _cssRepository.GetCSSForDiscount(request.ContractorStatusStatementId, ct);
        if (result is null) return Result.Failure<ContractorStatusStatementDiscountOperationsResponse>(CSSErrors.ContractorStatusStatementWithIdNotFound);
        var value = result;

        if (value.Status == CSSStatus.PaymentConfirmation || value.Status == CSSStatus.Paid)
            return Result.Failure<ContractorStatusStatementDiscountOperationsResponse>(CSSErrors.CantAddDiscount);

        foreach (var item in request.ContractorStatusStatementDiscounts)
            if (item.Id is not null && item.IsDeleted)
            {
                var response = await _mediator.Send(new DeleteContractorStatusStatementDiscountCommand(item.Id!.Value), ct);
                if (response.IsBad()) return response.Failure<ContractorStatusStatementDiscountOperationsResponse>()!;
            }
            else if (item.Id is not null && !item.IsDeleted)
            {
                var response = await _mediator.Send(new UpdateContractorStatusStatementDiscountCommand(
                    item.Id!.Value, item.DiscountPrice!.Value!, item.RegistrationDate, item.Description), ct);
                if (response.IsBad()) return response.Failure<ContractorStatusStatementDiscountOperationsResponse>()!;
            }
            else if (item.Id is null && !item.IsDeleted)
            {
                if (item.RequestRewardId != null)
                {
                    var requestReward = await _mediator.Send(new GetRequestRewardByIdQuery(item.RequestRewardId!.Value!), ct);
                    if (requestReward.IsBad())
                        return requestReward.Failure<ContractorStatusStatementDiscountOperationsResponse>()!;
                    if (item.DiscountPrice is null)
                        return Result.Failure<ContractorStatusStatementDiscountOperationsResponse>(CSSErrors.DiscountIsEmpty);
                    var response = await _mediator.Send(new CreateContractorStatusStatementDiscountCommand(
                    value, requestReward.Value, item.DiscountPrice!.Value!, item.RegistrationDate, item.Description), ct);
                    if (response.IsBad()) return response.Failure<ContractorStatusStatementDiscountOperationsResponse>()!;
                }
                else
                {
                    var response = await _mediator.Send(new CreateContractorStatusStatementDiscountCommand(
                    value, null, item.DiscountPrice!.Value!, item.RegistrationDate, item.Description), ct);
                    if (response.IsBad()) return response.Failure<ContractorStatusStatementDiscountOperationsResponse>()!;
                }
            }

        await _unitOfWork.CommitAsync(ct);
        return new ContractorStatusStatementDiscountOperationsResponse(true);
    }

    public async Task<Result<DeleteContractorStatusStatementResponse?>> DeleteContractorStatusStatement(
        DeleteContractorStatusStatementRequest request, CT ct)
    {
        var result = await GetContractorStatusStatementExecute(request.Id, IncludeType.Delete, ct);
        if (result.IsBad()) return result.Failure<DeleteContractorStatusStatementResponse>()!;
        var value = result.Value!;

        if (!CSSStatusRules.AllowForDelete.Any(x => x.Equals(value.Status)))
            return Result.Failure<DeleteContractorStatusStatementResponse>(CSSErrors.InValidStatusForDelete);

        var response = await _mediator.Send(new DeleteContractorStatusStatementCommand(value), ct);
        if (response.IsBad()) return response.Failure<DeleteContractorStatusStatementResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteContractorStatusStatementResponse(true);
    }

    public async Task<Result<GetCStatementSContractsResponse>> GetCStatementSContracts(
         GetCStatementSContractsRequest request, CT ct)
    {
        var result = await GetCStatementSContractsExecute(request, ct);
        if (result.IsBad()) return result.Failure<GetCStatementSContractsResponse>()!;
        var values = result.Value!.Where(x => x.DailyServices!.Any()).SelectMany(x => x.DailyServices!);

        var measureIds = values.Where(x => x.ProjectOperationMeasureId.HasValue && x.ServiceInfoMeasureId.HasValue)
            .SelectMany(x => new[] { x.ProjectOperationMeasureId!.Value, x.ServiceInfoMeasureId!.Value }).Where(x => x > 0).Distinct().ToList();
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var creatorIds = values.NullListed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var contractorIds = result.Value!.Listed(x => x.ContractorId);
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        foreach (var item in result.Value!)
        {
            if (contractors.HasAny())
                item.Contractor = contractors?.FirstOrDefault(m => m!.Id == item.ContractorId)?.FullName;

            item.DailyServices!.ForEach(x =>
            {
                x.ServiceInfoMeasurement = measures?.FirstOrDefault(m => m.Id == x.ServiceInfoMeasureId)?.Name;
                x.ProjectOperationMeasurement = measures?.FirstOrDefault(m => m.Id == x.ProjectOperationMeasureId)?.Name;
                x.Creator = creators?.FirstOrDefault(m => m.UserId == x.CreatorId)?.FullName;
            });
        }

        return new GetCStatementSContractsResponse(result.Value!);
    }

    public async Task<Result<GetCStatementFContractsResponse>> GetCStatementFContracts(
         GetCStatementFContractsRequest request, CT ct)
    {
        var result = await GetCStatementFContractsExecute(request, ct);
        if (result.IsBad()) return result.Failure<GetCStatementFContractsResponse>()!;
        var values = result.Value!.Where(x => x.DailyServices!.Any()).SelectMany(x => x.DailyServices!);

        var measureIds = values.Where(x => x.ProjectOperationMeasureId.HasValue && x.ServiceInfoMeasureId.HasValue)
            .SelectMany(x => new[] { x.ProjectOperationMeasureId!.Value, x.ServiceInfoMeasureId!.Value }).Where(x => x > 0).Distinct().ToList();
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var creatorIds = values.NullListed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var contractorIds = result.Value!.Listed(x => x.ContractorId);
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        foreach (var item in result.Value!)
        {
            if (contractors.HasAny())
                item.Contractor = contractors?.FirstOrDefault(m => m!.Id == item.ContractorId)?.FullName;

            item.DailyServices!.ForEach(x =>
            {
                x.ServiceInfoMeasurement = measures?.FirstOrDefault(m => m.Id == x.ServiceInfoMeasureId)?.Name;
                x.ProjectOperationMeasurement = measures?.FirstOrDefault(m => m.Id == x.ProjectOperationMeasureId)?.Name;
                x.Creator = creators?.FirstOrDefault(m => m.UserId == x.CreatorId)?.FullName;
            });
        }

        return new GetCStatementFContractsResponse(result.Value!);
    }

    public async Task<Result<ContractorStatusStatementCodeCreatorResponse?>> ContractorStatusStatementCodeCreator(
        ContractorStatusStatementCodeCreatorRequest request, CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<ContractorStatusStatementCodeCreatorResponse>(GlobalErrors.InvalidCompany);

        var result = await _mediator.Send(new ContractorStatusStatementCodeCreatorCommand(companyId), ct);
        if (result.IsBad()) return result.Failure<ContractorStatusStatementCodeCreatorResponse>()!;

        return new ContractorStatusStatementCodeCreatorResponse(result.Value);
    }

    public async Task<Result<ContractorStatusStatementStatusChangerResponse?>> ContractorStatusStatementStatusChanger(
        ContractorStatusStatementStatusChangerRequest request, CT ct)
    {
        var result = await StatusChanger(request, ct);
        if (result.IsBad()) return result.Failure<ContractorStatusStatementStatusChangerResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new ContractorStatusStatementStatusChangerResponse(result.Value!.Id);
    }

    public async Task<Result<CSSGroupStatusChangerResponse?>> CSSGroupStatusChanger(
        CSSGroupStatusChangerRequest request, CT ct)
    {
        foreach (var item in request.Items)
        {
            var newReq = new ContractorStatusStatementStatusChangerRequest(
                item.Id,
                request.Status,
                null,
                null,
                null,
                item.ConfirmedAmount,
                null,
                null,
                null,
                null,
                item.Description,
                null,
                null,
                null,
                null,
                null);
            var result = await StatusChanger(newReq, ct);
            if (result.IsBad()) return result.Failure<CSSGroupStatusChangerResponse>()!;
        }

        await _unitOfWork.CommitAsync(ct);
        return new CSSGroupStatusChangerResponse(true);
    }

    public async Task<Result<GetContractorStatusStatementByIdResponse?>> GetContractorStatusStatementById(
        GetContractorStatusStatementByIdRequest request, CT ct)
    {
        var result = await GetModeledContractorStatusStatementByIdExecute(request.Id, ct);
        if (result.IsBad()) return result.Failure<GetContractorStatusStatementByIdResponse>()!;
        var value = result.Value!;
        {
            var contractor = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(
                [value.ContractorId!.Value], null, null, _mediator, ct);
            value.Contractor = contractor?.FirstOrDefault()?.FullName;
            value.ContractorPreferentialCode = contractor?.FirstOrDefault()?.PreferentialReferenceCode;
        }

        if (value.CurrencyId is not null && value.CurrencyId > 0)
        {
            var currency = await WebServicesLogic.CurrenciesDataReceiver([value.CurrencyId.Value], _mediator, ct);
            value.Currency = currency?.FirstOrDefault()?.Name;
        }

        if (value.Products?.Count > 0)
        {
            var productIds = value.Products.NullListed(x => x.ProductId);
            var productsInfo = await _productRepo.GetProductByIds(productIds, ct);
            foreach (var item in value.Products)
            {
                var product = productsInfo?.FirstOrDefault(x => x.Id == item.ProductId);
                item.ProductName = product?.Name;
                item.ProductGroupId = product?.Group.Id;
                item.MeasureUnit = product?.Group.Measure;
                item.RequestNumber = product?.Id;
            }
        }

        if (value.ProjectPreferentialCode is not null && value.ContractorPreferentialCode is not null)
        {
            var preferentialProject = await _mediator.Send(new GetFilteredPreferentialsQuery(
                null, null, null, null, null, [value.ProjectPreferentialCode!.Value, value.ContractorPreferentialCode!.Value], 1, 10), ct);
            if (!preferentialProject.IsBad() && preferentialProject.Value!.Data.HasAny())
            {
                var proId = preferentialProject.Value.Data!.FirstOrDefault(x => x.ReferenceCode == value.ProjectPreferentialCode)!.Id;
                var therId = value.ContractorId;
                var ressss = await _mediator.Send(new GetFilteredPaymentOrdersSummaryQuery(therId!.Value, proId, 0, 0), ct);
                if (ressss.Value is not null && ressss.Value.Data is not null)
                    value.PaymentOrdersSummaries = ressss.Value!.Data.Adapt<List<PaymentOrdersSummaryModel>>();
                value.Paymented = value.PaymentOrdersSummaries?.Sum(x => x.FilledAmount);
            }
        }

        var responses = await GetsPaidContractorStatusStatementExecute(
            value.ContractorId!.Value, value.ProjectId!.Value, ct);
        var values = responses.Value?.Data;
        if (values is not null && values.Any())
        {
            value.PaidContractorStatus = values.Sum(x => x.ConfirmedPrice);
            value.PaidContractorStatusStatements = values;
        }

        if (value.CostOvers is not null && value.CostOvers.Any())
        {
            var ids = value.CostOvers.Listed(x => x.ProjectOperationsDetailServiceId);
            var infoResponse = await _mediator.Send(new GetInfoByContractorServiceIdQuery(ids), ct);
            var infos = infoResponse.Value?.Data;

            if (infos is not null && infos.Any())
                foreach (var costOver in value.CostOvers)
                {
                    var match = infos.FirstOrDefault(x => x.Id == costOver.ProjectOperationsDetailServiceId);
                    if (match is not null)
                    {
                        costOver.ServiceId = match.ServiceId;
                        costOver.ServiceName = match.ServiceName;
                        costOver.ProjectOperationId = match.ProjectOperationId;
                        costOver.ProjectOperationName = match.ProjectOperationName;
                        costOver.ProjectOperationsDetailId = match.ProjectOperationDetailId;
                        costOver.ProjectOperationsDetailName = match.ProjectOperationDetailName;
                    }
                }
        }

        var paymentConfirmatio = await PaymentConfirmationCSSAmountsExecute(
            value.ContractorId!.Value, value.ProjectId!.Value, ct);
        value.PendToPayment = paymentConfirmatio.Value;
        return value;
    }

    public async Task<Result<GetFilteredContractorStatusStatementResponse?>> GetFilteredContractorStatusStatement(
        GetFilteredContractorStatusStatementRequest request, CT ct)
    {
        List<long>? filteredIds = null;
        if (!string.IsNullOrEmpty(request.ContractorFilter))
        {
            var responseContractorIds = await GetsContractorStatusStatementContractorIdsExecute(request, ct);
            if (responseContractorIds.IsFailure)
                return Result.Failure<GetFilteredContractorStatusStatementResponse>(responseContractorIds.Error!);
            var contractorFilteredIds = responseContractorIds.Value!.Data!;
            var contractorFiltered = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorFilteredIds, request.ContractorFilter, null, _mediator, ct);
            if (contractorFiltered is not null)
                filteredIds = contractorFiltered.Where(x => x is not null).Select(x => x!.Id).Distinct().ToList();
        }

        var result = await GetFilteredContractorStatusStatementExecute(request, null, filteredIds, ct);
        if (result.IsBad()) return result.Failure<GetFilteredContractorStatusStatementResponse>()!;
        var values = result.Value!.Data!;

        var currencyIds = values?.Where(x => x.CurrencyId is not null && x.CurrencyId > 0).Select(x => x.CurrencyId!.Value).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var creatorIds = values?.Where(x => x.CreatorId is not null && x.CreatorId > 0).Select(c => c.CreatorId!.Value).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var contractorIds = values?.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => x.ContractorId!.Value).ToList();
        contractorIds!.AddRange(values?.Where(x => x.ProjectManagerId != null && x.ProjectManagerId > 0).Select(x => x.ProjectManagerId!.Value).ToList()!);
        var metaIds = contractorIds.Distinct().ToList();
        List<UserModel?>? contractors = [];
        if (metaIds is not null && metaIds.Count > 0)
            contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(metaIds, null, null, _mediator, ct);

        List<FilteredPreferentialModel>? prefrentials = [];
        if (contractors is not null && contractors.Count > 0)
        {
            var prefrentialGuids = contractors.NullListed(x => x!.PreferentialReferenceCode!);
            if (prefrentialGuids is not null && prefrentialGuids.Count > 0)
            {
                var getPreferentialResponse = await _mediator.Send(new GetActiveFilteredPreferentialsQuery(
                null, null, null, null, null, prefrentialGuids, 1, prefrentialGuids.Count), ct);
                prefrentials = getPreferentialResponse.Value?.Data;
            }
        }

        if (values is not null)
            foreach (var data in values)
            {
                var value = values?.FirstOrDefault(x => x.Id == data.Id);
                data.ProjectManager = contractors?.Where(x => x is not null && x.Id.Equals(data.ProjectManagerId)).FirstOrDefault()?.FullName;
                data.CurrencyId = value?.CurrencyId;
                data.Creator = creators?.Where(m => m.UserId == data.CreatorId).FirstOrDefault()?.FullName;
                data.Currency = currencies?.FirstOrDefault(x => x.Id == data.CurrencyId)?.Name;
                data.Contractor = contractors?.FirstOrDefault(x => x?.Id == data.ContractorId)?.FullName;
                data.ContractorPreferentialReferenceCode = contractors?.FirstOrDefault(x => x?.Id == data.ContractorId)?.PreferentialReferenceCode;
                data.Nickname = contractors?.FirstOrDefault(x => x?.Id == data.ContractorId)?.Nickname;
                data.PrefrentialId = prefrentials?.FirstOrDefault(x => x?.ReferenceCode == data.ContractorPreferentialReferenceCode)?.Id;
                data.StartDateShamsi = TimeCalculator.ConvertToShamsi(value?.StartDate);
                data.EndDateShamsi = TimeCalculator.ConvertToShamsi(value?.EndDate);
            }

        return new GetFilteredContractorStatusStatementResponse(values!, result.Value!.RowCount!);
    }

    public async Task<Result<GetsContractorStatusStatementDiscountByIdResponse?>> GetsContractorStatusStatementDiscountById(
        GetsContractorStatusStatementDiscountByIdRequest request, CT ct)
    {
        var result = await GetsContractorStatusStatementDiscountByIdExecute(request, ct);
        if (result.IsBad()) return result.Failure<GetsContractorStatusStatementDiscountByIdResponse>()!;

        return new GetsContractorStatusStatementDiscountByIdResponse(
            result.Value!.Data!.Adapt<List<GetsContractorStatusStatementDiscountByIdModel>>(), result.Value!.RowCount!);
    }

    public async Task<Result<GetsContractorStatusStatementHistoryResponse?>> GetsContractorStatusStatementHistory(
        GetsContractorStatusStatementHistoryRequest request, CT ct)
    {
        var result = await GetsContractorStatusStatementHistoryExecute(request, ct);
        if (result.IsBad()) return result.Failure<GetsContractorStatusStatementHistoryResponse>()!;
        var values = result.Value!.Data!;

        var creatorIds = values.Select(x => x.CreatorId).ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);
        foreach (var data in values)
            data.Creator = creators?.Where(m => m.UserId == data.CreatorId).FirstOrDefault()?.FullName;

        return new GetsContractorStatusStatementHistoryResponse(values, result.Value!.RowCount!);
    }

    public async Task<Result<GetContractorStatusStatementStatusResponse?>> GetContractorStatusStatementStatus(
        GetContractorStatusStatementStatusRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<CSSStatus>());
        var codes = CSSStatusRules.DoNotShow.Select(x => (int)x);
        response = response.Where(x => !codes.Contains(x.Code)).ToList();
        return new GetContractorStatusStatementStatusResponse(response);
    }

    public async Task<Result<GetsContractorStatusStatementExcelEnumResponse?>> GetsContractorStatusStatementExcelEnum(
        GetsContractorStatusStatementExcelEnumRequest request, CT ct)
    {
        return await Task.FromResult(new GetsContractorStatusStatementExcelEnumResponse(
            EnumExt.GetEnumObjectList<ContractorStatusStatementExcelEnum>()));
    }

    public async Task<Result<GetsContractorStatusStatementExcelExporterResponse?>> GetsContractorStatusStatementExcelExporter(
        GetsContractorStatusStatementExcelExporterRequest request, CT ct)
    {
        var result = await GetFilteredContractorStatusStatementExecute(
            request.Adapt<GetFilteredContractorStatusStatementRequest>(), request.Ids, null, ct);
        if (result.IsBad()) return result.Failure<GetsContractorStatusStatementExcelExporterResponse>()!;
        var values = result.Value!.Data!;

        var currencyIds = values?.Where(x => x.CurrencyId is not null && x.CurrencyId > 0).Select(x => x.CurrencyId!.Value).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var contractorIds = values?.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => (long)x.ContractorId!).Distinct().ToList();
        List<UserModel?>? contractors = [];
        if (contractorIds is not null && contractorIds.Count > 0)
            contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        var datas = values.Adapt<List<GetFilteredContractorStatusStatementModel>>();
        foreach (var data in datas)
        {
            var value = values?.FirstOrDefault(x => x.Id == data.Id);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            data.CurrencyId = value.CurrencyId;
            data.Currency = currencies?.FirstOrDefault(x => x.Id == data.CurrencyId)?.Name;
            data.Contractor = contractors?.FirstOrDefault(x => x.Id == data.ContractorId)?.FullName;
            data.Nickname = contractors?.FirstOrDefault(x => x.Id == data.ContractorId)?.Nickname;
            data.StartDateShamsi = TimeCalculator.ConvertToShamsi(value.StartDate);
            data.EndDateShamsi = TimeCalculator.ConvertToShamsi(value.EndDate);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        List<GetsContractorStatusStatementExcelExporterModel>? exporterModels = [];
        exporterModels = datas.Adapt<List<GetsContractorStatusStatementExcelExporterModel>>();
        var file = new FileContentResult(ContractorStatusStatementExcels.ContractorStatusStatementToExcel(
            exporterModels, request.ExcelFilters), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"ContractorStatusStatement-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetsContractorStatusStatementExcelExporterResponse(file);
    }

    public async Task<Result<GetsIntegratedCSSResponse?>> GetsIntegratedCSS(
        GetsIntegratedCSSRequest request, CT ct)
    {
        List<long>? filteredContractorIds = null;
        if (!string.IsNullOrEmpty(request.ContractorFilter))
        {
            var newReq = new GetFilteredContractorStatusStatementRequest()
            {
                ContractorId = null,
                CostCenterId = request.CostCenterId,
                ProjectId = request.ProjectId,
                ProjectManagerId = null,
                ContractorContractHeaderId = request.ContractorContractHeaderId,
                ContractNumber = null,
                Code = null,
                ContractorFilter = null,
                ManagerAmount = null,
                ManagerDescription = null,
                Statuses = request.Statuses,
                IsPayment = false,
                MultiPayment = null,
                IsPrimaryManagerConfirmed = null,
                IsFinalManagerConfirmed = null,
                StartDate = null,
                EndDate = null,
                FilterData = request.FilterData,
                OrderBy = null,
                PageIndex = 0,
                PageSize = 0
            };
            var responseContractorIds = await GetsContractorStatusStatementContractorIdsExecute(newReq, ct);
            if (responseContractorIds.IsFailure)
                return Result.Failure<GetsIntegratedCSSResponse>(responseContractorIds.Error!);
            var contractorFilteredIds = responseContractorIds.Value!.Data!;
            var contractorFiltered = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorFilteredIds, request.ContractorFilter, null, _mediator, ct);
            if (contractorFiltered is not null)
                filteredContractorIds = contractorFiltered.Where(x => x is not null).Select(x => x!.Id).Distinct().ToList();
        }

        var response = await GetsIntegratedCSSExecute(request, filteredContractorIds, ct);
        if (response.IsFailure)
            return Result.Failure<GetsIntegratedCSSResponse>(response.Error!);
        var values = response.Value!.Data!;

        var contractorIds = values?.Where(x => x.ContractorId != null && x.ContractorId > 0).Select(x => x.ContractorId!.Value).Distinct().ToList();
        List<ThirdPartyAlias>? contractors = [];
        if (contractorIds is not null && contractorIds.Count > 0)
            contractors = await WebServicesLogic.ThirdPartiesDataReceiver(contractorIds, _mediator, ct); // بره سراغ متا دیتا

        if (values is not null)
            foreach (var item in values)
            {
                var value = values?.FirstOrDefault(x => x.Id == item.Id);
                item.Contractor = contractors?.FirstOrDefault(x => x?.Id == item.ContractorId)?.FullName;
                item.ContractorReferenceCode = contractors?.FirstOrDefault(x => x?.Id == item.ContractorId)?.PreferentialReferenceCode;
                item.StartDateShamsi = TimeCalculator.ConvertToShamsi(value?.StartDate);
                item.EndDateShamsi = TimeCalculator.ConvertToShamsi(value?.EndDate);
            }

        var responses = values!.GroupBy(item => new
        {
            item.ContractorId
        }).Select(value => new GetsIntegratedCSSResponseModel
        {
            ContractorId = value.FirstOrDefault()!.ContractorId,
            ContractorReferenceCode = value.FirstOrDefault()!.ContractorReferenceCode,
            Contractor = value.FirstOrDefault()!.Contractor,
            PrimaryManagerConfirmedAmount = value.Sum(x => x.PrimaryManagerConfirmedAmount),
            FinalManagerConfirmedAmount = value.Sum(x => x.FinalManagerConfirmedAmount),
            ManagementApprovalAmount = value.Sum(x => x.ManagementApprovalAmount),
            ManagementConfirmedAmount = value.Sum(x => x.ManagementConfirmedAmount),
            ConfirmedPrice = value.Sum(x => x.ConfirmedPrice),
            PaymentedAmount = value.Sum(x => x.PaymentedAmount),
            LastDescription = value.FirstOrDefault()!.LastDescription,
            Description = value.FirstOrDefault()!.Description,
            Urls = value.Where(x => x.Urls is not null && x.Urls.Any()).SelectMany(x => x.Urls!).ToList(),
            ContractorStatusStatements = value.ToList()
        }).ToList();

        var requestModels = responses.Select(item => new GetPaymentOrdersTotalSummaryRequestModel
        {
            ThirdPartyPreferentialReferenceCode = item.ContractorReferenceCode,
        }).ToList();

        var responsePaymentOrders = await _mediator.Send(new GetPaymentOrdersTotalSummaryQuery(requestModels), ct);
        var paymentOrderValues = responsePaymentOrders.Value?.Data;
        if (paymentOrderValues is not null)
            foreach (var responseItem in responses)
            {
                var paymentOrderValue = paymentOrderValues.FirstOrDefault(x => x.ThirdPartyPreferentialReferenceCode == responseItem.ContractorReferenceCode);
                if (paymentOrderValue is not null)
                {
                    responseItem.Amount = paymentOrderValue.Amount;
                    responseItem.FilledAmount = paymentOrderValue.FilledAmount;
                    responseItem.RefundAmount = paymentOrderValue.RefundAmount;
                    responseItem.RemainigAmount = paymentOrderValue.RemainigAmount;
                }
            }

        var data = responses.SetPaging(request.PageIndex, request.PageSize);
        return new GetsIntegratedCSSResponse(data!, responses.Count());
    }

    public async Task<Result<GetsIntegratedCssExcelEnumResponse>> GetIntegratedCSSExcelEnum(
        GetsIntegratedCssExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetIntegratedCSSExcelEnum");
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<ContractorStatusStatementDailyServiceExcelEnum>());
        return new GetsIntegratedCssExcelEnumResponse(response);
    }

    public async Task<Result<GetCSSDailyServiceUrlsResponse>> GetCSSDailyServiceUrls(
        GetCSSDailyServiceUrlsRequest request, CT ct)
    {
        _logger.LogInformation("GetCSSDailyServiceUrls");
        var result = await GetCSSDailyServiceUrlsExecute(request, ct);
        if (result.IsBad()) return result.Failure<GetCSSDailyServiceUrlsResponse>()!;
        return new GetCSSDailyServiceUrlsResponse(result.Value!, result.Value!.Count);
    }

    public async Task<Result<GetCSSPaymentsResponse>> GetCSSPayments(
        GetCSSPaymentsRequest request, CT ct)
    {
        _logger.LogInformation("GetCSSPayments");
        var result = await GetCSSPaymentsExecute(request, ct);
        if (result.IsBad()) return result.Failure<GetCSSPaymentsResponse>()!;
        return new GetCSSPaymentsResponse(result.Value!.Data!, result.Value.RowCount);
    }

    public async Task<Result<GetCStatementFContractsEnumResponse>> GetCStatementFContractsEnum(
        GetCStatementFContractsEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetIntegratedCSSExcelEnum");
        var contract = await Task.Run(() => EnumExt.GetEnumObjectList<GetCStatementFContractsEnum>());
        var daily = await Task.Run(() => EnumExt.GetEnumObjectList<GetCStatementFContractsDailiesEnum>());
        return new GetCStatementFContractsEnumResponse(contract, daily);
    }

    public async Task<Result<GetCStatementFContractsExporterResponse>> GetCStatementFContractsExporter(
         GetCStatementFContractsExporterRequest request, CT ct)
    {
        var result = await GetCStatementFContractsExecute(request.Adapt<GetCStatementFContractsRequest>(), ct);
        if (result.IsBad()) return result.Failure<GetCStatementFContractsExporterResponse>()!;
        var values = result.Value!.Where(x => x.DailyServices!.Any()).SelectMany(x => x.DailyServices!);

        var measureIds = values.Where(x => x.ProjectOperationMeasureId.HasValue && x.ServiceInfoMeasureId.HasValue)
            .SelectMany(x => new[] { x.ProjectOperationMeasureId!.Value, x.ServiceInfoMeasureId!.Value }).Where(x => x > 0).Distinct().ToList();
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var creatorIds = values.NullListed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var contractorIds = result.Value!.Listed(x => x.ContractorId);
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        foreach (var item in result.Value!)
        {
            if (contractors.HasAny())
                item.Contractor = contractors?.FirstOrDefault(m => m!.Id == item.ContractorId)?.FullName;

            item.DailyServices!.ForEach(x =>
            {
                x.ServiceInfoMeasurement = measures?.FirstOrDefault(m => m.Id == x.ServiceInfoMeasureId)?.Name;
                x.ProjectOperationMeasurement = measures?.FirstOrDefault(m => m.Id == x.ProjectOperationMeasureId)?.Name;
                x.Creator = creators?.FirstOrDefault(m => m.UserId == x.CreatorId)?.FullName;
            });
        }

        var file = new FileContentResult(ContractorStatusStatementExcels.GetCStatementFContractsToExcel(
            result.Value, request.CSFContractFilters, request.CSFDailyFilters!), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"CStatementFContracts-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetCStatementFContractsExporterResponse(file);
    }

    public async Task<Result<GetCStatementSContractsEnumResponse>> GetCStatementSContractsEnum(
        GetCStatementSContractsEnumRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetIntegratedCSSExcelEnum");
        var service = await Task.Run(() => EnumExt.GetEnumObjectList<GetCStatementSContractsEnum>());
        var daily = await Task.Run(() => EnumExt.GetEnumObjectList<GetCStatementSContractsDailiesEnum>());
        return new GetCStatementSContractsEnumResponse(service, daily);
    }

    public async Task<Result<GetCSSCreatorsResponse>> GetCSSCreators(
        GetCSSCreatorsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCSSCreators");

        var values = await _cssRepository.GetCSSCreators(ct);
        if (values is null || values.Count < 1)
            return Result.Failure<GetCSSCreatorsResponse>(CSSErrors.NoCreatorFound)!;
        var creators = await _tPRRepo.GetByUserIds(values, ct);
        if (!string.IsNullOrWhiteSpace(request.FilterData))
        {
            creators = creators
            .Where(x =>
                $"{x.FirstName} {x.LastName}"
                    .Contains(request.FilterData, StringComparison.OrdinalIgnoreCase))
            .ToList();

            values = creators
                .NullListed(x => x.UserId);
        }

        List<GetCSSCreatorsModel> models = [];
        foreach (var id in values)
        {
            var creator = creators.FirstOrDefault(x => x.UserId == id);
            if (creator is null)
                continue;
            models.Add(new GetCSSCreatorsModel
            {
                CreatorId = id,
                CreatorName = $"{creator.FirstName} {creator.LastName}"
            });
        }

        if (request.PageSize > 0 && request.PageIndex > 0)
            models = models.Page(request.PageIndex, request.PageSize).ToList();

        return new GetCSSCreatorsResponse(models, models.Count);
    }

    public async Task<Result<GetCStatementFContractsExporterResponse>> GetCStatementSContractsExporter(
         GetCStatementSContractsExporterRequest request, CT ct)
    {
        var result = await GetCStatementSContractsExecute(request.Adapt<GetCStatementSContractsRequest>(), ct);
        if (result.IsBad()) return result.Failure<GetCStatementFContractsExporterResponse>()!;
        var values = result.Value!.Where(x => x.DailyServices!.Any()).SelectMany(x => x.DailyServices!);

        var measureIds = values.Where(x => x.ProjectOperationMeasureId.HasValue && x.ServiceInfoMeasureId.HasValue)
            .SelectMany(x => new[] { x.ProjectOperationMeasureId!.Value, x.ServiceInfoMeasureId!.Value }).Where(x => x > 0).Distinct().ToList();
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var creatorIds = values.NullListed(x => x.CreatorId);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var contractorIds = result.Value!.Listed(x => x.ContractorId);
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);

        foreach (var item in result.Value!)
        {
            if (contractors.HasAny())
                item.Contractor = contractors?.FirstOrDefault(m => m!.Id == item.ContractorId)?.FullName;

            item.DailyServices!.ForEach(x =>
            {
                x.ServiceInfoMeasurement = measures?.FirstOrDefault(m => m.Id == x.ServiceInfoMeasureId)?.Name;
                x.ProjectOperationMeasurement = measures?.FirstOrDefault(m => m.Id == x.ProjectOperationMeasureId)?.Name;
                x.Creator = creators?.FirstOrDefault(m => m.UserId == x.CreatorId)?.FullName;
            });
        }

        var file = new FileContentResult(ContractorStatusStatementExcels.GetCStatementSContractsToExcel(
            result.Value, request.CSSContractFilters!, request.CSSDailyFilters!), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"CStatementSContracts-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };

        return new GetCStatementFContractsExporterResponse(file);
    }

    public async Task<Result<GetIntegratedCSSByProjectIdResponse>> GetIntegratedCSSByProjectId(
         GetIntegratedCSSByProjectIdRequest request, CT ct)
    {
        List<long>? filteredContractorIds = null;
        if (!string.IsNullOrEmpty(request.ContractorFilter))
        {
            var responseContractorIds = await GetsContractorStatusStatementContractorIdsExecute(
                request.Adapt<GetFilteredContractorStatusStatementRequest>(), ct);
            if (responseContractorIds.IsFailure)
                return responseContractorIds.Failure<GetIntegratedCSSByProjectIdResponse>()!;
            var contractorFilteredIds = responseContractorIds.Value!.Data!;
            var contractorFiltered = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(
                contractorFilteredIds, request.ContractorFilter, null, _mediator, ct);
            if (contractorFiltered is not null)
                filteredContractorIds = contractorFiltered.Where(x => x is not null).Listed(x => x!.Id);
        }

        var result = await GetIntegratedCSSByProjectIdExecute(request, filteredContractorIds, ct);
        if (result.IsBad()) return result.Failure<GetIntegratedCSSByProjectIdResponse>()!;
        var value = result.Value!;
        var values = value.Data!;

        var contractorIds = values.Select(x => x.ContractorId).Where(id => id.HasValue).Select(id => id!.Value).ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(contractorIds, null, null, _mediator, ct);
        if (contractors.HasAny())
        {
            values!.ForEach(item =>
            {
                var contractor = contractors!.FirstOrDefault(x => x!.Id == item.ContractorId);
                item.Contractor = contractor?.FullName;
                item.ContractorReferenceCode = contractor?.PreferentialReferenceCode;
            });
        }

        var totalPrice = new GetIntegratedCSSPriceModel
        {
            ManagementConfirmedAmount = values.Sum(x => x.ManagementConfirmedAmount),
            PrimaryManagerConfirmedAmount = values.Sum(x => x.PrimaryManagerConfirmedAmount),
            FinalManagerConfirmedAmount = values.Sum(x => x.FinalManagerConfirmedAmount),
        };

        return new GetIntegratedCSSByProjectIdResponse
        (
            values,
            totalPrice,
            value.RowCount
        );
    }
}
