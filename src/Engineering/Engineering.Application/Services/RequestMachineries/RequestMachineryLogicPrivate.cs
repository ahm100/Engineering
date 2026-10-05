using Engineering.Application.Services.RequestMachineries.Models.GetManagerConfirmMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReports;
using Engineering.Application.Services.RequestMachineries.Models.GetSendManagerMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.RequestMachineryModel;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Gita.Backend.Shared.Application.WebServices.FinancialServices.Preferential.Models;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.Services.RequestMachineries;

public partial class RequestMachineryLogic : IRequestMachineryLogic
{
    private List<GetSendManagerMachineryDetailReportsModel> CreateSendManagerDetailResponseModel(
        List<RequestMachinery>? requestMachineries,
        List<RequestMachineryHistory>? histories,
        List<FilteredUserResponseModel>? userInfo,
        List<Currency>? currencyInfo,
        List<UserModel?>? thirdParties,
        List<Company>? companies,
        Currency? defaultCurrency)
    {
        var detailModels = new List<GetSendManagerMachineryDetailReportsModel>();

        if (requestMachineries == null || requestMachineries.Count == 0)
            return detailModels;

        foreach (var item in requestMachineries)
        {
            var times = GetTimeRequireds(item);
            var projectOperations = GetProjectOperations(item);
            var projectOperationIds = GetProjectOperationIds(item);
            var projectOperationDetails = GetProjectOperationDetails(item);
            var projectOperationDetailIds = GetProjectOperationDetailIds(item);
            var history = histories?.FirstOrDefault(x => x.RequestMachinery.Id == item.Id);
            var driver = thirdParties?.FirstOrDefault(x => x?.Id == item.DriverId);
            var company = companies?.FirstOrDefault(x => x?.Id == item.CompanyId);
            var contractorName = GetContractorName(item, thirdParties, company);
            var price = CalculateTotalPrice(item);

            var detailModel = new GetSendManagerMachineryDetailReportsModel
            {
                Unit = item.Unit,
                ConfirmFromDate = item.ConfirmFromDate,
                ConfirmFromTime = item.ConfirmFromDate?.TimeOfDay ?? TimeSpan.Zero,
                ConfirmToDate = item.ConfirmToDate,
                ConfirmToTime = item.ConfirmToDate?.TimeOfDay ?? TimeSpan.Zero,
                FromDate = item.FromDate,
                FromTime = item.FromDate?.TimeOfDay ?? TimeSpan.Zero,
                ToDate = item.ToDate,
                ToTime = item.ToDate?.TimeOfDay ?? TimeSpan.Zero,
                MachineryGroupName = item.Machinery?.MachineriesGroup.GroupName,
                MachineryName = item.Machinery?.MachineryName,
                ProjectName = item.Project.ProjectName,
                RequestCount = item.RequestCount,
                RequestMachineryId = item.Id,
                Status = item.Status,
                TimeRequired = times.timeRequired,
                ProjectOperations = projectOperations,
                ProjectOperationIds = projectOperationIds,
                ProjectOperationDetails = projectOperationDetails,
                ProjectOperationDetailIds = projectOperationDetailIds,
                Creator = userInfo?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName,
                CreatorId = userInfo?.FirstOrDefault(x => x.UserId == item.CreatorId)?.UserId,
                ConfirmDate = history?.Created,
                ConfirmUserId = userInfo?.FirstOrDefault(x => x.UserId == history?.CreatorId)?.UserId,
                ConfirmUser = userInfo?.FirstOrDefault(x => x.UserId == history?.CreatorId)?.FullName,
                ContractorId = item.ContractorId,
                Contractor = contractorName,
                CostCenterName = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                CostCenterId = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                Created = item.Created,
                CurrencyId = currencyInfo?.FirstOrDefault(x => item.InquiryOperators.Any(z => z.Inquiries.Any(y => y.CurrencyId == x.Id)))?.Id ?? defaultCurrency?.Id,
                Currency = currencyInfo?.FirstOrDefault(x => item.InquiryOperators.Any(z => z.Inquiries.Any(y => y.CurrencyId == x.Id)))?.Name ?? defaultCurrency?.Name,
                MachineryId = item.Machinery?.Id,
                MachineryCode = item.Machinery?.MachineryCode,
                ProjectId = item.Project.Id,
                TotalPrice = price.TotalPrice,
                OperatorUserId = item.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed)?.RequestMachineryInquiryOperator.OperatorAppoinmentUserId,
                OperatorId = item.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed)?.RequestMachineryInquiryOperator.OperatorAppoinmentId,
                Operator = userInfo?.FirstOrDefault(x => x.UserId == item.InquiryOperators.SelectMany(z => z.Inquiries).FirstOrDefault(z => z.IsConfirmed)?.RequestMachineryInquiryOperator.OperatorAppoinmentUserId)?.FullName,
                ConfirmedDescription = item.ConfirmedDescription,
                ConfirmedTimeRequired = times.confirmTimeRequired,
                RequestNumber = item.RequestNumber,
                CompanyId = item.CompanyId,
                Company = company?.NameFa,
                DriverId = item.DriverId,
                Driver = driver?.FullName,
                DriverName = item.DriverName,
                RequestMachineryBillDocuments = item.RequestMachineryBillDocuments.Select(x => x.Url).ToList() ?? [],
                RequestMachineryDocuments = item.RequestMachineryDocuments.Select(x => x.Url).ToList() ?? [],
                Description = item.Description,
                UnitPrice = price.UnitPrice,
                ManagerDescription = item.ManagerDescription
            };

