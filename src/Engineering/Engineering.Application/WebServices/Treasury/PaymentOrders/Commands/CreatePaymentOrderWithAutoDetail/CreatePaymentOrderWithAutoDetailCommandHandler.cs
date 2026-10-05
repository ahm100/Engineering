using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Domain.Entities.ContractorStatusStatements;
using Gita.Backend.Shared.Application.WebServices.TreasuryServices.PaymentOrders.Models;
using System.Text;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreatePaymentOrderWithAutoDetail;

public class CreatePaymentOrderWithAutoDetailCommandHandler : ICommandHandler<CreatePaymentOrderWithAutoDetailCommand, CreatePaymentOrderWithAutoDetailResponseModel?>
{
    private readonly ILogger<CreatePaymentOrderWithAutoDetailCommandHandler> _logger;
    private readonly ITreasuryService _treasuryService;
    private readonly IServiceInfoRepository _serviceInfoRepository;
    private readonly IUserInfoService _userInfoService;

    public CreatePaymentOrderWithAutoDetailCommandHandler(
        ILogger<CreatePaymentOrderWithAutoDetailCommandHandler> logger,
        ITreasuryService treasuryService,
        IUserInfoService userInfoService,
        IServiceInfoRepository serviceInfoRepository)
    {
        _logger = logger;
        _treasuryService = treasuryService;
        _serviceInfoRepository = serviceInfoRepository;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreatePaymentOrderWithAutoDetailResponseModel?>> Handle(CreatePaymentOrderWithAutoDetailCommand request, CT ct)
    {
        try
        {
            var servieInfo = await _serviceInfoRepository.FindByName("بدون خدمت", _userInfoService.UserCompanyId, ct);

            string? referenceDetails = null;
            var statusStatement = request.StatusStatement!;
            var season = request.Season!;
            var branch = request.Season.Branch!;
            var category = request.Season.Branch.Category!;
            var currenyId = statusStatement.CurrencyId!.Value;

            var attachments = new List<PaymentOrderAttachmentViaSubSystemRequest>();
            var costDetail = new CreatePaymentOrderViaSubSystemCostDetailRequest(
                (Guid)statusStatement?.Project!.ProjectCostCenters.FirstOrDefault()!.CostCenter.PreferentialReferenceCode!,
                (Guid)statusStatement?.Project!.PreferentialReferenceCode!,
                request.ConfirmedPrice ?? 0,
                category.PreferentialReferenceCode,
                branch.PreferentialReferenceCode,
                season.PreferentialReferenceCode,
                servieInfo is null ? null : servieInfo.PreferentialReferenceCode,
                1,
                null,
                null,
                null,
                null,
                null,
                servieInfo is null ? null : servieInfo.ServiceInfoName,
                null,
                request.CostCategoryId,
                request.CostGroupId,
                statusStatement.Id,
                request.DocumentTypeId,
                request.PreferentialTypeId);

            referenceDetails = BuildReferenceDetails(statusStatement, costDetail.Amount, request.ThirdParty, request.PrimaryManager, request.FinalManager, ct);

            var index = 1;
            var title = $"RequestMachineryStatusStatement-{statusStatement.Id.ToString()}-{index}";

            foreach (var doc in statusStatement.ContractorStatusStatementDocuments)
                attachments.Add(new PaymentOrderAttachmentViaSubSystemRequest(index.ToString(), title, doc.Url, (int)PaymentOrderAttachmentType.Primary));

            if (request.Urls is not null && request.Urls.Count > 0)
                foreach (var url in request.Urls)
                    attachments.Add(new PaymentOrderAttachmentViaSubSystemRequest(index.ToString(), title, url, (int)PaymentOrderAttachmentType.Primary));

            var result = await _treasuryService.CreatePaymentOrderWithAutoDetail(new CreatePaymentOrderWithAutoDetailRequest(
                    currenyId,
                    request.ThirdParty!.PreferentialReferenceCode,
                    PaymentOrderTypeCode: "5",
                    ReferenceId: statusStatement.Id,
                    statusStatement.Code,
                    referenceDetails,
                    statusStatement.Project!.ProjectCostCenters.FirstOrDefault()?.CostCenter.PreferentialReferenceCode,
                    statusStatement.Project!.PreferentialReferenceCode,
                    DateTime.Now.Date,
                    request.ConfirmedPaymentDate!.Value,
                    costDetail.Amount,
                    0,
                    0,
                    0,
                    0,
                    costDetail.Amount,
                    request.Description ?? " ",
                    request.ConfirmedBankAccountId!.Value,
                    attachments,
                    [costDetail],
                    false,
                    null,
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
    private string? BuildReferenceDetails(
       ContractorStatusStatement statusStatement,
       decimal paymentAmount,
       ThirdPartyByIdModel? thirdParty,
       ManagerDataModel? primaryManager,
       ManagerDataModel? finalManager,
       CT ct)
    {
        try
        {
            var sb = new StringBuilder();

            var costCenter = statusStatement.Project!.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName;
            var project = statusStatement.Project.ProjectName;

            sb.AppendLine($"شماره صورت وضعیت پیمانکار: {statusStatement.Code}");
            sb.AppendLine($"پیمانکار: {thirdParty?.FullName ?? string.Empty}");

            sb.AppendLine();
            sb.AppendLine($"مرکز هزینه: {costCenter}");

            sb.AppendLine();
            sb.AppendLine($"پروژه: {project}");

            sb.AppendLine();
            sb.AppendLine($"قیمت نهایی: {paymentAmount:#,##0}");

            sb.AppendLine();
            sb.AppendLine($"وضعیت تایید مدیر اول: {primaryManager?.StatusDescription} ({primaryManager?.FullName})");

            sb.AppendLine();
            sb.AppendLine($"توضیحات: {primaryManager?.Description}");

            sb.AppendLine();
            sb.AppendLine($"وضعیت تایید مدیر دوم: {finalManager?.StatusDescription} ({finalManager?.FullName})");

            sb.AppendLine();
            sb.AppendLine($"توضیحات: {finalManager?.Description}");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }
}
