using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.Extensions.StringExtensions;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Gita.Backend.Shared.Application.WebServices.TreasuryServices.PaymentOrders.Models;
using System.Text;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreateRequestMachineryPaymentOrder;

public class CreateRequestMachineryPaymentOrderCommandHandler : ICommandHandler<CreateRequestMachineryPaymentOrderCommand, CreatePaymentOrderWithAutoDetailResponseModel?>
{
    private readonly ILogger<CreateRequestMachineryPaymentOrderCommandHandler> _logger;
    private readonly ITreasuryService _treasuryService;
    private readonly IServiceInfoRepository _serviceInfoRepository;
    private readonly IUserInfoService _userInfoService;

    public CreateRequestMachineryPaymentOrderCommandHandler(
        ILogger<CreateRequestMachineryPaymentOrderCommandHandler> logger,
        ITreasuryService treasuryService,
        IUserInfoService userInfoService,
        IServiceInfoRepository serviceInfoRepository)
    {
        _logger = logger;
        _treasuryService = treasuryService;
        _serviceInfoRepository = serviceInfoRepository;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreatePaymentOrderWithAutoDetailResponseModel?>> Handle(CreateRequestMachineryPaymentOrderCommand request, CT ct)
    {
        try
        {
            var servieInfo = await _serviceInfoRepository.FindByName("بدون خدمت", _userInfoService.UserCompanyId, ct);

            string? referenceDetails = null;
            var requestMachineryStatusStatement = request.RequestMachineryStatusStatement!;
            var season = request.Season!;
            var branch = request.Season.Branch!;
            var category = request.Season.Branch.Category!;

            List<PaymentOrderAttachmentViaSubSystemRequest>? attachments = [];

            List<CreatePaymentOrderViaSubSystemCostDetailRequest>? costs = [];
            List<CreatePaymentOrderViaSubSystemCostDetailRequest>? costDetails = [];

            var details = requestMachineryStatusStatement.RequestMachineryStatusStatementDetails.ToList();

            var projects = details.Select(x => x.Project).ToList();
            foreach (var detail in details)
            {
                var costDetail = new CreatePaymentOrderViaSubSystemCostDetailRequest(
                    (Guid)detail.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.PreferentialReferenceCode!,
                    (Guid)detail.Project?.PreferentialReferenceCode!,
                    detail.FinalPrice,
                    category.PreferentialReferenceCode,
                    branch.PreferentialReferenceCode,
                    season.PreferentialReferenceCode,
                    servieInfo is null ? null : servieInfo.PreferentialReferenceCode,
                    1,
                    null,
                    null,
                    detail.FinalPrice,
                    null,
                    detail.FinalPrice,
                    servieInfo is null ? null : servieInfo.ServiceInfoName,
                    null,
                    request.CostCategoryId,
                    request.CostGroupId,
                    requestMachineryStatusStatement.Id,
                    request.DocumentTypeId,
                    request.PreferentialTypeId);

                costs.Add(costDetail);
            }

            var groupedCosts = costs
                .GroupBy(item => new { item.ProjectGuid, item.CostCenterGuid, item.CategoryGuid, item.BranchGuid, item.SeasonGuid })
                .Select(group => new CreatePaymentOrderViaSubSystemCostDetailRequest(
                    group.Key.CostCenterGuid,
                    group.Key.ProjectGuid,
                    group.Sum(item => item.Amount),
                    group.Key.CategoryGuid,
                    group.Key.BranchGuid,
                    group.Key.SeasonGuid,
                    servieInfo is null ? null : servieInfo.PreferentialReferenceCode,
                    1,
                    null,
                    null,
                    group.Sum(item => item.Amount),
                    null,
                    group.Sum(item => item.Amount),
                    servieInfo is null ? null : servieInfo.ServiceInfoName,
                    null,
                    request.CostCategoryId,
                    request.CostGroupId,
                    requestMachineryStatusStatement.Id,
                    request.DocumentTypeId,
                    request.PreferentialTypeId)).ToList();

            costDetails = groupedCosts;

            var costCenterGuid = details.FirstOrDefault()?.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.PreferentialReferenceCode;
            var projectGuid = details.FirstOrDefault()?.Project.PreferentialReferenceCode;

            var currenyId = details.FirstOrDefault()?.CurrencyId;

            var docs = details.SelectMany(x => x.RequestMachinery.RequestMachineryDocuments).ToList();

            if (docs is not null && docs.Count > 0)
            {
                var index = 1;
                var urls = docs.Where(x => !string.IsNullOrEmpty(x.Url)).Select(x => x.Url).ToList();
                foreach (var url in urls)
                {
                    var title = $"RequestMachineryStatusStatement-{requestMachineryStatusStatement.Id.ToString()}-{index}";
                    attachments.Add(new PaymentOrderAttachmentViaSubSystemRequest(index.ToString(), title, url, (int)PaymentOrderAttachmentType.Primary));
                    index++;
                }
            }

            referenceDetails = BuildReferenceDetails(requestMachineryStatusStatement, details, request.ThirdParty, ct);

            Guid? pettyCashGuid = null;
            if (request.IsPettyCash == true)
                if (!string.IsNullOrEmpty(request.pettyCashId))
                    pettyCashGuid = Guid.Parse(request.pettyCashId);

            var result = await _treasuryService.CreatePaymentOrderWithAutoDetail(new CreatePaymentOrderWithAutoDetailRequest(
                    currenyId!.Value,
                    request.ThirdParty!.PreferentialReferenceCode,
                    PaymentOrderTypeCode: "4",
                    ReferenceId: requestMachineryStatusStatement.Id,
                    requestMachineryStatusStatement.Id.ToString(),
                    referenceDetails,
                    costCenterGuid,
                    projectGuid,
                    DateTime.Now.Date,
                    requestMachineryStatusStatement.PaymentDate == null ? requestMachineryStatusStatement.Created : requestMachineryStatusStatement.PaymentDate.Value,
                    request.ConfirmedPrice ?? 0,
                    0,
                    0,
                    0,
                    0,
                    request.ConfirmedPrice ?? 0,
                    request.Description ?? " ",
                    request.ConfirmedBankAccountId,
                    attachments,
                    costDetails,
                    request.IsPettyCash,
                    pettyCashGuid,
                    request.CostCategoryId,
                    request.CostGroupId,
                    request.DocumentTypeId,
                    request.PreferentialTypeId), ct);
            if (result is null || result.IsFailure)
                return Result.Failure<CreatePaymentOrderWithAutoDetailResponseModel>(TreasuryErrors.ProviderError(result?.Error));

            return new CreatePaymentOrderWithAutoDetailResponseModel(result.Value!.Id);
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var response = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            response!.Error.StatusCode = 422;
            return Result.Failure<CreatePaymentOrderWithAutoDetailResponseModel?>(TreasuryErrors.ProviderError(response!.Error.Message!));
        }
    }

    private string? BuildReferenceDetails(RequestMachineryStatusStatement statusStatement, List<RequestMachineryStatusStatementDetail> statusStatementDetails, ThirdPartyByIdModel? thirdParty, CT ct)
    {
        var sb = new StringBuilder();

        var machineries = statusStatement.RequestMachineryStatusStatementDetails.Select(oo => oo.Machinery).ToList();
        var costCenters = statusStatementDetails.Select(x => x.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter).Distinct().ToList();
        var projects = statusStatementDetails.Select(x => x.Project).Distinct().ToList();

        sb.AppendLine($"شماره صورت وضعیت ماشین آلات: {statusStatement.Id.ToString()}");
        sb.AppendLine($"پیمانکار: {thirdParty?.FullName ?? string.Empty}");

        sb.AppendLine();
        sb.AppendLine("مرکز هزینه:");
        sb.AppendLine($"- {costCenters?.Select(x => x.CostCenterName).DashJoinList() ?? string.Empty}");

        sb.AppendLine();
        sb.AppendLine("پروژه:");
        sb.AppendLine($"- {projects?.Select(x => x.ProjectName).DashJoinList() ?? string.Empty}");

        var index = 1;
        foreach (var detail in statusStatementDetails)
        {
            var machinery = machineries?.FirstOrDefault(x => x?.Id == detail.Machinery.Id);
            if (machinery is null) continue;

            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine($"{index}- {machinery.MachineryName}");
            sb.AppendLine($"تعداد: {detail.RequestedCount:#,##0.00}");
            sb.AppendLine($"مبلغ واحد: {detail.FinalPrice:#,##0.00}");
            sb.AppendLine($"مبلغ نهایی: {detail.RequestedCount * detail.FinalPrice:#,##0}");
        }

        sb.AppendLine();
        sb.AppendLine();
        sb.AppendLine($"قیمت نهایی: {statusStatement.TotalFinalPrice:#,##0}");
        sb.AppendLine($"هزینه پیمانکار: {statusStatement.ContractorPrice:#,##0.00}");

        return sb.ToString();
    }
}
