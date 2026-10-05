using Engineering.Application.Services.ContractorMachineries.Queries.GetContractorMachineryById;
using Engineering.Application.Services.RequestMachineries.Commands.ChangeRequestMachineryStatus;
using Engineering.Application.Services.RequestMachineries.Commands.RequestMachineryAssignments;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryBillDocumentModel;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryDocumentModel;
using Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryById;
using Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiry;
using Engineering.Application.Services.RequestMachineryManagements.Commands.CreateRequestMachineryInquiryOperator;
using Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryAssignments;
using Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryInquiries;
using Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryReservations;
using Engineering.Application.Services.RequestMachineryManagements.Commands.RemoveRequestContractorMachinery;
using Engineering.Application.Services.RequestMachineryManagements.Commands.SetUnConfirmRequestMachinery;
using Engineering.Application.Services.RequestMachineryManagements.Models.AssignMachineryForRequestMachinery;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetFilteredRequestMachineryInquieries;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetFilteredRequestMachineryManagements;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryInquiries;
using Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryManagementById;
using Engineering.Application.Services.TelegramChats.Queries.GetsTelegramChatByCostCenterIds;
using Engineering.Application.Services.TelegramChats.TelegramServices;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models;
using Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators.Queries.GetMachineryOperators;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.ContractorMachineries;
using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using Engineering.Domain.Entities.Messengers.Enums;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Gita.Backend.Shared.Application.Shared.Models.UserProfiles;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetUserById;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.RequestMachineryManagements;

public partial class RequestMachineryManagementLogic : IRequestMachineryManagementLogic
{
    private List<long> IdCollectors(List<RequestMachinery?> items)
    {
        var allIds = new List<long>();
        foreach (var item in items)
        {
            if (item!.CreatorId != 0)
                allIds.Add(item!.CreatorId);

            if (item!.OperatorAppoinmentUserId != 0 && item.OperatorAppoinmentUserId != null)
                allIds.Add(item!.OperatorAppoinmentUserId.Value);

            if (item!.Histories is not null && item.Histories.Count > 0)
                foreach (var item1 in item!.Histories)
                    allIds.Add(item1!.CreatorId);
        }

        return allIds.Where(x => x != 0).Distinct().ToList();
    }

    private List<long> RequestMachineryHistoryIdCollectors(List<RequestMachineryHistory?> items)
    {
        var allIds = new List<long>();
        foreach (var item in items)
        {
            if (item!.CreatorId != 0)
                allIds.Add(item!.CreatorId);
        }

        return allIds.Where(x => x != 0).Distinct().ToList();
    }

    private GetRequestMachineryInquiriesResponseModel? InquiryDataModeling(RequestMachineryInquiry inquiry, List<Currency>? currencies, List<UserModel?>? thirdParties)
    {
        var inquiryModel = new GetRequestMachineryInquiriesResponseModel();

        var documents = new List<GetRequestMachineryInquiryDetailByIdDocumentModelResponse>();
        foreach (var commerceRequestInquiryDocument in inquiry.RequestMachineryInquiryDocuments)
        {
            documents.Add(new GetRequestMachineryInquiryDetailByIdDocumentModelResponse()
            {
                Id = commerceRequestInquiryDocument.Id,
                Url = commerceRequestInquiryDocument.Url
            });
        }

        var currency = currencies?.Where(x => x.Id == inquiry.CurrencyId).FirstOrDefault();
        var thirdParty = thirdParties?.Where(x => x?.Id == inquiry.ThirdPartyId).FirstOrDefault();
        var operatorUser = thirdParties?.Where(x => x?.Id == inquiry.RequestMachineryInquiryOperator.OperatorAppoinmentId).FirstOrDefault();

        inquiryModel = new GetRequestMachineryInquiriesResponseModel()
        {
            RequestMachineryInquiryId = inquiry.Id,
            Count = inquiry.Count,
            ThirdPartyId = inquiry.ThirdPartyId,
            TotalPrice = inquiry.TotalPrice,
            Unit = inquiry.Unit,
            CurrencyId = inquiry.CurrencyId,
            UnitPrice = inquiry.UnitPrice,
            CurrencyName = currency?.Name,
            ThirdParty = thirdParty?.FullName,
            Documents = documents,
            Description = inquiry.Description,
            OperatorId = operatorUser?.Id,
            OperatorName = operatorUser?.FullName,
            OperatorUserId = operatorUser?.UserId,
            InquiryRequestedTime = inquiry.InquiryRequestedTime,
            Created = inquiry.Created
        };

        return inquiryModel;
    }