            detailModels.Add(detailModel);
        }

        return detailModels;
    }

    private List<GetSendManagerMachineryReportsModel> CreateSendManagerReportsResponseModel(
        List<GetSendManagerMachineryDetailReportsModel>? detailModels,
        List<UserModel?>? thirdParties,
        List<FilteredPreferentialModel>? prefrentials
        )
    {
        var models = new List<GetSendManagerMachineryReportsModel>();

        if (detailModels == null || detailModels.Count == 0)
            return models;

        var modelMap = new Dictionary<(long? CostCenterId, long? ProjectId, long? ContractorId, long? MachineryId,
            RequestMachineryUnit Unit, RequestMachineryStatus Status), GetSendManagerMachineryReportsModel>();

        foreach (var item in detailModels)
        {
            var key = (item.CostCenterId, item.ProjectId, item.ContractorId, item.MachineryId, item.Unit, item.Status);

            var contractor = thirdParties?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId);
            var prefrentialId = prefrentials?.FirstOrDefault(x => x.ReferenceCode == contractor?.PreferentialReferenceCode)?.Id;

            if (!modelMap.TryGetValue(key, out var model))
            {
                model = new GetSendManagerMachineryReportsModel()
                {
                    CostCenterId = item.CostCenterId,
                    ContractorId = item.ContractorId,
                    Contractor = item.Contractor,
                    CostCenterName = item.CostCenterName,
                    Machinerycode = item.MachineryCode,
                    MachineryId = item.MachineryId,
                    MachineryName = item.MachineryName,
                    ProjectId = item.ProjectId,
                    ProjectName = item.ProjectName,
                    Unit = item.Unit,
                    RequestMachineryStatus = item.Status,
                    PrefernialId = prefrentialId,
                    Details = new List<GetSendManagerMachineryDetailReportsModel>()
                };
                modelMap[key] = model;
                models.Add(model);
            }

            model.Details?.Add(item.Adapt<GetSendManagerMachineryDetailReportsModel>());

            if (item.ConfirmFromDate.HasValue && item.ConfirmToDate.HasValue)
            {
                var fromDate = item.ConfirmFromDate.Value.Date;
                var toDate = item.ConfirmToDate.Value.Date;

                if (model.FromDate == null || fromDate < model.FromDate)
                    model.FromDate = fromDate;

                if (model.ToDate == null || toDate > model.ToDate)
                    model.ToDate = toDate;

                if (item.ConfirmFromTime.HasValue && item.ConfirmToTime.HasValue)
                {
                    var workHours = item.ConfirmToTime.Value - item.ConfirmFromTime.Value;
                    model.TotalTimeWork = (model.TotalTimeWork ?? TimeSpan.Zero) + workHours;
                }
            }

            decimal? totalHours = null;
            if (!string.IsNullOrEmpty(item.ConfirmedTimeRequired))
                if (model.Unit == RequestMachineryUnit.Hourly)
                {
                    TimeSpan time = TimeSpan.Parse(item.ConfirmedTimeRequired);
                    totalHours = (decimal)time.TotalHours;
                }
                else
                    totalHours = Convert.ToDecimal(item.ConfirmedTimeRequired);

            model.TotalFinalPrice = (model.TotalFinalPrice ?? 0) + (item.TotalPrice ?? 0);
            model.TotalRequestedCount = (model.TotalRequestedCount ?? 0) + (item.RequestCount);
            model.TotalCount = (model.TotalCount ?? 0) + 1;
            model.TotalTimeRequired = (model.TotalTimeRequired ?? 0) + totalHours ?? 0;
        }

        foreach (var model in models)
        {
            if (model.FromDate.HasValue && model.ToDate.HasValue)
                model.TotalDayWork = (model.ToDate.Value - model.FromDate.Value).Days + 1;
            else
                model.TotalDayWork = 1;
        }

        return models;
    }

    private List<GetOnProjectMachineryDetailReportsModel> CreateDetailResponseModel(
        List<RequestMachinery>? requestMachineries,
        List<RequestMachineryHistory>? histories,
        List<FilteredUserResponseModel>? userInfo,
        List<Currency>? currencyInfo,
        List<UserModel?>? thirdParties,
        List<Company>? companies,
        Currency? defaultCurrency)
    {
        var detailModels = new List<GetOnProjectMachineryDetailReportsModel>();

        if (requestMachineries == null || requestMachineries.Count == 0)
            return detailModels;

        foreach (var item in requestMachineries)
        {
            var times = GetTimeRequireds(item);
            var projectOperations = GetProjectOperations(item);
            var projectOperationIds = GetProjectOperationIds(item);
            var projectOperationDetails = GetProjectOperationDetails(item);
            var projectOperationDetailIds = GetProjectOperationDetailIds(item);
            var history = histories?.FirstOrDefault(x => x.RequestMachinery.Id == item.Id);
            var driver = thirdParties?.FirstOrDefault(x => x?.Id == item.DriverId);
            var company = companies?.FirstOrDefault(x => x?.Id == item.CompanyId);
            var contractorName = GetContractorName(item, thirdParties, company);
            var price = CalculateTotalPrice(item);

            var detailModel = new GetOnProjectMachineryDetailReportsModel
            {
                Unit = item.Unit,
                ConfirmFromDate = item.ConfirmFromDate,
                ConfirmFromTime = item.ConfirmFromDate?.TimeOfDay ?? TimeSpan.Zero,
                ConfirmToDate = item.ConfirmToDate,
                ConfirmToTime = item.ConfirmToDate?.TimeOfDay ?? TimeSpan.Zero,
                FromDate = item.FromDate,
                FromTime = item.FromDate?.TimeOfDay ?? TimeSpan.Zero,
                ToDate = item.ToDate,
                ToTime = item.ToDate?.TimeOfDay ?? TimeSpan.Zero,
                MachineryGroupName = item.Machinery?.MachineriesGroup.GroupName,
                MachineryName = item.Machinery?.MachineryName,
                ProjectName = item.Project.ProjectName,
                RequestCount = item.RequestCount,
                RequestMachineryId = item.Id,
                Status = item.Status,
                TimeRequired = times.timeRequired,
                ProjectOperations = projectOperations,
                ProjectOperationIds = projectOperationIds,
                ProjectOperationDetails = projectOperationDetails,
                ProjectOperationDetailIds = projectOperationDetailIds,
                Creator = userInfo?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName,
                CreatorId = userInfo?.FirstOrDefault(x => x.UserId == item.CreatorId)?.UserId,
                ConfirmDate = history?.Created,
                ConfirmUserId = userInfo?.FirstOrDefault(x => x.UserId == history?.CreatorId)?.UserId,
                ConfirmUser = userInfo?.FirstOrDefault(x => x.UserId == history?.CreatorId)?.FullName,
                ContractorId = item.ContractorId,
                Contractor = contractorName,
                CostCenterName = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                CostCenterId = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                Created = item.Created,
                CurrencyId = currencyInfo?.FirstOrDefault(x => item.InquiryOperators.Any(z => z.Inquiries.Any(y => y.CurrencyId == x.Id)))?.Id ?? defaultCurrency?.Id,
                Currency = currencyInfo?.FirstOrDefault(x => item.InquiryOperators.Any(z => z.Inquiries.Any(y => y.CurrencyId == x.Id)))?.Name ?? defaultCurrency?.Name,
                MachineryId = item.Machinery?.Id,
                MachineryCode = item.Machinery?.MachineryCode,
                ProjectId = item.Project.Id,
                TotalPrice = price.TotalPrice,
                OperatorUserId = item.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed)?.RequestMachineryInquiryOperator.OperatorAppoinmentUserId,
                OperatorId = item.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed)?.RequestMachineryInquiryOperator.OperatorAppoinmentId,
                Operator = userInfo?.FirstOrDefault(x => x.UserId == item.InquiryOperators.SelectMany(z => z.Inquiries).FirstOrDefault(z => z.IsConfirmed)?.RequestMachineryInquiryOperator.OperatorAppoinmentUserId)?.FullName,
                ConfirmedDescription = item.ConfirmedDescription,
                ConfirmedTimeRequired = times.confirmTimeRequired,
                RequestNumber = item.RequestNumber,
                CompanyId = item.CompanyId,
                Company = company?.NameFa,
                DriverId = item.DriverId,
                Driver = driver?.FullName,
                DriverName = item.DriverName,
                RequestMachineryBillDocuments = item.RequestMachineryBillDocuments.Select(x => x.Url).ToList() ?? [],
                RequestMachineryDocuments = item.RequestMachineryDocuments.Select(x => x.Url).ToList() ?? [],
                Description = item.Description,
                UnitPrice = price.UnitPrice,
                ManagerDescription = item.ManagerDescription
            };

            detailModels.Add(detailModel);
        }

        return detailModels;
    }

    private List<GetOnProjectRequestReportsModel> CreateResponseModel(
        List<RequestMachinery>? requestMachineries,
        List<RequestMachineryHistory>? histories,
        List<FilteredUserResponseModel>? userInfo,
        List<Currency>? currencyInfo,
        List<UserModel?>? thirdParties,
        List<Company>? companies,
        Currency? defaultCurrency)
    {
        var detailModels = new List<GetOnProjectRequestReportsModel>();

        if (requestMachineries == null || requestMachineries.Count == 0)
            return detailModels;

        foreach (var item in requestMachineries)
        {
            var Times = GetTimeRequireds(item);
            var projectOperations = GetProjectOperations(item);
            var projectOperationIds = GetProjectOperationIds(item);
            var projectOperationDetails = GetProjectOperationDetails(item);
            var projectOperationDetailIds = GetProjectOperationDetailIds(item);

            var history = histories?.FirstOrDefault(x => x.RequestMachinery.Id == item.Id);
            var driver = thirdParties?.FirstOrDefault(x => x?.Id == item.DriverId);
            var company = companies?.FirstOrDefault(x => x?.Id == item.CompanyId);

            var contractorName = GetContractorName(item, thirdParties, company);
            var price = CalculateTotalPrice(item);

            var detailModel = new GetOnProjectRequestReportsModel
            {
                Unit = item.Unit,
                ConfirmFromDate = item.ConfirmFromDate,
                ConfirmFromTime = item.ConfirmFromDate?.TimeOfDay ?? TimeSpan.Zero,
                ConfirmToDate = item.ConfirmToDate,
                ConfirmToTime = item.ConfirmToDate?.TimeOfDay ?? TimeSpan.Zero,
                FromDate = item.FromDate,
                FromTime = item.FromDate?.TimeOfDay ?? TimeSpan.Zero,
                ToDate = item.ToDate,
                ToTime = item.ToDate?.TimeOfDay ?? TimeSpan.Zero,
                MachineryGroupName = item.Machinery?.MachineriesGroup.GroupName,
                MachineryName = item.Machinery?.MachineryName,
                ProjectName = item.Project.ProjectName,
                RequestCount = item.RequestCount,
                RequestMachineryId = item.Id,
                Status = item.Status,
                TimeRequired = Times.timeRequired,
                ProjectOperations = projectOperations,
                ProjectOperationIds = projectOperationIds,
                ProjectOperationDetails = projectOperationDetails,
                ProjectOperationDetailIds = projectOperationDetailIds,
                Creator = userInfo?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName,
                CreatorId = userInfo?.FirstOrDefault(x => x.UserId == item.CreatorId)?.UserId,
                ConfirmDate = history?.Created,
                ConfirmUserId = userInfo?.FirstOrDefault(x => x.UserId == history?.CreatorId)?.UserId,
                ConfirmUser = userInfo?.FirstOrDefault(x => x.UserId == history?.CreatorId)?.FullName,
                ContractorId = item.ContractorId,
                Contractor = contractorName,
                CostCenterName = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                CostCenterId = item.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                Created = item.Created,
                CurrencyId = currencyInfo?.FirstOrDefault(x => item.InquiryOperators.Any(z => z.Inquiries.Any(y => y.CurrencyId == x.Id)))?.Id ?? defaultCurrency?.Id,
                Currency = currencyInfo?.FirstOrDefault(x => item.InquiryOperators.Any(z => z.Inquiries.Any(y => y.CurrencyId == x.Id)))?.Name ?? defaultCurrency?.Name,
                MachineryId = item.Machinery?.Id,
                MachineryCode = item.Machinery?.MachineryCode,
                ProjectId = item.Project.Id,
                TotalPrice = price.TotalPrice,
                OperatorUserId = item.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed)?.RequestMachineryInquiryOperator.OperatorAppoinmentUserId,
                OperatorId = item.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed)?.RequestMachineryInquiryOperator.OperatorAppoinmentId,
                Operator = userInfo?.FirstOrDefault(x => x.UserId == item.InquiryOperators.SelectMany(z => z.Inquiries).FirstOrDefault(z => z.IsConfirmed)?.RequestMachineryInquiryOperator.OperatorAppoinmentUserId)?.FullName,
                ConfirmedDescription = item.ConfirmedDescription,
                ConfirmedTimeRequired = Times.confirmTimeRequired,
                RequestNumber = item.RequestNumber,
                CompanyId = item.CompanyId,
                Company = company?.NameFa,
                DriverId = item.DriverId,
                Driver = driver?.FullName,
                DriverName = item.DriverName,
                UnitPrice = price.UnitPrice,
                PaymentType = item.RequestMachineryStatusStatementDetails.Any() &&
                              item.RequestMachineryStatusStatementDetails.Any(z => !z.RequestMachineryStatusStatement.IsDeleted) &&
                              (item.RequestMachineryStatusStatementDetails.Any(z => z.RequestMachineryStatusStatement.PaymentDate != null) ||
                               item.RequestMachineryStatusStatementDetails.Any(z => z.RequestMachineryStatusStatement.PaymentOrderId != null)) ?
                              RequestMachineryPaymentType.Paid : RequestMachineryPaymentType.NotPaid,
            };

            detailModels.Add(detailModel);
        }

        return detailModels;
    }

    private List<GetOnProjectMachineryReportsModel> CreateReportsResponseModel(
        List<GetOnProjectMachineryDetailReportsModel>? detailModels,
        List<UserModel?>? thirdParties,
        List<FilteredPreferentialModel>? prefrentials
        )
    {
        var models = new List<GetOnProjectMachineryReportsModel>();

        if (detailModels == null || detailModels.Count == 0)
            return models;

        var modelMap = new Dictionary<(long? CostCenterId, long? ProjectId, long? ContractorId, long? MachineryId,
            RequestMachineryUnit Unit, RequestMachineryStatus Status), GetOnProjectMachineryReportsModel>();

        foreach (var item in detailModels)
        {
            var key = (item.CostCenterId, item.ProjectId, item.ContractorId, item.MachineryId, item.Unit, item.Status);

            var contractor = thirdParties?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId);
            var prefrentialId = prefrentials?.FirstOrDefault(x => x.ReferenceCode == contractor?.PreferentialReferenceCode)?.Id;

            if (!modelMap.TryGetValue(key, out var model))
            {
                model = new GetOnProjectMachineryReportsModel()
                {
                    CostCenterId = item.CostCenterId,
                    ContractorId = item.ContractorId,
                    Contractor = item.Contractor,
                    CostCenterName = item.CostCenterName,
                    Machinerycode = item.MachineryCode,
                    MachineryId = item.MachineryId,
                    MachineryName = item.MachineryName,
                    ProjectId = item.ProjectId,
                    ProjectName = item.ProjectName,
                    Unit = item.Unit,
                    RequestMachineryStatus = item.Status,
                    PrefernialId = prefrentialId,
                    Details = new List<GetOnProjectMachineryDetailReportsModel>()
                };
                modelMap[key] = model;
                models.Add(model);
            }

            model.Details?.Add(item.Adapt<GetOnProjectMachineryDetailReportsModel>());

            if (item.ConfirmFromDate.HasValue && item.ConfirmToDate.HasValue)
            {
                var fromDate = item.ConfirmFromDate.Value.Date;
                var toDate = item.ConfirmToDate.Value.Date;

                if (model.FromDate == null || fromDate < model.FromDate)
                    model.FromDate = fromDate;

                if (model.ToDate == null || toDate > model.ToDate)
                    model.ToDate = toDate;

                if (item.ConfirmFromTime.HasValue && item.ConfirmToTime.HasValue)
                {
                    var workHours = item.ConfirmToTime.Value - item.ConfirmFromTime.Value;
                    model.TotalTimeWork = (model.TotalTimeWork ?? TimeSpan.Zero) + workHours;
                }
            }

            decimal? totalHours = null;
            if (!string.IsNullOrEmpty(item.ConfirmedTimeRequired))
                if (model.Unit == RequestMachineryUnit.Hourly)
                {
                    TimeSpan time = TimeSpan.Parse(item.ConfirmedTimeRequired);
                    totalHours = (decimal)time.TotalHours;
                }
                else
                    totalHours = Convert.ToDecimal(item.ConfirmedTimeRequired);

            model.TotalFinalPrice = (model.TotalFinalPrice ?? 0) + (item.TotalPrice ?? 0);
            model.TotalRequestedCount = (model.TotalRequestedCount ?? 0) + (item.RequestCount);
            model.TotalCount = (model.TotalCount ?? 0) + 1;
            model.TotalTimeRequired = (model.TotalTimeRequired ?? 0) + totalHours ?? 0;
        }

        // محاسبه TotalDayWork برای هر مدل
        foreach (var model in models)
        {
            if (model.FromDate.HasValue && model.ToDate.HasValue)
                model.TotalDayWork = (model.ToDate.Value - model.FromDate.Value).Days + 1;
            else
                model.TotalDayWork = 1;
        }

        return models;
    }

    private List<GetManagerConfirmMachineryReportsModel> CreateManagerConfirmReportsResponseModel(
        List<GetManagerConfirmMachineryDetailReportsModel>? detailModels,
        List<UserModel?>? thirdParties,
        List<FilteredPreferentialModel>? prefrentials
        )
    {
        var models = new List<GetManagerConfirmMachineryReportsModel>();

        if (detailModels == null || detailModels.Count == 0)
            return models;

        var modelMap = new Dictionary<(long? CostCenterId, long? ProjectId, long? ContractorId, long? MachineryId,
            RequestMachineryUnit Unit, RequestMachineryStatus Status), GetManagerConfirmMachineryReportsModel>();

        foreach (var item in detailModels)
        {
            var key = (item.CostCenterId, item.ProjectId, item.ContractorId, item.MachineryId, item.Unit, item.Status);

            var contractor = thirdParties?.FirstOrDefault(x => x is not null && x.Id == item.ContractorId);
            var prefrentialId = prefrentials?.FirstOrDefault(x => x.ReferenceCode == contractor?.PreferentialReferenceCode)?.Id;

            if (!modelMap.TryGetValue(key, out var model))
            {
                model = new GetManagerConfirmMachineryReportsModel()
                {
                    CostCenterId = item.CostCenterId,
                    ContractorId = item.ContractorId,
                    Contractor = item.Contractor,
                    CostCenterName = item.CostCenterName,
                    Machinerycode = item.MachineryCode,
                    MachineryId = item.MachineryId,
                    MachineryName = item.MachineryName,
                    ProjectId = item.ProjectId,
                    ProjectName = item.ProjectName,
                    Unit = item.Unit,
                    RequestMachineryStatus = item.Status,
                    PrefernialId = prefrentialId,
                    Details = new List<GetManagerConfirmMachineryDetailReportsModel>()
                };
                modelMap[key] = model;
                models.Add(model);
            }

            model.Details?.Add(item.Adapt<GetManagerConfirmMachineryDetailReportsModel>());

            if (item.ConfirmFromDate.HasValue && item.ConfirmToDate.HasValue)
            {
                var fromDate = item.ConfirmFromDate.Value.Date;
                var toDate = item.ConfirmToDate.Value.Date;

                if (model.FromDate == null || fromDate < model.FromDate)
                    model.FromDate = fromDate;

                if (model.ToDate == null || toDate > model.ToDate)
                    model.ToDate = toDate;

                if (item.ConfirmFromTime.HasValue && item.ConfirmToTime.HasValue)
                {
                    var workHours = item.ConfirmToTime.Value - item.ConfirmFromTime.Value;
                    model.TotalTimeWork = (model.TotalTimeWork ?? TimeSpan.Zero) + workHours;
                }
            }

            decimal? totalHours = null;
            if (!string.IsNullOrEmpty(item.ConfirmedTimeRequired))
                if (model.Unit == RequestMachineryUnit.Hourly)
                {
                    TimeSpan time = TimeSpan.Parse(item.ConfirmedTimeRequired);
                    totalHours = (decimal)time.TotalHours;
                }
                else
                {
                    totalHours = Convert.ToDecimal(item.ConfirmedTimeRequired);
                }

            model.TotalFinalPrice = (model.TotalFinalPrice ?? 0) + (item.TotalPrice ?? 0);
            model.TotalRequestedCount = (model.TotalRequestedCount ?? 0) + (item.RequestCount);
            model.TotalCount = (model.TotalCount ?? 0) + 1;
            model.TotalTimeRequired = (model.TotalTimeRequired ?? 0) + totalHours ?? 0;
        }

        // محاسبه TotalDayWork برای هر مدل
        foreach (var model in models)
        {
            if (model.FromDate.HasValue && model.ToDate.HasValue)
            {
                model.TotalDayWork = (model.ToDate.Value - model.FromDate.Value).Days + 1;
            }
            else
            {
                model.TotalDayWork = 1;
            }
        }

        return models;
    }
    private List<long> IdCollectors(List<RequestMachinery?> items)
    {
        List<long>? allIds = [];
        foreach (var item in items)
        {
            if (item!.CreatorId != 0)
                allIds.Add(item!.CreatorId);

            if (item!.Histories is not null && item.Histories.Count > 0)
                allIds.AddRange(item.Histories.Where(x => x.CreatorId > 0).Select(x => x.CreatorId).Distinct().ToList());

            var inquiries = item.InquiryOperators.Where(x => x.Inquiries != null && x.Inquiries.Count > 0)
                .SelectMany(x => x.Inquiries).ToList();
            if (inquiries != null && inquiries.Count > 0)
                allIds.AddRange(inquiries.Select(x => x.RequestMachineryInquiryOperator.OperatorAppoinmentUserId).Distinct().ToList() ?? []);
        }

        return allIds.Where(x => x != 0).Distinct().ToList();
    }
    private List<long> thirdPartyIdCollectors(List<RequestMachinery?> items)
    {
        var allIds = new List<long>();
        foreach (var item in items)
        {
            allIds.AddRange(items.Where(x => x is not null && x.ContractorId is not null && x.ContractorId > 0).Select(x => (long)x?.ContractorId!).Distinct().ToList());
            allIds.AddRange(items.Where(x => x is not null && x.DriverId is not null && x.DriverId > 0).Select(x => (long)x?.DriverId!).Distinct().ToList());
        }

        if (_userInfoProvider.CompanyThirdPartyId > 0)
            allIds.Add(_userInfoProvider.CompanyThirdPartyId);

        return allIds.Distinct().ToList();
    }
    private List<long> CurrencyIdCollectors(List<RequestMachinery?> items)
    {
        var allIds = new List<long>();
        foreach (var item in items)
        {
            var inquiries = item!.InquiryOperators.Where(x => x.Inquiries != null && x.Inquiries.Count > 0)
                .SelectMany(x => x.Inquiries).ToList();
            if (inquiries != null && inquiries.Count > 0)
                allIds.AddRange(inquiries.Select(x => x.CurrencyId).Distinct().ToList() ?? []);
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
    private RequestNumberPlatesModel? SetNumberPlates(string? numberPlates)
    {
        if (!string.IsNullOrEmpty(numberPlates))
        {
            var onlyNumbers = new String(numberPlates.Where(char.IsDigit).ToArray());
            var onlyLetters = new String(numberPlates.Where(char.IsLetter).ToArray());
            string part1 = onlyNumbers.Substring(0, 2); // "12"
            string part2 = onlyNumbers.Substring(2, 3); // "345"
            string part3 = onlyNumbers.Substring(5, 2); // "67"

            return new RequestNumberPlatesModel()
            {
                Letter = onlyLetters,
                Part1 = part1,
                Part2 = part2,
                Part3 = part3,
            };
        }
        else
            return new RequestNumberPlatesModel();
    }
    private string GetProjectOperations(RequestMachinery item)
    {
        return string.Join(",", item.ProjectOperations.Select(oo => oo.ProjectOperation.OperationInfo.OperationInfoName));
    }
    private List<long>? GetProjectOperationIds(RequestMachinery item)
    {
        return item.ProjectOperations.Select(oo => oo.ProjectOperation.Id).ToList();
    }
    private string GetProjectOperationDetails(RequestMachinery item)
    {
        return string.Join(",", item.ProjectOperationDetails.Select(oo => oo.ProjectOperationDetail.OperationLocation.PublicName));
    }
    private List<long>? GetProjectOperationDetailIds(RequestMachinery item)
    {
        return item.ProjectOperationDetails.Select(oo => oo.ProjectOperationDetail.Id).ToList();
    }
    private string? GetContractorName(RequestMachinery item, List<UserModel?>? thirdParties, Company? company)
    {
        if (!(RequestMachineryStatusValidator.AllowforShowInGetFiltered.Any(x => x == item.Status)))
            return null;

        return item.ContractorId is not null && item.ContractorId > 0
            ? thirdParties?.FirstOrDefault(x => x?.Id == item.ContractorId)?.FullName
            : company?.NameFa;
    }
    private (decimal? TotalPrice, decimal? UnitPrice) CalculateTotalPrice(RequestMachinery item)
    {
        var confirmedInquiry = item.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed);
        if (confirmedInquiry is not null)
            return (confirmedInquiry.TotalPrice, confirmedInquiry.UnitPrice);

        var thirdPartyCompany = _userInfoProvider.CompanyThirdPartyId;
        if ((item.ContractorId > 0 && item.ContractorId != thirdPartyCompany) || item.MachineryReservations == null || item.MachineryReservations.Count == 0)
            return (null, null);

        var reserve = item.MachineryReservations.FirstOrDefault();
        var fixMachineryRate = reserve?.FixAssetMachinery.FixAssetMachineryRates
            .LastOrDefault(x => x.StartDate.Date <= reserve.StartDate.Date && x.EndDate.Date >= reserve.EndDate.Date);

        decimal? priceRate = reserve?.Unit switch
        {
            MachineryReservationUnit.Hourly => fixMachineryRate?.HourlyRate,
            MachineryReservationUnit.Daily => fixMachineryRate?.DailyRate,
            MachineryReservationUnit.Serviced => fixMachineryRate?.ServiceRate,
            MachineryReservationUnit.Volume => fixMachineryRate?.VolumeRate,
            _ => null
        };

        return (item.RequestCount * priceRate * item.ConfirmedTimeRequired, priceRate);
    }
    private (decimal? unitPrice, decimal? totalPrice) CalculateTotalPriceForGet(RequestMachinery item)
    {
        var confirmedInquiry = item.InquiryOperators.SelectMany(x => x.Inquiries).FirstOrDefault(x => x.IsConfirmed);
        if (confirmedInquiry is not null)
            return (confirmedInquiry.UnitPrice, confirmedInquiry.TotalPrice);

        var thirdPartyCompany = _userInfoProvider.CompanyThirdPartyId;
        if ((item.ContractorId > 0 && item.ContractorId != thirdPartyCompany) || item.MachineryReservations == null || item.MachineryReservations.Count == 0)
            return (null, null);

        var reserve = item.MachineryReservations.FirstOrDefault();
        var fixMachineryRate = reserve?.FixAssetMachinery.FixAssetMachineryRates
            .LastOrDefault(x => x.StartDate.Date <= reserve.StartDate.Date && x.EndDate.Date >= reserve.EndDate.Date);

        decimal? priceRate = reserve?.Unit switch
        {
            MachineryReservationUnit.Hourly => fixMachineryRate?.HourlyRate,
            MachineryReservationUnit.Daily => fixMachineryRate?.DailyRate,
            MachineryReservationUnit.Serviced => fixMachineryRate?.ServiceRate,
            MachineryReservationUnit.Volume => fixMachineryRate?.VolumeRate,
            _ => null
        };

        return (priceRate, item.RequestCount * priceRate * item.ConfirmedTimeRequired);
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
    private (TimeSpan operationWork, decimal? unitPrice, decimal? totalPrice) GetDatas(RequestMachinery requestMachinery)
    {
        TimeSpan totalWorkHours = TimeSpan.Zero;
        if (requestMachinery.ConfirmFromDate.HasValue && requestMachinery.ConfirmToDate.HasValue)
        {
            var subTime = requestMachinery.ConfirmToDate.Value.TimeOfDay - requestMachinery.ConfirmFromDate.Value.TimeOfDay;
            totalWorkHours = subTime;
        }

        var priceValues = CalculateTotalPriceForGet(requestMachinery);

        return (totalWorkHours, priceValues.unitPrice, priceValues.totalPrice);
    }
    private (string? TotalTimeWork, int? TotalDayWork, decimal? TotalHour) CalculateTotals(IEnumerable<GetOnProjectRequestReportsModel> models)
    {
        TimeSpan? totalTimeWork = null;
        string? totalTimeWorks = null;
        int? totalDayWork = null;
        decimal? totalHour = null;

        foreach (var model in models)
        {
            if (model.ConfirmFromDate.HasValue && model.ConfirmToDate.HasValue &&
                model.ConfirmFromTime.HasValue && model.ConfirmToTime.HasValue)
            {
                var workHours = model.ConfirmToTime.Value - model.ConfirmFromTime.Value;
                totalTimeWork = (totalTimeWork ?? TimeSpan.Zero) + workHours;
            }

            if (!string.IsNullOrEmpty(model.ConfirmedTimeRequired))
            {
                if (model.Unit == RequestMachineryUnit.Hourly)
                {
                    TimeSpan time = TimeSpan.Parse(model.ConfirmedTimeRequired);
                    totalHour = (totalHour ?? 0) + (decimal)time.TotalHours;
                }
                else
                {
                    totalHour = (totalHour ?? 0) + Convert.ToDecimal(model.ConfirmedTimeRequired);
                }
            }

            if (model.ConfirmFromDate.HasValue && model.ConfirmToDate.HasValue)
            {
                totalDayWork = (totalDayWork ?? 0) + ((model.ConfirmToDate.Value - model.ConfirmFromDate.Value).Days + 1);
            }
            else
            {
                totalDayWork = (totalDayWork ?? 0) + 1;
            }
        }

        if (totalTimeWork is not null && totalTimeWork.Value.Days >= 0)
        {
            var totalHours = (totalTimeWork!.Value.Days * 24) + totalTimeWork.Value.Hours;

            var hours = totalHours >= 10 ? totalHours.ToString() : $"0{totalHours}";
            var minutes = totalTimeWork.Value.Minutes >= 10 ? totalTimeWork.Value.Minutes.ToString() : $"0{totalTimeWork.Value.Minutes}";
            var seconds = totalTimeWork.Value.Seconds >= 10 ? totalTimeWork.Value.Seconds.ToString() : $"0{totalTimeWork.Value.Seconds}";

            totalTimeWorks = $"{hours}H:{minutes}M:{seconds}S";
        }

        return (totalTimeWorks, totalDayWork, totalHour);
    }
}
