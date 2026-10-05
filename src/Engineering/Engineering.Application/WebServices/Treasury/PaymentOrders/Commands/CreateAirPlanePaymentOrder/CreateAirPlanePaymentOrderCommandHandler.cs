using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Domain.Entities.Transportations;
using Gita.Backend.Shared.Application.WebServices.TreasuryServices.PaymentOrders.Models;
using System.Text;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreateAirPlanePaymentOrder;

public class CreateAirPlanePaymentOrderCommandHandler : ICommandHandler<CreateAirPlanePaymentOrderCommand, CreatePaymentOrderWithAutoDetailResponseModel?>
{
    private readonly ILogger<CreateAirPlanePaymentOrderCommandHandler> _logger;
    private readonly ITreasuryService _treasuryService;
    private readonly IServiceInfoRepository _serviceInfoRepository;
    private readonly IUserInfoService _userInfoService;

    public CreateAirPlanePaymentOrderCommandHandler(ILogger<CreateAirPlanePaymentOrderCommandHandler> logger,
        ITreasuryService treasuryService,
        IUserInfoService userInfoService,
        IServiceInfoRepository serviceInfoRepository)
    {
        _logger = logger;
        _treasuryService = treasuryService;
        _serviceInfoRepository = serviceInfoRepository;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreatePaymentOrderWithAutoDetailResponseModel?>> Handle(CreateAirPlanePaymentOrderCommand request, CT ct)
    {
        try
        {
            var servieInfo = await _serviceInfoRepository.FindByName("بدون خدمت", _userInfoService.UserCompanyId, ct);
            string? referenceDetails = null;
            var transportationRequest = request.AirPlane!;
            var season = request.Season!;
            var branch = request.Season.Branch!;
            var category = request.Season.Branch.Category!;

            List<PaymentOrderAttachmentViaSubSystemRequest>? attachments = [];
            List<CreatePaymentOrderViaSubSystemCostDetailRequest>? costDetails = [];

            var transportationCostCenters = transportationRequest.TransportationRequestCostCenters.Select(x => x.CostCenter).ToList();
            foreach (var costCenter in transportationCostCenters)
            {
                var transportPrice = transportationRequest.Price / transportationCostCenters.Count;

                var transportationProjects = transportationRequest.TransportationRequestProjects?.Where(x => x.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter == costCenter).Select(x => x.Project).ToList();
                if (transportationProjects is not null && transportationProjects.Count > 0)
                {
                    var transportProjectPrice = transportPrice / transportationProjects.Count;

                    foreach (var project in transportationProjects)
                    {
                        var costDetail = new CreatePaymentOrderViaSubSystemCostDetailRequest(
                        (Guid)costCenter.PreferentialReferenceCode!,
                        (Guid)project?.PreferentialReferenceCode!,
                        transportProjectPrice ?? 0,
                        category.PreferentialReferenceCode,
                        branch.PreferentialReferenceCode,
                        season.PreferentialReferenceCode,
                        servieInfo is null ? null : servieInfo.PreferentialReferenceCode,
                        1,
                        0,
                        0,
                        transportProjectPrice,
                        0,
                        transportProjectPrice,
                        servieInfo is null ? null : servieInfo.ServiceInfoName,
                        null,
                        request.CostCategoryId,
                        request.CostGroupId,
                        transportationRequest.Id,
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
                       transportPrice ?? 0,
                       category.PreferentialReferenceCode,
                       branch.PreferentialReferenceCode,
                       season.PreferentialReferenceCode,
                       null,
                       1,
                       0,
                       0,
                       transportPrice ?? 0,
                       0,
                       transportPrice ?? 0,
                       "هواپیما",
                       null,
                       request.CostCategoryId,
                       request.CostGroupId,
                       transportationRequest.Id,
                       request.DocumentTypeId,
                       request.PreferentialTypeId);

                    costDetails.Add(costDetail);
                }
            }

            var costCenterGuid = transportationRequest.TransportationRequestCostCenters.FirstOrDefault()?.CostCenter.PreferentialReferenceCode;
            var projectGuid = transportationRequest.TransportationRequestProjects?.FirstOrDefault()?.Project.PreferentialReferenceCode;

            var currenyId = transportationRequest.CurrencyUnitId;

            var index = 1;
            if (request.AirPlane.TransportationRequestDocuments is not null && request.AirPlane.TransportationRequestDocuments.Count > 0)
            {
                var urls = request.AirPlane.TransportationRequestDocuments.Where(x => !string.IsNullOrEmpty(x.Url)).Select(x => x.Url).ToList();
                foreach (var url in urls)
                {
                    var title = $"TransportationRequest-{transportationRequest.Id.ToString()}-{index}";
                    attachments.Add(new PaymentOrderAttachmentViaSubSystemRequest(index.ToString(), title, url, (int)PaymentOrderAttachmentType.Primary));
                    index++;
                }
            }

            referenceDetails = BuildReferenceDetails(transportationRequest, request.ThirdParty, ct);

            Guid? pettyCashGuid = null;
            if (request.IsPettyCash == true)
                if (!string.IsNullOrEmpty(request.PettyCashId))
                    pettyCashGuid = Guid.Parse(request.PettyCashId);

            var result = await _treasuryService.CreatePaymentOrderWithAutoDetail(new CreatePaymentOrderWithAutoDetailRequest(
                    currenyId!.Value,
                    request.ThirdParty?.PreferentialReferenceCode,
                    PaymentOrderTypeCode: "6",
                    ReferenceId: transportationRequest.Id,
                    transportationRequest.RequestNumber.ToString(),
                    referenceDetails,
                    costCenterGuid,
                    projectGuid ?? null,
                    DateTime.Now.Date,
                    request.ConfirmedPaymentDate!.Value,
                    transportationRequest.Price!.Value,
                    0,
                    0,
                    0,
                    0,
                    transportationRequest.Price!.Value,
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

    private string? BuildReferenceDetails(
        TransportationRequest transportationRequest,
        ThirdPartyByIdModel? thirdParty,
        CT ct)
    {
        try
        {
            var sb = new StringBuilder();

            var costCenters = transportationRequest.TransportationRequestCostCenters.Select(x => x.CostCenter.CostCenterName).ToList();
            var projects = transportationRequest.TransportationRequestProjects.Select(x => x.Project.ProjectName).ToList();

            sb.AppendLine($"شماره درخواست هواپیما: {transportationRequest.RequestNumber.ToString()}");
            sb.AppendLine($"پرداخت کننده بلیط: {thirdParty?.FullName ?? string.Empty}");

            sb.AppendLine();
            sb.AppendLine($"مرکز هزینه ها: {string.Join("-", costCenters)}");

            sb.AppendLine();
            sb.AppendLine($"پروژه ها: {string.Join("-", projects)}");

            sb.AppendLine();
            sb.AppendLine($"از تاریخ: {TimeCalculator.ConvertToShamsi(transportationRequest.StartDate)}");

            sb.AppendLine();
            sb.AppendLine($"تا تاریخ: {TimeCalculator.ConvertToShamsi(transportationRequest.EndDate)}");

            sb.AppendLine();
            sb.AppendLine($"توضیحات: شماره درخواست -  {transportationRequest.RequestNumber.ToString()}");
            sb.AppendLine($"{transportationRequest.Description}");

            sb.AppendLine();
            sb.AppendLine($"قیمت نهایی: {transportationRequest.Price:#,##0}");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
    }
}