    private List<GetFilteredRequestMachineryInquieriesModel>? GetFilteredInquiryDataModeling(List<RequestMachinery> requestMachineries, List<RequestMachineryHistory>? requestMachineryHistories, List<RequestMachineryHistory>? histories,
        List<FilteredUserResponseModel>? thirdParties, List<Company>? companies)
    {
        List<GetFilteredRequestMachineryInquieriesModel>? models = [];

        foreach (var requestMachinery in requestMachineries)
        {
            var projectOperations = string.Join(",", requestMachinery.ProjectOperations.Select(oo => oo.ProjectOperation).Select(oo => oo.OperationInfo.OperationInfoName).ToList());
            var projectOperationDetails = string.Join(",", requestMachinery.ProjectOperationDetails.Select(oo => oo.ProjectOperationDetail).Select(oo => oo.OperationLocation.PublicName).ToList());
            var requestMachineryHistory = requestMachineryHistories?.FirstOrDefault(x => x.RequestMachinery.Id == requestMachinery.Id);
            var company = companies?.FirstOrDefault(x => x.Id == requestMachinery?.CompanyId);
            var creator = thirdParties?.FirstOrDefault(x => x.UserId == requestMachinery.CreatorId);
            var confirmer = thirdParties?.FirstOrDefault(x => x.UserId == requestMachineryHistory?.CreatorId);

            var requestHistory = histories?.LastOrDefault(x => x.RequestMachinery.Id == requestMachinery.Id);
            var historyCreator = thirdParties?.FirstOrDefault(x => x.UserId == requestHistory?.CreatorId)?.FullName;
            string? statusDesc = string.Empty;
            if (requestMachinery.Status == RequestMachineryStatus.Confirmed ||
              requestMachinery.Status == RequestMachineryStatus.Rejected ||
              requestMachinery.Status == RequestMachineryStatus.Returned)
                statusDesc = $"{historyCreator} وضعیت درخواست را به {requestMachinery.Status.GetEnumDescription()} به دلیل ({requestHistory?.RequestDescription}) در تاریخ {TimeCalculator.ConvertToShamsi(requestHistory?.Created)} تغییر داد";
            else
                statusDesc = $"{historyCreator} وضعیت درخواست را به {requestMachinery.Status.GetEnumDescription()} در تاریخ {TimeCalculator.ConvertToShamsi(requestHistory?.Created)} تغییر داد";

            models.Add(new GetFilteredRequestMachineryInquieriesModel()
            {
                CostCenterName = requestMachinery.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                Created = requestMachinery.Created,
                FromDate = requestMachinery.FromDate,
                MachineryGroupName = requestMachinery.Machinery?.MachineriesGroup.GroupName,
                MachineryName = requestMachinery.Machinery?.MachineryName,
                ProjectName = requestMachinery.Project.ProjectName,
                RequestCount = requestMachinery.RequestCount,
                RequestMachineryId = requestMachinery.Id,
                Status = requestMachinery.Status,
                TimeRequired = Convert.ToString(requestMachinery.TimeRequired),
                ToDate = requestMachinery.ToDate,
                Unit = requestMachinery.Unit,
                ProjectOperations = projectOperations,
                ProjectOperationDetails = projectOperationDetails,
                Creator = creator?.FullName,
                CreatorId = requestMachinery.CreatorId,
                RequestNumber = requestMachinery.RequestNumber,
                Description = requestMachinery.Description,
                ConfirmDate = requestMachineryHistory?.Created,
                ConfirmUserId = confirmer?.UserId,
                ConfirmUser = confirmer?.FullName,
                CompanyId = requestMachinery.CompanyId,
                CompanyNameFa = company?.NameFa,
                changeStatusDescription = statusDesc,
            });
        }
        return models;
    }

