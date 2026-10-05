using Commercial.Application.WebServices.ObjectStorages.DownloadFiles.Models.DownloadMultipleFileStream;
using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.Extensions;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.IdentityServices.Users.Models;
using Engineering.Application.IdentityServices.Users.Queries.GetsUserById;
using Engineering.Application.Services.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetCompanyById;
using Financial.Application.WebServices.MetaDataServices.Companies.Models;
using Financial.Application.WebServices.MetaDataServices.Companies.Queries.GetCompaniesByIds;
using Gita.Backend.Shared.Application.Abstractions.Interfaces;
using Gita.Backend.Shared.Application.Extensions;
using Gita.Shared.FileGenerators;
using Gita.Shared.FileGenerators.PdfGenerator;
using IdentityServer.ClientSdk.Services;
using MediatR;

namespace Engineering.Api.Controllers.RequestGoodsSupplies.Reports.GetDetailByRGSIdReport;

public class GetReferenceTypeHistoryFaReportHandle
{
    private readonly IRequestGoodsSupplyLogic _logic;
    private readonly IMediator _mediator;
    private readonly IUserInfoProvider _userInfoProvider;
    private readonly IUserInfoService _userInfoService;
    private readonly IMetaDataService _metaDataService;
    public GetReferenceTypeHistoryFaReportHandle(
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

    public async Task<IResult> Handle(GetReferenceTypeHistoryRequest request, CT ct)
    {
        var response = await _logic.GetReferenceTypeHistory(
            request, ct);

        var data = await PrepareGoodSupplyProductPrintData(response.Value.Data, ct);

        var ctx = new FileGeneratorContext
        {
            DataProvider = new InMemoryDataProvider<GetReferenceTypeHistoryPdfModel>(data.Value.Data),
            Parameters = new PdfGeneratorParameters(data.Value.Parameters)
            {
                ReportName = data.Value.ReportName,
            },
        };

        return new PdfFileResult(data.Value.ReportName, ctx);
    }

    private async Task<Result<(List<GetReferenceTypeHistoryPdfModel> Data, string ReportName, Dictionary<string, object?> Parameters)>>
        PrepareGoodSupplyProductPrintData(List<GetReferenceTypeHistoryModel> Data, CT ct)
    {
        var reportName = "ReferenceTypeHistoryFaReport";
        var headerData = await GetHeaderData(ct);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        var companyData = await _metaDataService.GetCompanyById(new GetCompanyByIdRequest(companyId.Value), ct);
        var company = companyData.Value;

        int index = 1;
        var data = new List<GetReferenceTypeHistoryPdfModel>();
        foreach (var item in Data!)
        {
            data.Add(new()
            {
                Index = Convert.ToString(index++),
                Type = item.TypeDescription,
                DeliveryDeadline = item.DelivaryDeadLineShamsi,
                Creator = item.Creator,
                RequestedCount = item.RequestedCount.ToString(),
                Created = item.CreatedShamsi,
                ProjectName = item.ProjectModel?.ProjectName,
                Importance = item.ImportanceDescription,
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
            { "Reference", $"{Data.FirstOrDefault()?.ReferenceName}" },
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
