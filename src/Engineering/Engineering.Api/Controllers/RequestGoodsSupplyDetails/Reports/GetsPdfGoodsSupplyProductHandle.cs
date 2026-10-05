using Commercial.Application.WebServices.ObjectStorages.DownloadFiles.Models.DownloadMultipleFileStream;
using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.Extensions;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.RequestGoodsSupplyDetails;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsPdfGoodsSupplyProduct;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetCompanyById;
using Financial.Application.WebServices.MetaDataServices.Companies.Models;
using Financial.Application.WebServices.MetaDataServices.Companies.Queries.GetCompaniesByIds;
using Gita.Backend.Shared.Application.Abstractions.Interfaces;
using Gita.Backend.Shared.Application.Extensions;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Models;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetsUserById;
using Gita.Shared.FileGenerators;
using Gita.Shared.FileGenerators.PdfGenerator;
using IdentityServer.ClientSdk.Services;
using MediatR;

namespace Engineering.Api.Controllers.RequestGoodsSupplyDetails.Reports;

public class GetsPdfGoodsSupplyProductHandle
{
    private readonly IRequestGoodsSupplyDetailLogic _logic;
    private readonly IMediator _mediator;
    private readonly IUserInfoProvider _userInfoProvider;
    private readonly IUserInfoService _userInfoService;
    private readonly IMetaDataService _metaDataService;
    public GetsPdfGoodsSupplyProductHandle(IMediator mediator,
        IUserInfoProvider userInfoProvider,
        IRequestGoodsSupplyDetailLogic logic,
        IMetaDataService metaDataService,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _userInfoProvider = userInfoProvider;
        _logic = logic;
        _metaDataService = metaDataService;
        _userInfoService = userInfoService;
    }

    public async Task<IResult> Handle(GetsPdfGoodsSupplyProductRequest request, CT ct)
    {
        var response = await _logic.GetsGoodsSupplyProduct(request.Adapt<GetsGoodsSupplyProductRequest>(), ct);

        var data = await PrepareGoodSupplyProductPrintData(response.Value?.Data, ct);
        var ctx = new FileGeneratorContext
        {
            DataProvider = new InMemoryDataProvider<GetsPdfGoodsSupplyProductModel>(data.Value.Data),
            Parameters = new PdfGeneratorParameters(data.Value.Parameters)
            {
                ReportName = data.Value.ReportName
            }
        };

        return new PdfFileResult(data.Value.ReportName, ctx);
    }

    private async Task<Result<(List<GetsPdfGoodsSupplyProductModel> Data, string ReportName, Dictionary<string, object?> Parameters)>>
        PrepareGoodSupplyProductPrintData(List<GetsGoodsSupplyProductModel>? detail, CT ct)
    {
        var reportName = "GoodsSupplyProductReport";
        var headerData = await GetHeaderData(ct);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        var companyData = await _metaDataService.GetCompanyById(new GetCompanyByIdRequest(companyId.Value), ct);
        var company = companyData.Value;

        int index = 1;
        var data = new List<GetsPdfGoodsSupplyProductModel>();
        foreach (var item in detail!)
        {
            data.Add(new()
            {
                Number = Convert.ToString(index++),
                Created = TimeCalculator.ConvertToShamsi(item.Created),
                CostCenterName = item.CostCenterName,
                Creator = item.Creator,
                GroupNumber = item.RGSRequestNumber,
                Measure = item.ProductGroupMeasurementName,
                ProductName = $"{item.ProductGroupName} {item.ProductName} {item.ProductBrand} {item.ProductBrandModel}",
                ProductCode = item.ProductCode,
                ProjectName = item.ProjectName,
                RequestedCount = item.RequestedCount != null ? Convert.ToString(item.RequestedCount) : string.Empty,
                RequestNumber = item.RequestNumber,
                Type = item.TypeDescription,
            });
        }
        var logo = await GetCompanyLogo(company, ct);

        string logoBase64 = logo != null && logo.Files.FirstOrDefault() != null
            ? Convert.ToBase64String(logo.Files.FirstOrDefault()!.Content)
            : string.Empty;

        var arm = logoBase64 ?? headerData.base64Image;
        var supplier = detail.FirstOrDefault()?.Type == Domain.Entities.RequestGoodsSupplies.Enums.GoodsSupplyType.Contractor ?
            detail.FirstOrDefault()?.ContractorFullName : detail.FirstOrDefault()?.SupplyerFullName;
        var parameters = new Dictionary<string, object?>()
        {
            { "PrintDate", $"{TimeCalculator.ConvertToShamsi(DateTime.Now)}" },
            { "PrintUser", $"{headerData.currentUser?.FullName}" },
            { "Company", $"{company?.NameFa}" },
            { "Arm", $"{arm}" },
            { "ReportName", $"{reportName}" },
            { "WarehouseName", $"{detail.FirstOrDefault()?.DestinationWarehouseName ?? detail.FirstOrDefault()?.DefaultWarehouse}" },
            { "Supplier", $"{supplier}" },
            { "Description", $"{detail.FirstOrDefault()?.Description}" },
        };

        return (data, reportName, parameters);
    }

    private async Task<(CompanyModel? company, User? currentUser, string? base64Image)> GetHeaderData(CT cancellationToken)
    {
        CompanyModel? company = null;
        var companyData = await _mediator.Send(new GetCompaniesByIdsQuery([_userInfoProvider.CompanyId], 1, 1), cancellationToken);
        company = companyData.Value?.Data?.FirstOrDefault();

        var userResult = await _mediator.Send(new GetsUserByIdQuery([_userInfoProvider.UserId]), cancellationToken);
        var currentUser = userResult.Value?.Data?.FirstOrDefault();

        string? base64Image = await GetCompanyImage(company, cancellationToken);

        return (company, currentUser, base64Image);
    }

    private async Task<string?> GetCompanyImage(CompanyModel? company, CT cancellationToken)
    {
        string? base64Image = null;
        if (!string.IsNullOrEmpty(company?.LogoUrl))
        {
            var downloadFiles = (await SharedWebServicesExtensions.DownloadMultipleFileStream(
                [new Guid(company.LogoUrl)], _mediator, cancellationToken));

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
