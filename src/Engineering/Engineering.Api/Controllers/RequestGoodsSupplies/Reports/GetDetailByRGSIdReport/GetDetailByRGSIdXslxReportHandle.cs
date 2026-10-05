using Commercial.Application.WebServices.ObjectStorages.DownloadFiles.Models.DownloadMultipleFileStream;
using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.Extensions;
using Engineering.Application.Services.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSIdReport.Xslx;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct.GetsRequestGoodsSupplyProductNewExcelEnum;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetCompanyById;
using Engineering.Domain.Errors;
using Gita.Backend.Shared.Application.Abstractions.Interfaces;
using Gita.Backend.Shared.Application.Extensions;
using MediatR;
namespace Engineering.Api.Controllers.RequestGoodsSupplies.Reports.GetDetailByRGSIdReport;

public class GetDetailByRGSIdXslxReportHandle
{
    private readonly IRequestGoodsSupplyLogic _logic;
    private readonly IMediator _mediator;
    private readonly IUserInfoService _userInfoService;
    private readonly IMetaDataService _metaDataService;

    public GetDetailByRGSIdXslxReportHandle(IRequestGoodsSupplyLogic logic,
        IMediator mediator,
        IMetaDataService metaDataService,
        IUserInfoService userInfoService)
    {
        _logic = logic;
        _userInfoService = userInfoService;
        _mediator = mediator;
        _metaDataService = metaDataService;
    }

    public async Task<IResult> Handle(
    GetDetailByRGSIdXslxReportRequest request,
    CT ct)
    {
        var excel = await GenerateRGSExcel(request, ct);

        if (excel.IsFailure)
            return excel.GetHttpResponse();

        var result = new FileContentResult(
            excel.Value!,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"GenerateRGS-{DateTime.UtcNow:yyyyMMddHHmmss}.xlsx"
        };

        return Result
            .Success<GetRGSProductXlsxReportResponse?>(new(result))
            .GetHttpResponse();
    }

    private async Task<Result<byte[]>> GenerateRGSExcel(
        GetDetailByRGSIdXslxReportRequest request,
        CT ct)
    {
        var response = await _logic.GetRequestGoodsSupplyById(
            new GetRequestGoodsSupplyByIdRequest(request.Id),
            ct);

        if (response.IsFailure)
            return Result.Failure<byte[]>(response.Error)!;

        var details = await _logic.GetDetailByRGSId(
            new GetDetailByRGSIdRequest(
                request.Id,
                request.PageIndex,
                request.PageSize),
            ct);

        if (details.IsFailure)
            return Result.Failure<byte[]>(details.Error)!;

        if (details.Value?.Data is null)
            return Result.Failure<byte[]>(RequestGoodsSupplyErrors.NoHaveDetails)!;

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);

        var companyData = await _metaDataService.GetCompanyById(
            new GetCompanyByIdRequest(companyId.Value),
            ct);

        var value = response.Value;

        var head = new GetRGSNewExporterHeaderModel
        {
            ProjectName = value.ProjectName,
            PurchaseLocation = value.PurchaseLocationDescription,
            PurchaseReason = value.PurchaseReasonDescription,
            RequestingOrganization = value.RequestingOrganization,
        };

        var rows = details.Value.Data
            .Select((item, index) =>
                new GetRequestGoodsSupplyDetailNewExporterModel
                {
                    Index = index + 1,
                    Reference = item.Reference,
                    ReferenceCode = item.ReferenceCode,
                    ProjectName = value.ProjectName,
                    Count = item.RequestedCount.ToString(),
                    CostCenterName = item.CostCenterName,
                    Creator = item.Creator
                })
            .ToList();

        var logo = await GetCompanyImage(companyData.Value, ct);

        var excel = RGSupplyExcels.GenerateRGSHeaderTable(
            head,
            companyData.Value.NameFa,
            logo?.Files?.FirstOrDefault()?.Content,
            rows);

        return Result.Success(excel);
    }

    private async Task<DownloadMultipleFileStreamsModelValue?> GetCompanyImage(
        Company? company,
        CT cancellationToken)
    {
        if (string.IsNullOrEmpty(company?.LogoUrl))
            return null;

        return await SharedWebServicesExtensions.DownloadMultipleFileStream(
            [new Guid(company.LogoUrl)],
            _mediator,
            cancellationToken);
    }
}
