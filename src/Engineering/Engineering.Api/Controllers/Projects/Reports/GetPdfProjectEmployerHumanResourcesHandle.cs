using Commercial.Application.WebServices.ObjectStorages.DownloadFiles.Models.DownloadMultipleFileStream;
using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.Extensions;
using Engineering.Application.Extensions.TimeCalculator;
using Engineering.Application.Services.EmployerContracts;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetECThirdParties;
using Engineering.Application.Services.Projects;
using Engineering.Application.Services.Projects.Models.GetProjectForPdf;
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

namespace Engineering.Api.Controllers.Projects.Reports;

public class GetPdfProjectEmployerHumanResourcesHandle
{
    private readonly IProjectLogic _logic;
    private readonly IEContractHeaderLogic _contractLogic;
    private readonly IMediator _mediator;
    private readonly IUserInfoProvider _userInfoProvider;
    private readonly IUserInfoService _userInfoService;
    private readonly IMetaDataService _metaDataService;
    public GetPdfProjectEmployerHumanResourcesHandle(IMediator mediator,
        IUserInfoProvider userInfoProvider,
        IProjectLogic logic,
        IEContractHeaderLogic contractLogic,
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

    public async Task<IResult> Handle(GetECThirdPartiesRequest request, CT ct)
    {
        var project = await _logic.GetProjectForPdf(new GetProjectForPdfRequest(request.ProjectId), ct);
        if (project.IsBad()) return project.GetHttpResponse();

        var details = await _contractLogic.GetECThirdParties(request, ct);
        if (details.IsBad()) return details.GetHttpResponse();

        var data = await PrepareProjectPrintData(project.Value!, details.Value, ct);
        var ctx = new FileGeneratorContext
        {
            DataProvider = new InMemoryDataProvider<ECThirdPartiesPdfModel>(data.Value.Data),
            Parameters = new PdfGeneratorParameters(data.Value.Parameters)
            {
                ReportName = data.Value.ReportName,
            }
        };

        return new PdfFileResult(data.Value.ReportName, ctx);
    }

    private async Task<Result<(List<ECThirdPartiesPdfModel> Data, string ReportName, Dictionary<string, object?> Parameters)>>
        PrepareProjectPrintData(GetProjectForPdfResponse header, GetECThirdPartiesResponse? detail, CT ct)
    {
        var reportName = "ProjectEmployerHumanResourceReport";
        var headerData = await GetHeaderData(ct);

        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        var companyData = await _metaDataService.GetCompanyById(new GetCompanyByIdRequest(companyId!.Value), ct);
        var company = companyData!.Value;

        int index = 1;

        var logo = await GetCompanyLogo(company, ct);

        var data = new List<ECThirdPartiesPdfModel>();
        foreach (var item in detail.Data!)
        {
            data.Add(new()
            {
                Number = Convert.ToString(index++),
                WorkStartDate = item.StartDateShamsi,
                WorkEndDate = item.EndDateShamsi,
                Employer = item.Employer,
                FullName = item.ThirdParty,
                ContractNumber = item.EContractHeadCode,
                Skill = item.Skill,
            });
        }

        string logoBase64 = logo != null && logo.Files.FirstOrDefault() != null
            ? Convert.ToBase64String(logo.Files.FirstOrDefault()!.Content)
            : string.Empty;

        var arm = logoBase64 ?? headerData.base64Image;
        var parameters = new Dictionary<string, object?>()
        {
            { "PrintDate", $"{TimeCalculator.ConvertToShamsi(DateTime.Now)}" },
            { "PrintUser", $"{headerData.currentUser?.FullName}" },
            { "Company", $"{company?.NameFa}" },
            { "Arm", $"{arm}" },
            { "ReportName", $"{reportName}" },
            { "ProjectCode", $"{header.ProjectCode}" },
            { "ProjectName", $"{header.ProjectName}" },
            { "ProjectStatus", $"{header.StatusTitle}" },
            { "ApprovedBudget", $"{header.ApprovedBudget}" },
            { "Employer", $"{header.EmployerName}" },
            { "ProjectManager", $"{header.ProjectManagerName}" },
            { "Consultant", $"{header.AdvisorName}" },
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