using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDriver;
using Engineering.Application.Services.RequestMachineries.Models.GetOperationContractors;
using Engineering.Application.Services.RequestMachineries.Queries.GetOperationContractors;
using Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryByIdIncludeLess;
using Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryDriver;
using Engineering.Application.Services.RequestMachineryBills.Commands.CreateRequestMachineryBill;
using Engineering.Application.Services.RequestMachineryBills.Commands.DeleteRequestMachineryBill;
using Engineering.Application.Services.RequestMachineryBills.Commands.UpdateRequestMachineryBill;
using Engineering.Application.Services.RequestMachineryBills.Models.CreateRequestMachineryBill;
using Engineering.Application.Services.RequestMachineryBills.Models.DeleteRequestMachineryBill;
using Engineering.Application.Services.RequestMachineryBills.Models.GetFilteredRequestMachineryBills;
using Engineering.Application.Services.RequestMachineryBills.Models.GetRequestMachineryBillById;
using Engineering.Application.Services.RequestMachineryBills.Models.GetSupplierDrivers;
using Engineering.Application.Services.RequestMachineryBills.Models.UpdateRequestMachineryBill;
using Engineering.Application.Services.RequestMachineryBills.Queries.GetFilteredRequestMachineryBills;
using Engineering.Application.Services.RequestMachineryBills.Queries.GetRequestMachineryBillById;
using Engineering.Application.Services.RequestMachineryBills.Queries.GetSupplierDrivers;
using Engineering.Application.Services.RequestMachineryBills.Queries.IsDuplicateBill;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Financial.Application.AccountingDocuments.Models.PrintAccountingDocument;
using Financial.Application.WebServices.PdfMaker.Report.Commands.RequestMachineryBillReport;

namespace Engineering.Application.Services.RequestMachineryBills;

