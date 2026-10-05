using Engineering.Application.Services.BillOfLadings;
using Engineering.Application.Services.BillOfLadings.Contracts.BillOfLadingExcelImports;
using Engineering.Application.Services.Branchs;
using Engineering.Application.Services.Branchs.Models.BranchExcelImports;
using Engineering.Application.Services.CabinTypes;
using Engineering.Application.Services.CabinTypes.Models.CabinTypeExcelImports;
using Engineering.Application.Services.Categories;
using Engineering.Application.Services.Categories.Models.CategoryExcelImports;
using Engineering.Application.Services.CostCenterTypes;
using Engineering.Application.Services.CostCenterTypes.Models.CostCenterTypeExcelImports;
using Engineering.Application.Services.CostOvers;
using Engineering.Application.Services.CostOvers.Models.CostOverExcelImports;
using Engineering.Application.Services.Machineries;
using Engineering.Application.Services.Machineries.Models.MachineryExcelImports;
using Engineering.Application.Services.MachineriesGroups;
using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupExcelImports;
using Engineering.Application.Services.MachineTypes;
using Engineering.Application.Services.MachineTypes.Models.MachineTypeExcelImports;
using Engineering.Application.Services.OperationInfoGroups;
using Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupExcelImports;
using Engineering.Application.Services.OperationInfos;
using Engineering.Application.Services.OperationInfos.Models.FehrestBaha;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoExcelImports;
using Engineering.Application.Services.OperationInfos.Models.RasteReshteExcelImporter;
using Engineering.Application.Services.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.PODetailExcelImport;
using Engineering.Application.Services.ProjectTypes;
using Engineering.Application.Services.ProjectTypes.Models.ProjectTypeExcelImports;
using Engineering.Application.Services.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.PRGSupplyImport;
using Engineering.Application.Services.RequestGoodsSupplies.Models.RGSupplyImport;
using Engineering.Application.Services.Seasons;
using Engineering.Application.Services.Seasons.Models.SeasonExcelImports;
using Engineering.Application.Services.ServiceInfos;
using Engineering.Application.Services.ServiceInfos.Models.ServiceInfoExcelImports;
using Engineering.Application.Services.ShippingCosts;
using Engineering.Application.Services.ShippingCosts.Models.ShippingCostExcelImports;
using Engineering.Application.Services.TransportationContractors;
using Engineering.Application.Services.TransportationContractors.Models.PriceWeightExcelImports;
using Engineering.Application.Services.TransportationRequests;
using Engineering.Application.Services.TransportationRequests.Models.SnapRequestExcelImports;
using Engineering.Application.Services.Transportations;
using Engineering.Application.Services.Transportations.Models.TransportationExcelImports;
using Engineering.Application.Services.Trips;
using Engineering.Application.Services.Trips.Models.TripExcelImports;
using System.ComponentModel;

namespace Engineering.Api.HttpHandlers;

/// <summary>
/// کنترلر دریافت فایل ها
/// </summary>
[Authorize]
[Route("api/engineering/v1/[controller]")]
public class DocumentImportController : ControllerBase
{
    private readonly ILogger<DocumentImportController> _logger;
    private readonly IBillOfLadingLogic _billOfLadingLogic;
    private readonly IBranchLogic _branchLogic;
    private readonly ICategoryLogic _categoryLogic;
    private readonly ICostCenterTypeLogic _costCenterTypeLogic;
    private readonly ICostOverLogic _costOverLogic;
    private readonly IMachineriesGroupLogic _machineriesGroupLogic;
    private readonly IMachineryLogic _machineryLogic;
    private readonly IMachineTypeLogic _machineTypeLogic;
    private readonly IProjectTypeLogic _projectTypeLogic;
    private readonly ISeasonLogic _seasonLogic;
    private readonly IServiceInfoLogic _serviceInfoLogic;
    private readonly ITransportationLogic _transportationLogic;
    private readonly ITripLogic _tripLogic;
    private readonly IOperationInfoGroupLogic _operationInfoGroupLogic;
    private readonly ICabinTypeLogic _cabinTypeLogic;
    private readonly ITransportationRequestLogic _transportationRequestLogic;
    private readonly IOperationInfoLogic _operationInfoLogic;
    private readonly IShippingCostLogic _shippingCostLogic;
    private readonly ITransportationContractorLogic _transportationContractorLogic;
    private readonly IProjectOperationDetailLogic _projectOperationDetailLogic;
    private readonly IRequestGoodsSupplyLogic _requestGoodsSupplyLogic;

