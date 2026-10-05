using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Domain.Entities.Transportations;
using Gita.Backend.Shared.Application.WebServices.TreasuryServices.PaymentOrders.Models;
using System.Text;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreateSnapPaymentOrder;

public class CreateSnapPaymentOrderCommandHandler : ICommandHandler<CreateSnapPaymentOrderCommand, CreatePaymentOrderWithAutoDetailResponseModel?>
{
    private readonly ILogger<CreateSnapPaymentOrderCommandHandler> _logger;
    private readonly ITreasuryService _treasuryService;
    private readonly IServiceInfoRepository _serviceInfoRepository;
    private readonly IUserInfoService _userInfoService;

    public CreateSnapPaymentOrderCommandHandler(ILogger<CreateSnapPaymentOrderCommandHandler> logger,
        ITreasuryService treasuryService,
        IUserInfoService userInfoService,
        IServiceInfoRepository serviceInfoRepository)
    {
        _logger = logger;
        _treasuryService = treasuryService;
        _serviceInfoRepository = serviceInfoRepository;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreatePaymentOrderWithAutoDetailResponseModel?>> Handle(CreateSnapPaymentOrderCommand request, CT ct)
    {
        try
        {
            var servieInfo = await _serviceInfoRepository.FindByName("بدون خدمت", _userInfoService.UserCompanyId, ct);
            string? referenceDetails = null;
            var snaps = request.SnapRequests!;
            var season = request.Season!;
            var branch = request.Season.Branch!;
            var category = request.Season.Branch.Category!;

            List<PaymentOrderAttachmentViaSubSystemRequest>? attachments = [];
            List<CreatePaymentOrderViaSubSystemCostDetailRequest>? costDetails = [];

            foreach (var snap in snaps)
            {
                var thirdParty = request.ThirdParties?.FirstOrDefault(x => x.Id == snap.SnapRequester);

                var transportationCostCenters = snap.TransportationRequestCostCenters.Select(x => x.CostCenter).ToList();
                if (transportationCostCenters is not null && transportationCostCenters.Count > 0)
                {
                    var snapFareAmount = snap.Price / transportationCostCenters.Count;
                    foreach (var costCenter in transportationCostCenters)
                    {
                        var transportationProjects = snap.TransportationRequestProjects.Where(x => x.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter == costCenter).Select(x => x.Project).ToList();
                        if (transportationProjects is not null && transportationProjects.Count > 0)
                        {
                            var snapProjectFareAmount = snapFareAmount / transportationProjects.Count;
                            foreach (var project in transportationProjects)
                            {
                                var costDetail = new CreatePaymentOrderViaSubSystemCostDetailRequest(
                                (Guid)costCenter.PreferentialReferenceCode!,
                                (Guid)project.PreferentialReferenceCode!,
                                snapProjectFareAmount ?? 0,
                                category.PreferentialReferenceCode,
                                branch.PreferentialReferenceCode,
                                season.PreferentialReferenceCode,
                                servieInfo is null ? null : servieInfo.PreferentialReferenceCode,
                                1,
                                0,
                                0,
                                snapProjectFareAmount,
                                0,
                                snapProjectFareAmount,
                                servieInfo is null ? null : servieInfo.ServiceInfoName,
                                thirdParty?.PreferentialReferenceCode,
                                request.CostCategoryId ?? snap.CostCategoryId,
                                request.CostGroupId ?? snap.CostGroupId,
                                snap.Id,
                                request.DocumentTypeId,
                                request.PreferentialTypeId);

                                costDetails.Add(costDetail);
                            }
                        }
                        else
                        {
                            var costDetail = new CreatePaymentOrderViaSubSystemCostDetailRequest(
                                (Guid)costCenter.PreferentialReferenceCode!,
                                null,
                                snapFareAmount ?? 0,
                                category.PreferentialReferenceCode,
                                branch.PreferentialReferenceCode,
                                season.PreferentialReferenceCode,
                                null,
                                1,
                                0,
                                0,
                                snapFareAmount,
                                0,
                                snapFareAmount,
                                "اسنپ",
                                thirdParty?.PreferentialReferenceCode,
                                request.CostCategoryId ?? snap.CostCategoryId,
                                request.CostGroupId ?? snap.CostGroupId,
                                snap.Id,
                                request.DocumentTypeId,
                                request.PreferentialTypeId);

                            costDetails.Add(costDetail);
                        }
                    }

                    var index = 1;
                    if (snap.TransportationRequestDocuments is not null && snap.TransportationRequestDocuments.Count > 0)
                    {
                        var urls = snap.TransportationRequestDocuments.Where(x => !string.IsNullOrEmpty(x.Url)).Select(x => x.Url).ToList();
                        foreach (var url in urls)
                        {
                            var title = $"TransportationRequest-{snap.Id.ToString()}-{index}";
                            attachments.Add(new PaymentOrderAttachmentViaSubSystemRequest(index.ToString(), title, url, (int)PaymentOrderAttachmentType.Primary));
                            index++;
                        }
                    }
                }
            }

            var thirdparty = request.ThirdParties?.FirstOrDefault(x => x.Id == request.ThirdpartyId);
            var name = request.IsPettyCash == true ? request.ThirdpartyName : thirdparty?.FullName;

            referenceDetails = BuildReferenceDetails(snaps, name, request.ConfirmedPrice, ct);

            var costCenterGuid = snaps.FirstOrDefault()?.TransportationRequestCostCenters.FirstOrDefault()?.CostCenter.PreferentialReferenceCode;
            var projectGuid = snaps.FirstOrDefault()?.TransportationRequestProjects.FirstOrDefault()?.Project.PreferentialReferenceCode;

            var currenyId = snaps.FirstOrDefault(x => x.CurrencyUnitId is not null && x.CurrencyUnitId > 0)?.CurrencyUnitId;
            if (currenyId == null || currenyId <= 0)
                currenyId = request.DefaultCurrencyId;

            if (currenyId == null)
                return Result.Failure<CreatePaymentOrderWithAutoDetailResponseModel>(TransportationRequestErrors.CurrencyIsEmpty);

            Guid? pettyCashGuid = null;
            if (request.IsPettyCash == true)
                if (!string.IsNullOrEmpty(request.PettyCashId))
                    pettyCashGuid = Guid.Parse(request.PettyCashId);

            var result = await _treasuryService.CreatePaymentOrderWithAutoDetail(new CreatePaymentOrderWithAutoDetailRequest(
                    currenyId!.Value,
                    request.IsPettyCash == false ? thirdparty?.PreferentialReferenceCode : null,
                    PaymentOrderTypeCode: "31",
                    ReferenceId: null,
                    null,
                    referenceDetails,
                    costCenterGuid,
                    projectGuid ?? null,
                    DateTime.Now.Date,
                    request.ConfirmedPaymentDate!.Value,
                    request.ConfirmedPrice ?? 0,
                    0,
                    0,
                    0,
                    0,
                    request.ConfirmedPrice ?? 0,
                    request.Description ?? " ",
                    request.IsPettyCash == false ? request.BankAccountId : null,
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

    private string? BuildReferenceDetails(
        List<TransportationRequest> SnapRequests,
        string? ThirdParty,
        decimal? Price,
        CT ct)
    {
        try
        {
            var sb = new StringBuilder();

            var costCenters = SnapRequests.SelectMany(x => x.TransportationRequestCostCenters).Select(x => x.CostCenter.CostCenterName).ToList();
            var projects = SnapRequests.SelectMany(x => x.TransportationRequestProjects).Select(x => x.Project.ProjectName).ToList();

            sb.AppendLine($"شماره درخواست های اسنپ: {string.Join("-", SnapRequests.Select(x => x.RequestNumber.ToString()).ToList())}");
            sb.AppendLine($"تنخواه گردان / طرف حساب: {ThirdParty ?? string.Empty}");

            sb.AppendLine();
            sb.AppendLine($"مرکز هزینه ها: {string.Join("-", costCenters)}");

            sb.AppendLine();
            sb.AppendLine($"پروژه ها: {string.Join("-", projects)}");

            sb.AppendLine();
            sb.AppendLine($"بازه درخواست از تاریخ: {TimeCalculator.ConvertToShamsi(SnapRequests.FirstOrDefault()?.StartDate)}");

            sb.AppendLine();
            sb.AppendLine($"بازه درخواست تا تاریخ: {TimeCalculator.ConvertToShamsi(SnapRequests.LastOrDefault()?.EndDate)}");

            sb.AppendLine();
            sb.AppendLine($"قیمت نهایی: {Price:#,##0}");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }
}