    private List<GetFilteredRequestMachineryManagementsModel>? GetFilteredManagementDataModeling(List<RequestMachinery> requestMachineries, List<RequestMachineryHistory>? requestMachineryHistories, List<RequestMachineryHistory>? histories,
        List<FilteredUserResponseModel>? thirdParties, List<UserModel?>? contractors, List<Company>? companies)
    {
        List<GetFilteredRequestMachineryManagementsModel>? models = [];

        foreach (var requestMachinery in requestMachineries)
        {
            var times = GetTimeRequireds(requestMachinery);
            var projectOperations = string.Join(",", requestMachinery.ProjectOperations.Select(oo => oo.ProjectOperation).Select(oo => oo.OperationInfo.OperationInfoName).ToList());
            var projectOperationDetails = string.Join(",", requestMachinery.ProjectOperationDetails.Select(oo => oo.ProjectOperationDetail).Select(oo => oo.OperationLocation.PublicName).ToList());
            var requestMachineryHistory = requestMachineryHistories?.FirstOrDefault(x => x.RequestMachinery.Id == requestMachinery.Id);
            var company = companies?.FirstOrDefault(x => x.Id == requestMachinery?.CompanyId);

            var creator = thirdParties?.FirstOrDefault(x => x.UserId == requestMachinery.CreatorId);
            var confirmer = thirdParties?.FirstOrDefault(x => x.UserId == requestMachineryHistory?.CreatorId);

            var requestHistory = histories?.LastOrDefault(x => x.RequestMachinery.Id == requestMachinery.Id);
            var historyCreator = thirdParties?.FirstOrDefault(x => x.UserId == requestHistory?.CreatorId)?.FullName;
            string? statusDesc = string.Empty;
            if (requestMachinery.Status == RequestMachineryStatus.Confirmed ||
                requestMachinery.Status == RequestMachineryStatus.Rejected ||
                requestMachinery.Status == RequestMachineryStatus.Returned)
                statusDesc = $"{historyCreator} وضعیت درخواست را به {requestMachinery.Status.GetEnumDescription()} به دلیل ({requestHistory?.RequestDescription}) در تاریخ {TimeCalculator.ConvertToShamsi(requestHistory?.Created)} تغییر داد";
            else
                statusDesc = $"{historyCreator} وضعیت درخواست را به {requestMachinery.Status.GetEnumDescription()} در تاریخ {TimeCalculator.ConvertToShamsi(requestHistory?.Created)} تغییر داد";

            var appoinment = new GetRequestMachineryManagementByIdOperatorModel()
            {
                AppointmentId = requestMachinery.OperatorAppoinmentId,
                AppointmentUserId = requestMachinery.OperatorAppoinmentUserId,
                AppointmentFullName = thirdParties?.Where(x => x.UserId == requestMachinery.OperatorAppoinmentUserId).FirstOrDefault()?.FullName,
            };

            List<RequestMachineryDocumentResponseModel>? documents = [];
            if (requestMachinery.RequestMachineryDocuments is not null && requestMachinery.RequestMachineryDocuments.Count > 0)
                foreach (var item in requestMachinery.RequestMachineryDocuments)
                {
                    documents.Add(new RequestMachineryDocumentResponseModel(item.Id, item.Url));
                }

            List<RequestMachineryBillDocumentResponseModel>? billDocuments = [];
            if (requestMachinery.RequestMachineryBillDocuments is not null && requestMachinery.RequestMachineryBillDocuments.Count > 0)
                foreach (var item in requestMachinery.RequestMachineryBillDocuments)
                {
                    billDocuments.Add(new RequestMachineryBillDocumentResponseModel(item.Id, item.Url));
                }

            var driver = contractors?.FirstOrDefault(x => x is not null && x.Id == requestMachinery.DriverId);

            string? contractorName = null;
            if (RequestMachineryStatusValidator.AllowforShowInGetFiltered.Any(x => x == requestMachinery.Status))
            {
                if (requestMachinery.ContractorId is not null && requestMachinery.ContractorId > 0)
                {
                    contractorName = contractors?.FirstOrDefault(x => x?.Id == requestMachinery.ContractorId)?.FullName;
                }
                else
                    contractorName = company?.NameFa;
            }
            else
            {
                contractorName = null;
            }

            models.Add(new GetFilteredRequestMachineryManagementsModel()
            {
                CostCenterName = requestMachinery.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                Created = requestMachinery.Created,
                FromDate = requestMachinery.FromDate,
                FromTime = requestMachinery.FromDate != null ? requestMachinery.FromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
                ToDate = requestMachinery.ToDate,
                ToTime = requestMachinery.ToDate != null ? requestMachinery.ToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
                MachineryGroupName = requestMachinery.Machinery?.MachineriesGroup.GroupName,
                MachineryName = requestMachinery.Machinery?.MachineryName,
                ProjectName = requestMachinery.Project.ProjectName,
                RequestCount = requestMachinery.RequestCount,
                RequestMachineryId = requestMachinery.Id,
                Status = requestMachinery.Status,
                TimeRequired = times.timeRequired,
                Unit = requestMachinery.Unit,
                ProjectOperations = projectOperations,
                ProjectOperationDetails = projectOperationDetails,
                Creator = creator?.FullName,
                CreatorId = requestMachinery.CreatorId,
                InquiryOperator = appoinment,
                ConfirmDate = requestMachineryHistory?.Created,
                ConfirmUserId = confirmer?.UserId,
                ConfirmUser = confirmer?.FullName,
                CompanyId = requestMachinery.CompanyId,
                CompanyNameFa = company?.NameFa,
                RequestMachineryDocuments = documents,
                ConfirmFromDate = requestMachinery.ConfirmFromDate,
                ConfirmFromTime = requestMachinery.ConfirmFromDate != null ? requestMachinery.ConfirmFromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
                ConfirmToDate = requestMachinery.ConfirmToDate,
                ConfirmToTime = requestMachinery.ConfirmToDate != null ? requestMachinery.ConfirmToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
                changeStatusDescription = statusDesc,
                ContractorId = requestMachinery.ContractorId,
                Contractor = contractorName,
                ConfirmedDescription = requestMachinery.ConfirmedDescription,
                ConfirmedTimeRequired = times.confirmTimeRequired,
                RequestNumber = requestMachinery.RequestNumber,
                Driver = driver?.FullName,
                DriverName = requestMachinery.DriverName,
                DriverId = requestMachinery.DriverId,
                RequestMachineryBillDocuments = billDocuments,
                Description = requestMachinery.Description,
                UnitPrice = requestMachinery.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed == true)?.UnitPrice,
                TotalPrice = requestMachinery.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed == true)?.TotalPrice,
                PaymentType = requestMachinery.RequestMachineryStatusStatementDetails.Any() &&
                              requestMachinery.RequestMachineryStatusStatementDetails.Any(z => !z.RequestMachineryStatusStatement.IsDeleted) &&
                              (requestMachinery.RequestMachineryStatusStatementDetails.Any(z => z.RequestMachineryStatusStatement.PaymentDate != null) ||
                               requestMachinery.RequestMachineryStatusStatementDetails.Any(z => z.RequestMachineryStatusStatement.PaymentOrderId != null)) ?
                              RequestMachineryPaymentType.Paid : RequestMachineryPaymentType.NotPaid,
            });
        }
        return models;
    }

    private RequestMachineryUnit GetUnit(ContractorMachinery contractorMachinery)
    {
        RequestMachineryUnit unit = new RequestMachineryUnit();
        if (contractorMachinery.Unit == Domain.Entities.ContractorMachineries.Enums.ContractorMachineryUnit.Daily)
            unit = RequestMachineryUnit.Daily;

        if (contractorMachinery.Unit == Domain.Entities.ContractorMachineries.Enums.ContractorMachineryUnit.Hourly)
            unit = RequestMachineryUnit.Hourly;

        if (contractorMachinery.Unit == Domain.Entities.ContractorMachineries.Enums.ContractorMachineryUnit.Serviced)
            unit = RequestMachineryUnit.Serviced;

        if (contractorMachinery.Unit == Domain.Entities.ContractorMachineries.Enums.ContractorMachineryUnit.Volume)
            unit = RequestMachineryUnit.Volume;

        return unit;
    }

    private async Task<bool> SendMessageRequestMachinery(RequestMachinery requestMachinery, string? confirmer, CT ct)
    {
        var getTelegramChats = await _messengerChannelRepo.GetFltrChannel([requestMachinery.Project.Id],
            [requestMachinery.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId], MessengerMessageType.RequestMachinery, ct);

        if (getTelegramChats is null || getTelegramChats.Count > 0)
            return false;

        if (getTelegramChats is not null && getTelegramChats.Count > 0)
        {
            List<Guid> ids = new();
            if (requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList() is not null &&
                requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList().Any())
                foreach (var item in requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList())
                    ids.Add(Guid.Parse(item));

            var creator = "";
            var currentUserId = requestMachinery.CreatorId;
            var getUser = await _mediator.Send(new GetUserByIdQuery(currentUserId), ct);
            if (getUser.IsSuccess && getUser.Value is not null)
                creator = getUser.Value.FullName;

            var inquiry = requestMachinery.InquiryOperators.Where(x => x.Inquiries is not null && x.Inquiries.Count > 0)
                .SelectMany(x => x.Inquiries).Where(x => x.IsConfirmed == true).FirstOrDefault();

            var contractor = "";
            if (inquiry is not null)
            {
                var thirdParty = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long> { inquiry.ThirdPartyId }, null, true, null), ct);
                if (thirdParty.IsSuccess && thirdParty.Value is not null)
                    contractor = thirdParty.Value.Data?.FirstOrDefault()?.FullName;
            }

            foreach (var chat in getTelegramChats!)
            {
                var createDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.Created);
                var fromDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.FromDate);
                var toDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.ToDate);
                var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                var createTime = TimeZoneInfo.ConvertTime(requestMachinery.Created, tehranTimeZone).ToString("HH:mm:ss");

                var resultUrl = requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList() != null
                    ? string.Join(",", requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList())
                    : string.Empty;

                var message = RequestMachineryMessageModel(requestMachinery, inquiry, contractor, confirmer, creator, ct);
                await TelegramServicesLogic.SendMessage(chat, message, ids, _mediator, _authorization, _messageSenderConfig, ct);
            }
        }

        return true;
    }

    private async Task<bool> SendTelegramMessageRequestMachinery(RequestMachinery requestMachinery, string? confirmer, CT ct)
    {
        var costCenterId = requestMachinery.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.Id;

        // Get TelegramChats
        var getTelegramChats = await _mediator.Send(new GetsTelegramChatByCostCenterIdsQuery(
            [costCenterId],
            Domain.Entities.TelegramChats.Enums.TelegramMessageType.RequestMachinery,
            null,
            null,
            true,
            0,
            0), ct);
        if (getTelegramChats.IsFailure || getTelegramChats.Value is null || getTelegramChats.Value?.Data is null)
            return false;

        var telegramChats = getTelegramChats.Value.Data;

        var validChats = telegramChats
            .SelectMany(chat => chat.TelegramChatTypes)
            .Where(x => x.TelegramMessageType is Domain.Entities.TelegramChats.Enums.TelegramMessageType.RequestMachinery or
            Domain.Entities.TelegramChats.Enums.TelegramMessageType.OtherGroups)
            .GroupBy(x => x.ChatId)
            .Select(g => g.FirstOrDefault())
            .Where(chat => chat is not null)
            .ToList();

        if (validChats is not null && validChats.Count > 0)
        {
            List<Guid> ids = new();
            if (requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList() is not null &&
                requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList().Any())
                foreach (var item in requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList())
                    ids.Add(Guid.Parse(item));

            var creator = "";
            var currentUserId = requestMachinery.CreatorId;
            var getUser = await _mediator.Send(new GetUserByIdQuery(currentUserId), ct);
            if (getUser.IsSuccess && getUser.Value is not null)
                creator = getUser.Value.FullName;

            var inquiry = requestMachinery.InquiryOperators.Where(x => x.Inquiries is not null && x.Inquiries.Count > 0)
                .SelectMany(x => x.Inquiries).Where(x => x.IsConfirmed == true).FirstOrDefault();

            var contractor = "";
            if (inquiry is not null)
            {
                var thirdParty = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long> { inquiry.ThirdPartyId }, null, true, null), ct);
                if (thirdParty.IsSuccess && thirdParty.Value is not null)
                    contractor = thirdParty.Value.Data?.FirstOrDefault()?.FullName;
            }

            foreach (var chat in validChats!)
            {
                var createDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.Created);
                var fromDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.FromDate);
                var toDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.ToDate);
                var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
                var createTime = TimeZoneInfo.ConvertTime(requestMachinery.Created, tehranTimeZone).ToString("HH:mm:ss");

                var resultUrl = requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList() != null
                    ? string.Join(",", requestMachinery.RequestMachineryDocuments.Select(x => x.Url).ToList())
                    : string.Empty;

