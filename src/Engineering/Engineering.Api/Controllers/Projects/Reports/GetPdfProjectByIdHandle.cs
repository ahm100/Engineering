using Commercial.Application.WebServices.ObjectStorages.DownloadFiles.Models.DownloadMultipleFileStream;
using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.Extensions;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.IdentityServices.Users.Models;
using Engineering.Application.IdentityServices.Users.Queries.GetsUserById;
using Engineering.Application.Services.ContractorContracts;
using Engineering.Application.Services.Projects;
using Engineering.Application.Services.Projects.Models.GetProjectById;
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

namespace Engineering.Api.Controllers.Projects.Reports;

public class GetPdfProjectByIdHandle
{
    private readonly IProjectLogic _logic;
    private readonly IContractorContractLogic _contractLogic;
    private readonly IMediator _mediator;
    private readonly IUserInfoProvider _userInfoProvider;
    private readonly IUserInfoService _userInfoService;
    private readonly IMetaDataService _metaDataService;
    public GetPdfProjectByIdHandle(IMediator mediator,
        IUserInfoProvider userInfoProvider,
        IProjectLogic logic,
        IContractorContractLogic contractLogic,
        IMetaDataService metaDataService,
        IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _userInfoProvider = userInfoProvider;
        _logic = logic;
        _contractLogic = contractLogic;
        _metaDataService = metaDataService;
        _userInfoService = userInfoService;
    }

    public async Task<IResult> Handle(GetProjectByIdRequest request, CT ct)
    {
        var project = await _logic.GetProjectById(request, ct);
        if (project.IsBad()) return project.GetHttpResponse();

        var data = await PrepareProjectPrintData(project.Value!, ct);
        var ctx = new FileGeneratorContext
        {
            DataProvider = new InMemoryDataProvider<object>(Enumerable.Empty<object>()),
            Parameters = new PdfGeneratorParameters(data.Value.Parameters)
            {
                ReportName = data.Value.ReportName,
            }
        };

        return new PdfFileResult(data.Value.ReportName, ctx);
    }

    private async Task<Result<(string ReportName, Dictionary<string, object?> Parameters)>>
        PrepareProjectPrintData(GetProjectByIdResponse header, CT ct)
    {
        var reportName = "ProjectReport";
        var headerData = await GetHeaderData(ct);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        var companyData = await _metaDataService.GetCompanyById(new GetCompanyByIdRequest(companyId.Value), ct);
        var company = companyData.Value;

        int index = 1;

        string? arm = null;

        try
        {
            var logo = await GetCompanyLogo(company, ct);

            string logoBase64 = logo != null && logo.Files.FirstOrDefault() != null
                ? Convert.ToBase64String(logo.Files.FirstOrDefault().Content)
                : string.Empty;

            arm = logoBase64 ?? headerData.base64Image;
        }
        catch (Exception)
        {
        }

        var parameters = new Dictionary<string, object?>()
        {
            { "PrintDate", $"{TimeCalculator.ConvertToShamsi(DateTime.Now)}" },
            { "PrintUser", $"{headerData.currentUser?.FullName}" },
            { "Company", $"{company?.NameFa}" },
            { "Arm", $"{arm}" },
            { "ProjectCode", $"{header.ProjectCode}" },
            { "ProjectTitle", $"{header.ProjectName}" },

            { "Budget", $"{header.ApprovedBudget}" },
            { "Employer", $"{header.EmployerName}" },
            { "ProjectManager", $"{header.ProjectManagerName}" },
            { "Consultant", $"{header.AdvisorName}" },

            { "CostCenter", $"{header.CostCenter?.CostCenterName}" },
            { "Supervisor", $"{header.Supervisor?.FullName}" },
            { "PlanningAssistant", $"{header.PlanningAssistant?.FullName}" },

            { "Country", $"{header.ProjectCityInfo?.Country}" },
            { "Province", $"{header.ProjectCityInfo?.Province}" },
            { "City", $"{header.ProjectCityInfo?.City}" },
            { "AddressDescription", $"{header.ProjectCityInfo?.AddressDescription}" },
        };

        return (reportName, parameters);
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