using Engineering.Application.Services.ConsumableVolumes.Commands.Experts.CreateExpertConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Commands.Machineries.CreateMachineryConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Commands.Products.CreateProductConsumableVolume;
using Engineering.Application.Services.ConsumableVolumes.Commands.UpdateConsumableVolumes;
using Engineering.Application.Services.OperationInfoDependencies.Queries.GetOperationInfoDependence;
using Engineering.Application.Services.ProjectOperations.Models.CreateProjectOperationActions;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Models.Models;
using Engineering.Application.Services.ProjectOperations.Models.ProjectOperationExcelImports;
using Engineering.Application.Services.ProjectOperations.Models.ProjectOperationWorkloadManagement;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Models;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Queries.GetMeasureunitById;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.Enums;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Errors.Actions;
using System.Globalization;

namespace Engineering.Application.Services.ProjectOperations;

public partial class ProjectOperationLogic : IProjectOperationLogic
{

    private async Task<bool> OperationInfoChanger(OperationInfo oldOperationInfo, OperationInfo newOperationInfo, long projectOperationId, List<ProjectOperationDetail>? projectOperationDetails, CT ct)
    {
        if (!oldOperationInfo.HaveStandard && !newOperationInfo.HaveStandard)
            return true;

        if (projectOperationDetails is null)
            return true;

        if (!projectOperationDetails.Any())
            return true;

        var experts = projectOperationDetails.SelectMany(x => x.ConsumableVolumeExperts).ToList();
        var machineries = projectOperationDetails.SelectMany(x => x.ConsumableVolumeMachineries).ToList();
        var products = projectOperationDetails.SelectMany(x => x.ConsumableVolumeProducts).ToList();
        if (!experts.Any() && !machineries.Any() && !products.Any())
            return true;

        var newExperts = newOperationInfo.ConsumptionStandardExperts.ToList();
        var newMachineries = newOperationInfo.ConsumptionStandardMachineries.ToList();
        var newProducts = newOperationInfo.ConsumptionStandardProduct.ToList();

        if (experts.Count > 0)
            foreach (var expert in experts)
            {
                if (expert.IsStandard)
                    expert.SetIsStandard(false);

                var newExpert = newExperts.FirstOrDefault(x => x.ExpertUnitId.Equals(expert.ExpertId));
                if (newExpert is not null)
                {
                    var pod = expert.ProjectOperationDetail;
                    var finalAmount = pod.Length * pod.Width * pod.Height * pod.Number * pod.Weight;

                    expert.SetIsStandard(true);
                    expert.SetStandardValue(newExpert.TimeSpant);
                    expert.SetFinalValue(newExpert.TimeSpant * (long)finalAmount);
                    expert.SetNumber(newExpert.ExpertNumber);
                    expert.SetUnusedPercentage(newExpert.UnusedPercentage);
                }
            }

        if (machineries.Count > 0)
            foreach (var machinery in machineries)
            {
                if (machinery.IsStandard)
                    machinery.SetIsStandard(false);

                var newMachinery = newMachineries.FirstOrDefault(x => x.Machinery.Id.Equals(machinery.Machinery.Id));
                if (newMachinery is not null)
                {
                    var pod = machinery.ProjectOperationDetail;
                    var finalAmount = pod.Length * pod.Width * pod.Height * pod.Number * pod.Weight;

                    machinery.SetIsStandard(true);
                    machinery.SetStandardValue(newMachinery.TimeSpant);
                    machinery.SetFinalValue(newMachinery.TimeSpant * (long)finalAmount);
                    machinery.SetNumber(newMachinery.MachineryNumber);
                    machinery.SetUnusedPercentage(newMachinery.UnusedPercentage);
                }
            }

        if (products.Count > 0)
            foreach (var product in products)
            {
                if (product.IsStandard)
                    product.SetIsStandard(false);

                var newProduct = newProducts.FirstOrDefault(x => x.ProductUnitId.Equals(product.ProductGroupId));
                if (newProduct is not null)
                {
                    var pod = product.ProjectOperationDetail;
                    var finalAmount = pod.Length * pod.Width * pod.Height * pod.Number * pod.Weight;

                    product.SetIsStandard(true);
                    product.SetStandardValue(newProduct.Number);
                    product.SetFinalValue(newProduct.Number * finalAmount);
                    product.SetUnusedPercentage(newProduct.UnusedPercentage);
                }
            }

        var response = await _mediator.Send(new UpdateConsumableVolumesCommand(experts, machineries, products), ct);

        foreach (var item in projectOperationDetails)
            foreach (var expert in newExperts)
                if (!item.ConsumableVolumeExperts.Any(x => x.ExpertId.Equals(expert.ExpertUnitId)))
                {
                    var createExpert = await _mediator.Send(new CreateConsumableVolumeExpertCommand(item, expert.ExpertUnitId, expert.ExpertNumber,
                        expert.UnusedPercentage, true, expert.TimeSpant, expert.TimeSpant), ct);
                    if (createExpert.IsFailure)
                        return false;
                }

        foreach (var item in projectOperationDetails)
            foreach (var machinery in newMachineries)
                if (!item.ConsumableVolumeMachineries.Any(x => x.Machinery.Id.Equals(machinery.Machinery.Id)))
                {
                    var createExpert = await _mediator.Send(new CreateConsumableVolumeMachineryCommand(item, machinery.Machinery, machinery.MachineryNumber,
                        machinery.UnusedPercentage, true, machinery.TimeSpant, machinery.TimeSpant, null), ct);
                    if (createExpert.IsFailure)
                        return false;
                }

        foreach (var item in projectOperationDetails)
            foreach (var product in newProducts)
                if (!item.ConsumableVolumeProducts.Any(x => x.ProductGroupId.Equals(product.ProductUnitId)))
                {
                    if (product.StandardProductType == StandardProductType.ProductGroup && (!item.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup)
                    .Any(x => x.ProductGroupId.Equals(product.ProductUnitId))))
                    {
                        var createProduct = await _mediator.Send(new CreateConsumableVolumeProductCommand(item, product.ProductUnitId, product.UnusedPercentage, true, product.Number, product.Number, VolumeProductType.ProductGroup), ct);
                        if (createProduct.IsFailure)
                            return false;
                    }
                    else if (product.StandardProductType == StandardProductType.Category && (!item.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.Category)
                        .Any(x => x.ProductGroupId.Equals(product.ProductUnitId))))
                    {
                        var createProduct = await _mediator.Send(new CreateConsumableVolumeProductCommand(item, product.ProductUnitId, product.UnusedPercentage, true, product.Number, product.Number, VolumeProductType.Category), ct);
                        if (createProduct.IsFailure)
                            return false;
                    }
                }

        return response.Value!;
    }

    private async Task<OperationInfoDependencyModel?> OperationInfoDependencyDataReceiver(OperationInfo operationInfo, List<Measureunit>? measureUnits, CT ct)
    {
        OperationInfo? relationInfo = null;
        Measureunit? measureunit = null;
        var dependencyInfo = await _mediator.Send(new GetOperationInfoDependenceQuery(operationInfo.Id), ct);
        if (dependencyInfo.Value is not null)
        {
            relationInfo = dependencyInfo.Value.OperationInfo;

            if ((measureUnits is not null && measureUnits.Count > 0 && !measureUnits.Any(x => x.Id.Equals(relationInfo!.UnitOfMeasurementId))) || measureUnits is null)
            {
                var measureunitData = await _mediator.Send(new GetMeasureunitByIdQuery(relationInfo!.UnitOfMeasurementId), ct);
                if (measureunitData.Value is not null)
                    measureunit = measureunitData.Value;
            }
            else
                measureunit = measureUnits.Where(x => x.Id == relationInfo?.UnitOfMeasurementId).FirstOrDefault();
        }

        return new OperationInfoDependencyModel(dependencyInfo.Value?.Id, dependencyInfo.Value?.Id != 0 && dependencyInfo.Value?.Id is not null ? true : null,
            relationInfo?.Id, relationInfo?.OperationInfoName, relationInfo?.OperationInfoCode, relationInfo?.UnitOfMeasurementId, measureunit?.Name,
            dependencyInfo.Value?.WorkingDays, dependencyInfo.Value?.DependencyType, dependencyInfo.Value?.DependencyType.GetEnumDescription());
    }

    private async Task<OperationInfoDependencyModel?> OperationInfoDependencyDataReceiver(OperationInfoDataModel operationInfoModel, List<Measureunit>? measureUnits, CT ct)
    {
        OperationInfo? relationInfo = null;
        Measureunit? measureunit = null;
        var dependencyInfo = await _mediator.Send(new GetOperationInfoDependenceQuery(operationInfoModel.OperationInfoId), ct);
        if (dependencyInfo.Value is not null)
        {
            relationInfo = dependencyInfo.Value.OperationInfo;

            if ((measureUnits is not null && measureUnits.Count > 0 && !measureUnits.Any(x => x.Id.Equals(relationInfo!.UnitOfMeasurementId))) || measureUnits is null)
            {
                var measureunitData = await _mediator.Send(new GetMeasureunitByIdQuery(relationInfo!.UnitOfMeasurementId), ct);
                if (measureunitData.Value is not null)
                    measureunit = measureunitData.Value;
            }
            else
                measureunit = measureUnits.Where(x => x.Id == relationInfo?.UnitOfMeasurementId).FirstOrDefault();
        }

        return new OperationInfoDependencyModel(dependencyInfo.Value?.Id, dependencyInfo.Value?.Id != 0 && dependencyInfo.Value?.Id is not null ? true : null,
            relationInfo?.Id, relationInfo?.OperationInfoName, relationInfo?.OperationInfoCode, relationInfo?.UnitOfMeasurementId, measureunit?.Name,
            dependencyInfo.Value?.WorkingDays, dependencyInfo.Value?.DependencyType, dependencyInfo.Value?.DependencyType.GetEnumDescription());
    }

    private ProjectOperationModel FullModeling(ProjectOperation item, List<Measureunit>? measureunits, ProjectOperationDependencyModel? dependency)
    {
        var info = item.OperationInfo;
        var project = item.Project;
        var costCenter = item.Project?.ProjectCostCenters.FirstOrDefault()?.CostCenter;

        ///TODO Employers
        //var contract = item.EmployerContract;
        var projectMersur = measureunits?.Where(m => m.Id == item.UnitOfMeasurementId).FirstOrDefault();
        var infoMersur = measureunits?.Where(m => m.Id == item.OperationInfo.UnitOfMeasurementId).FirstOrDefault();
        var startDate = item.ProjectOperationDetails.Where(x => x.StartDate is not null).Min(x => x.StartDate);
        var endDate = item.ProjectOperationDetails.Where(x => x.EndDate is not null).Max(x => x.EndDate);
        var workload = WorkloadCalc(item);

        ///TODO Employers
        //var haveContract = contract is null ? false : true;
        var contractCode = "بدون قرارداد";

        ///TODO Employers
        //if (contract is not null)
        //{
        //    if (!string.IsNullOrEmpty(contract.Code))
        //        contractCode = contract.Code;
        //    else
        //        contractCode = "بدون  شماره قرارداد";
        //}

        ///TODO Employers
        var urls = item.ProjectOperationDocuments.Select(x => x.Url).ToList();
        return new ProjectOperationModel(
            item.Id,
            info.Id,
            info.OperationInfoName,
            info.OperationInfoCode,
            info.HaveStandard,
            info.UnitOfMeasurementId,
            infoMersur?.Name,
            project?.Id,
            project?.ProjectName,
            project?.ProjectCode,
            costCenter?.Id,
            costCenter?.CostCenterName,
            costCenter?.CostCenterCode,
            //haveContract,
            //contract?.Id,
            contractCode,
            item.UnitOfMeasurementId,
            projectMersur?.Name,
            workload.Workload,
            workload.UsedWorkload,
            workload.RemainingWorkload,
            item.TolerancePercentage,
            item.Price,
            item.Priority,
            item.ProjectOperationStatus,
            item.ProjectOperationStatus.GetEnumDescription(),
            dependency?.Id,
            dependency?.IsDefaultRelation,
            dependency?.RelationId,
            dependency?.RelationName,
            dependency?.RelationCode,
            dependency?.RelationMeasurementId,
            dependency?.RelationMeasurementName,
            dependency?.RelationDays,
            dependency?.Type,
            dependency?.TypeDescription,
            startDate,
            endDate,
            item.Created,
            urls,
            item.GoodsInProgress,
            item.Description);
    }

    //بررسی حجم کار
    private ProjectOperationWorkloadManagementResponse WorkloadCalc(ProjectOperation value)
    {
        var usedWorkload = Convert.ToInt64(value.ProjectOperationDetails.Where(x => !x.IsDeleted).Select(x => x.FinalAmount).Sum());
        var remainingWorkload = value.Workload - usedWorkload;

        return new ProjectOperationWorkloadManagementResponse(value.Id, value.Workload, usedWorkload, remainingWorkload);
    }

    private bool Workloader(ProjectOperation projectOperation, decimal workload)
    {
        var finalAmounts = projectOperation.ProjectOperationDetails.Select(x => x.FinalAmount).Sum();
        if (finalAmounts > workload)
            return false;
        else
            return true;
    }

    private List<long> UserIdCollectors(List<ProjectOperation> items)
    {
        List<long> ids = [];
        if (items is not null)
            if (items!.Count > 0)
            {
                var implementationAssistants = items.SelectMany(x => x.Project.ProjectImplementationAssistants).ToList();
                if (implementationAssistants.Any())
                    ids.AddRange(implementationAssistants.Select(x => x.ImplementationAssistantUserId).ToList());

                var technicalAssistants = items.SelectMany(x => x.Project.ProjectTechnicalAssistants).ToList();
                if (technicalAssistants.Any())
                    ids.AddRange(technicalAssistants.Select(x => x.TechnicalAssistantUserId).ToList());

                var projectOperationDetails = items.Where(x => x.ProjectOperationDetails.Any(d => d.DailyOperations.Any())).SelectMany(x => x.ProjectOperationDetails).ToList();
                var dailyOperations = projectOperationDetails.Where(x => x.DailyOperations.Any(d => d.DailyProjectOperationServices.Any())).SelectMany(x => x.DailyOperations).ToList();
                var dailyProjectOperationServices = dailyOperations.Where(x => x.DailyProjectOperationServices.Any()).SelectMany(x => x.DailyProjectOperationServices).ToList();
                var services = dailyProjectOperationServices.Select(x => x.ProjectOperationDetailContractorService).ToList();
                if (services.Any())
                    ids.AddRange(services.Where(x => x.ContractorId is not null && x.ContractorId > 0).Select(x => x.ContractorId!.Value).ToList());
            }

        return ids.Where(x => x > 0).Distinct().ToList();
    }

    private List<long> UserIdCollectors(List<GetsProjectOperationReportingModel> items)
    {
        List<long> ids = [];
        if (items is not null)
            if (items!.Count > 0)
            {
                var implementationAssistantIds = items.Where(x => x.ImplementationAssistantIds is not null && x.ImplementationAssistantIds.Count > 0).SelectMany(x => x.ImplementationAssistantIds!).Where(x => x > 0).Select(x => x).ToList();
                if (implementationAssistantIds.Any())
                    ids.AddRange(implementationAssistantIds);

                var technicalAssistantIds = items.Where(x => x.TechnicalAssistantIds is not null && x.TechnicalAssistantIds.Count > 0).SelectMany(x => x.TechnicalAssistantIds!).Where(x => x > 0).Select(x => x).ToList();
                if (technicalAssistantIds.Any())
                    ids.AddRange(technicalAssistantIds);

                var contractorIds = items.Where(x => x.ContractorIds is not null && x.ContractorIds.Count > 0).SelectMany(x => x.ContractorIds!).Where(x => x != null && x > 0).Select(x => x).ToList();
                if (contractorIds is not null && contractorIds.Count > 0 && contractorIds.Any(x => x != null && x > 0))
                {
                    var contractids = contractorIds.Adapt<List<long>>();
                    ids.AddRange(contractids);
                }
            }

        return ids.Where(x => x > 0).Distinct().ToList();
    }

    private List<long> CreatorIdCollectors(List<ProjectOperation> items)
    {
        List<long> ids = [];
        if (items is not null)
            if (items!.Count > 0)
            {
                ids.AddRange(items!.Select(x => x.CreatorId).ToList());

                var projectOperationDetails = items.Where(x => x.ProjectOperationDetails.Any(d => d.DailyOperations.Any())).SelectMany(x => x.ProjectOperationDetails).ToList();
                var dailyOperations = projectOperationDetails.Where(x => x.DailyOperations.Any(d => d.DailyProjectOperationServices.Any())).SelectMany(x => x.DailyOperations).ToList();

                ids.AddRange(dailyOperations!.Select(x => x.CreatorId).ToList());
            }

        return ids.Where(x => x > 0).Distinct().ToList();
    }

    private List<long> CreatorIdCollectors(List<GetsProjectOperationReportingModel> items)
    {
        List<long> ids = [];
        if (items is not null)
            if (items!.Count > 0)
            {
                ids.AddRange(items!.Select(x => x.CreatorId).Distinct().ToList());

                ids.AddRange(items!.Where(x => x.DailyCreatorsIds is not null && x.DailyCreatorsIds.Count > 0).SelectMany(x => x.DailyCreatorsIds!).Select(x => x).Where(x => x > 0).Distinct().ToList());
            }

        return ids.Where(x => x > 0).Distinct().ToList();
    }

    private async Task<Result<CreateProjectOperationActionsResponse>> CreateProjectOperationActionsHandler(
    CreateProjectOperationActionsRequest request,
    ProjectOperation projectOperation,
    CT ct)
    {
        var reqActionIds = request.actions
            .Select(x => x.OperationInfoActionId)
            .Distinct()
            .ToList();
        var actions = await _operationInfoActionRepository.GetOperationInfoActionByActionId(reqActionIds, ct);
        if (actions == null || actions.Count < reqActionIds.Count)
            return Result.Failure<CreateProjectOperationActionsResponse>(
                ActionErrors.ActionWithIdNotFound)!;
        var existingRelations = await _projectOperationActionRepository
            .GetPOActionByPOId(request.ProjectOperationId, ct);
        if (existingRelations.Any())
        {
            var actionIdsToDelete = existingRelations
                .Select(x => x.Id)
                .ToList();

            var deleteResult = await DeleteProjectOperationActionsHandler(
                actionIdsToDelete,
                ct);

            if (deleteResult.IsBad())
                return deleteResult.Failure<CreateProjectOperationActionsResponse>();
        }
        if (actions.Any())
        {
            var createResult = await CreateProjectOperationActionsCommand(
                actions,
                projectOperation,
                ct);

            if (createResult.IsBad())
                return createResult.Failure<CreateProjectOperationActionsResponse>();
        }
        projectOperation.UpdatePrice(null, request.IncreaseRate);
        return new CreateProjectOperationActionsResponse(true);
    }

    private (Dictionary<int, string> RowErrors, List<(int Index, ProjectOperationExcelImportsModel Row, DateTime? Start, DateTime? Finish, long OInfoId, long UnitId)> ValidRows) ValidateExcelImportRows(
        List<ProjectOperationExcelImportsModel> rows,
        HashSet<string> invalidCodes,
        HashSet<string> duplicatesInFile,
        HashSet<string> existingCodesInProject,
        Dictionary<string, (long Id, long UnitOfMeasurementId)> operationInfoMap)
    {
        var rowErrors = new Dictionary<int, string>();
        var validRows = new List<(int Index, ProjectOperationExcelImportsModel Row, DateTime? Start, DateTime? Finish, long OInfoId, long UnitId)>();
        var pc = new PersianCalendar();

        for (int i = 0; i < rows.Count; i++)
        {
            var row = rows[i];
            var errors = new List<string>();

            if (invalidCodes.Contains(row.OperationInfoCode))
                errors.Add("کد شرح عملیات نامعتبر است یا در سیستم یافت نشد");
            else if (duplicatesInFile.Contains(row.OperationInfoCode))
                errors.Add("کد شرح عملیات در این فایل اکسل تکراری است");
            else if (existingCodesInProject.Contains(row.OperationInfoCode))
                errors.Add("این شرح عملیات قبلاً در این پروژه ثبت شده است");

            if (row.Workload < 1)
                errors.Add("حجم کار باید بزرگتر یا مساوی 1 باشد");

            if (row.TolerancePercentage < 1 || row.TolerancePercentage > 100)
                errors.Add("درصد تلورانس باید بین 1 تا 100 باشد");

            DateTime? startDate = null;
            if (!string.IsNullOrWhiteSpace(row.StartDate))
            {
                if (!TryParseDate(row.StartDate, pc, out var parsedStart))
                    errors.Add("فرمت تاریخ شروع نامعتبر است");
                else
                    startDate = parsedStart;
            }

            DateTime? finishDate = null;
            if (!string.IsNullOrWhiteSpace(row.FinishDate))
            {
                if (!TryParseDate(row.FinishDate, pc, out var parsedFinish))
                    errors.Add("فرمت تاریخ پایان نامعتبر است");
                else
                    finishDate = parsedFinish;
            }

            if (startDate.HasValue && finishDate.HasValue && finishDate < startDate)
                errors.Add("تاریخ پایان نمی‌تواند قبل از تاریخ شروع باشد");

            if (errors.Any())
                rowErrors[i] = string.Join(" | ", errors);
            else
            {
                var mappedInfo = operationInfoMap[row.OperationInfoCode];
                validRows.Add((i, row, startDate, finishDate, mappedInfo.Id, mappedInfo.UnitOfMeasurementId));
            }
        }

        return (rowErrors, validRows);
    }

    private bool TryParseDate(string? dateStr, PersianCalendar pc, out DateTime result)
    {
        result = DateTime.MinValue;
        if (string.IsNullOrWhiteSpace(dateStr))
            return false;

        var parts = dateStr.Split('/', '-');
        if (parts.Length == 3 &&
            int.TryParse(parts[0], out int year) &&
            int.TryParse(parts[1], out int month) &&
            int.TryParse(parts[2], out int day))
        {
            if (year >= 1300 && year <= 1499)
            {
                try { result = pc.ToDateTime(year, month, day, 0, 0, 0, 0); return true; }
                catch { return false; }
            }

            try { result = new DateTime(year, month, day); return true; }
            catch { return false; }
        }

        return DateTime.TryParse(dateStr, out result);
    }
}