using Commercial.Application.WebServices.ObjectStorages.DownloadFiles.Models.DownloadMultipleFileStream;
using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.Extensions;
using Engineering.Application.Extensions.Excels.Exporters;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.RequestGoodsSupplyDetails;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct.GetsRequestGoodsSupplyProductNewExcelEnum;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetCompanyById;
using Gita.Backend.Shared.Application.Abstractions.Interfaces;
using Gita.Backend.Shared.Application.Extensions;
using MediatR;

namespace Engineering.Api.Controllers.RequestGoodsSupplyDetails.Reports;

public class GetRGSProductXlsxReportHandle
{

    private readonly IRequestGoodsSupplyDetailLogic _logic;
    private readonly IMediator _mediator;
    private readonly IUserInfoService _userInfoService;
    private readonly IMetaDataService _metaDataService;

    public GetRGSProductXlsxReportHandle(IRequestGoodsSupplyDetailLogic logic,
        IMediator mediator,
        IMetaDataService metaDataService,
        IUserInfoService userInfoService)
    {
        _logic = logic;
        _userInfoService = userInfoService;
        _mediator = mediator;
        _metaDataService = metaDataService;
    }

    public async Task<IResult> Handle(GetRGSProductXlsxReportRequest request, CT ct)
    {
        var req = request.Adapt<GetsRequestGoodsSupplyProductRequest>();
        var response = await _logic.GetsRequestGoodsSupplyProduct(req, ct);

        var value = response.Value.Data.FirstOrDefault();

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        var companyData = await _metaDataService.GetCompanyById(new GetCompanyByIdRequest(companyId.Value), ct);
        var company = companyData.Value;

        var head = new GetsRequestGoodsSupplyProductNewExporterHeaderModel
        {
            RGSRequestNumber = value.RGSRequestNumber,
            CostCenterName = value.CostCenterName,
            ProjectName = value.ProjectName,
            RequestedDate = value.RequestedDate,
            Creator = value.Creator,
        };

        var rows = response.Value.Data
            .Select(x => new GetsRequestGoodsSupplyProductNewExporterModel
            {
                RequestNumber = x.RequestNumber,
                ProductName = x.ProductName,
                Count = x.RequestedCount,
                MeasureName = x.ProductGroupMeasurementName,
                ProductCode = x.ProductTechnicalCode,
                RequestedDate = x.RequestedDate,
                Creator = x.Creator
            })
            .ToList();
        var logo = await GetCompanyImage(company, ct);
        var result = new FileContentResult(RGSupplyExcels.GenerateRGSHeaderTable(head, company.NameFa, logo is not null && logo.Files.Any() && logo.Files.Count > 0 ? logo.Files.FirstOrDefault().Content : null, rows),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"GenerateRGSHeaderTable-{TimeCalculator.DatePiker(DateTime.UtcNow)}.xlsx",
            LastModified = DateTime.UtcNow
        };
        return Result.Success<GetRGSProductXlsxReportResponse?>(
            new(result)).GetHttpResponse();
    }

    private async Task<DownloadMultipleFileStreamsModelValue?> GetCompanyImage(Company? company, CT cancellationToken)
    {
        string? base64Image = null;
        DownloadMultipleFileStreamsModelValue? val = null;
        if (!string.IsNullOrEmpty(company?.LogoUrl))
        {
            var downloadFiles = await SharedWebServicesExtensions.DownloadMultipleFileStream(
                [new Guid(company.LogoUrl)], _mediator, cancellationToken);

            val = downloadFiles;
        }

        return val;
    }
}

