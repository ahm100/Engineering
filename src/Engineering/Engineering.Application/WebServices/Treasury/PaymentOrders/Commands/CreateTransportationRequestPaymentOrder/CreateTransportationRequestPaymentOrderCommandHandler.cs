using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Domain.Entities.Transportations;
using Gita.Backend.Shared.Application.WebServices.TreasuryServices.PaymentOrders.Models;
using System.Text;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreateTransportationRequestPaymentOrder;

public class CreateTransportationRequestPaymentOrderCommandHandler : ICommandHandler<CreateTransportationRequestPaymentOrderCommand, CreatePaymentOrderWithAutoDetailResponseModel?>
{
    private readonly ILogger<CreateTransportationRequestPaymentOrderCommandHandler> _logger;
    private readonly ITreasuryService _treasuryService;
    private readonly IServiceInfoRepository _serviceInfoRepository;
    private readonly IUserInfoService _userInfoService;

    public CreateTransportationRequestPaymentOrderCommandHandler(
        ILogger<CreateTransportationRequestPaymentOrderCommandHandler> logger,
        ITreasuryService treasuryService,
        IUserInfoService userInfoService,
        IServiceInfoRepository serviceInfoRepository)
    {
        _logger = logger;
        _treasuryService = treasuryService;
        _serviceInfoRepository = serviceInfoRepository;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreatePaymentOrderWithAutoDetailResponseModel?>> Handle(CreateTransportationRequestPaymentOrderCommand request, CT ct)
    {
        try
        {
            var servieInfo = await _serviceInfoRepository.FindByName("بدون خدمت", _userInfoService.UserCompanyId, ct);
            string? referenceDetails = null;
            var transportationRequest = request.TransportationRequest!;
            var season = request.Season!;
            var branch = request.Season.Branch!;
            var category = request.Season.Branch.Category!;

            List<PaymentOrderAttachmentViaSubSystemRequest>? attachments = [];
            List<CreatePaymentOrderViaSubSystemCostDetailRequest>? costDetails = [];

            var i = 0;
            decimal costcenterAmount = 0;
            var transportationCostCenters = transportationRequest.TransportationRequestCostCenters.Select(x => x.CostCenter).ToList();
            foreach (var costCenter in transportationCostCenters)
            {
                decimal transportPrice = Math.Round(transportationRequest.Price!.Value / transportationCostCenters.Count, 2);
                if (transportationCostCenters.Count > 2 && transportationCostCenters.Count % 2 != 0)
                {
                    costcenterAmount += transportPrice;
                    if (transportationCostCenters.Count == i + 1)
                    {
                        var diff = transportationRequest.Price.Value - costcenterAmount;
                        transportPrice += diff;
                    }
                    i++;
                }

                var j = 0;
                decimal projectAmount = 0;
                var transportationProjects = transportationRequest.TransportationRequestProjects?.Where(x => x.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter == costCenter).Select(x => x.Project).ToList();
                if (transportationProjects is not null && transportationProjects.Count > 0)
                {
                    foreach (var project in transportationProjects)
                    {
                        var transportProjectPrice = Math.Round(transportPrice! / transportationProjects.Count, 2);
                        if (transportationProjects.Count > 2 && transportationProjects.Count % 2 != 0)
                        {
                            projectAmount += transportProjectPrice;
                            if (transportationProjects.Count == j + 1)
                            {
                                var diff = transportPrice - projectAmount;
                                transportProjectPrice += diff;
                            }
                            j++;
                        }

                        var costDetail = new CreatePaymentOrderViaSubSystemCostDetailRequest(
                        (Guid)costCenter.PreferentialReferenceCode!,
                        (Guid)project?.PreferentialReferenceCode!,
                        transportProjectPrice,
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
                       transportPrice,
                       category.PreferentialReferenceCode,
                       branch.PreferentialReferenceCode,
                       season.PreferentialReferenceCode,
                       null,
                       1,
                       0,
                       0,
                       transportPrice,
                       0,
                       transportPrice,
                       "ترابری",
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
            if (!string.IsNullOrEmpty(request.TransportationRequest.BillOfLadingImage))
            {
                var title = $"TransportationRequest-{transportationRequest.Id.ToString()}-{index}";
                attachments.Add(new PaymentOrderAttachmentViaSubSystemRequest(index.ToString(), title, request.TransportationRequest.BillOfLadingImage, (int)PaymentOrderAttachmentType.Primary));
                index++;
            }

            if (request.TransportationRequest.TransportationRequestDocuments is not null && request.TransportationRequest.TransportationRequestDocuments.Count > 0)
            {
                var urls = request.TransportationRequest.TransportationRequestDocuments.Where(x => !string.IsNullOrEmpty(x.Url)).Select(x => x.Url).ToList();
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

            sb.AppendLine($"شماره درخواست ترابری: {transportationRequest.RequestNumber.ToString()}");
            sb.AppendLine($"راننده: {thirdParty?.FullName ?? string.Empty}");

            sb.AppendLine();
            sb.AppendLine($"مرکز هزینه ها: {string.Join("-", costCenters)}");

            sb.AppendLine();
            sb.AppendLine($"پروژه ها: {string.Join("-", projects)}");

            sb.AppendLine();
            sb.AppendLine($"از تاریخ: {TimeCalculator.ConvertToShamsi(transportationRequest.StartDate)}");

            sb.AppendLine();
            sb.AppendLine($"تا تاریخ: {TimeCalculator.ConvertToShamsi(transportationRequest.EndDate)}");

            sb.AppendLine();
            sb.AppendLine($"نوع ماشین: {transportationRequest.MachineType?.MachineTypeTitle}");

            sb.AppendLine();
            sb.AppendLine($"نوع بارنامه: {transportationRequest.BillOfLading?.BillOfLadingName}");

            sb.AppendLine();
            sb.AppendLine($"شماره بارنامه: {transportationRequest.FreightNumber}");

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