public partial class RequestMachineryBillLogic : IRequestMachineryBillLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<RequestMachineryBillLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserInfoService _userInfoService;
    private readonly long _currnetUserId;

    public RequestMachineryBillLogic(IMediator mediator, ILogger<RequestMachineryBillLogic> logger, IUnitOfWork unitOfWork, IUserProfileService userProfileService, IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _userInfoService = userInfoService;
        _currnetUserId = userProfileService.GetProfileInfo().UserId;
    }

    public async Task<Result<CreateRequestMachineryBillResponse?>> CreateRequestMachineryBill(CreateRequestMachineryBillRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateRequestMachineryBillValidator, CreateRequestMachineryBillRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateRequestMachineryBillResponse>(isValidRequest.Error!);

        var requestMachineryQuery = await _mediator.Send(new GetRequestMachineryByIdIncludeLessQuery(request.RequestMachineryId), ct);
        if (requestMachineryQuery.IsFailure)
            return Result.Failure<CreateRequestMachineryBillResponse>(requestMachineryQuery.Error!);
        var requestMachinery = requestMachineryQuery.Value;

        if (!string.IsNullOrEmpty(request.NumberPlates))
        {
            var onlyNumbers = new String(request.NumberPlates.Where(char.IsDigit).ToArray());
            var onlyLetters = new String(request.NumberPlates.Where(char.IsLetter).ToArray());

            var onlyLettersCount = onlyLetters.Count();
            if (onlyLetters == "الف")
                onlyLettersCount = 1;

            if (!(onlyNumbers.Count() == 7 && onlyLettersCount == 1))
                return Result.Failure<CreateRequestMachineryBillResponse>(FixAssetMachineryErrors.UnNumberPlates);
        }

        if (request.DriverId is not null && request.DriverName is not null)
            return Result.Failure<CreateRequestMachineryBillResponse>(FixAssetMachineryErrors.DriverInfoUnValid);

        if (request.DriverId is not null and > 0)
        {
            List<long> ids = [request.DriverId.Value];
            var driverQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (driverQuery.IsFailure)
                return Result.Failure<CreateRequestMachineryBillResponse>(driverQuery.Error!);
        }

        if (request.BillConfirmerId is not null and > 0)
        {
            List<long> ids = [request.BillConfirmerId.Value];
            var billConfirmerQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (billConfirmerQuery.IsFailure)
                return Result.Failure<CreateRequestMachineryBillResponse>(billConfirmerQuery.Error!);
        }

        if (request.ContractorId is not null and > 0)
        {
            List<long> ids = [request.ContractorId.Value];
            var contractorQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (contractorQuery.IsFailure)
                return Result.Failure<CreateRequestMachineryBillResponse>(contractorQuery.Error!);
        }

        if (request.SupplierId is not null and > 0)
        {
            List<long> ids = [request.SupplierId.Value];
            var contractorQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (contractorQuery.IsFailure)
                return Result.Failure<CreateRequestMachineryBillResponse>(contractorQuery.Error!);
        }

        var qrCodeUrl = $"{request.QRCodeUrl}{requestMachinery!.Id}";

        RequestMachineryBill? requestMachineryBill = null;
        for (DateTime currentDate = request.FromDate; currentDate <= request.ToDate; currentDate = currentDate.AddDays(1))
        {
            DateTime? fromDate = request.FromDate;
            DateTime? ToDate = request.ToDate;
            if (requestMachinery.RequestMachineryBills is not null && requestMachinery.RequestMachineryBills.Count > 0)
            {
                var requestBill = requestMachinery.RequestMachineryBills.Last();
                fromDate = requestBill.ToDate;
                ToDate = request.ToDate;
            }

            var IsDulicateQuery = await _mediator.Send(new IsDuplicateBillQuery(requestMachinery!.Id, fromDate!.Value, ToDate!.Value), ct);
            if (IsDulicateQuery.Value == true)
            {
                return Result.Failure<CreateRequestMachineryBillResponse>(RequestMachineryBillErrors.IsDuplicate);
            }

            var dateData = GetDates(currentDate, fromDate.Value, ToDate.Value);

            decimal? totalHours = (decimal)dateData.operationWork.TotalHours;
            var TotalPrice = (totalHours is null || totalHours <= 0 ? 1 : totalHours) * request.UnitPrice;

            var createResponse = await _mediator.Send(new CreateRequestMachineryBillCommand(
                requestMachinery,
                DateTime.Now,
                request.ContractorId,
                request.SupplierId,
                request.BillConfirmerId,
                request.DriverId,
                request.DriverName,
                request.NumberPlates,
                request.MachineryAssignment,
                qrCodeUrl,
                request.UnitPrice,
                TotalPrice,
                dateData.currentFrom,
                dateData.currentTo,
                dateData.operationWork.Ticks,
                request.Description),
                ct);
            if (createResponse.IsFailure)
                return Result.Failure<CreateRequestMachineryBillResponse>(createResponse.Error!);
            requestMachineryBill = createResponse.Value!;
        }

        if (request.DriverId is not null || request.DriverName is not null)
        {
            var updateDriver = await _mediator.Send(new UpdateRequestMachineryDriverCommand(requestMachinery!, request.DriverId, request.DriverName), ct);
            if (updateDriver.IsFailure)
            {
                return Result.Failure<CreateRequestMachineryBillResponse>(updateDriver.Error!);
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateRequestMachineryBillResponse(requestMachineryBill!.Id, true);
    }

    public async Task<Result<UpdateRequestMachineryBillResponse?>> UpdateRequestMachineryBill(UpdateRequestMachineryBillRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateRequestMachineryBillValidator, UpdateRequestMachineryBillRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateRequestMachineryBillResponse>(isValidRequest.Error!);

        var requestMachineryBillQuery = await _mediator.Send(new GetRequestMachineryBillByIdQuery(request.Id), ct);
        if (requestMachineryBillQuery.IsFailure)
            return Result.Failure<UpdateRequestMachineryBillResponse>(requestMachineryBillQuery.Error!);

        var requestMachineryBill = requestMachineryBillQuery.Value;

        if (!string.IsNullOrEmpty(request.NumberPlates))
        {
            var onlyNumbers = new String(request.NumberPlates.Where(char.IsDigit).ToArray());
            var onlyLetters = new String(request.NumberPlates.Where(char.IsLetter).ToArray());

            var onlyLettersCount = onlyLetters.Count();
            if (onlyLetters == "الف")
                onlyLettersCount = 1;

            if (!(onlyNumbers.Count() == 7 && onlyLettersCount == 1))
                return Result.Failure<UpdateRequestMachineryBillResponse>(FixAssetMachineryErrors.UnNumberPlates);
        }

        if (request.DriverId is not null && request.DriverName is not null)
            return Result.Failure<UpdateRequestMachineryBillResponse>(FixAssetMachineryErrors.DriverInfoUnValid);

        if (request.DriverId is not null and > 0)
        {
            List<long> ids = [request.DriverId.Value];
            var driverQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (driverQuery.IsFailure)
                return Result.Failure<UpdateRequestMachineryBillResponse>(driverQuery.Error!);
        }

        if (request.BillConfirmerId is not null and > 0)
        {
            List<long> ids = [request.BillConfirmerId.Value];
            var driverQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (driverQuery.IsFailure)
                return Result.Failure<UpdateRequestMachineryBillResponse>(driverQuery.Error!);
        }

        if (request.ContractorId is not null and > 0)
        {
            List<long> ids = [request.ContractorId.Value];
            var contractorQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (contractorQuery.IsFailure)
                return Result.Failure<UpdateRequestMachineryBillResponse>(contractorQuery.Error!);
        }

        if (request.SupplierId is not null and > 0)
        {
            List<long> ids = [request.SupplierId.Value];
            var supplierQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, ids.Count, ids, null, false, null), ct);
            if (supplierQuery.IsFailure)
                return Result.Failure<UpdateRequestMachineryBillResponse>(supplierQuery.Error!);
        }

        TimeSpan totalWorkHours = TimeSpan.Zero;
        if (request.ToDate.HasValue && request.FromDate.HasValue)
        {
            TimeSpan difference = request.ToDate.Value.Date.Subtract(request.FromDate.Value.Date);
            int days = difference.Days + 1;

            var subTime = request.ToDate.Value.TimeOfDay - request.FromDate.Value.TimeOfDay;
            totalWorkHours = subTime * days;
        }

        decimal? totalHours = (decimal)totalWorkHours.TotalHours;
        var TotalPrice = (totalHours is null || totalHours <= 0 ? 1 : totalHours) * request.UnitPrice;

        var updateResponse = await _mediator.Send(new UpdateRequestMachineryBillCommand(
            requestMachineryBill!,
            request.ContractorId,
            request.SupplierId,
            request.BillConfirmerId,
            request.DriverId,
            request.DriverName,
            request.NumberPlates,
            request.MachineryAssignment,
            request.UnitPrice,
            TotalPrice,
            request.FromDate,
            request.ToDate,
            totalWorkHours.Ticks,
            request.Description),
            ct);
        if (updateResponse.IsFailure)
            return Result.Failure<UpdateRequestMachineryBillResponse>(updateResponse.Error!);
        var response = updateResponse.Value!;

        if (request.DriverId is not null || request.DriverName is not null)
        {
            var updateDriver = await _mediator.Send(new UpdateRequestMachineryDriverCommand(
                requestMachineryBill!.RequestMachinery!,
                request.DriverId,
                request.DriverName), ct);
            if (updateDriver.IsFailure)
                return Result.Failure<UpdateRequestMachineryBillResponse>(updateDriver.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateRequestMachineryBillResponse(response.Id);
    }

    public async Task<Result<DeleteRequestMachineryBillResponse?>> DeleteRequestMachineryBill(DeleteRequestMachineryBillRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<DeleteRequestMachineryBillValidator, DeleteRequestMachineryBillRequest>(ct);
        if (isValidRequest.IsFailure)
        {
            return Result.Failure<DeleteRequestMachineryBillResponse>(isValidRequest.Error!);
        }

        var getRequestMachineryBill = await _mediator.Send(new GetRequestMachineryBillByIdQuery(request.RequestMachineryBillId), ct);
        if (getRequestMachineryBill.IsFailure)
        {
            return Result.Failure<DeleteRequestMachineryBillResponse>(getRequestMachineryBill.Error!);
        }

        var RequestMachineryBill = getRequestMachineryBill.Value!;

        var deleteResponse = await _mediator.Send(new DeleteRequestMachineryBillCommand(request.RequestMachineryBillId), ct);
        if (deleteResponse.IsFailure)
        {
            return Result.Failure<DeleteRequestMachineryBillResponse>(deleteResponse.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new DeleteRequestMachineryBillResponse(RequestMachineryBill.Id);
    }

    public async Task<Result<GetFilteredRequestMachineryBillsResponse?>> GetFilteredRequestMachineryBills(GetFilteredRequestMachineryBillsRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredRequestMachineryBillsValidator, GetFilteredRequestMachineryBillsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredRequestMachineryBillsResponse>(isValidRequest.Error!);

        long? companyId = _userInfoService.UserCompanyId is <= 0 or null ? null : _userInfoService.UserCompanyId;
        var getFiltered = await _mediator.Send(new GetFilteredRequestMachineryBillsQuery(
            null,
            request.RequestMachineryId,
            request.CostCenterId,
            request.ProjectId,
            request.ContractorIds,
            request.ProjectOperationIds,
            request.MachineriesGroupId,
            request.MachineryId,
            request.FromDate,
            request.ToDate,
            request.CreatorId,
            null,
            request.FilterData,
            companyId,
            request.OrderBy,
            request.PageIndex,
            request.PageSize), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetFilteredRequestMachineryBillsResponse>(getFiltered.Error!);
        var requestMachineryBills = getFiltered.Value!.Data!;

        var allIds = requestMachineryBills.SelectMany(x => new[] { x.CreatorId, x.RequestMachinery.CreatorId }).Distinct().ToList();
        var users = await WebServicesLogic.UserDataReceiver(allIds.Distinct().ToList(), null, _mediator, ct);

        var companyIds = requestMachineryBills?.Where(x => x.RequestMachinery.CompanyId is not null && x.RequestMachinery.CompanyId > 0)
            .Select(x => (long)x.RequestMachinery.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var allThirdPartyIds = requestMachineryBills?
            .SelectMany(x => new[] { x.ContractorId is not null && x.ContractorId > 0 ? (long)x.ContractorId!:0,
                                     x.SupplierId is not null && x.SupplierId > 0 ? (long)x.SupplierId!:0,
                                     x.DriverId is not null && x.DriverId > 0 ? (long)x.DriverId!:0,
                                     x.BillConfirmerId is not null && x.BillConfirmerId > 0 ? (long)x.BillConfirmerId!:0
                                   }
            ).Where(x => x > 0).Distinct().ToList();
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allThirdPartyIds, null, null, _mediator, ct);

        var models = new List<GetFilteredRequestMachineryBillsModel>();
        foreach (var bill in requestMachineryBills!)
        {
            var projectOperations = string.Join(",", bill.RequestMachinery.ProjectOperations.Select(oo => oo.ProjectOperation).Select(oo => oo.OperationInfo.OperationInfoName).ToList());
            var projectOperationDetails = string.Join(",", bill.RequestMachinery.ProjectOperationDetails.Select(oo => oo.ProjectOperationDetail).Select(oo => oo.OperationLocation.PublicName).ToList());

            var company = companies?.FirstOrDefault(x => x.Id == bill.RequestMachinery.CompanyId);

            var contractor = contractors?.FirstOrDefault(x => x is not null && x.Id == bill.ContractorId);
            var driver = contractors?.FirstOrDefault(x => x is not null && x.Id == bill.DriverId);
            var supplier = contractors?.FirstOrDefault(x => x is not null && x.Id == bill.SupplierId);
            var billConfirmer = contractors?.FirstOrDefault(x => x is not null && x.Id == bill.BillConfirmerId);

            string? supplierName = null;
            if (bill.RequestMachinery.Status == RequestMachineryStatus.OnProject)
            {
                if (bill.SupplierId is not null && bill.SupplierId > 0)
                {
                    supplierName = contractors?.FirstOrDefault(x => x?.Id == bill.SupplierId)?.FullName;
                }
                else
                    supplierName = company?.NameFa;
            }

            models.Add(new GetFilteredRequestMachineryBillsModel()
            {
                CostCenterId = bill.RequestMachinery.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                CostCenterName = bill.RequestMachinery.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                Created = bill.Created,
                MachineryGroupId = bill.RequestMachinery.Machinery!.MachineriesGroup.Id,
                MachineryGroupName = bill.RequestMachinery.Machinery!.MachineriesGroup.GroupName,
                MachineryId = bill.RequestMachinery.Machinery?.Id,
                MachineryName = bill.RequestMachinery.Machinery?.MachineryName,
                ProjectId = bill.RequestMachinery.Project.Id,
                ProjectName = bill.RequestMachinery.Project.ProjectName,
                RequestMachineryBillId = bill.Id,
                Status = bill.RequestMachinery.Status,
                Unit = bill.RequestMachinery.Unit,
                ProjectOperations = projectOperations,
                ProjectOperationDetails = projectOperationDetails,
                Creator = users?.FirstOrDefault(x => x.UserId == bill.CreatorId)?.FullName,
                CreatorId = users?.FirstOrDefault(x => x.UserId == bill.CreatorId)?.UserId,
                RequestCreator = users?.FirstOrDefault(x => x.UserId == bill.RequestMachinery.CreatorId)?.FullName,
                RequestCreatorId = users?.FirstOrDefault(x => x.UserId == bill.RequestMachinery.CreatorId)?.UserId,
                CompanyId = bill.RequestMachinery.CompanyId,
                CompanyNameFa = company?.NameFa,
                FromDate = bill?.FromDate,
                FromTime = bill?.FromDate != null ? bill!.FromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
                ToDate = bill?.ToDate,
                ToTime = bill?.ToDate != null ? bill!.ToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
                ContractorId = bill?.ContractorId,
                Contractor = contractor?.FullName,
                ConfirmedDescription = bill?.RequestMachinery.ConfirmedDescription,
                ConfirmedTimeRequired = bill?.RequestMachinery.ConfirmedTimeRequired.ToString(),
                RequestNumber = bill?.RequestMachinery.RequestNumber,
                BillDate = bill!.BillDate,
                BillNumber = bill!.BillNumber,
                Description = bill.Description,
                DriverId = bill?.DriverId,
                DriverName = bill?.DriverName,
                Driver = driver?.FullName,
                MachineryAssignment = bill?.MachineryAssignment,
                NumberPlates = bill?.NumberPlate,
                OperationDuration = bill?.OperationDuration != null && bill.OperationDuration.HasValue ? TimeCalculator.TicksToTimeSpan(bill.OperationDuration.Value) : null,
                RequestMachineryId = bill?.RequestMachinery.Id,
                NumberPlatesModel = SetNumberPlates(bill?.NumberPlate),
                QRCodeUrl = bill?.QRCodeUrl,
                SupplierId = bill?.SupplierId,
                Supplier = supplierName,
                TotalPrice = bill?.TotalPrice,
                UnitPrice = bill?.UnitPrice,
                BillConfirmerId = bill?.BillConfirmerId,
                BillConfirmer = billConfirmer?.FullName
            });
        }
        return new GetFilteredRequestMachineryBillsResponse(models, getFiltered.Value!.RowCount!);
    }

    public async Task<Result<GetSupplierDriversResponse?>> GetSupplierDrivers(GetSupplierDriversRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetSupplierDriversValidator, GetSupplierDriversRequest>(ct);
        if (isValidRequest.IsFailure)
        {
            return Result.Failure<GetSupplierDriversResponse>(isValidRequest.Error!);
        }

        List<string?>? driverNames = [];

        var getFiltered = await _mediator.Send(new GetSupplierDriversQuery(request.SupplierId, request.FilterData), ct);
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        driverNames.AddRange(getFiltered.Value ?? []);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

        if (request.RequestMachineryId is not null && request.RequestMachineryId > 0)
        {
            var getDriver = await _mediator.Send(new GetRequestMachineryDriverQuery(request.RequestMachineryId), ct);
            if (getDriver.Value is not null)
            {
                var driverModel = getDriver.Value;
                if (driverModel.DriverId.HasValue && driverModel.DriverId > 0)
                {
                    var drivers = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver([(long)driverModel.DriverId], null, null, _mediator, ct);
                    driverNames.Add(drivers?.FirstOrDefault()?.FullName);
                }

                if (!string.IsNullOrEmpty(driverModel.DriverName))
                {
                    driverNames.Add(driverModel.DriverName);
                }
            }
        }

        var models = new List<GetSupplierDriversModel>();
        if (driverNames is not null)
            if (driverNames.Count > 0)
                foreach (var name in driverNames!)
                {
                    if (!string.IsNullOrEmpty(name))
                        models.Add(new GetSupplierDriversModel()
                        {
                            DriverName = name
                        });
                }

        return new GetSupplierDriversResponse(models ?? [], models?.Count ?? 0);
    }

    public async Task<Result<GetRequestMachineryBillByIdResponse?>> GetRequestMachineryBillById(GetRequestMachineryBillByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetRequestMachineryBillByIdValidator, GetRequestMachineryBillByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetRequestMachineryBillByIdResponse>(isValidRequest.Error!);

        var getRequestMachineryBill = await _mediator.Send(new GetRequestMachineryBillByIdQuery(request.RequestMachineryBillId), ct);
        if (getRequestMachineryBill.IsFailure)
            return Result.Failure<GetRequestMachineryBillByIdResponse>(getRequestMachineryBill.Error!);
        var requestMachineryBill = getRequestMachineryBill.Value!;

        List<long>? allIds = [requestMachineryBill.CreatorId, requestMachineryBill.RequestMachinery.CreatorId];
        var users = await WebServicesLogic.UserDataReceiver(allIds.Distinct().ToList(), null, _mediator, ct);

        var companyId = requestMachineryBill?.RequestMachinery.CompanyId;
        var company = await WebServicesLogic.CompanyDataReceiver(companyId, _mediator, ct);

        List<long>? allThirdPartyIds = [
            requestMachineryBill?.ContractorId ?? 0,
            requestMachineryBill?.DriverId ?? 0,
            requestMachineryBill?.SupplierId ?? 0,
            requestMachineryBill?.BillConfirmerId ?? 0
            ];
        var contractors = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(allThirdPartyIds.Where(x => x > 0).Distinct().ToList(), null, null, _mediator, ct);

        var projectOperations = string.Join(",", requestMachineryBill!.RequestMachinery.ProjectOperations.Select(oo => oo.ProjectOperation).Select(oo => oo.OperationInfo.OperationInfoName).ToList());
        var projectOperationDetails = string.Join(",", requestMachineryBill!.RequestMachinery.ProjectOperationDetails.Select(oo => oo.ProjectOperationDetail).Select(oo => oo.OperationLocation.PublicName).ToList());

        var response = new GetRequestMachineryBillByIdResponse()
        {
            CostCenterName = requestMachineryBill.RequestMachinery.Project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
            CostCenterId = requestMachineryBill.RequestMachinery.Project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
            Created = requestMachineryBill.Created,
            MachineryGroupId = requestMachineryBill.RequestMachinery.Machinery!.MachineriesGroup.Id,
            MachineryGroupName = requestMachineryBill.RequestMachinery.Machinery!.MachineriesGroup.GroupName,
            MachineryId = requestMachineryBill.RequestMachinery.Machinery?.Id,
            MachineryName = requestMachineryBill.RequestMachinery.Machinery?.MachineryName,
            ProjectId = requestMachineryBill.RequestMachinery.Project.Id,
            ProjectName = requestMachineryBill.RequestMachinery.Project.ProjectName,
            RequestMachineryBillId = requestMachineryBill.Id,
            Status = requestMachineryBill.RequestMachinery.Status,
            Unit = requestMachineryBill.RequestMachinery.Unit,
            ProjectOperations = projectOperations,
            ProjectOperationDetails = projectOperationDetails,
            Creator = users?.FirstOrDefault(x => x.UserId == requestMachineryBill.CreatorId)?.FullName,
            CreatorId = users?.FirstOrDefault(x => x.UserId == requestMachineryBill.CreatorId)?.UserId,
            RequestCreator = users?.FirstOrDefault(x => x.UserId == requestMachineryBill.RequestMachinery.CreatorId)?.FullName,
            RequestCreatorId = users?.FirstOrDefault(x => x.UserId == requestMachineryBill.RequestMachinery.CreatorId)?.UserId,
            CompanyId = requestMachineryBill.RequestMachinery.CompanyId,
            CompanyNameFa = company?.NameFa,
            FromDate = requestMachineryBill?.FromDate,
            FromTime = requestMachineryBill?.FromDate != null ? requestMachineryBill!.FromDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
            ToDate = requestMachineryBill?.ToDate,
            ToTime = requestMachineryBill?.ToDate != null ? requestMachineryBill!.ToDate.Value.TimeOfDay : new TimeSpan(0, 0, 0),
            ContractorId = requestMachineryBill?.ContractorId,
            Contractor = contractors?.FirstOrDefault(x => x?.Id == requestMachineryBill?.ContractorId)?.FullName,
            SupplierId = requestMachineryBill?.SupplierId,
            Supplier = requestMachineryBill?.SupplierId is not null && requestMachineryBill.SupplierId > 0 ? contractors?.FirstOrDefault(x => x?.Id == requestMachineryBill.SupplierId)?.FullName : company?.NameFa,
            ConfirmedDescription = requestMachineryBill?.RequestMachinery.ConfirmedDescription,
            ConfirmedTimeRequired = requestMachineryBill?.RequestMachinery.ConfirmedTimeRequired.ToString(),
            RequestNumber = requestMachineryBill?.RequestMachinery.RequestNumber,
            BillDate = requestMachineryBill!.BillDate,
            BillNumber = requestMachineryBill!.BillNumber,
            Description = requestMachineryBill.Description,
            DriverId = requestMachineryBill?.DriverId,
            DriverName = requestMachineryBill?.DriverName,
            Driver = contractors?.FirstOrDefault(x => x?.Id == requestMachineryBill?.DriverId)?.FullName,
            MachineryAssignment = requestMachineryBill?.MachineryAssignment,
            NumberPlates = requestMachineryBill?.NumberPlate,
            OperationDuration = requestMachineryBill?.OperationDuration != null && requestMachineryBill.OperationDuration.HasValue ? TimeCalculator.TicksToTimeSpan(requestMachineryBill.OperationDuration.Value) : null,
            RequestMachineryId = requestMachineryBill?.RequestMachinery.Id,
            NumberPlatesModel = SetNumberPlates(requestMachineryBill?.NumberPlate),
            QRCodeUrl = requestMachineryBill?.QRCodeUrl,
            MachineryPlate = !string.IsNullOrEmpty(requestMachineryBill?.NumberPlate) ? requestMachineryBill?.NumberPlate : requestMachineryBill?.MachineryAssignment,
            DriverFullName = !string.IsNullOrEmpty(requestMachineryBill?.DriverName) ? requestMachineryBill?.DriverName : contractors?.FirstOrDefault(x => x?.Id == requestMachineryBill?.DriverId)?.FullName,
            TotalPrice = requestMachineryBill?.TotalPrice,
            UnitPrice = requestMachineryBill?.UnitPrice,
            BillConfirmerId = requestMachineryBill?.BillConfirmerId,
            BillConfirmer = contractors?.FirstOrDefault(x => x?.Id == requestMachineryBill?.BillConfirmerId)?.FullName,
        };

        return response;
    }

    public async Task<Result<RequestMachineryBillReportResponse?>> RequestMachineryBillReport(RequestMachineryBillReportRequest request, CT ct)
    {
        _logger.LogInformation("Get AccountingDocument by id, id:{Id}", request.Ids);

        var accountDocumentPrintData = await RequestMachineryBillReportData(request.Ids, ct);
        if (accountDocumentPrintData.IsFailure)
        {
            return Result.Failure<RequestMachineryBillReportResponse>(accountDocumentPrintData.Error!);
        }

        var (data, parameters) = accountDocumentPrintData.Value;

        var createInvoiceReportCmd = new RequestMachineryBillReportCommand(data, parameters);
        var createInvoiceReportResult = await _mediator.Send(createInvoiceReportCmd, ct);
        if (createInvoiceReportResult.IsFailure)
        {
            return Result.Failure<RequestMachineryBillReportResponse>(createInvoiceReportResult.Error!);
        }

        var result = createInvoiceReportResult.Value!;

        return new RequestMachineryBillReportResponse(result);
    }

    public async Task<Result<GetOperationContractorsResponse?>> GetOperationContractors(GetOperationContractorsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetTransportationRequestContractors");

        var isValidRequest = await request.IsValidAsync<GetOperationContractorsRequestValidator, GetOperationContractorsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetOperationContractorsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetOperationContractorsQuery(request.RequestMachineryId), ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<GetOperationContractorsResponse>(RequestMachineryErrors.FilteredMachineryContractorNotFound);
        var ids = response?.Value?.Data!;

        List<UserModel?>? contractors = new();
        var data = new List<GetOperationContractorsResponseModel>();
        if (ids?.Count > 0)
        {
            var responseValue = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(ids!, request.FilterData, null, _mediator, ct);
            if (responseValue is not null && responseValue.Count > 0)
                foreach (var item in responseValue)
                {
                    contractors.Add(item);
                }

            if (!string.IsNullOrEmpty(request.FilterData))
            {
                foreach (var id in ids)
                {
                    if (!contractors.Any(x => x?.Id == id))
                        continue;

                    var contractor = contractors.Where(x => x?.Id == id).FirstOrDefault();
                    data.Add(new GetOperationContractorsResponseModel()
                    {
                        Id = contractor!.Id,
                        UserId = contractor?.UserId,
                        DefaultPhoneNo = contractor?.DefaultPhoneNo,
                        OrganizationCode = contractor?.OrganizationCode,
                        FullName = contractor?.FullName,
                        Nickname = contractor?.Nickname,
                    });
                }
            }
            else
            {
                foreach (var id in ids)
                {
                    var contractor = contractors.Where(x => x?.Id == id).FirstOrDefault();
                    data.Add(new GetOperationContractorsResponseModel()
                    {
                        Id = contractor!.Id,
                        UserId = contractor?.UserId,
                        DefaultPhoneNo = contractor?.DefaultPhoneNo,
                        OrganizationCode = contractor?.OrganizationCode,
                        FullName = contractor?.FullName,
                        Nickname = contractor?.Nickname,
                    });
                }
            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetOperationContractorsResponse(responseData ?? new List<GetOperationContractorsResponseModel>(0), contractors?.Count ?? 0);

    }
}