    /// <summary>
    /// سازنده کنترلر
    /// </summary>
    /// <param name="logger">سرویس افزودن لاگ</param>
    /// <param name="billOfLadingLogic">سرویس های بارنامه</param>
    /// <param name="branchLogic">سرویس های رشته</param>
    /// <param name="categoryLogic">سرویس های رسته</param>
    /// <param name="costCenterTypeLogic">سرویس های نوع مرکزهزینه</param>
    /// <param name="costOverLogic">سرویس های هزینه های سربار</param>
    /// <param name="machineriesGroupLogic">سرویس های گروه ماشین آلات</param>
    /// <param name="machineryLogic">سرویس های ماشین آلات</param>
    /// <param name="machineTypeLogic">سرویس های نوع ماشین</param>
    /// <param name="projectTypeLogic">سرویس های نوع پروژه</param>
    /// <param name="seasonLogic">سرویس های خدمات</param>
    /// <param name="serviceInfoLogic">سرویس های خدمات</param>
    /// <param name="transportationLogic">سرویس های ترابری</param>
    /// <param name="tripLogic">سرویس های سفر</param>
    /// <param name="operationInfoGroupLogic">سرویس های گروه شرح عملیات</param>
    /// <param name="cabinTypeLogic">سرویس های نوع اتاق</param>
    /// <param name="transportationRequestLogic">درخواست اسنپ</param>
    public DocumentImportController(ILogger<DocumentImportController> logger, IBillOfLadingLogic billOfLadingLogic, IBranchLogic branchLogic, ICategoryLogic categoryLogic,
        ICostCenterTypeLogic costCenterTypeLogic, ICostOverLogic costOverLogic, IMachineriesGroupLogic machineriesGroupLogic, IMachineryLogic machineryLogic,
        IMachineTypeLogic machineTypeLogic, IProjectTypeLogic projectTypeLogic, ISeasonLogic seasonLogic, IServiceInfoLogic serviceInfoLogic,
        ITransportationLogic transportationLogic, ITripLogic tripLogic, IOperationInfoGroupLogic operationInfoGroupLogic, ICabinTypeLogic cabinTypeLogic,
        ITransportationRequestLogic transportationRequestLogic, IOperationInfoLogic operationInfoLogic, ITransportationContractorLogic transportationContractorLogic,
        IShippingCostLogic shippingCostLogic, IProjectOperationDetailLogic projectOperationDetailLogic, IRequestGoodsSupplyLogic requestGoodsSupplyLogic) : base()
    {
        _logger = logger;
        _billOfLadingLogic = billOfLadingLogic;
        _branchLogic = branchLogic;
        _categoryLogic = categoryLogic;
        _costCenterTypeLogic = costCenterTypeLogic;
        _costOverLogic = costOverLogic;
        _machineriesGroupLogic = machineriesGroupLogic;
        _machineryLogic = machineryLogic;
        _machineTypeLogic = machineTypeLogic;
        _projectTypeLogic = projectTypeLogic;
        _seasonLogic = seasonLogic;
        _serviceInfoLogic = serviceInfoLogic;
        _transportationLogic = transportationLogic;
        _tripLogic = tripLogic;
        _operationInfoGroupLogic = operationInfoGroupLogic;
        _cabinTypeLogic = cabinTypeLogic;
        _transportationRequestLogic = transportationRequestLogic;
        _operationInfoLogic = operationInfoLogic;
        _transportationContractorLogic = transportationContractorLogic;
        _shippingCostLogic = shippingCostLogic;
        _projectOperationDetailLogic = projectOperationDetailLogic;
        _requestGoodsSupplyLogic = requestGoodsSupplyLogic;
    }

