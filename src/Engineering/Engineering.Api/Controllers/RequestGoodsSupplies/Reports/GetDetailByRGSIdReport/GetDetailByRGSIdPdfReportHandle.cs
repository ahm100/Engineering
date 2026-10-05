using Commercial.Application.WebServices.ObjectStorages.DownloadFiles.Models.DownloadMultipleFileStream;
using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.Extensions;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.IdentityServices.Users.Models;
using Engineering.Application.IdentityServices.Users.Queries.GetsUserById;
using Engineering.Application.Services.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSIdReport.Pdf;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyById;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetCompanyById;
using Engineering.Domain.Errors;
using Engineering.Domain.Errors.WebServices;
using Financial.Application.WebServices.MetaDataServices.Companies.Models;
using Financial.Application.WebServices.MetaDataServices.Companies.Queries.GetCompaniesByIds;
using Gita.Backend.Shared.Application.Abstractions.Interfaces;
using Gita.Backend.Shared.Application.Extensions;
using Gita.Backend.Shared.Domain.Errors;
using Gita.Shared.FileGenerators;
using Gita.Shared.FileGenerators.PdfGenerator;
using IdentityServer.ClientSdk.Services;
using MediatR;

namespace Engineering.Api.Controllers.RequestGoodsSupplies.Reports.GetDetailByRGSIdReport;

public class GetDetailByRGSIdPdfReportHandle
{
    private readonly IRequestGoodsSupplyLogic _logic;
    private readonly IMediator _mediator;
    private readonly IUserInfoProvider _userInfoProvider;
    private readonly IUserInfoService _userInfoService;
    private readonly IMetaDataService _metaDataService;
    public GetDetailByRGSIdPdfReportHandle(
        IMediator mediator,
        IUserInfoProvider userInfoProvider,
        IRequestGoodsSupplyLogic logic,
        IMetaDataService metaDataService,
        IUserInfoService userInfoService)
    {
        _logic = logic;
        _mediator = mediator;
        _userInfoProvider = userInfoProvider;
        _userInfoService = userInfoService;
        _metaDataService = metaDataService;
    }
    public async Task<IResult> Handle(GetDetailByRGSIdPdfReportRequest request, CT ct)
    {
        var response = await _logic.GetRequestGoodsSupplyById(
            new GetRequestGoodsSupplyByIdRequest(request.Id), ct);

        if (response.IsFailure)
            return Results.BadRequest(response.Error);

        var details = await _logic.GetDetailByRGSId(
            new GetDetailByRGSIdRequest(request.Id, request.PageIndex, request.PageSize), ct);

        if (details.IsFailure)
            return Results.BadRequest(details.Error);

        if (details.Value?.Data is null)
            return Results.BadRequest(RequestGoodsSupplyErrors.NoHaveDetails);

        var data = await PrepareGoodSupplyProductPrintData(response.Value, details.Value!.Data, ct);

        var ctx = new FileGeneratorContext
        {
            DataProvider = new InMemoryDataProvider<GetDetailByRGSIdPdfReportDetailNewExporterModel>(data.Value.Data),
            Parameters = new PdfGeneratorParameters(data.Value.Parameters)
            {
                ReportName = data.Value.ReportName,
            },
        };

        return new PdfFileResult(data.Value.ReportName, ctx);
    }

    private async Task<Result<(List<GetDetailByRGSIdPdfReportDetailNewExporterModel> Data, string ReportName, Dictionary<string, object?> Parameters)>>
        PrepareGoodSupplyProductPrintData(GetRequestGoodsSupplyByIdResponse? header, List<GetDetailByRGSIdModel> Data, CT ct)
    {
        var reportName = "RGSTypeByIdReport";
        var headerData = await GetHeaderData(ct);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        var companyData = await _metaDataService.GetCompanyById(new GetCompanyByIdRequest(companyId!.Value), ct);
        if (companyData is null)
            return Result.Failure<(List<GetDetailByRGSIdPdfReportDetailNewExporterModel> Data, string ReportName, Dictionary<string, object?> Parameters)>(SharedErrors.ItemNotFound);
        var company = companyData.Value;

        int index = 1;
        var data = new List<GetDetailByRGSIdPdfReportDetailNewExporterModel>();
        foreach (var item in Data!)
        {
            data.Add(new()
            {
                Index = Convert.ToString(index++),
                Reference = item.Reference,
                ReferenceCode = item.ReferenceCode,
                CostCenterName = item.CostCenterName,
                RequestedCount = item.RequestedCount.ToString(),
                Creator = item.Creator,
                ProjectName = header.ProjectName,
                Measure = item.Measure,
                Inventory = "0"
            });
        }

        var logo = await GetCompanyLogo(company, ct);

        string logoBase64 = logo != null && logo.Files.FirstOrDefault() != null
            ? Convert.ToBase64String(logo.Files.FirstOrDefault()!.Content)
            : string.Empty;

        var arm = logoBase64 ?? headerData.base64Image;
        var companyName = company?.NameFa ?? headerData.company?.NameFa;

        var parameters = new Dictionary<string, object?>()
        {
            { "PrintDate", $"{TimeCalculator.ConvertToShamsi(DateTime.Now)}" },
            { "PrintUser", $"{headerData.currentUser?.FullName}" },
            { "Company", $"{companyName}" },
            { "Arm", $"{arm}" },
            { "ReportName", $"{reportName}" },
            { "PurchaseReason", $"{header?.PurchaseReasonDescription}" },
            { "PurchaseLocation", $"{header?.PurchaseLocationDescription}" },
            { "RequestingOrganization", $"{header?.RequestingOrganization}" },
            { "ProjectName", $"{header?.ProjectName}" },
            { "ConsumptionAddress", $"{header?.ConsumptionAddress}" },
        };

        return (data, reportName, parameters);
    }

    private async Task<(CompanyModel? company, User? currentUser, string? base64Image)> GetHeaderData(CT ct)
    {
        CompanyModel? company = null;
        var companyData = await _mediator.Send(new GetCompaniesByIdsQuery([_userInfoProvider.CompanyId], 1, 1), ct);
        company = companyData.Value?.Data?.FirstOrDefault();

        var userResult = await _mediator.Send(new GetsUserByIdQuery([_userInfoProvider.UserId]), ct);
        var currentUser = userResult.Value?.Data?.FirstOrDefault();

        string? base64Image = await GetCompanyImage(company, ct);

        return (company, currentUser, base64Image);
    }

    private async Task<string?> GetCompanyImage(CompanyModel? company, CT ct)
    {
        string? base64Image = null;
        if (!string.IsNullOrEmpty(company?.LogoUrl))
        {
            var downloadFiles = (await SharedWebServicesExtensions.DownloadMultipleFileStream(
                [new Guid(company.LogoUrl)], _mediator, ct));

            var imageBytes = downloadFiles?.Files?.FirstOrDefault()?.Content;

            if (imageBytes is not null)
            {
                base64Image = Convert.ToBase64String(imageBytes);
            }
        }

        return base64Image;
    }

    private async Task<DownloadMultipleFileStreamsModelValue?> GetCompanyLogo(Company? company, CT cancellationToken)
    {
        string? base64Image = null;
        DownloadMultipleFileStreamsModelValue? val = null;
        if (!string.IsNullOrEmpty(company?.LogoUrl))
        {
            var downloadFiles = (await SharedWebServicesExtensions.DownloadMultipleFileStream(
                [new Guid(company.LogoUrl)], _mediator, cancellationToken));

            val = downloadFiles;
        }

        return val;
    }
}