#pragma warning disable CS8604 // Possible null reference argument.
                var x = await TelegramServicesLogic.RequestMachineryMessage(
                       chat?.ChatId,
                       requestMachinery,
                       inquiry,
                       contractor,
                       fromDateShamsi,
                       toDateShamsi,
                       createDateShamsi,
                       createTime,
                       confirmer,
                       creator,
                       ids,
                       _mediator,
                       _telegramMessageHistoryLogic,
                       chat!.TelegramChat.Id,
                       resultUrl,
                       Domain.Entities.TelegramChats.Enums.TelegramMessageType.RequestMachinery,
                       _authorization,
                       _messageSenderConfig,
                       ct);
#pragma warning restore CS8604 // Possible null reference argument.
            }
        }

        return true;
    }

    private (string? timeRequired, string? confirmTimeRequired) GetTimeRequireds(RequestMachinery requestMachinery)
    {
        string? totalHours = null;
        string? confirmTotalHours = null;
        if (requestMachinery.Unit == RequestMachineryUnit.Hourly)
        {
            double timeDouble = (double)requestMachinery.TimeRequired;
            int hours = (int)timeDouble;
            double decimalPart = timeDouble - hours;
            int minutes = (int)Math.Round(decimalPart * 60);
            TimeSpan time = new TimeSpan(hours, minutes, 0);
            totalHours = time.ToString(@"hh\:mm");

            if (requestMachinery.ConfirmedTimeRequired != null)
            {
                double timeConfirmedDouble = (double)requestMachinery.ConfirmedTimeRequired;
                int hoursConfirmed = (int)timeConfirmedDouble;
                double decimalPartConfirmed = timeConfirmedDouble - hoursConfirmed;
                int minutesConfirmed = (int)Math.Round(decimalPartConfirmed * 60);
                TimeSpan timeConfirmed = new TimeSpan(hoursConfirmed, minutesConfirmed, 0);
                confirmTotalHours = timeConfirmed.ToString(@"hh\:mm");
            }
        }
        else
        {
            totalHours = Convert.ToString(requestMachinery.TimeRequired);
            confirmTotalHours = Convert.ToString(requestMachinery.ConfirmedTimeRequired);
        }
        return (totalHours, confirmTotalHours);
    }

    private (TimeSpan? confirmTotalHours, decimal? confirmTotalDays, decimal? confirmTotalServices, decimal? confirmTotalVolumes)
        GetTotalTimeRequireds(RequestMachinery requestMachinery)
    {
        TimeSpan? confirmTotalHours = null;
        decimal? confirmTotalDays = null;
        decimal? confirmTotalServices = null;
        decimal? confirmTotalVolumes = null;
        if (requestMachinery.Unit == RequestMachineryUnit.Hourly)
        {
            double timeConfirmedDouble = (double)(requestMachinery.ConfirmedTimeRequired ?? 0);
            int hoursConfirmed = (int)timeConfirmedDouble;
            double decimalPartConfirmed = timeConfirmedDouble - hoursConfirmed;
            int minutesConfirmed = (int)Math.Round(decimalPartConfirmed * 60);
            TimeSpan timeConfirmed = new TimeSpan(hoursConfirmed, minutesConfirmed, 0);
            confirmTotalHours = timeConfirmed;
        }

        if (requestMachinery.Unit == RequestMachineryUnit.Daily)
            confirmTotalDays = requestMachinery.ConfirmedTimeRequired;

        if (requestMachinery.Unit == RequestMachineryUnit.Serviced)
            confirmTotalServices = requestMachinery.ConfirmedTimeRequired;

        if (requestMachinery.Unit == RequestMachineryUnit.Volume)
            confirmTotalVolumes = requestMachinery.ConfirmedTimeRequired;

        return (confirmTotalHours, confirmTotalDays, confirmTotalServices, confirmTotalVolumes);
    }

    private async Task<Result<RequestMachinery?>> GetRequestMachineryAsync(long requestMachineryId, CT ct)
    {
        var response = await _mediator.Send(new GetRequestMachineryByIdQuery(requestMachineryId), ct);
        return response.IsFailure ? Result.Failure<RequestMachinery>(response.Error!) : response.Value;
    }

    private async Task<Result<ContractorMachinery?>> GetContractorMachineryAsync(long contractorMachineryId, CT ct)
    {
        var response = await _mediator.Send(new GetContractorMachineryByIdQuery(contractorMachineryId), ct);
        return response.IsFailure ? Result.Failure<ContractorMachinery>(response.Error!) : response.Value;
    }

    private (decimal? confirmTime, decimal? totalHours) CalculateConfirmTime(AssignMachineryForRequestMachineryRequest request, RequestMachinery requestMachinery)
    {
        decimal? confirmTime = null;
        decimal? totalHours = null;

        if (!string.IsNullOrEmpty(request.ConfirmedTimeRequired))
        {
            if (requestMachinery.Unit == RequestMachineryUnit.Hourly)
            {
                if (!(request.ConfirmedTimeRequired.Split(':')[0].Count() >= 2 && request.ConfirmedTimeRequired.Split(':')[1].Count() == 2))
                    return (null, null);

                if (int.Parse(request.ConfirmedTimeRequired.Split(':')[1]) > 59)
                    return (null, null);

                TimeSpan time = TimeSpan.Parse(request.ConfirmedTimeRequired);
                totalHours = (decimal)time.TotalHours;
            }
            else
            {
                totalHours = Convert.ToDecimal(request.ConfirmedTimeRequired);
            }
        }

        confirmTime = totalHours ?? requestMachinery.TimeRequired;
        return (confirmTime, totalHours);
    }

    private async Task<Result<RequestMachineryInquiryOperator?>> GetOperatorUserAsync(ExtraInfo? currentUser, RequestMachinery requestMachinery, CT ct)
    {
        var getOfficers = await _mediator.Send(new GetMachineryOperatorsQuery(currentUser?.ThirdPartyId, null, null, null, null, 1, 1), ct);
        if (getOfficers.IsFailure)
            return Result.Failure<RequestMachineryInquiryOperator>(RequestMachineryErrors.RequestMachineryOperatorNotFound);

        var officers = getOfficers.Value!.Data!.FirstOrDefault()!;
        if (officers.UserId != currentUser?.Id)
            return Result.Failure<RequestMachineryInquiryOperator>(RequestMachineryInquiryErrors.InValidRequestMachineryInquiryOperator);

        return new RequestMachineryInquiryOperator(officers.Id, officers.UserId.HasValue ? officers.UserId.Value : 0, requestMachinery);
    }

    private async Task<Result> CreateInquiryAsync(ContractorMachinery contractorMachinery, RequestMachinery requestMachinery,
        RequestMachineryInquiryOperator operatorUser, decimal confirmTime, decimal totalPrice, string description, CT ct)
    {
        var createInquiryResponse = await _mediator.Send(new CreateRequestMachineryInquiryCommand(
            contractorMachinery.ContractorId, contractorMachinery.CurrencyId, requestMachinery.RequestCount, GetUnit(contractorMachinery),
            contractorMachinery.MachineryPrice, totalPrice, description, confirmTime, operatorUser), ct);
        return createInquiryResponse.IsFailure ? Result.Failure(createInquiryResponse.Error!) : Result.Success();
    }

    private async Task<Result> CreateInquiryOperatorAsync(RequestMachinery requestMachinery, long operatorId, long operatorUserId, CT ct)
    {
        var createResponse = await _mediator.Send(new CreateRequestMachineryInquiryOperatorCommand(operatorId, operatorUserId, requestMachinery), ct);
        return createResponse.IsFailure ? Result.Failure(createResponse.Error!) : Result.Success();
    }

    private async Task<Result> UpdateRequestMachineryStatusAsync(RequestMachinery requestMachinery, CT ct)
    {
        if (requestMachinery.Status != RequestMachineryStatus.Confirmed)
        {
            var confirmedResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(requestMachinery, RequestMachineryStatus.Confirmed, null, null), ct);
            if (confirmedResponse.IsFailure)
                return Result.Failure(confirmedResponse.Error!);
        }

        var inquiryDoneResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(requestMachinery, RequestMachineryStatus.InquiryDone, null, null), ct);
        if (inquiryDoneResponse.IsFailure)
            return Result.Failure(inquiryDoneResponse.Error!);

        var updateStatusResponse = await _mediator.Send(new ChangeRequestMachineryStatusCommand(requestMachinery, RequestMachineryStatus.MachineryAppoinment, null, null), ct);
        return updateStatusResponse.IsFailure ? Result.Failure(updateStatusResponse.Error!) : Result.Success();
    }

    private async Task<Result> CreateAssignmentAsync(ContractorMachinery contractorMachinery, RequestMachinery requestMachinery, CT ct)
    {
        var assignmentIdentifier = !string.IsNullOrEmpty(contractorMachinery.NumberPlates) ? contractorMachinery.NumberPlates :
                                   !string.IsNullOrEmpty(contractorMachinery.MachineryIdentifier) ? contractorMachinery.MachineryIdentifier : "ماشین آلات فاقد پلاک";

        var createAssignmentResponse = await _mediator.Send(new CreateRequestMachineryAssignmentCommand(null, assignmentIdentifier, requestMachinery, false), ct);
        return createAssignmentResponse.IsFailure ? Result.Failure(createAssignmentResponse.Error!) : Result.Success();
    }



    private Result ValidateRequestMachineryStatus(RequestMachinery requestMachinery)
    {
        if (!RequestMachineryStatusValidator.AllowStatusForReturned.Contains(requestMachinery.Status))
            return Result.Failure(RequestMachineryErrors.InValidStatus);

        return Result.Success();
    }

    private async Task<Result> CleanupRequestMachineryResources(RequestMachinery requestMachinery, CT ct)
    {
        var inquiries = requestMachinery.InquiryOperators.SelectMany(x => x.Inquiries).ToList();
        var assignments = requestMachinery.RequestMachineryAssignments.ToList();
        var reserves = requestMachinery.MachineryReservations.ToList();
        var contractorMachinery = requestMachinery.ContractorMachinery;

        var deleteInquiriesResult = await DeleteInquiries(requestMachinery, inquiries, ct);
        if (deleteInquiriesResult.IsFailure)
            return deleteInquiriesResult;

        var deleteAssignmentsResult = await DeleteAssignments(requestMachinery, assignments, ct);
        if (deleteAssignmentsResult.IsFailure)
            return deleteAssignmentsResult;

        var deleteReservesResult = await DeleteReserves(requestMachinery, reserves, ct);
        if (deleteReservesResult.IsFailure)
            return deleteReservesResult;

        var removeContractorMachineryResult = await RemoveContractorMachinery(requestMachinery, contractorMachinery, ct);
        if (removeContractorMachineryResult.IsFailure)
            return removeContractorMachineryResult;

        var unConfirmRequest = await UnConfirmRequestMachinery(requestMachinery, ct);
        if (unConfirmRequest.IsFailure)
            return unConfirmRequest;

        return Result.Success();
    }

    private async Task<Result> DeleteInquiries(RequestMachinery requestMachinery, List<RequestMachineryInquiry>? inquiries, CT ct)
    {
        if (inquiries is not null && inquiries.Count > 0)
        {
            var deleteInquiries = await _mediator.Send(new DeleteRequestMachineryInquiriesCommand(requestMachinery), ct);
            return deleteInquiries;
        }
        return Result.Success();
    }

    private async Task<Result> DeleteAssignments(RequestMachinery requestMachinery, List<RequestMachineryAssignment>? assignments, CT ct)
    {
        if (assignments is not null && assignments.Count > 0)
        {
            var deleteAssignments = await _mediator.Send(new DeleteRequestMachineryAssignmentsCommand(requestMachinery), ct);
            return deleteAssignments;
        }
        return Result.Success();
    }

    private async Task<Result> DeleteReserves(RequestMachinery requestMachinery, List<MachineryReservation>? reserves, CT ct)
    {
        if (reserves is not null && reserves.Count > 0)
        {
            var deleteReserves = await _mediator.Send(new DeleteRequestMachineryReservationsCommand(requestMachinery), ct);
            return deleteReserves;
        }
        return Result.Success();
    }

    private async Task<Result> RemoveContractorMachinery(RequestMachinery requestMachinery, ContractorMachinery? contractorMachinery, CT ct)
    {
        if (contractorMachinery is not null)
        {
            var removeContractorMachinery = await _mediator.Send(new RemoveRequestContractorMachineryCommand(requestMachinery), ct);
            return removeContractorMachinery;
        }
        return Result.Success();
    }

    private async Task<Result> UnConfirmRequestMachinery(RequestMachinery? requestMachinery, CT ct)
    {
        if (requestMachinery is not null)
        {
            var unConfirm = await _mediator.Send(new SetUnConfirmRequestMachineryCommand(requestMachinery), ct);
            return unConfirm;
        }
        return Result.Success();
    }

    private decimal CalculateTotalPrice(RequestMachinery item)
    {
        var confirmedInquiry = item.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed);
        if (confirmedInquiry is not null)
            return confirmedInquiry.TotalPrice;

        if (item.ContractorId > 0 || item.MachineryReservations == null || item.MachineryReservations.Count == 0)
            return 0;

        var reserve = item.MachineryReservations.FirstOrDefault();
        var fixMachinery = reserve?.FixAssetMachinery;

        decimal? priceRate = reserve?.Unit switch
        {
            MachineryReservationUnit.Hourly => fixMachinery?.FixAssetMachineryRates.FirstOrDefault(x => x.StartDate.Date <= item.ConfirmFromDate!.Value.Date && x.EndDate.Date >= item.ConfirmToDate!.Value.Date)?.HourlyRate,
            MachineryReservationUnit.Daily => fixMachinery?.FixAssetMachineryRates.FirstOrDefault(x => x.StartDate.Date >= item.ConfirmFromDate!.Value.Date && x.EndDate.Date <= item.ConfirmToDate!.Value.Date)?.DailyRate,
            MachineryReservationUnit.Serviced => fixMachinery?.FixAssetMachineryRates.FirstOrDefault(x => x.StartDate.Date >= item.ConfirmFromDate!.Value.Date && x.EndDate.Date <= item.ConfirmToDate!.Value.Date)?.ServiceRate,
            MachineryReservationUnit.Volume => fixMachinery?.FixAssetMachineryRates.FirstOrDefault(x => x.StartDate.Date >= item.ConfirmFromDate!.Value.Date && x.EndDate.Date <= item.ConfirmToDate!.Value.Date)?.VolumeRate,
            _ => 0
        };

        return ((item.RequestCount * priceRate * item.ConfirmedTimeRequired) ?? 0);
    }
    private (decimal? TotalPrice, string? TotalHourly, decimal? TotalDaily, decimal? TotalServiced, decimal? TotalVolumes)
        CalculateMachineryTotals(List<RequestMachinery> requestMachineries)
    {
        decimal totalPrice = 0;
        TimeSpan? totalHourly = TimeSpan.Zero;
        string? totalHourlies = string.Empty;
        decimal? totalDaily = 0;
        decimal? totalServiced = 0;
        decimal? totalVolumes = 0;

        foreach (var item in requestMachineries)
        {
            var price = CalculateTotalPrice(item);
            totalPrice += price;

            var times = GetTotalTimeRequireds(item);

            switch (item.Unit)
            {
                case RequestMachineryUnit.Hourly:
                    totalHourly += (times.confirmTotalHours ?? TimeSpan.Zero);
                    break;
                case RequestMachineryUnit.Daily:
                    totalDaily += (times.confirmTotalDays ?? 0);
                    break;
                case RequestMachineryUnit.Serviced:
                    totalServiced += (times.confirmTotalServices ?? 0);
                    break;
                case RequestMachineryUnit.Volume:
                    totalVolumes += (times.confirmTotalVolumes ?? 0);
                    break;
            }
        }

        if (totalHourly is not null && totalHourly.Value.Days >= 0)
        {
            var totalHours = (totalHourly!.Value.Days * 24) + totalHourly.Value.Hours;

            var hours = totalHours >= 10 ? totalHours.ToString() : $"0{totalHours}";
            var minutes = totalHourly.Value.Minutes >= 10 ? totalHourly.Value.Minutes.ToString() : $"0{totalHourly.Value.Minutes}";
            var seconds = totalHourly.Value.Seconds >= 10 ? totalHourly.Value.Seconds.ToString() : $"0{totalHourly.Value.Seconds}";

            totalHourlies = $"{hours}H:{minutes}M:{seconds}S";
        }

        return (totalPrice, totalHourlies, totalDaily, totalServiced, totalVolumes);
    }

    public static string RequestMachineryMessageModel(RequestMachinery? requestMachinery,
        RequestMachineryInquiry? requestMachineryInquiry,
        string? contractor,
        string? confirmer,
        string? creator, CT ct)
    {
        var createDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.Created);
        var fromDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.FromDate);
        var toDateShamsi = TimeCalculator.ConvertToShamsi(requestMachinery.ToDate);
        var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        var createTime = TimeZoneInfo.ConvertTime(requestMachinery.Created, tehranTimeZone).ToString("HH:mm:ss");

        string message = string.Empty;

        var priceWithComma = requestMachineryInquiry?.TotalPrice is not null
                ? requestMachineryInquiry.TotalPrice.ToString("N0") + " ریال"
                : null;

        message = $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}";
        message += $"{TelegramValues.TruckIcon}<b>درخواست ماشین آلات : {requestMachinery?.Machinery.MachineryName}</b>  {Environment.NewLine}{Environment.NewLine}" +
                   $"<b>شماره درخواست :</b> {requestMachinery?.RequestNumber} {Environment.NewLine}" +
                   $"<b>مرکز هزینه :</b> {requestMachinery?.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName} {Environment.NewLine}" +
                   $"<b>پروژه :</b> {requestMachinery?.Project.ProjectName} {Environment.NewLine}" +
                   $"<b>پیمانکار :</b> {contractor} {Environment.NewLine}" +
                   $"<b>مبلغ :</b> {priceWithComma} {Environment.NewLine}" +
                   $"<b>از تاریخ :</b> {fromDateShamsi} {Environment.NewLine}" +
                   $"<b>تا تاریخ :</b> {toDateShamsi} {Environment.NewLine}" +
                   $"<b>زمان مورد نیاز :</b> {requestMachinery?.TimeRequired} {Environment.NewLine}" +
                   $"<b>واحد :</b> {requestMachinery?.Unit.GetEnumDescription()} {Environment.NewLine}" +
                   $"<b>تایید کننده :</b> {confirmer} {Environment.NewLine}" +
                   $"<b>درخواست دهنده :</b> {creator} {Environment.NewLine}" +
                   $"<b>تاریخ و ساعت درخواست :</b> {createTime} {createDateShamsi} {Environment.NewLine}" +
                   $"<b>توضیحات :</b> {requestMachinery?.Description} {Environment.NewLine}";

        return message;
    }
}
