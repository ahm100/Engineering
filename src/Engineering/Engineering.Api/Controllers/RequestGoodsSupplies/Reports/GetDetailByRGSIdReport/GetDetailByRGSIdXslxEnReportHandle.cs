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

public class GetDetailByRGSIdXslxEnReportHandle
{
    private readonly IRequestGoodsSupplyLogic _logic;
    private readonly IMediator _mediator;
    private readonly IUserInfoService _userInfoService;
    private readonly IMetaDataService _metaDataService;

    public GetDetailByRGSIdXslxEnReportHandle(IRequestGoodsSupplyLogic logic,
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
        GetDetailByRGSIdXslxEnReportRequest request,
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
        GetDetailByRGSIdXslxEnReportRequest request,
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
            ProjectName = value.ProjectEnName,
            PurchaseLocation = value.PurchaseLocation?.ToString() ?? string.Empty,
            PurchaseReason = value.PurchaseReason?.ToString() ?? string.Empty,
            RequestingOrganization = value.RequestingOrganizationEn,
        };

        var rows = details.Value.Data
            .Select((item, index) =>
                new GetRequestGoodsSupplyDetailNewExporterModel
                {
                    Index = index + 1,
                    Reference = item.ReferenceEn,
                    ReferenceCode = item.ReferenceCode,
                    ProjectName = value.ProjectEnName,
                    Count = item.RequestedCount.ToString(),
                    CostCenterName = item.CostCenterEnName,
                    Creator = item.CreatorEnName
                })
            .ToList();

        var logo = await GetCompanyImage(companyData.Value, ct);

        var excel = RGSupplyEnExcels.GenerateRGSHeaderTable(
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
