using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Services;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsConsumableVolumes;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Application.WebServices.MessageSender.MessageSenders.Commands.CreateMessage;
using Engineering.Application.WebServices.MessageSender.MessageSenders.Models;
using Engineering.Application.WebServices.MessageSender.MessageSenders.Models.CreateMessage;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetsCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.Enums;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Gita.Backend.Shared.Domain.Base;
using MathNet.Numerics;
using Company = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;
using TimeCalculator = Engineering.Application.Extensions.TimeCalculator.TimeCalculator;

namespace Engineering.Application.Services.ProjectOperationDetails;

public partial class ProjectOperationDetailLogic : IProjectOperationDetailLogic
{
    private async Task<Result<List<UserModel?>>> GetUserDataAsync(
        List<long>? requests,
        Error errorMessage,
        CT ct)
    {
        if (requests is null || requests.Count == 0)
            return Result.Success(new List<UserModel?>());

        var ids = requests.Where(x => x != default).ToList();
        if (!ids.Any())
            return Result.Success(new List<UserModel?>());

        var result = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(
            1,
            ids.Count,
            ids,
            null,
            false,
            null), ct);

        return result.IsFailure!
            ? Result.Failure<List<UserModel?>>(errorMessage)!
            : Result.Success(result.Value?.Data ?? new List<UserModel?>());
    }
    private async Task<Result<CreateMessageResponse?>> SendNotification(ProjectOperation projectOperation, OperationLocation? location, string? description, CT ct)
    {
        CreateMessageResponse? result = null;
        var currentUser = _userProfileService.GetProfileInfo();
        var queryCreators = await WebServicesLogic.UserDataReceiver([currentUser.UserId], null, _mediator, ct);

        long? userId = null;

        if(projectOperation.Project.ProjectManager is not null)
        {
            var managerInfos = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 10, [projectOperation.Project.ProjectManager.Value], null, false, null), ct); // بره سراغ متا دیتا
            userId = managerInfos.Value?.Data?.FirstOrDefault()?.UserId;
        }

        if (userId is not null)
        {
            var newContent =
                $"کاربر: {queryCreators?.FirstOrDefault()?.FullName}, " +
                $"حجم برآورد: {description}, " +
                $"از پروژه: {projectOperation.Project.ProjectName}, " +
                $"از شرح عملیات: {projectOperation.OperationInfo.OperationInfoName}, " +
                $"در موقعیت: {location?.PrivateName}, " +
                $"از حجم شرح عملیات بیشتر شده است.";
            var sendMessage = await _mediator.Send(new CreateMessageCommand(userId!.Value.ToString(), newContent, MessagePriority.High, MessageType.Notification), ct);

            result = sendMessage.Value;
        }
        return result;
    }

    //دریافت احجام مصرفی استاندارد 
    private async Task<GetsConsumableVolumesResponseModel> StandardConsumableVolumesCollector(OperationInfo operationInfo, decimal finalAmount, CT ct)
    {
        var expertsData = new List<ExpertDataModel>();
        var experts = operationInfo.ConsumptionStandardExperts.ToList();
        if (experts.Count > 0)
        {
            var expertIds = experts.Select(e => e.ExpertUnitId).ToList();
            var expertData = await WebServicesLogic.SkillsDataReceiver(expertIds, _mediator, ct);
            foreach (var item in experts)
            {
                var userInfo = expertData?.Where(x => x?.Id == item.ExpertUnitId).FirstOrDefault();
                expertsData.Add(new ExpertDataModel
                {
                    Id = item.Id,
                    ExpertId = item.ExpertUnitId,
                    ExpertName = userInfo?.Name,
                    ExpertCode = userInfo?.Code,
                    Number = item.ExpertNumber,
                    UnusedPercentage = item.UnusedPercentage,
                    IsStandard = true,
                    StandardValue = TimeCalculator.TicksToStringHM(item.TimeSpant),
                    FinalValue = TimeCalculator.TicksToStringHM((long)(item.TimeSpant * finalAmount))
                });
            }
        }


        var productUsersData = new List<ProductDataModel>();
        var products = operationInfo.ConsumptionStandardProduct.Where(x => x.ProductAllowedType == ProductAllowedType.IsStandard).ToList();
        if (products != null && products.Count > 0)
        {
            if (products.Any(x => x.StandardProductType == StandardProductType.ProductGroup))
            {
                var productGroups = products.Where(x => x.StandardProductType == StandardProductType.ProductGroup);
                var productIds = productGroups.Select(e => e.ProductUnitId).ToList();
                var productsData = await WebServicesLogic.GroupsDataReceiver(productIds, _mediator, ct);
                foreach (var item in productGroups)
                {
                    var product = productsData?.Where(x => x.Id == item.ProductUnitId).FirstOrDefault();
                    productUsersData.Add(new ProductDataModel
                    {
                        Id = item.Id,
                        ProductGroupId = item.ProductUnitId,
                        ProductGroupName = product?.Name,
                        ProductGroupCode = product?.Code,
                        UnusedPercentage = item.UnusedPercentage,
                        IsStandard = true,
                        StandardValue = item.Number,
                        FinalValueRes = item.Number * finalAmount,
                        MeasureUnitId = product?.MeasureUnitId,
                        MeasureUnitName = product?.MeasureUnitName,
                        VolumeProductType = VolumeProductType.ProductGroup,
                    });
                }
            }

            if (products.Any(x => x.StandardProductType == StandardProductType.Category))
            {
                var categories = products.Where(x => x.StandardProductType == StandardProductType.Category);
                var categoryIds = categories.Select(e => e.ProductUnitId).ToList();
                var categoriesData = await WebServicesLogic.CategoriesDataReceiver(categoryIds, null, _mediator, ct);
                foreach (var item in categories)
                {
                    var category = categoriesData?.Where(x => x.Id == item.ProductUnitId).FirstOrDefault();
                    productUsersData.Add(new ProductDataModel
                    {
                        Id = item.Id,
                        ProductGroupId = item.ProductUnitId,
                        ProductGroupName = category?.Title,
                        ProductGroupCode = category?.Code,
                        UnusedPercentage = item.UnusedPercentage,
                        IsStandard = true,
                        StandardValue = item.Number,
                        FinalValueRes = item.Number * finalAmount,
                        MeasureUnitId = null,
                        MeasureUnitName = null,
                        VolumeProductType = VolumeProductType.Category,
                    });
                }
            }
        }

        var machineryUsersData = new List<MachineryDataModel>();
        var machineries = operationInfo.ConsumptionStandardMachineries.ToList();
        if (machineries.Count > 0)
            foreach (var item in machineries.Where(x => x.Machinery is not null))
                machineryUsersData.Add(new MachineryDataModel
                {
                    Id = item.Id,
                    MachineryId = item.Machinery.Id,
                    MachineryName = item.Machinery.MachineryName,
                    MachineryCode = item.Machinery.MachineryCode,
                    Number = item.MachineryNumber,
                    UnusedPercentage = item.UnusedPercentage,
                    IsStandard = true,
                    StandardValue = TimeCalculator.TicksToStringHM(item.TimeSpant),
                    FinalValueDecimal = item.TimeSpant * finalAmount,
                    FinalValue = ""
                });

        return new GetsConsumableVolumesResponseModel(expertsData, machineryUsersData, productUsersData);
    }

    //بررسی احجام مصرفی کلی 
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private async Task<GetsConsumableVolumesResponseModel> ConsumableVolumesCollector(GetsConsumableVolumesResponseModel standards, ProjectOperationDetail value, decimal finalAmount, decimal oldFinalAmount, CT ct)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var expertsData = new List<ExpertDataModel>();
        var standardExperts = standards.ExpertData!.ToList();
        var volumeExperts = value.ConsumableVolumeExperts.ToList();
        if (volumeExperts.Count > 0)
        {
            if (standardExperts.Count == 0)
            {
                foreach (var item in volumeExperts)
                    expertsData.Add(new ExpertDataModel
                    {
                        Id = item.Id,
                        ExpertId = item.ExpertId,
                        Number = item.Number,
                        UnusedPercentage = item.UnusedPercentage,
                        IsStandard = item.IsStandard,
                        StandardValue = TimeCalculator.TicksToStringHM(item.StandardValue),
                        FinalValue = item.IsStandard ? TimeCalculator.TicksToStringHM(item.StandardValue * (long)finalAmount) : TimeCalculator.TicksToStringHM(item.FinalValue)
                    });
            }
            else
            {
                List<long> ids = [];
                var standardIds = standardExperts.Select(x => x.ExpertId).ToList();
                var volumeIds = volumeExperts.Select(x => x.ExpertId).ToList();
                ids.AddRange(standardIds);
                ids.AddRange(volumeIds);
                ids = ids.Distinct().ToList();
                foreach (var id in ids)
                {
                    var standard = standardExperts.FirstOrDefault(x => x.ExpertId.Equals(id));
                    if (standard is not null)
                    {
                        var productConsumable = volumeExperts.FirstOrDefault(x => x.ExpertId.Equals(id));
                        if (productConsumable is not null)
                        {
                            standard.Id = productConsumable.Id;
                            expertsData.Add(standard);
                        }
                        else
                        {
                            standard.Id = null;
                            expertsData.Add(standard);
                        }
                    }
                    else
                    {
                        var expertConsumable = volumeExperts.FirstOrDefault(x => x.ExpertId.Equals(id));
                        if (expertConsumable is not null)
                            expertsData.Add(new ExpertDataModel
                            {
                                Id = expertConsumable.Id,
                                ExpertId = expertConsumable.ExpertId,
                                Number = expertConsumable.Number,
                                UnusedPercentage = expertConsumable.UnusedPercentage,
                                IsStandard = false,
                                StandardValue = TimeCalculator.TicksToStringHM(0),
                                FinalValue = TimeCalculator.TicksToStringHM((long)((expertConsumable.FinalValue / oldFinalAmount) * finalAmount))
                            });
                    }
                }
            }
        }
        else
            foreach (var expert in standardExperts)
            {
                expert.Id = null;
                expertsData.Add(expert);
            }

        var machineriesData = new List<MachineryDataModel>();
        var standardMachineries = standards.MachineryData!.ToList();
        var volumeMachineries = value.ConsumableVolumeMachineries.ToList();
        if (volumeMachineries.Count > 0)
        {
            if (standardMachineries.Count == 0)
            {
                foreach (var item in volumeMachineries)
                    machineriesData.Add(new MachineryDataModel
                    {
                        Id = item.Id,
                        MachineryId = item.Machinery.Id,
                        MachineryName = item.Machinery.MachineryName,
                        MachineryCode = item.Machinery.MachineryCode,
                        Number = item.Number,
                        UnusedPercentage = item.UnusedPercentage,
                        IsStandard = item.IsStandard,
                        StandardValue = TimeCalculator.TicksToStringHM(item.StandardValue),
                        FinalValueDecimal = item.IsStandard && item.StandardValue is not null ? item.StandardValue.Value * finalAmount : item.FinalValue
                    });
            }
            else
            {
                List<long> ids = [];
                var standardIds = standardMachineries.Select(x => x.MachineryId).ToList();
                var volumeIds = volumeMachineries.Select(x => x.Machinery.Id).ToList();
                ids.AddRange(standardIds);
                ids.AddRange(volumeIds);
                ids = ids.Distinct().ToList();
                foreach (var id in ids)
                {
                    var standard = standardMachineries.FirstOrDefault(x => x.MachineryId.Equals(id));
                    if (standard is not null)
                    {
                        var machineryConsumable = volumeMachineries.FirstOrDefault(x => x.Machinery.Id.Equals(id));
                        if (machineryConsumable is not null)
                        {
                            standard.Id = machineryConsumable.Id;
                            machineriesData.Add(standard);
                        }
                        else
                        {
                            standard.Id = null;
                            machineriesData.Add(standard);
                        }
                    }
                    else
                    {
                        var machineryConsumable = volumeMachineries.FirstOrDefault(x => x.Machinery.Id.Equals(id));
                        if (machineryConsumable is not null)
                            machineriesData.Add(new MachineryDataModel
                            {
                                Id = machineryConsumable.Id,
                                MachineryId = machineryConsumable.Machinery.Id,
                                Number = machineryConsumable.Number,
                                UnusedPercentage = machineryConsumable.UnusedPercentage,
                                IsStandard = false,
                                StandardValue = TimeCalculator.TicksToStringHM(0),
                                FinalValueDecimal = ((machineryConsumable.FinalValue / oldFinalAmount) * finalAmount)
                            });
                    }
                }
            }
        }
        else
            foreach (var machinery in standardMachineries)
            {
                machinery.Id = null;
                machineriesData.Add(machinery);
            }

        var productUsersData = new List<ProductDataModel>();
        var standardProducts = standards.ProductData!.ToList();
        var volumeProducts = value.ConsumableVolumeProducts.ToList();
        if (volumeProducts is not null && volumeProducts.Count > 0)
        {
            if (standardProducts.Count == 0)
            {
                foreach (var item in volumeProducts)
                {
                    productUsersData.Add(new ProductDataModel
                    {
                        Id = item.Id,
                        ProductGroupId = item.ProductGroupId,
                        UnusedPercentage = item.UnusedPercentage,
                        IsStandard = item.IsStandard,
                        StandardValue = item.StandardValue,
                        FinalValueRes = item.IsStandard ? (item.StandardValue!.Value * finalAmount) : item.FinalValue,
                        VolumeProductType = item.VolumeProductType,
                    });
                }
            }
            else
            {
                List<long> ids = [];
                var standardIds = standardProducts!.Select(x => x.ProductGroupId).ToList();
                var volumeIds = volumeProducts.Select(x => x.ProductGroupId).ToList();
                ids.AddRange(standardIds);
                ids.AddRange(volumeIds);
                ids = ids.Distinct().ToList();
                foreach (var id in ids)
                {
                    var standard = standardProducts!.FirstOrDefault(x => x.ProductGroupId.Equals(id));
                    if (standard is not null)
                    {
                        var productConsumable = volumeProducts.FirstOrDefault(x => x.ProductGroupId.Equals(id));
                        if (productConsumable is not null)
                        {
                            standard.Id = productConsumable.Id;
                            productUsersData.Add(standard);
                        }
                        else
                        {
                            standard.Id = null;
                            productUsersData.Add(standard);
                        }
                    }
                    else
                    {
                        var productConsumable = volumeProducts.FirstOrDefault(x => x.ProductGroupId.Equals(id));
                        var x = Math.Round(oldFinalAmount, 5);
                        if (productConsumable is not null)
                            productUsersData.Add(new ProductDataModel
                            {
                                Id = productConsumable.Id,
                                ProductGroupId = productConsumable.ProductGroupId,
                                UnusedPercentage = productConsumable.UnusedPercentage,
                                IsStandard = false,
                                StandardValue = 0,
                                FinalValueRes = ((productConsumable.FinalValue / x) * finalAmount),
                                VolumeProductType = productConsumable.VolumeProductType,
                            });
                    }
                }
            }
        }
        else
            foreach (var product in standardProducts)
            {
                product.Id = null;
                productUsersData.Add(product);
            }

        return new GetsConsumableVolumesResponseModel(expertsData, machineriesData, productUsersData);
    }

    /// <summary>
    /// ایجاد اطلاعات داخل سرویسی متخصص
    /// </summary>
    /// <param name="requestModels">دیتای درخواستی</param>
    /// <param name="projectOperation">شرح عملیات پروژه</param>
    /// <param name="expertsData">اطلاعات متخصصین</param>
    /// <param name="finalAmount">مقدار نهایی</param>
    /// <returns></returns>
    private Tuple<bool, List<ExpertServiceModel>> ExpertModeling(List<CreateExpertRequestModel> requestModels, ProjectOperation projectOperation, List<Skill?>? expertsData)
    {
        var expertModel = new List<ExpertServiceModel>();
        foreach (var item in requestModels)
        {
            var standardData = projectOperation!.OperationInfo.ConsumptionStandardExperts.Where(x => x.ExpertUnitId == item?.ExpertId).FirstOrDefault();
            var expertData = expertsData?.Where(c => c?.Id == item?.ExpertId).FirstOrDefault();
            //if (standardData is not null && standardData.TimeSpant != TimeCalculator.StringToTicks(item.StandardValue))
            //    return System.Tuple.Create(false, expertModel);

            expertModel.Add(new ExpertServiceModel
            {
                TempId = item.TempId,
                ExpertId = item.ExpertId,
                ExpertName = expertData?.Name,
                ExpertCode = expertData?.Code,
                Number = item.Number,
                UnusedPercentage = item.UnusedPercentage,
                IsStandard = standardData is not null ? true : false,
                StandardValue = standardData is not null ? standardData.TimeSpant : 0,
                FinalValue = TimeCalculator.StringToTicks(item!.FinalValue)
            });
        }
        return System.Tuple.Create(true, expertModel);
    }

    /// <summary>
    /// ایجاد اطلاعات داخل سرویسی ماشین آلات و ابزار
    /// </summary>
    /// <param name="requestModels">دیتای درخواستی</param>
    /// <param name="projectOperation">شرح عملیات پروژه</param>
    /// <param name="machinerysData">اطلاعات ماشین آلات و ابزار</param>
    /// <param name="finalAmount">مقدار نهایی</param>
    private Tuple<bool, List<MachineryServiceModel>> MachineryModeling(List<CreateMachineryRequestModel> requestModels, ProjectOperation projectOperation, List<Machinery?>? machinerysData)
    {
        var machineryModel = new List<MachineryServiceModel>();
        foreach (var item in requestModels)
        {
            var standardData = projectOperation!.OperationInfo.ConsumptionStandardMachineries.Where(x => x.Machinery.Id == item?.MachineryId).FirstOrDefault();
            //if (standardData is not null && standardData.TimeSpant != TimeCalculator.StringToTicks(item.StandardValue))
            //    return System.Tuple.Create(false, machineryModel);

            machineryModel.Add(new MachineryServiceModel
            {
                Machinery = machinerysData?.Where(c => c?.Id == item?.MachineryId).FirstOrDefault()!,
                Number = item.Number,
                UnusedPercentage = item.UnusedPercentage,
                IsStandard = standardData is not null ? true : false,
                StandardValue = standardData is not null ? standardData!.TimeSpant : 0,
                FinalValue = (item.FinalValue.Contains(":") && item.Unit != null && item.Unit == RequestMachineryUnit.Hourly) ||
                (Convert.ToDecimal(item.FinalValue) > 1000000) ?
                Convert.ToDecimal(TimeCalculator.StringToTicks(item!.FinalValue)) : Convert.ToDecimal(item.FinalValue),
                Unit = item.Unit,
            });
        }
        return System.Tuple.Create(true, machineryModel);
    }

    /// <summary>
    /// ایجاد اطلاعات داخل سرویسی ماشین کالا
    /// </summary>
    /// <param name="requestModels">دیتای درخواستی</param>
    /// <param name="projectOperation">شرح عملیات پروژه</param>
    /// <param name="productsData">اطلاعات کالا</param>
    /// <param name="finalAmount">مقدار نهایی</param>
    private Tuple<bool, List<ProductServiceModel>> ProductModeling(List<CreateProductRequestModel> requestModels, ProjectOperation projectOperation, List<Group>? productsData, List<WarehouseCategory>? categoriesData)
    {
        var productModel = new List<ProductServiceModel>();

        var productGroups = requestModels.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup).ToList();
        if (productGroups is not null && productGroups.Count > 0)
        {
            foreach (var item in productGroups)
            {
                var standardData = projectOperation!.OperationInfo.ConsumptionStandardProduct
                    .Where(x => x.StandardProductType == StandardProductType.ProductGroup &&
                                x.ProductUnitId == item?.ProductGroupId)
                    .FirstOrDefault();
                var productData = productsData?.Where(c => c?.Id == item?.ProductGroupId).FirstOrDefault();
                //if (standardData is not null && standardData.Number != item.StandardValue)
                //    return System.Tuple.Create(false, productModel);

                productModel.Add(new ProductServiceModel
                {
                    ProductGroupId = item.ProductGroupId,
                    ProductGroupName = productData?.Name,
                    ProductGroupCode = productData?.Code,
                    MeasureUnitName = "",
                    UnusedPercentage = item.UnusedPercentage,
                    IsStandard = standardData is not null ? true : false,
                    StandardValue = standardData is not null ? standardData!.Number : 0,
                    FinalValue = item!.FinalValue,
                    VolumeProductType = item.VolumeProductType!.Value
                });
            }
        }

        var categories = requestModels.Where(x => x.VolumeProductType == VolumeProductType.Category).ToList();
        if (categories is not null && categories.Count > 0)
        {
            foreach (var item in categories)
            {
                var standardData = projectOperation!.OperationInfo.ConsumptionStandardProduct
                    .Where(x => x.StandardProductType == StandardProductType.Category &&
                                x.ProductUnitId == item?.ProductGroupId)
                    .FirstOrDefault();
                var categoryData = categoriesData?.Where(c => c?.Id == item?.ProductGroupId).FirstOrDefault();
                //if (standardData is not null && standardData.Number != item.StandardValue)
                //    return System.Tuple.Create(false, productModel);

                productModel.Add(new ProductServiceModel
                {
                    ProductGroupId = item.ProductGroupId,
                    ProductGroupName = categoryData?.Title,
                    ProductGroupCode = categoryData?.Code,
                    MeasureUnitName = "",
                    UnusedPercentage = item.UnusedPercentage,
                    IsStandard = standardData is not null ? true : false,
                    StandardValue = standardData is not null ? standardData!.Number : 0,
                    FinalValue = item!.FinalValue,
                    VolumeProductType = item.VolumeProductType!.Value
                });
            }
        }

        return System.Tuple.Create(true, productModel);
    }

    //دریافت تمامی شناسه های کاربران برای متادیتا یک ریزمتره 
    private List<long> IdCollector(ProjectOperationDetail value)
    {
        var allIds = new List<long>();

        if (value.UserImplementations.Any())
            allIds.AddRange(value.UserImplementations!.Select(x => x.ImplementationAssistantUserId).ToList());

        if (value.UserTechnicals.Any())
            allIds.AddRange(value.UserTechnicals!.Select(x => x.TechnicalAssistantUserId).ToList());

        if (value.UserPlaners.Any())
            allIds.AddRange(value.UserPlaners!.Select(x => x.UserPlanerId).ToList());

        if (value.ProjectOperationDetailContractorServices.Any())
            foreach (var item in value.ProjectOperationDetailContractorServices)
                if (item.ContractorId is not null)
                    allIds.Add((long)item.ContractorId);

        allIds.Add((long)value.ProjectOperation.Project.ProjectManager);

        return allIds.Where(x => x != default).ToList();
    }

    //دریافت تمامی شناسه های کاربران برای متادیتا لیستی از ریزمتره ها
    private List<long> IdCollectors(List<ProjectOperationDetail?> items)
    {
        var allIds = new List<long>();
        foreach (var item in items)
        {
            if (item!.UserPlaners.Any())
                allIds.AddRange(item!.UserPlaners.Select(x => x.UserPlanerId).ToList());

            if (item!.UserImplementations.Any())
                allIds.AddRange(item!.UserImplementations.Select(x => x.ImplementationAssistantUserId).ToList());

            if (item!.UserTechnicals.Any())
                allIds.AddRange(item!.UserTechnicals.Select(x => x.TechnicalAssistantUserId).ToList());

            if (item!.ConsumableVolumeExperts.Any())
                allIds.AddRange(item!.ConsumableVolumeExperts.Select(x => x.ExpertId).ToList());

            if (item!.ProjectOperationDetailContractorServices.Any())
            {
                var ids = item.ProjectOperationDetailContractorServices.Where(x => x.ContractorId is not null && x.ContractorId > 0).Select(x => x.ContractorId!.Value).ToList();
                if (ids.Count > 0)
                    allIds.AddRange(ids);
            }

            allIds.Add((long)item!.ProjectOperation.Project.ProjectManager);
        }

        return allIds.Where(x => x != default).ToList();
    }

    //دریافت تمامی شناسه های کاربران برای متادیتا لیستی از ریزمتره ها
    private List<long> IdCollectors(List<ProjectOperationDetailsModel>? items)
    {
        var allIds = new List<long>();
        if (items != null && items.Count > 0)
            foreach (var item in items)
            {
                if (item.PlannerUserIds is not null && item.PlannerUserIds.Count > 0)
                    allIds.AddRange(item!.PlannerUserIds.Where(x => x > 0).Select(x => x).Distinct().ToList());

                if (item.ImplementationAssistantUserIds is not null && item.ImplementationAssistantUserIds.Count > 0)
                    allIds.AddRange(item.ImplementationAssistantUserIds.Where(x => x > 0).Select(x => x).Distinct().ToList());

                if (item.TechnicalAssistantUserIds is not null && item.TechnicalAssistantUserIds.Count > 0)
                    allIds.AddRange(item.TechnicalAssistantUserIds.Where(x => x > 0).Select(x => x).Distinct().ToList());

                if (item.ContractorIds is not null && item.ContractorIds.Count > 0)
                    allIds.AddRange(item.ContractorIds.Where(x => x != null && x > 0).Select(x => (long)x!).Distinct().ToList());

                if (item.ProjectManagerId != null && item.ProjectManagerId > 0)
                    allIds.Add(item.ProjectManagerId.Value);
            }

        return allIds.Where(x => x != default).Distinct().ToList();
    }

    //سازنده لیستی از دیتای خروجی ریزمتره ها به همراه اطلاعات کابران
    private async Task<ProjectOperationDetailModel> DataCollector(
        ProjectOperationDetail detail,
        List<GetUserInfo>? usersInfos,
        List<Measureunit?>? measureunits,
        List<Group?>? products,
        List<WarehouseCategory?>? categories,
        List<Skill?>? experts,
        IdentityServices.Users.Models.User? creatorInfo,
        CT ct)
    {
        var userImplementationsData = new List<ImplementationAssistantDataModel?>();
        foreach (var item in detail.UserImplementations)
            userImplementationsData.Add(new ImplementationAssistantDataModel(item.Id, item.ImplementationAssistantUserId,
                usersInfos?.Where(x => x?.Id == item.ImplementationAssistantUserId).Select(x => x?.FullName).FirstOrDefault(),
                usersInfos?.Where(x => x?.Id == item.ImplementationAssistantUserId).Select(x => x?.Nickname).FirstOrDefault()));

        var userTechnicalsData = new List<TechnicalAssistantDataModel?>();
        foreach (var item in detail.UserTechnicals)
            userTechnicalsData.Add(new TechnicalAssistantDataModel(item.Id, item.TechnicalAssistantUserId,
                usersInfos?.Where(x => x?.Id == item.TechnicalAssistantUserId).Select(x => x?.FullName).FirstOrDefault(),
                usersInfos?.Where(x => x?.Id == item.TechnicalAssistantUserId).Select(x => x?.Nickname).FirstOrDefault()));

        var plannerUsersData = new List<PlannerDataModel?>();
        foreach (var item in detail.UserPlaners)
            plannerUsersData.Add(new PlannerDataModel(item.Id, item.UserPlanerId,
                usersInfos?.Where(x => x?.Id == item.UserPlanerId).Select(x => x?.FullName).FirstOrDefault(),
                usersInfos?.Where(x => x?.Id == item.UserPlanerId).Select(x => x?.Nickname).FirstOrDefault()));

        var expertUsersData = new List<ExpertDataModel?>();
        foreach (var item in detail.ConsumableVolumeExperts)
        {
            var standardTitle = item.IsStandard == true ? "استاندارد" : "غیراستاندارد";
            var expertData = usersInfos?.Where(c => c?.Id == item?.ExpertId).FirstOrDefault();
            var standardValue = TimeCalculator.TicksToStringHM((long)item.StandardValue!);
            var finalValue = TimeCalculator.TicksToStringHM(item.FinalValue);

            var adapt = item.Adapt<ExpertDataModel>();
            adapt.HasContractorExpert = item.ProjectOperationDetailContractorExperts.Any();
            expertUsersData.Add(adapt);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            expertUsersData.Where(x => x.ExpertId == item.ExpertId).FirstOrDefault().ExpertName = experts?.Where(x => x?.Id == item.ExpertId).FirstOrDefault()?.Name;
            expertUsersData.Where(x => x.ExpertId == item.ExpertId).FirstOrDefault().ExpertCode = experts?.Where(x => x?.Id == item.ExpertId).FirstOrDefault()?.Code;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        var machineriesData = new List<MachineryDataModel?>();
        foreach (var item in detail.ConsumableVolumeMachineries)
        {
            var standardTitle = item.IsStandard == true ? "استاندارد" : "غیراستاندارد";
            var standardValue = TimeCalculator.TicksToStringHM((long)item.StandardValue!);
            var finalValue = item.FinalValue;
            machineriesData.Add(item.Adapt<MachineryDataModel>());
        }

        var productsData = new List<ProductDataModel?>();
        if (detail.ConsumableVolumeProducts.Any(x => x.VolumeProductType == VolumeProductType.ProductGroup))
        {
            foreach (var item in detail.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.ProductGroup))
            {
                var productData = products?.Where(c => c?.Id == item?.ProductGroupId).FirstOrDefault();
                productsData.Add(new ProductDataModel
                {
                    Id = item!.Id,
                    ProductGroupId = item.ProductGroupId,
                    ProductGroupName = productData?.Name,
                    ProductGroupCode = productData?.Code,
                    UnusedPercentage = item.UnusedPercentage,
                    IsStandard = item.IsStandard,
                    IsActive = productData?.IsActive,
                    RequestedValue = SumRequestedProduct(item),
                    StandardValue = item.StandardValue!,
                    FinalValueRes = item.FinalValue!,
                    MeasureUnitId = productData?.MeasureUnitId,
                    MeasureUnitName = productData?.MeasureUnitName,
                    VolumeProductType = item.VolumeProductType
                });
            }
        }

        if (detail.ConsumableVolumeProducts.Any(x => x.VolumeProductType == VolumeProductType.Category))
        {
            foreach (var item in detail.ConsumableVolumeProducts.Where(x => x.VolumeProductType == VolumeProductType.Category))
            {
                var categoryData = categories?.Where(c => c?.Id == item?.ProductGroupId).FirstOrDefault();
                productsData.Add(new ProductDataModel
                {
                    Id = item!.Id,
                    ProductGroupId = item.ProductGroupId,
                    ProductGroupName = categoryData?.Title,
                    ProductGroupCode = categoryData?.Code,
                    UnusedPercentage = item.UnusedPercentage,
                    IsStandard = item.IsStandard,
                    RequestedValue = SumRequestedProduct(item),
                    IsActive = categoryData?.IsActive,
                    StandardValue = item.StandardValue!,
                    FinalValueRes = item.FinalValue!,
                    MeasureUnitId = null,
                    MeasureUnitName = null,
                    VolumeProductType = item.VolumeProductType
                });
            }
        }

        var contractorUsersData = new List<ContractorServiceDataModel?>();
        foreach (var item in detail.ProjectOperationDetailContractorServices)
        {
            var service = item.OperationInfoService.ServiceInfo;
            var projectService = item.ProjectServiceDetail?.ProjectService;
            var projectServiceInfo = projectService?.ServiceInfo;
            var user = usersInfos?.Where(x => x?.Id == item.ContractorId).FirstOrDefault();
            contractorUsersData.Add(new ContractorServiceDataModel(
                item.Id,
                item.ProjectServiceDetail?.Id,
                item.ContractorId,
                user?.FullName,
                user?.OrganizationCode,
                service.Id,
                service.ServiceInfoName,
                service.ServiceInfoCode,
                service.UnitOfMeasurementId,
                measureunits?.Where(x => x?.Id == service.UnitOfMeasurementId).Select(x => x?.Name).FirstOrDefault(),
                item.Volume,
                item.DailyOperationServices.Sum(x => x.Volume),
                item.Volume - item.DailyOperationServices.Sum(x => x.Volume),
                TimeCalculator.TicksToStringHM(item.TimeSpant),
                projectService?.Id,
                projectServiceInfo?.Id,
                projectServiceInfo?.ServiceInfoName,
                projectServiceInfo?.ServiceInfoCode,
                projectServiceInfo?.UnitOfMeasurementId,
                measureunits?.Where(x => x?.Id == projectServiceInfo?.UnitOfMeasurementId).Select(x => x?.Name).FirstOrDefault(),
                projectService?.Volume,
                projectService?.DoneVolume,
                projectService?.RemainderVolume,
                item.ProjectOperationDetailContractorExperts.Any(),
                item.IsActive));
        }

        var deductionsData = new List<DeductionDataModel?>();
        if (detail.ProjectOperationDetailDeductions is not null && detail.ProjectOperationDetailDeductions.Count > 0)
            foreach (var item in detail.ProjectOperationDetailDeductions)
            {
                var deduction = new DeductionDataModel()
                {
                    Id = item.Id,
                    Height = item.Height,
                    Length = item.Length,
                    Number = item.Number,
                    Weight = item.Weight,
                    Width = item.Width,
                };
                deductionsData.Add(deduction);
            }

        var finalAmount = detail.FinalAmount;
        var deductionFinalAmount = detail.ProjectOperationDetailDeductions?.Sum(x => x.FinalAmount);
        var totalFinalAmount = finalAmount - deductionFinalAmount;

        var productName = "";
        if (detail.CreatedProductId is not null)
        {
            var productData = await WebServicesLogic.ProductDataReceiver(detail.CreatedProductId, _mediator, _pRepo, ct);
            productName = productData?.Name;
        }

        var projectOP = detail.ProjectOperation;
        var project = detail.ProjectOperation.Project;
        var operationInfo = detail.ProjectOperation.OperationInfo;
        var userInfo = usersInfos?.Where(x => x?.Id == project.ProjectManager).Select(x => x?.FullName).FirstOrDefault();
        var location = detail.OperationLocation;
        var startDate = TimeCalculator.DatePiker(detail.StartDate);
        var endDate = TimeCalculator.DatePiker(detail.EndDate);
        var urls = detail.ProjectOperationDetailDocuments.Select(x => x.Url).ToList();
        return new ProjectOperationDetailModel(
            detail.Id,
            detail.Code,
            projectOP!.Id,
            project.Id,
            project.ProjectName,
            userInfo,
            operationInfo.Id,
            operationInfo.OperationInfoName,
            location.Id,
            location.PrivateName,
            location.PrivateCode,
            location.PublicName,
            location.PublicCode,
            startDate,
            endDate,
            detail.Length,
            detail.LengthChangeable,
            detail.Width,
            detail.WidthChangeable,
            detail.Height,
            detail.HeightChangeable,
            detail.Weight,
            detail.WeightChangeable,
            detail.Number,
            detail.NumberChangeable,
            finalAmount,
            deductionFinalAmount,
            totalFinalAmount,
            new((int)detail.Status!, detail.Status.GetEnumDescription()),
            detail.Priority,
            detail.Day,
            detail.Hour,
            detail.CreatedProductId,
            productName,
            detail.Description,
            urls,
            plannerUsersData,
            userImplementationsData,
            userTechnicalsData,
            contractorUsersData,
            expertUsersData,
            machineriesData,
            productsData,
            deductionsData,
            new(creatorInfo?.Id, creatorInfo?.FullName)
            );
    }

    // سازنده لیستی از دیتای خروجی ریزمتره ها به همراه اطلاعات کابران
    private List<ProjectOperationDetailsModel> DataCollectors(List<ProjectOperationDetailsModel> items, List<GetUserInfo>? usersInfos, List<FilteredUserResponseModel>? userInfos, List<Company>? companies)
    {
        items.ForEach(item =>
        {
            var company = companies?.Where(x => x.Id == item.CompanyId).FirstOrDefault();

            var userImplementationsData = new List<ImplementationAssistantDataModel?>();
            if (item.ImplementationAssistantUserIds != null && item.ImplementationAssistantUserIds.Count > 0)
            {
                foreach (var detail in item.ImplementationAssistantUserIds)
                    userImplementationsData.Add(new ImplementationAssistantDataModel(
                        null,
                        detail,
                        usersInfos?.Where(x => x?.Id == detail).Select(x => x?.FullName).FirstOrDefault(),
                        usersInfos?.Where(x => x?.Id == detail).Select(x => x?.Nickname).FirstOrDefault()));
            }

            var userTechnicalsData = new List<TechnicalAssistantDataModel?>();
            if (item.TechnicalAssistantUserIds != null && item.TechnicalAssistantUserIds.Count > 0)
            {
                foreach (var detail in item.TechnicalAssistantUserIds)
                    userTechnicalsData.Add(new TechnicalAssistantDataModel(
                        null,
                        detail,
                        usersInfos?.Where(x => x?.Id == detail).Select(x => x?.FullName).FirstOrDefault(),
                        usersInfos?.Where(x => x?.Id == detail).Select(x => x?.Nickname).FirstOrDefault()));
            }

            var plannerUsersData = new List<PlannerDataModel?>();
            if (item.PlannerUserIds != null && item.PlannerUserIds.Count > 0)
            {
                foreach (var detail in item.PlannerUserIds)
                    plannerUsersData.Add(new PlannerDataModel(
                        null,
                        detail,
                        usersInfos?.Where(x => x?.Id == detail).Select(x => x?.FullName).FirstOrDefault(),
                        usersInfos?.Where(x => x?.Id == detail).Select(x => x?.Nickname).FirstOrDefault()));
            }

            var userInfo = usersInfos?.Where(x => x?.Id == item.ProjectManagerId).Select(x => x?.FullName).FirstOrDefault();
            var creator = new CreatorModel(item.CreatorId, userInfos?.Where(x => x.UserId == item.CreatorId).FirstOrDefault()?.FullName);
            var updator = new UpdatorModel(item.UpdaterId, userInfos?.Where(x => x.UserId == item.UpdaterId).FirstOrDefault()?.FullName);

            var contractors = "";
            var contractorNickNames = "";
            if (item.ContractorIds is not null && item.ContractorIds.Count > 0)
            {
                var contractorIds = item.ContractorIds.Where(x => x is not null && x > 0).Select(x => (long)x!).Distinct().ToList();
                if (usersInfos is not null && usersInfos.Any())
                {
                    var contractorFullNames = usersInfos?.Where(x => contractorIds.Contains(x!.Id)).Select(x => x!.FullName).ToList();
                    if (contractorFullNames is not null && contractorFullNames.Any())
                        contractors = string.Join(" - ", contractorFullNames!);

                    var contractorNicks = usersInfos?.Where(x => contractorIds.Contains(x!.Id)).Select(x => x!.Nickname).ToList();
                    if (contractorNicks is not null && contractorNicks.Any())
                        contractorNickNames = string.Join(" - ", contractorNicks!);
                }
            }

            item.ProjectManagerName = userInfo;
            item.FinalAmount = item.DeductionAmounts is not null && item.DeductionAmounts.Count > 0 ? item.FinalAmount - item.DeductionAmounts.Sum(x => x) : item.FinalAmount;
            item.Planners = plannerUsersData;
            item.ImplementationAssistants = userImplementationsData;
            item.TechnicalAssistants = userTechnicalsData;
            item.Creator = creator;
            item.Updator = updator;
            item.UsedFinalAmount = item.DailyAmounts is not null && item.DailyAmounts.Count > 0 ? item.DailyAmounts.Sum(x => x) : 0;
            item.CompanyNameFa = company?.NameFa;
            item.Contractors = contractors;
            item.ContractorNicknames = contractorNickNames;
        });

        return items;
    }

    //بررسی حجم کار
    private bool Workloader(ProjectOperation projectOperation)
    {
        var workload = projectOperation.Workload;
        var finalAmounts = projectOperation.ProjectOperationDetails.Select(x => x.FinalAmount).Sum();
        if (finalAmounts > workload)
            return false;
        else
            return true;
    }

    private async Task<List<Currency>?> CurrencyDataReceiver(List<ContractorContractHeader>? headers, CT ct)
    {
        var dataResult = new List<Currency>();
        if (headers is not null && headers.Count > 0)
        {
            var allIds = headers.Where(x => x.CurrencyId != 0).Select(x => (long)x.CurrencyId!).ToList();
            var contractors = headers.SelectMany(x => x.ContractorContracts).ToList();

            foreach (var item in contractors)
            {
                var priceCurrencies = item.Details?.SelectMany(oo => oo.ContractorContractDetailPrices).Select(oo => oo.CurrencyId).ToList();
                if (priceCurrencies is not null && priceCurrencies.Count > 0)
                    allIds.AddRange(priceCurrencies);
            }

            if (allIds is not null && allIds.Count > 0)
            {
                var currenciesData = await _mediator.Send(new GetsCurrencyByIdQuery(1, allIds.Count, allIds, true), ct);
                dataResult = currenciesData.Value?.Data;
            }
        }
        return dataResult;
    }

    private bool ValidateProduct(ConsumableVolumeProduct product, decimal finalValue, decimal newUnusedPercentage)
    {
        if (product.RequestGoodsSupplyDetails is not null && product.RequestGoodsSupplyDetails.Count > 0)
        {
            var details = product.RequestGoodsSupplyDetails.Where(x =>
                    x.Status != GoodsSupplyDetailStatus.ProjectManagerReturned &&
                    x.Status != GoodsSupplyDetailStatus.ProjectManagerRejected &&
                    x.Status != GoodsSupplyDetailStatus.ManagementReturned &&
                    x.Status != GoodsSupplyDetailStatus.ManagementRejected &&
                    x.Status != GoodsSupplyDetailStatus.SupplyUnitReturned &&
                    x.Status != GoodsSupplyDetailStatus.SupplyUnitRejected &&
                    x.Status != GoodsSupplyDetailStatus.NotCompleteSupply &&
                    x.Status != GoodsSupplyDetailStatus.Closed).ToList();
            if (details.Any())
            {
                decimal unusedPercentageCount = 0;
                if (product.UnusedPercentage != newUnusedPercentage)
                    unusedPercentageCount = (product.FinalValue / 100) * newUnusedPercentage;
                else
                    unusedPercentageCount = (product.FinalValue / 100) * product.UnusedPercentage ?? 0;

                var sumRequest = details.Sum(x => x.RequestedCount);
                var finalCount = finalValue + unusedPercentageCount.Round(2);
                if (sumRequest > finalCount)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        else
            return false;
    }

    private decimal SumRequestedProduct(ConsumableVolumeProduct product)
    {
        if (product.RequestGoodsSupplyDetails is not null && product.RequestGoodsSupplyDetails.Count > 0)
        {
            var details = product.RequestGoodsSupplyDetails.Where(x =>
                    x.Status != GoodsSupplyDetailStatus.ProjectManagerReturned &&
                    x.Status != GoodsSupplyDetailStatus.ProjectManagerRejected &&
                    x.Status != GoodsSupplyDetailStatus.ManagementReturned &&
                    x.Status != GoodsSupplyDetailStatus.ManagementRejected &&
                    x.Status != GoodsSupplyDetailStatus.SupplyUnitReturned &&
                    x.Status != GoodsSupplyDetailStatus.SupplyUnitRejected &&
                    x.Status != GoodsSupplyDetailStatus.NotCompleteSupply &&
                    x.Status != GoodsSupplyDetailStatus.Closed).ToList();
            if (details.Any())
            {
                var sumRequest = details.Sum(x => x.RequestedCount);
                return sumRequest;
            }
            else
                return 0;
        }
        else
            return 0;
    }

}