    /// <summary>
    /// افزودن بارنامه با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("BillOfLadingExcelImports")]
    public async Task<IResult> BillOfLadingExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - BillOfLadingExcelImports");
        var result = await _billOfLadingLogic.BillOfLadingExcelImports(new BillOfLadingExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن رشته با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("BranchExcelImports")]
    public async Task<IResult> BranchExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - BranchExcelImports");
        var result = await _branchLogic.BranchExcelImports(new BranchExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن رسته با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("CategoryExcelImports")]
    public async Task<IResult> CategoryExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - CategoryExcelImports");
        var result = await _categoryLogic.CategoryExcelImports(new CategoryExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن نوع مرکزهزینه با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("CostCenterTypeExcelImports")]
    public async Task<IResult> CostCenterTypeExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - CostCenterTypeExcelImports");
        var result = await _costCenterTypeLogic.CostCenterTypeExcelImports(new CostCenterTypeExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن هزینه های سربار با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("CostOverExcelImports")]
    public async Task<IResult> CostOverExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - CostOverExcelImports");
        var result = await _costOverLogic.CostOverExcelImports(new CostOverExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن گروه ماشین آلات با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("MachineriesGroupExcelImports")]
    public async Task<IResult> MachineriesGroupExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - MachineriesGroupExcelImports");
        var result = await _machineriesGroupLogic.MachineriesGroupExcelImports(new MachineriesGroupExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن ماشین آلات با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("MachineryExcelImports")]
    public async Task<IResult> MachineryExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - MachineryExcelImports");
        var result = await _machineryLogic.MachineryExcelImports(new MachineryExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن نوع ماشین با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("MachineTypeExcelImports")]
    public async Task<IResult> MachineTypeExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - MachineTypeExcelImports");
        var result = await _machineTypeLogic.MachineTypeExcelImports(new MachineTypeExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن نوع پروژه با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("ProjectTypeExcelImports")]
    public async Task<IResult> ProjectTypeExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - ProjectTypeExcelImports");
        var result = await _projectTypeLogic.ProjectTypeExcelImports(new ProjectTypeExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن فصل با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("SeasonExcelImports")]
    public async Task<IResult> SeasonExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - SeasonExcelImports");
        var result = await _seasonLogic.SeasonExcelImports(new SeasonExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن خدمات با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("ServiceInfoExcelImports")]
    public async Task<IResult> ServiceInfoExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - ServiceInfoExcelImports");
        var result = await _serviceInfoLogic.ServiceInfoExcelImports(new ServiceInfoExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن ترابری با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("TransportationExcelImports")]
    public async Task<IResult> TransportationExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - TransportationExcelImports");
        var result = await _transportationLogic.TransportationExcelImports(new TransportationExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن نوع سفر با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("TripExcelImports")]
    public async Task<IResult> TripExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - TripExcelImports");
        var result = await _tripLogic.TripExcelImports(new TripExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن گروه شرح عملیات با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("OperationInfoGroupExcelImports")]
    public async Task<IResult> OperationInfoGroupExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - OperationInfoGroupExcelImports");
        var result = await _operationInfoGroupLogic.OperationInfoGroupExcelImports(new OperationInfoGroupExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن نوع اتاق با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("CabinTypeExcelImports")]
    public async Task<IResult> CabinTypeExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - CabinTypeExcelImports");
        var result = await _cabinTypeLogic.CabinTypeExcelImports(new CabinTypeExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    /// <summary>
    /// افزودن اسنپ با اکسل
    /// </summary>
    /// <param name="request">فایل اکسل</param>
    /// <param name="ct">متوقف کننده درخواست لغو شده به وسیله کاربر در عملیات های طولانی مدت</param>
    /// <returns>دیتای درخواست شده به همراه وضعیت درخواست و پیام متناسب با وضعیت</returns>
    [HttpPost("SnapRequestExcelImports")]
    public async Task<IResult> SnapRequestExcelImports(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - SnapRequestExcelImports");
        var result = await _transportationRequestLogic.SnapRequestExcelImports(new SnapRequestExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    [HttpPost("OperationInfoExcelImports")]
    public async Task<IResult> OperationInfoExcelImports(
        IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - OperationInfoExcelImports");
        var result = await _operationInfoLogic.OperationInfoExcelImports(
            new OperationInfoExcelImportsRequest(request), ct);
        return result.GetHttpResponse();
    }

    [HttpPost("FehrestBahaExcelImports")]
    [Description("Import Fehrest Baha Excel")]
    public async Task<IResult> FehrestBahaExcelImports(
    FehrestBahaExcelImportsRequest request,
    CT ct)
    {
        var result = await _operationInfoLogic
            .FehrestBahaExcelImports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("RasteReshteExcelImports")]
    [Description("Import Raste Reshte Excel")]
    public async Task<IResult> RasteReshteExcelImports(RasteReshteExcelImportsRequest request, CT ct)
    {
        var result = await _operationInfoLogic.RasteReshteExcelImports(new RasteReshteExcelImportsRequest(request.companyId,request.DocumentFile), ct);
        return result.GetHttpResponse();
    }

    [HttpPost("{id}/ShippingCostExcelImports")]
    public async Task<IResult> ShippingCostExcelImports(long id, IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - ShippingCostExcelImports");
        var result = await _shippingCostLogic.ShippingCostExcelImports(
            new ShippingCostExcelImportsRequest(id, request), ct);
        return result.GetHttpResponse();
    }

    [HttpPost("{id}/PriceWeightExcelImports")]
    public async Task<IResult> PriceWeightExcelImports(
        long id,
        IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - PriceWeightExcelImports");
        var result = await _transportationContractorLogic.PriceWeightExcelImports(
            new PriceWeightExcelImportsRequest(id, request), ct);
        return result.GetHttpResponse();
    }

    [HttpPost("PODetailExcelImport")]
    public async Task<IResult> PODetailExcelImport(IFormFile request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - PODetailExcelImport");
        var result = await _projectOperationDetailLogic.PODetailExcelImport(new PODetailExcelImportRequest(request), ct);
        return result.GetHttpResponse();
    }

    [HttpPost("PRGSupplyImport")]
    public async Task<IResult> PRGSupplyImport(PRGSupplyImportRequest request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - PRGSupplyImport");
        var result = await _requestGoodsSupplyLogic.PRGSupplyImport(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("RGSupplyImport")]
    public async Task<IResult> RGSupplyImport(RGSupplyImportRequest request, CT ct)
    {
        _logger.LogInformation($"DocumentImport - RGSupplyImport");
        var result = await _requestGoodsSupplyLogic.RGSupplyImport(request, ct);
        return result.GetHttpResponse();
    }
}