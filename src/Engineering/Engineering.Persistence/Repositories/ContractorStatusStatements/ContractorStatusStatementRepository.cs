using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.ContractorStatusStatements.Contracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSS.Service;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSSByProjectId;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementById;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetFilteredContractorStatusStatement;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Persistence.Repositories.ContractorStatusStatements;

public class ContractorStatusStatementRepository : BaseRepository<EngineeringDBContext, ContractorStatusStatement>, IContractorStatusStatementRepository
{
    public ContractorStatusStatementRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ContractorStatusStatement?> GetContractorStatusStatementByIdNoInclude(
        long id, CT ct)
    {
        var query = DbSet

            .Include(oo => oo.ContractorStatusStatementPayments)
            .Where(oo => oo.Id.Equals(id));

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<ContractorStatusStatement?> GetContractorStatusStatementByIdIncludeLess(
        long id, CT ct)
    {
        var query = DbSet

            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorContract)

            .Include(oo => oo.ContractorStatusStatementDocuments)

            .Include(oo => oo.ContractorStatusStatementPayments)
            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorStatusStatementServices)
                    .ThenInclude(oo => oo.ContractorStatusStatementServiceDailies)
                        .ThenInclude(oo => oo.DailyProjectOperationService.DailyProjectOperation)

            .Where(oo => oo.Id.Equals(id));

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<ContractorStatusStatement?> GetContractorStatusStatementByIdFullInclude(
        long id, CT ct)
    {
        var query = DbSet

            .Include(oo => oo.ContractorStatusStatementPayments)
            .Include(x => x.ContractorStatusStatementDetails)
            .ThenInclude(x => x.ContractorContract.ContractorContractHeader)

            .Include(x => x.ContractorStatusStatementHistories)
            .Include(oo => oo.ContractorStatusStatementDocuments)
            .Include(oo => oo.Project!.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)

            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorStatusStatementServices)
                    .ThenInclude(oo => oo.ContractorStatusStatementServiceDailies)
                        .ThenInclude(oo => oo.DailyProjectOperationService)
                            .ThenInclude(oo => oo.DailyProjectOperation)

            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorStatusStatementServices)
                    .ThenInclude(oo => oo.ContractorContractDetail)
                        .ThenInclude(oo => oo!.ContractorContractDetailServices)
                            .ThenInclude(oo => oo.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo)

            .Where(oo => oo.Id.Equals(id));

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<ContractorStatusStatement?> GetContractorStatusStatementById(
        long id, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.ContractorStatusStatementPayments)
            .Include(oo => oo.Season.Branch.Category)
            .Include(oo => oo.ContractorStatusStatementDocuments)
            .Include(oo => oo.Project.ProjectCostCenters)
                .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorContract)
                    .ThenInclude(oo => oo.ContractorContractHeader)

            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorContract)

            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorContract)
                    .ThenInclude(oo => oo.Details)
                    .ThenInclude(oo => oo.ContractorContractDetailServices)
                        .ThenInclude(oo => oo.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo)

            .Include(oo => oo.ContractorStatusStatementProducts)

            .Include(oo => oo.ContractorStatusStatementFines)
            .Include(oo => oo.ContractorStatusStatementDiscounts)

            .Include(oo => oo.ContractorStatusStatementRewards)

            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorStatusStatementServices)
                    .ThenInclude(oo => oo.ContractorStatusStatementServiceDailies)
                        .ThenInclude(oo => oo.DailyProjectOperationService)
                            .ThenInclude(oo => oo.DailyProjectOperation)

            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorStatusStatementServices)
                .ThenInclude(oo => oo.ContractorContractDetail)
                    .ThenInclude(oo => oo.ContractorContractDetailServices)
                .ThenInclude(oo => oo.ProjectOperationDetailContractorService)
                .ThenInclude(oo => oo.OperationInfoService)
                .ThenInclude(oo => oo.ServiceInfo)

            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorStatusStatementServices)
                    .ThenInclude(oo => oo.ContractorStatusStatementServiceThirdParties)

            .Where(oo => oo.Id.Equals(id));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<ContractorStatusStatement?> GetCSSForDelete(
        long id, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.ContractorStatusStatementDocuments)
            .Include(oo => oo.ContractorStatusStatementProducts)
            .Include(oo => oo.ContractorStatusStatementHistories)
            .Include(oo => oo.ContractorStatusStatementFines)
            .Include(oo => oo.ContractorStatusStatementRewards)
            .Include(oo => oo.ContractorStatusStatementDiscounts)
            .Include(oo => oo.ContractorStatusStatementCostOvers)
            .Include(oo => oo.ContractorStatusStatementPayments)
            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorStatusStatementServices)
                .ThenInclude(oo => oo.ContractorStatusStatementServiceDailies)

            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorStatusStatementServices)
                .ThenInclude(oo => oo.ContractorStatusStatementServiceThirdParties)

            .Where(oo => oo.Id.Equals(id));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<ContractorStatusStatement?> GetCSSForDiscount(
        long id, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.ContractorStatusStatementFines)
            .Include(oo => oo.ContractorStatusStatementRewards)
            .Include(oo => oo.ContractorStatusStatementDiscounts)
            .Include(oo => oo.ContractorStatusStatementCostOvers)
            .Include(oo => oo.ContractorStatusStatementPayments)

            .Where(oo => oo.Id.Equals(id));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<ContractorStatusStatement?> GetContractorStatusStatementHeaderById(
        long id, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.ContractorStatusStatementPayments)
            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorContract)
                    .ThenInclude(oo => oo.ContractorContractHeader)

            .Where(oo => oo.Id.Equals(id));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<GetContractorStatusStatementByIdResponse?> GetModeledContractorStatusStatementById(
        long id, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var query = DbSet

            .Where(oo => oo.Id.Equals(id))

            .Select(item => new GetContractorStatusStatementByIdResponse()
            {
                Id = item.Id,
                CategoryId = item.Season.Branch.Category.Id,
                CategoryName = item.Season.Branch.Category.CategoryName,
                BranchId = item.Season.Branch.Id,
                BranchName = item.Season.Branch.BranchName,
                SeasonId = item.Season.Id,
                SeasonName = item.Season.SeasonName,
                ProjectId = item.Project.Id,
                Project = item.Project.ProjectName,
                ProjectPreferentialCode = item.Project.PreferentialReferenceCode,
                CostCenterId = item.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
                CostCenterName = item.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                FixedAmount = item.ContractorStatusStatementDetails.Where(x => x.ContractorContract.ContractorContractType == ContractorContractType.Fixed).Sum(x => x.TotalAmount),
                ContractorId = item.ContractorId,
                CurrencyId = item.CurrencyId,
                PayableAmount = item.PayableAmount,
                RemainingAmount = item.RemainingAmount,
                CanPayableAmount = item.CanPayableAmount,
                DiscountPrice = item.DiscountPrice,
                Code = item.Code,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Status = item.Status,
                TotalPercentageDoingJobWell = item.TotalPercentageDoingJobWell,
                TotalDoingJobWellAmount = item.TotalDoingJobWellAmount,
                TotalAdvancePaymentAmount = item.TotalAdvancePaymentAmount,
                Type = item.Type,
                TotalPercentageAdvancePayment = item.TotalPercentageAdvancePayment,
                CreatorConfirmedAmount = item.CreatorConfirmedAmount,
                ProjectManagerConfirmedAmount = item.ProjectManagerConfirmedAmount,
                ManagementConfirmedAmount = item.ManagementConfirmedAmount,
                TotalDailyLatenessPenalty = item.TotalDailyLatenessPenalty,
                TotalWorkDonePercent = item.TotalWorkDonePercent,
                TotalWorkDeliveryPercent = item.TotalWorkDeliveryPercent,
                TotalWorkCompletionPercent = item.TotalWorkCompletionPercent,
                ProductsAmount = item.ProductsAmount,
                CreatorId = item.CreatorId,
                FinesAmount = item.FinesAmount,
                MultiPayment = item.MultiPayment,
                FinalTotalAmount = item.FinalTotalAmount,
                PrimaryManagerConfirmedAmount = item.PrimaryManagerConfirmedAmount,
                PrimaryManagerConfirmed = item.PrimaryManagerConfirmed,
                FinalManagerConfirmedAmount = item.FinalManagerConfirmedAmount,
                FinalManagerConfirmed = item.FinalManagerConfirmed,
                RewardsAmount = item.RewardsAmount,
                CostOversAmount = item.CostOversAmount,
                ThirdPartiesAmount = item.ThirdPartiesAmount,
                Paymented = item.PaymentedAmount,
                DiscountedAmount = item.ContractorStatusStatementDiscounts.Sum(x => x.DiscountPrice),
                Description = item.Description,
                ProjectManagmentDescription = item.ProjectManagmentDescription,
                ManagmentDescription = item.ManagmentDescription,
                PrimaryManagerDescription = item.PrimaryManagerDescription,
                FinalManagerDescription = item.FinalManagerDescription,
                LastDescription = item.LastDescription,
                Urls = item.ContractorStatusStatementDocuments
                .Where(x => x.ContractorStatusStatementPayment == null).Select(x => x.Url).ToList(),

                Discounts = item.ContractorStatusStatementDiscounts.Select(d => new GetContractorStatusStatementByIdDiscounts()
                {
                    Id = d.Id,
                    DiscountPrice = d.DiscountPrice,
                    RequestRewardId = d.RequestRewardId,
                    RequestRewardDescription = d.RequestReward.Description,
                    ManagerDescription = d.RequestReward.ManagerDescription,
                    OfferedPrice = d.RequestReward.OfferedPrice,
                    Price = d.DiscountPrice,
                    Created = d.Created,
                    Documents = d.RequestReward.RequestRewardDocuments.Select(x => x.Url).ToList(),
                    RegistrationDate = d.RegistrationDate,
                    Description = d.RequestReward.Description,
                }).ToList(),

                ContractorContractHeaders = item.ContractorStatusStatementDetails.Select(x => x.ContractorContract.ContractorContractHeader).Distinct().Select(header => new GetsCSSContractorContractHeaderModel()
                {
                    Id = header.Id,
                    ContractorId = header.ContractorId,
                    CurrencyId = header.CurrencyId,
                    StartDate = header.ContractorContracts.Min(x => x.StartDate),
                    EndDate = header.ContractorContracts.Max(x => x.EndDate),
                    CreatorId = header.CreatorId,
                    Created = header.Created,
                    Description = header.Description,
                    Urls = item.ContractorStatusStatementDocuments
                        .Where(x => x.ContractorStatusStatementPayment == null).Select(x => x.Url).ToList(),

                    ContractorContracts = header.ContractorContracts.Select(contract => new GetsCSSContractorContractModel()
                    {
                        Id = contract.Id,
                        ContractorContractStatusStatementId = item.ContractorStatusStatementDetails.FirstOrDefault(x => x.ContractorContract.Id == contract.Id).Id,
                        ContractorContractTypeId = contract.ContractorContractType,
                        StartDate = contract.StartDate,
                        EndDate = contract.EndDate,
                        TotalAmount = contract.TotalAmount,
                        PercentageTotalAmount = item.ContractorStatusStatementDetails.FirstOrDefault(x => x.ContractorContract.Id == contract.Id).TotalAmount,
                        FixedContractPct = item.ContractorStatusStatementDetails.FirstOrDefault(x => x.ContractorContract.Id == contract.Id).FixedContractPct,
                        FixedContractPctDesc = item.ContractorStatusStatementDetails.FirstOrDefault(x => x.ContractorContract.Id == contract.Id).FixedContractPctDesc,
                        ProjectFixedContractPct = item.ContractorStatusStatementDetails.FirstOrDefault(x => x.ContractorContract.Id == contract.Id).FixedContractPct,
                        ProjectFixedContractPctDesc = item.ContractorStatusStatementDetails.FirstOrDefault(x => x.ContractorContract.Id == contract.Id).FixedContractPctDesc,
                        ManagerFixedContractPct = item.ContractorStatusStatementDetails.FirstOrDefault(x => x.ContractorContract.Id == contract.Id).FixedContractPct,
                        ManagerFixedContractPctDesc = item.ContractorStatusStatementDetails.FirstOrDefault(x => x.ContractorContract.Id == contract.Id).FixedContractPctDesc,
                        PercentageDoingJobWell = contract.PercentageDoingJobWell,
                        DoingJobWellAmount = contract.DoingJobWellAmount,
                        PercentageAdvancePayment = contract.PercentageAdvancePayment,
                        AdvancePaymentAmount = contract.AdvancePaymentAmount,
                        DailyLatenessPenalty = contract.DailyLatenessPenalty,
                        WorkDonePercent = contract.WorkDonePercent,
                        WorkDeliveryPercent = contract.WorkDeliveryPercent,
                        WorkCompletionPercent = contract.WorkCompletionPercent,
                        Description = contract.Description
                    }).ToList(),
                }).ToList(),

                Products = item.ContractorStatusStatementProducts.Where(x => !x.IsPurchaseForContractor).Select(p => new GetModeledContractorStatusStatementByIdProduct()
                {
                    Id = p.Id,
                    RequestGoodsSupplyDetailId = p.RequestGoodsSupplyDetail.Id,
                    ProductId = p.ProductId,
                    OperationInfoName = p.RequestGoodsSupplyDetail.ConsumableVolumeProduct.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                    SupplyCount = p.RequestedCount,
                    Price = p.Price,
                    OtherPrice = p.OtherPrice,
                    TransferPrice = p.TransferPrice,
                    DiscountOnInvoiceNumber = p.DiscountOnInvoiceNumber,
                    CustomerInvoiceNumber = p.CustomerInvoiceNumber,
                    CreatedDate = p.RequestGoodsSupplyDetail.Created,
                    RequestCount = p.RequestGoodsSupplyDetail.RequestedCount,
                    UnitPrice = p.RequestGoodsSupplyDetail.UnitPrice,
                    TaxNumber = p.RequestGoodsSupplyDetail.TaxNumber,
                    DiscountByNum = p.RequestGoodsSupplyDetail.DiscountByNumber,
                    TotalPrice = p.TotalPrice,
                    Urls = p.RequestGoodsSupplyDetail.RequestGoodsSupplyDetailDocuments.Select(x => x.Url).ToList(),
                }).ToList(),

                ForContractorProducts = item.ContractorStatusStatementProducts.Where(x => x.IsPurchaseForContractor).Select(p => new GetModeledContractorStatusStatementByIdForContractorProduct()
                {
                    Id = p.Id,
                    RequestGoodsSupplyDetailId = p.RequestGoodsSupplyDetail.Id,
                    ProductId = p.ProductId,
                    OperationInfoName = p.RequestGoodsSupplyDetail.ConsumableVolumeProduct.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                    SupplyCount = p.RequestedCount,
                    Price = p.Price,
                    OtherPrice = p.OtherPrice,
                    TransferPrice = p.TransferPrice,
                    DiscountOnInvoiceNumber = p.DiscountOnInvoiceNumber,
                    CustomerInvoiceNumber = p.CustomerInvoiceNumber,
                    CreatedDate = p.RequestGoodsSupplyDetail.Created,
                    RequestCount = p.RequestGoodsSupplyDetail.RequestedCount,
                    UnitPrice = p.RequestGoodsSupplyDetail.UnitPrice,
                    TaxNumber = p.RequestGoodsSupplyDetail.TaxNumber,
                    DiscountByNum = p.RequestGoodsSupplyDetail.DiscountByNumber,
                    TotalPrice = p.TotalPrice,
                    Urls = p.RequestGoodsSupplyDetail.RequestGoodsSupplyDetailDocuments.Select(x => x.Url).ToList(),
                }).ToList(),

                Fines = item.ContractorStatusStatementFines.Select(f => new GetModeledContractorStatusStatementByIdFines()
                {
                    Id = f.Id,
                    RequestRewardId = f.RequestReward.Id,
                    RegistrationDate = f.RegistrationDate,
                    RequestRewardDescription = f.RequestReward.Description,
                    RequestRewardManagerDescription = f.RequestReward.ManagerDescription,
                    Type = f.RequestReward.Type,
                    OfferedPrice = f.ConfirmedPrice,
                    Documents = f.RequestReward.RequestRewardDocuments.Select(x => x.Url).ToList(),
                }).ToList(),
                CostOvers = item.ContractorStatusStatementCostOvers.Select(f => new GetModeledContractorStatusStatementByIdCostOvers()
                {
                    Id = f.Id,
                    CostOverDetailId = f.ContractorContractDetailCostOver.Id,
                    CostOverId = f.ContractorContractDetailCostOver.CostOver.Id,
                    CostOverName = f.ContractorContractDetailCostOver.CostOver.CostOverName,
                    CostOverCode = f.ContractorContractDetailCostOver.CostOver.CostOverCode,
                    Percentage = f.ContractorContractDetailCostOver.Percentage,
                    Amount = f.ContractorContractDetailCostOver.Amount,
                    Description = f.ContractorContractDetailCostOver.Description,
                    ProjectOperationsDetailServiceId = f.ContractorContractDetailCostOver.ContractorContract == null ?
                        f.ContractorContractDetailCostOver.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault().ProjectOperationDetailContractorService.Id : null,
                    ContractorContractId = f.ContractorContractDetailCostOver.ContractorContract != null ?
                            f.ContractorContractDetailCostOver.ContractorContract.Id : null,
                    ContractorContractDescription = f.ContractorContractDetailCostOver.ContractorContract != null ?
                        f.ContractorContractDetailCostOver.ContractorContract.Description : null,
                }).ToList(),

                Rewards = item.ContractorStatusStatementRewards.Select(r => new GetModeledContractorStatusStatementByIdRewards()
                {
                    Id = r.Id,
                    RequestRewardId = r.RequestReward.Id,
                    RegistrationDate = TimeCalculator.DatePiker(r.RegistrationDate),
                    RequestRewardDescription = r.RequestReward.Description,
                    RequestRewardManagerDescription = r.RequestReward.ManagerDescription,
                    OfferedPrice = r.ConfirmedPrice,
                    Urls = r.RequestReward.RequestRewardDocuments.Select(x => x.Url).ToList(),
                }).ToList(),

            });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<string> CodeCreator(
        long? companyId, CT ct)
    {
        var query = await DbSet
           .Where(x => (companyId == null || x.CompanyId == companyId) &&
           EF.Functions.IsNumeric(x.Code)).Select(x => Convert.ToInt64(x.Code)).ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<ContractorStatusStatement?> GetLastContractorStatusStatementById(
        long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project)
             .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorContract)
                    .ThenInclude(oo => oo.ContractorContractHeader)

            .Where(oo => oo.Id.Equals(id));

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<List<ContractorStatusStatement>> GetContractorStatusStatementByHeaderId(
        long contractorContractId, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project)
            .Include(oo => oo.ContractorStatusStatementDetails)
                .ThenInclude(oo => oo.ContractorContract)
                    .ThenInclude(oo => oo.ContractorContractHeader)

            .Where(oo => oo.ContractorStatusStatementDetails.Any(x => x.ContractorContract.ContractorContractHeader.Id.Equals(contractorContractId)))
            .OrderByDescending(oo => oo.Created);

        return await query.ToListAsync(ct);
    }

    public async Task<(List<PaidContractorStatusStatementModel> Data, int RowCount)> GetsPaidContractorStatusStatement(
        long contractorId,
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var query = DbSet

            .Where(oo =>
                oo.ContractorId == contractorId &&
                oo.Status == CSSStatus.Paid &&
                oo.Project!.Id == projectId)

            .Select(item => new PaidContractorStatusStatementModel()
            {
                Id = item.Id,
                CategoryId = item.Season.Branch.Category.Id,
                CategoryName = item.Season.Branch.Category.CategoryName,
                BranchId = item.Season.Branch.Id,
                BranchName = item.Season.Branch.BranchName,
                SeasonId = item.Season.Id,
                SeasonName = item.Season.SeasonName,
                Code = item.Code,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Status = item.Status,
                TotalPercentageDoingJobWell = item.TotalPercentageDoingJobWell,
                TotalDoingJobWellAmount = item.TotalDoingJobWellAmount,
                TotalAdvancePaymentAmount = item.TotalAdvancePaymentAmount,
                TotalPercentageAdvancePayment = item.TotalPercentageAdvancePayment,
                TotalDailyLatenessPenalty = item.TotalDailyLatenessPenalty,
                TotalWorkDonePercent = item.TotalWorkDonePercent,
                TotalWorkDeliveryPercent = item.TotalWorkDeliveryPercent,
                TotalWorkCompletionPercent = item.TotalWorkCompletionPercent,
                CreatorConfirmedAmount = item.CreatorConfirmedAmount,
                ProjectManagerConfirmedAmount = item.ProjectManagerConfirmedAmount,
                ManagementConfirmedAmount = item.ManagementConfirmedAmount,
                CreatorId = item.CreatorId,
                ConfirmedPrice = item.ConfirmedPrice,
                LastDescription = item.LastDescription,
                ProductsAmount = item.ProductsAmount,
                UserApprovalAmount = item.ThirdPartiesAmount,
                ProjectManagerApprovalAmount = item.ProjectManagerApprovalAmount,
                ManagementApprovalAmount = item.ManagementApprovalAmount,
                FinesAmount = item.FinesAmount,
                FinalTotalAmount = item.FinalTotalAmount,
                RewardsAmount = item.RewardsAmount,
                ThirdPartiesAmount = item.ThirdPartiesAmount,
                PaymentedAmount = item.PaymentedAmount,
                DiscountedAmount = item.ContractorStatusStatementDiscounts.Sum(x => x.DiscountPrice),
                Description = item.Description,
                ManagmentDescription = item.ManagmentDescription,
                Urls = item.ContractorStatusStatementDocuments
                    .Where(x => x.ContractorStatusStatementPayment == null).Select(x => x.Url).ToList(),
            });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<decimal?> PaymentConfirmationCSSAmounts(
        long contractorId,
        long projectId,
        CT ct)
    {
        var totalConfirmedPrice = await DbSet
            .Where(x =>
                x.ContractorId == contractorId &&
                x.Status == CSSStatus.PaymentConfirmation &&
                x.Project!.Id == projectId)
            .SumAsync(x => (decimal?)x.ConfirmedPrice, ct);

        return totalConfirmedPrice ?? 0;
    }

    public async Task<(List<GetFilteredContractorStatusStatementModel> Data, int RowCount)> GetFilteredContractorStatusStatement(
        List<long>? ids,
        long? contractorId,
        List<long>? contractorIds,
        long? costCenterId,
        long? projectId,
        long? projectManagerId,
        long? contractorContractId,
        long? creatorId,
        string? contractNumber,
        string? code,
        string? managerAmount,
        string? managerDescription,
        List<CSSStatus>? statuses,
        bool isPayment,
        bool? multiPayment,
        DateTime? startDate,
        DateTime? endDate,
        bool? isPrimaryManagerConfirmed,
        bool? isFinalManagerConfirmed,
        bool? isManager,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet

            .Where(oo =>
                (ids == null || ids.Contains(oo.Id)) &&
                (code == null || oo.Code == code) &&
                (contractorIds == null || contractorIds.Contains(oo.ContractorId!.Value)) &&
                (contractorId == null || oo.ContractorId == contractorId) &&
                (contractNumber == null || oo.ContractorStatusStatementDetails.Any(x => x.ContractorContract.ContractorContractHeader.ContractorId.ToString().Equals(contractNumber))) &&
                (projectId == null || (oo.Project.Id == projectId)) &&
                (projectManagerId == null || (oo.Project.ProjectManager == projectManagerId)) &&
                (costCenterId == null || (oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId))) &&
                (contractorContractId == null || oo.ContractorStatusStatementDetails.Any(x => x.ContractorContract.ContractorContractHeader.Id.Equals(contractorContractId))) &&
                (startDate == null || oo.StartDate.Date >= startDate.Value.Date) &&
                (endDate == null || oo.EndDate.Date <= endDate.Value.Date) &&
                (string.IsNullOrWhiteSpace(filterData) || oo.Code.Equals(filterData)) &&
                (string.IsNullOrWhiteSpace(managerDescription) ||
                string.IsNullOrWhiteSpace(managerDescription) || EF.Functions.Like(oo.FinalManagerDescription, managerDescription.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(managerDescription) || EF.Functions.Like(oo.PrimaryManagerDescription, managerDescription.MakeLikePattern())) &&

                (string.IsNullOrWhiteSpace(managerAmount) ||
                string.IsNullOrWhiteSpace(managerAmount) || EF.Functions.Like(oo.FinalManagerConfirmedAmount.ToString(), managerAmount.MakeLikePattern()) ||
                string.IsNullOrWhiteSpace(managerAmount) || EF.Functions.Like(oo.PrimaryManagerDescription.ToString(), managerAmount.MakeLikePattern())) &&

                (statuses == null || statuses.Contains(oo.Status)) &&
                (isPrimaryManagerConfirmed == null || oo.PrimaryManagerConfirmed == isPrimaryManagerConfirmed) &&
                (creatorId == null || oo.CreatorId == creatorId) &&
                (isFinalManagerConfirmed == null || oo.PrimaryManagerConfirmed == isFinalManagerConfirmed));

        if (isPayment)
            query = query.Where(x => x.FinalManagerConfirmed && x.PrimaryManagerConfirmed && x.Status == CSSStatus.ManagementConfirmed);

        if (multiPayment is not null)
            query = query.Where(x => x.MultiPayment == multiPayment);

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var newQuery = query.Select(item => new GetFilteredContractorStatusStatementModel()
        {
            Id = item.Id,
            CostCenterId = item.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
            CostCenterName = item.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
            ProjectId = item.Project.Id,
            MultiPayment = item.MultiPayment,
            Project = item.Project.ProjectName,
            ProjectCode = item.Project.ProjectCode,
            ProjectManagerId = item.Project.ProjectManager,
            ContractorId = item.ContractorId,
            CurrencyId = item.CurrencyId,
            CreatorConfirmedAmount = item.CreatorConfirmedAmount,
            ProjectManagerConfirmedAmount = item.ProjectManagerConfirmedAmount,
            ManagementConfirmedAmount = item.ManagementConfirmedAmount,
            CategoryId = item.Season.Branch.Category.Id,
            CategoryName = item.Season.Branch.Category.CategoryName,
            BranchId = item.Season.Branch.Id,
            BranchName = item.Season.Branch.BranchName,
            SeasonId = item.Season.Id,
            SeasonName = item.Season.SeasonName,
            PayableAmount = item.PayableAmount,
            CanPayableAmount = item.CanPayableAmount,
            DiscountPrice = item.DiscountPrice,
            Code = item.Code,
            LastDescription = item.LastDescription,
            PrimaryManagerConfirmedAmount = item.PrimaryManagerConfirmedAmount,
            FinalManagerConfirmedAmount = item.FinalManagerConfirmedAmount,
            StartDate = item.StartDate,
            EndDate = item.EndDate,
            Status = item.Status,
            TotalPercentageDoingJobWell = item.TotalPercentageDoingJobWell,
            TotalDoingJobWellAmount = item.TotalDoingJobWellAmount,
            TotalAdvancePaymentAmount = item.TotalAdvancePaymentAmount,
            TotalPercentageAdvancePayment = item.TotalPercentageAdvancePayment,
            TotalDailyLatenessPenalty = item.TotalDailyLatenessPenalty,
            TotalWorkDonePercent = item.TotalWorkDonePercent,
            TotalWorkDeliveryPercent = item.TotalWorkDeliveryPercent,
            TotalWorkCompletionPercent = item.TotalWorkCompletionPercent,
            CreatorId = item.CreatorId,
            Created = item.Created,
            ConfirmedPrice = item.ConfirmedPrice,
            ProductsAmount = item.ProductsAmount,
            ForContractorProductsAmount = item.ForContractorProductsAmount,
            UserApprovalAmount = item.ThirdPartiesAmount,
            ProjectManagerApprovalAmount = item.ProjectManagerApprovalAmount,
            ManagementApprovalAmount = item.ManagementApprovalAmount,
            FinesAmount = item.FinesAmount,
            FinalTotalAmount = item.FinalTotalAmount,
            Type = item.Type,
            RewardsAmount = item.RewardsAmount,
            ThirdPartiesAmount = item.ThirdPartiesAmount,
            PaymentedAmount = item.PaymentedAmount,
            DiscountedAmount = item.ContractorStatusStatementDiscounts.Sum(x => x.DiscountPrice),
            Description = item.Description,
            FinalManagerConfirmed = item.FinalManagerConfirmed,
            PrimaryManagerConfirmed = item.PrimaryManagerConfirmed,
            PrimaryManagerDescription = item.PrimaryManagerDescription,
            FinalManagerDescription = item.FinalManagerDescription,
            ManagmentDescription = item.ManagmentDescription,
            ProjectManagmentDescription = item.ProjectManagmentDescription,
            Urls = item.ContractorStatusStatementDocuments
                .Where(x => x.ContractorStatusStatementPayment == null).Select(x => x.Url).ToList(),
        });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        newQuery = newQuery.OrderByDescending(oo => oo.Created);

        var count = await newQuery.CountAsync(ct);

        if (orderBy?.Length > 0)
            newQuery = newQuery.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            newQuery = newQuery.Page(pageIndex, pageSize);

        var entities = await newQuery.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<(List<long> Data, int RowCount)> GetsContractorStatusStatementContractorIds(
        long? costCenterId,
        long? projectId,
        long? projectManagerId,
        long? contractorContractId,
        string? contractNumber,
        string? code,
        List<CSSStatus>? statuses,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet

            .Where(oo =>
                (code == null || oo.Code == code) &&
                (contractNumber == null || oo.ContractorStatusStatementDetails.Any(x => x.ContractorContract.ContractorContractHeader.ContractorId.ToString().Equals(contractNumber))) &&
                (projectId == null || (oo.Project.Id == projectId)) &&
                (projectManagerId == null || (oo.Project.ProjectManager == projectManagerId)) &&
                (costCenterId == null || oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (contractorContractId == null || oo.ContractorStatusStatementDetails.Any(x => x.ContractorContract.ContractorContractHeader.Id.Equals(contractorContractId))) &&
                (startDate == null || oo.StartDate.Date >= startDate.Value.Date) &&
                (endDate == null || oo.EndDate.Date <= endDate.Value.Date) &&
                (string.IsNullOrWhiteSpace(filterData) || oo.Code.Equals(filterData)) &&
                (statuses == null || statuses.Contains(oo.Status)))
            .Select(x => x.ContractorId!.Value).Distinct();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var count = await query.CountAsync(ct);

        var entities = await query.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<(List<GetsDraftableContractorStatusStatementModel> Data, int RowCount)> GetsDraftableContractorStatusStatement(
        long contractorId,
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var query = DbSet

            .Where(oo =>
                 oo.ContractorStatusStatementDetails.Any(x => contractorId == x.ContractorContract.ContractorContractHeader.ContractorId) &&
                 CSSStatusRules.AllowForDraftable.Contains(oo.Status) &&
                 oo.Project.Id == projectId)

            .Select(item => new GetsDraftableContractorStatusStatementModel()
            {
                Id = item.Id,
                CreatorConfirmedAmount = item.CreatorConfirmedAmount,
                ProjectManagerConfirmedAmount = item.ProjectManagerConfirmedAmount,
                ManagementConfirmedAmount = item.ManagementConfirmedAmount,
                CategoryId = item.Season.Branch.Category.Id,
                CategoryName = item.Season.Branch.Category.CategoryName,
                BranchId = item.Season.Branch.Id,
                BranchName = item.Season.Branch.BranchName,
                SeasonId = item.Season.Id,
                SeasonName = item.Season.SeasonName,
                Code = item.Code,
                LastDescription = item.LastDescription,
                PrimaryManagerConfirmedAmount = item.PrimaryManagerConfirmedAmount,
                FinalManagerConfirmedAmount = item.FinalManagerConfirmedAmount,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                Status = item.Status,
                TotalPercentageDoingJobWell = item.TotalPercentageDoingJobWell,
                TotalDoingJobWellAmount = item.TotalDoingJobWellAmount,
                TotalAdvancePaymentAmount = item.TotalAdvancePaymentAmount,
                TotalPercentageAdvancePayment = item.TotalPercentageAdvancePayment,
                TotalDailyLatenessPenalty = item.TotalDailyLatenessPenalty,
                TotalWorkDonePercent = item.TotalWorkDonePercent,
                TotalWorkDeliveryPercent = item.TotalWorkDeliveryPercent,
                TotalWorkCompletionPercent = item.TotalWorkCompletionPercent,
                CreatorId = item.CreatorId,
                Created = item.Created,
                ConfirmedPrice = item.ConfirmedPrice,
                ProductsAmount = item.ProductsAmount,
                UserApprovalAmount = item.ThirdPartiesAmount,
                ProjectManagerApprovalAmount = item.ProjectManagerApprovalAmount,
                ManagementApprovalAmount = item.ManagementApprovalAmount,
                FinesAmount = item.FinesAmount,
                FinalTotalAmount = item.FinalTotalAmount,
                Type = item.Type,
                RewardsAmount = item.RewardsAmount,
                ThirdPartiesAmount = item.ThirdPartiesAmount,
                PaymentedAmount = item.PaymentedAmount,
                DiscountedAmount = item.ContractorStatusStatementDiscounts.Sum(x => x.DiscountPrice),
                Description = item.Description,
                FinalManagerConfirmed = item.FinalManagerConfirmed,
                PrimaryManagerConfirmed = item.PrimaryManagerConfirmed,
                PrimaryManagerDescription = item.PrimaryManagerDescription,
                FinalManagerDescription = item.FinalManagerDescription,
                ManagmentDescription = item.ManagmentDescription,
                ProjectManagmentDescription = item.ProjectManagmentDescription,
                Urls = item.ContractorStatusStatementDocuments
                    .Where(x => x.ContractorStatusStatementPayment == null).Select(x => x.Url).ToList(),
            });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<(List<GetsIntegratedCSSModel> Data, int RowCount)> GetsIntegratedCSS(
        long? contractorId,
        List<long>? contractorIds,
        long? costCenterId,
        long? projectId,
        long? contractorContractId,
        List<CSSStatus>? statuses,
        bool isPrimaryManager,
        bool isFinalManager,
        string? filterData,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var query = DbSet
                .Where(oo =>
                    (contractorIds == null || contractorIds.Contains(oo.ContractorId!.Value)) &&
                    (contractorId == null || oo.ContractorStatusStatementDetails.Any(x => x.ContractorContract.ContractorContractHeader.ContractorId.Equals(contractorId))) &&
                    (costCenterId == null || oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                    (projectId == null || oo.Project.Id == projectId) &&
                    (contractorContractId == null || oo.ContractorStatusStatementDetails.Any(x => x.ContractorContract.ContractorContractHeader.Id.Equals(contractorContractId))) &&
                    (string.IsNullOrWhiteSpace(filterData) || oo.Code.Equals(filterData)) &&
                    (statuses == null || statuses.Contains(oo.Status)) &&
                    (CSSStatusRules.AllowForIntegratedCSS.Contains(oo.Status)));


        var newQuery = query.Select(item => new GetsIntegratedCSSModel()
        {
            Id = item.Id,
            CostCenterId = item.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
            CostCenterName = item.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
            CostCenterReferenceCode = item.Project.ProjectCostCenters.FirstOrDefault().CostCenter.PreferentialReferenceCode,
            ProjectId = item.Project.Id,
            Project = item.Project.ProjectName,
            ContractorId = item.ContractorId,
            CreatorConfirmedAmount = item.CreatorConfirmedAmount,
            ProjectManagerConfirmedAmount = item.ProjectManagerConfirmedAmount,
            ManagementConfirmedAmount = item.ManagementConfirmedAmount,
            Code = item.Code,
            PrimaryManagerConfirmedAmount = item.PrimaryManagerConfirmedAmount,
            FinalManagerConfirmedAmount = item.FinalManagerConfirmedAmount,
            StartDate = item.StartDate,
            EndDate = item.EndDate,
            Status = item.Status,
            TotalPercentageDoingJobWell = item.TotalPercentageDoingJobWell,
            TotalDoingJobWellAmount = item.TotalDoingJobWellAmount,
            TotalAdvancePaymentAmount = item.TotalAdvancePaymentAmount,
            TotalPercentageAdvancePayment = item.TotalPercentageAdvancePayment,
            TotalDailyLatenessPenalty = item.TotalDailyLatenessPenalty,
            TotalWorkDonePercent = item.TotalWorkDonePercent,
            TotalWorkDeliveryPercent = item.TotalWorkDeliveryPercent,
            TotalWorkCompletionPercent = item.TotalWorkCompletionPercent,
            Created = item.Created,
            ConfirmedPrice = item.ConfirmedPrice,
            ProductsAmount = item.ProductsAmount,
            UserApprovalAmount = item.ThirdPartiesAmount,
            ProjectManagerApprovalAmount = item.ProjectManagerApprovalAmount,
            ManagementApprovalAmount = item.ManagementApprovalAmount,
            FinesAmount = item.FinesAmount,
            FinalTotalAmount = item.FinalTotalAmount,
            RewardsAmount = item.RewardsAmount,
            ThirdPartiesAmount = item.ThirdPartiesAmount,
            PaymentedAmount = item.PaymentedAmount,
            PrimaryManagerConfirmed = item.PrimaryManagerConfirmed,
            FinalManagerConfirmed = item.FinalManagerConfirmed,
            DiscountedAmount = item.ContractorStatusStatementDiscounts.Sum(x => x.DiscountPrice),
            Description = item.Description,
            ProjectManagmentDescription = item.ProjectManagmentDescription,
            ManagmentDescription = item.ManagmentDescription,
            PrimaryManagerDescription = item.PrimaryManagerDescription,
            FinalManagerDescription = item.FinalManagerDescription,
            LastDescription = item.LastDescription,
            Urls = item.ContractorStatusStatementDocuments
                .Where(x => x.ContractorStatusStatementPayment == null).Select(x => x.Url).ToList(),
        });

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        newQuery = newQuery.OrderByDescending(oo => oo.Created);

        var count = await newQuery.CountAsync(ct);

        var entities = await newQuery.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<(List<GetIntegratedCSSByProjectIdModel> Data, int RowCount)> GetsIntegratedCSSByProjectId(
        long? contractorId,
        List<long>? contractorIds,
        long? costCenterId,
        long? projectId,
        long? contractorContractId,
        List<CSSStatus>? statuses,
        bool isPrimaryManager,
        bool isFinalManager,
        string? filterData,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        IQueryable<ContractorStatusStatement>? query = null;
        if (statuses is not null && statuses.Count > 0)
        {
            query = DbSet
                .Where(oo =>
                    (contractorIds == null || contractorIds.Contains(oo.ContractorId!.Value)) &&
                    (contractorId == null || oo.ContractorStatusStatementDetails.Any(x => x.ContractorContract.ContractorContractHeader.ContractorId.Equals(contractorId))) &&
                    (costCenterId == null || oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                    (projectId == null || oo.Project.Id == projectId) &&
                    (contractorContractId == null || oo.ContractorStatusStatementDetails.Any(x => x.ContractorContract.ContractorContractHeader.Id.Equals(contractorContractId))) &&
                    (string.IsNullOrWhiteSpace(filterData) || oo.Code.Equals(filterData)) &&
                    (statuses == null || statuses.Contains(oo.Status)));
        }
        else
        {
            query = DbSet
                .Where(oo =>
                    (contractorIds == null || contractorIds.Contains(oo.ContractorId!.Value)) &&
                    (contractorId == null || oo.ContractorStatusStatementDetails.Any(x => x.ContractorContract.ContractorContractHeader.ContractorId.Equals(contractorId))) &&
                    (costCenterId == null || oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                    (projectId == null || oo.Project.Id == projectId) &&
                    (contractorContractId == null || oo.ContractorStatusStatementDetails.Any(x => x.ContractorContract.ContractorContractHeader.Id.Equals(contractorContractId))) &&
                    (string.IsNullOrWhiteSpace(filterData) || oo.Code.Equals(filterData)) &&
                    (oo.Status == CSSStatus.ManagementConfirmed) &&
                    (statuses == null || statuses.Contains(oo.Status)));

            if (isPrimaryManager == true)
                query = query.Where(x => x.Status == CSSStatus.ManagementConfirmed && x.PrimaryManagerConfirmed == false);

            if (isFinalManager == true)
                query = query.Where(x => x.Status == CSSStatus.ManagementConfirmed && x.FinalManagerConfirmed == false);
        }

        var newQuery = query.Select(css => new GetIntegratedCSSByProjectIdModel()
        {
            Id = css.Id,
            Code = css.Code,
            CostCenterId = css.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
            CostCenterReferenceCode = css.Project.ProjectCostCenters.FirstOrDefault().CostCenter.PreferentialReferenceCode,
            CostCenterName = css.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
            ProjectId = css.Project.Id,
            ProjectReferenceCode = css.Project.PreferentialReferenceCode,
            Project = css.Project.ProjectName,
            ProjectCode = css.Project.ProjectCode,
            ContractorId = css.ContractorId,
            ManagementConfirmedAmount = css.ManagementConfirmedAmount,
            ManagementDescription = css.ManagmentDescription,
            PrimaryManagerConfirmed = css.PrimaryManagerConfirmed,
            PrimaryManagerConfirmedAmount = css.PrimaryManagerConfirmedAmount,
            PrimaryManagerDescription = css.PrimaryManagerDescription,
            FinalManagerConfirmed = css.FinalManagerConfirmed,
            FinalManagerConfirmedAmount = css.FinalManagerConfirmedAmount,
            FinalManagerDescription = css.FinalManagerDescription,
            Paymented = css.PaymentedAmount,
            Urls = css.ContractorStatusStatementDocuments
                .Where(x => x.ContractorStatusStatementPayment == null).Select(x => x.Url).ToList(),
        });

        var count = await newQuery.CountAsync(ct);

        var entities = await newQuery.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<List<long>?> GetCSSCreators(
        CT ct)
    {
        var query = DbSet
            .Select(x => x.CreatorId).Distinct();

        return await query.ToListAsync(ct);
    }
}