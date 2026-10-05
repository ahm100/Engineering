using Engineering.Application.Services.ProjectOperations.Models.GetsStatus;
using Engineering.Application.Services.Projects.Commands.CreateProjectCode;
using Engineering.Application.Services.Projects.Models.CreateProjectProduct;
using Engineering.Application.Services.Projects.Models.DeleteProjectProduct;
using Engineering.Application.Services.Projects.Models.GetProjectById;
using Engineering.Application.Services.Projects.Models.GetsContractedProject;
using Engineering.Application.Services.Projects.Models.GetSummarizedProjectById;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Application.Services.Projects.Models.UpdateProjectProduct;
using Engineering.Application.Services.Projects.Queries.GetLastProjectCode;
using Engineering.Application.Services.Projects.Queries.GetProjectByCode;
using Engineering.Application.Services.Projects.Queries.GetProjectById;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Synonyms.MetaData.Cities;

namespace Engineering.Application.Services.Projects;

partial class ProjectLogic
{
    private async Task<Result<UpdateProjectProductResponse?>> UpdateProjectProductHandler(
        UpdateProjectProductRequest request,
        long? companyId,
        CT ct)
    {
        var project = await _mediator.Send(
            new GetProjectByIdIncludelessQuery(request.ProjectId), ct);

        if (project.IsBad())
            return project.Failure<UpdateProjectProductResponse?>()!;

        var toCreate = request.Products
            .Where(p => p.ProjectProductId is null && p.IsDelete != true)
            .ToList();

        var toUpdate = request.Products
            .Where(p => p.ProjectProductId != null && p.IsDelete == false)
            .ToList();

        var toDelete = request.Products
            .Where(p => p.ProjectProductId != null && p.IsDelete == true)
            .ToList();

        if (toDelete.Any())
        {
            var deleteRequest = new DeleteProjectProductRequest(
                toDelete.Select(p => p.ProjectProductId!.Value).ToList());

            var deleteResult = await DeleteProjectProductCommand(deleteRequest, ct);

            if (deleteResult.IsBad())
                return deleteResult.Failure<UpdateProjectProductResponse?>()!;
        }

        if (toUpdate.Any())
        {
            var updateRequest = new UpdateProjectProductRequest(
                request.ProjectId,
                toUpdate);

            var updateResult = await UpdateProjectProductCommand(updateRequest, ct);

            if (updateResult.IsBad())
                return updateResult.Failure<UpdateProjectProductResponse?>()!;
        }

        if (toCreate.Any())
        {
            if (toCreate.Any(p => p.ProductGroupId is null))
                return Result.Failure<UpdateProjectProductResponse?>(
                    ProjectErrors.ProjectProductWithIdsNotFound);

            if (toCreate.Any(p => p.TolerancePercentage is null))
                return Result.Failure<UpdateProjectProductResponse?>(
                    ProjectErrors.TolerancePercentageIsEmpty);

            var createRequest = new CreateProjectProductRequest(
                request.ProjectId,
                null,
                toCreate.Select(p =>
                    new CreateProjectProductModel(
                        p.ProductGroupId,
                        p.ProductCategoryId,
                        p.RequestQuantity,
                        p.TolerancePercentage!.Value,
                        p.IsActive,
                        p.DefaultManagerSet,
                        p.ProductType
                    )).ToList());

            var createResult = await CreateProjectProductCommand(
                createRequest,
                project.Value,
                companyId,
                ct);

            if (createResult.IsBad())
                return createResult.Failure<UpdateProjectProductResponse?>()!;
        }

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectProductResponse(true);
    }

    private async Task<Result<CreateProjectProductResponse?>> CreateProjectProductHandler(
        CreateProjectProductRequest request,
        long? companyId,
        CT ct)
    {
        var project = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId));
        if (project.IsBad())
            return project.Failure<CreateProjectProductResponse>()!;
        if (!project.Value!.ProjectCostCenters.Any())
            return Result.Failure<CreateProjectProductResponse>(ProjectErrors.CostCenterNotSet);

        var existingProjectProducts = await _ppRepo.GetProductByProjectId(request.ProjectId, ct);
        var groupIds = request.Products.NullListed(x => x.ProductGroupId);
        var existingGroupIds = existingProjectProducts.NullListed(x => x.ProductGroupId);
        if (groupIds.Any() && existingGroupIds.Any(id => groupIds.Contains(id)))
            return Result.Failure<CreateProjectProductResponse>(ProjectErrors.GroupWithIdAssigned);

        var existingCatIds = existingProjectProducts.NullListed(x => x.ProductCategoryId);
        var catIds = request.Products.NullListed(x => x.ProductCategoryId);
        if (catIds.Any() && existingCatIds.Any(id => catIds.Contains(id)))
            return Result.Failure<CreateProjectProductResponse>(ProjectErrors.CategoryWithIdAssigned);

        if (request.Products.Any(x => x.ProductGroupId == null) && request.Products.Any(x => x.ProductCategoryId == null))
            return Result.Failure<CreateProjectProductResponse>(ProjectErrors.UnAssignBothType);
        if (request.Products.Any(x => x.ProductGroupId.HasValue) && request.Products.Any(x => x.ProductCategoryId.HasValue))
            return Result.Failure<CreateProjectProductResponse>(ProjectErrors.AssignBothType);
        if (request.Products.Any(x => x.ProductGroupId.HasValue) && existingProjectProducts.Any(x => x.ProductCategoryId.HasValue))
            return Result.Failure<CreateProjectProductResponse>(ProjectErrors.CategoryIsAssigned);
        if (existingProjectProducts.Any(x => x.ProductGroupId.HasValue) && request.Products.Any(x => x.ProductCategoryId.HasValue))
            return Result.Failure<CreateProjectProductResponse>(ProjectErrors.GroupIsAssigned);

        var categoryIds = request.Products.Select(x => x.ProductCategoryId);
        var existingCategoryIds = existingProjectProducts.NullListed(x => x.ProductCategoryId);
        if (categoryIds.Any() && existingCategoryIds.Any(id => categoryIds.Contains(id)))
            return Result.Failure<CreateProjectProductResponse>(ProjectErrors.CategoryWithIdAssigned);

        var groupProducts = request.Products
            .Where(x => x.ProductType == ProjectProductType.ProductGroup)
            .NullListed(x => x.ProductGroupId);
        var create = await CreateProjectProductCommand(request, project.Value!, companyId, ct);
        if (create.IsBad())
            return create.Failure<CreateProjectProductResponse>()!;

        return new CreateProjectProductResponse(true);
    }


    //دریافت تمامی شناسه های کاربران برای متادیتا یک پروژه
    private List<long> IdCollector(Project project)
    {
        var userImplementations = project.ProjectImplementationAssistants;
        var userImplementationIds = new List<long>();
        if (userImplementations.Any())
            userImplementationIds = userImplementations!.Where(x => x.ImplementationAssistantUserId > 0).Select(x => x.ImplementationAssistantUserId).ToList();

        var userTechnicals = project.ProjectTechnicalAssistants;
        var userTechnicalsIds = new List<long>();
        if (userTechnicals.Any())
            userTechnicalsIds = userTechnicals!.Where(x => x.TechnicalAssistantUserId > 0).Select(x => x.TechnicalAssistantUserId).ToList();

        List<long> allIds = [];

        if (project.EmployerId is not null && project.EmployerId > 0)
            allIds.Add((long)project.EmployerId!);

        if (project.SupervisorEngineer is not null && project.SupervisorEngineer > 0)
            allIds.Add((long)project.SupervisorEngineer!);
        if (project.Advisor is not null && project.Advisor > 0)
            allIds.Add((long)project.Advisor!);
        if (project.ProjectManager > 0)
            allIds.Add((long)project.ProjectManager!);
        if (project.PlanningAssistant is not null && project.PlanningAssistant > 0)
            allIds.Add((long)project.PlanningAssistant!);

        allIds.AddRange(userImplementationIds);
        allIds.AddRange(userTechnicalsIds);
        return allIds.Where(x => x != 0).Distinct().ToList();
    }
    // سازنده لیستی از دیتای خروجی پروژه ها به همراه اطلاعات کابران برای یک پروژه
    private ProjectModel ProjectDataCollector(Project project, List<GetUserInfo>? usersInfos, Company? company)
    {
        var UserImplementationsData = new List<ImplementationModel>();
        foreach (var item in project.ProjectImplementationAssistants)
            UserImplementationsData.Add(new ImplementationModel(item.ImplementationAssistantUserId,
                usersInfos?.Where(x => x?.Id == item.ImplementationAssistantUserId).Select(x => x?.FullName).FirstOrDefault()));

        var UserTechnicalsData = new List<TechnicalModel>();
        foreach (var item in project.ProjectTechnicalAssistants)
            UserTechnicalsData.Add(new TechnicalModel(item.TechnicalAssistantUserId,
                usersInfos?.Where(x => x?.Id == item.TechnicalAssistantUserId).Select(x => x?.FullName).FirstOrDefault()));

        ProjectUserModel? employer = null;
        var employerInfo = usersInfos?.Where(x => x?.Id == project.EmployerId).FirstOrDefault();
        if (employerInfo != null)
            employer = new ProjectUserModel(project.EmployerId, employerInfo?.UserId, employerInfo?.FullName, employerInfo?.AvatarUrl, employerInfo?.OrganizationCode);

        ProjectUserModel? supervisor = null;
        var supervisorInfo = usersInfos?.Where(x => x?.Id == project.SupervisorEngineer).FirstOrDefault();
        if (supervisorInfo != null)
            supervisor = new ProjectUserModel(project.SupervisorEngineer, supervisorInfo?.UserId, supervisorInfo?.FullName, supervisorInfo?.AvatarUrl, supervisorInfo?.OrganizationCode);

        ProjectUserModel? advisor = null;
        var advisorInfo = usersInfos?.Where(x => x?.Id == project.Advisor).FirstOrDefault();
        if (advisorInfo != null)
            advisor = new ProjectUserModel(project.Advisor, advisorInfo?.UserId, advisorInfo?.FullName, advisorInfo?.AvatarUrl, advisorInfo?.OrganizationCode);

        ProjectUserModel? projectManager = null;
        var projectManagerInfo = usersInfos?.Where(x => x?.Id == project.ProjectManager).FirstOrDefault();
        if (projectManagerInfo != null)
            projectManager = new ProjectUserModel(project.ProjectManager, projectManagerInfo?.UserId, projectManagerInfo?.FullName, projectManagerInfo?.AvatarUrl, projectManagerInfo?.OrganizationCode);

        ProjectUserModel? planningAssistant = null;
        var planningAssistantInfo = usersInfos?.Where(x => x?.Id == project.PlanningAssistant).FirstOrDefault();
        if (planningAssistantInfo != null)
            planningAssistant = new ProjectUserModel(project.PlanningAssistant, planningAssistantInfo?.UserId, planningAssistantInfo?.FullName, planningAssistantInfo?.AvatarUrl, planningAssistantInfo?.OrganizationCode);

        var status = new ProjectStatusModel((int)project.Status, project.Status.GetEnumDescription());

#pragma warning disable CS8604 // Possible null reference argument.
        var catData = new List<ProjectCategoryModel>();
        foreach (var cat in project.ProjectCategories)
            catData.Add(new ProjectCategoryModel(cat.Id, cat.Category.CategoryName, cat.Category.CategoryCode));

        var response = new ProjectModel(project.Id, project.ProjectType!.Id, project.ProjectType!.ProjectTypeTitle, project.ProjectName, project.ProjectCode,
            project.EmployerId, usersInfos?.Where(x => x?.Id == project.EmployerId).Select(x => x?.FullName).FirstOrDefault(),
            catData.Select(x => x.Id.ToString()).JoinList(), catData.Select(x => x.CategoryName).JoinList(), project.ProjectCostCenters.FirstOrDefault().CostCenter.Id, project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
            project.SupervisorEngineer, usersInfos?.Where(x => x?.Id == project.SupervisorEngineer).Select(x => x?.FullName).FirstOrDefault(),
            project.Advisor, usersInfos?.Where(x => x?.Id == project.Advisor).Select(x => x?.FullName).FirstOrDefault(),
            project.ProjectManager, usersInfos?.Where(x => x?.Id == project.ProjectManager).Select(x => x?.FullName).FirstOrDefault(),
            project.PlanningAssistant, usersInfos?.Where(x => x?.Id == project.PlanningAssistant).Select(x => x?.FullName).FirstOrDefault(),
            UserImplementationsData != null && UserImplementationsData.Count > 0 ? UserImplementationsData : [],
            UserTechnicalsData != null && UserTechnicalsData.Count > 0 ? UserTechnicalsData : [],
            project.Status, project.Status.GetEnumDescription(), project.CollectiveService, project.IsActive, project.Contractual, status, project.ProjectType.Adapt<ProjectTypeModel>(),
            catData, project.ProjectCostCenters.FirstOrDefault()?.CostCenter.Adapt<ProjectCostCenterModel>(),
            employer != null ? employer : null,
            supervisor != null ? supervisor : null,
            advisor != null ? advisor : null,
            projectManager != null ? projectManager : null,
            planningAssistant != null ? planningAssistant : null,
            project.CompanyId, company?.NameFa, project.PreferentialReferenceCode);
#pragma warning restore CS8604 // Possible null reference argument.
        return response;
    }

    private GetProjectByIdResponse ProjectDatasCollector(Project project, List<GetUserInfo>? usersInfos, Company? company)
    {
        var UserImplementationsData = new List<ImplementationModel>();
        foreach (var item in project.ProjectImplementationAssistants)
            UserImplementationsData.Add(new ImplementationModel(item.ImplementationAssistantUserId,
                usersInfos?.Where(x => x?.Id == item.ImplementationAssistantUserId).Select(x => x?.FullName).FirstOrDefault()));

        var UserTechnicalsData = new List<TechnicalModel>();
        foreach (var item in project.ProjectTechnicalAssistants)
            UserTechnicalsData.Add(new TechnicalModel(item.TechnicalAssistantUserId,
                usersInfos?.Where(x => x?.Id == item.TechnicalAssistantUserId).Select(x => x?.FullName).FirstOrDefault()));

        ProjectUserModel? employer = null;
        var employerInfo = usersInfos?.Where(x => x?.Id == project.EmployerId).FirstOrDefault();
        if (employerInfo != null)
            employer = new ProjectUserModel(project.EmployerId, employerInfo?.UserId, employerInfo?.FullName, employerInfo?.AvatarUrl, employerInfo?.OrganizationCode);

        ProjectUserModel? supervisor = null;
        var supervisorInfo = usersInfos?.Where(x => x?.Id == project.SupervisorEngineer).FirstOrDefault();
        if (supervisorInfo != null)
            supervisor = new ProjectUserModel(project.SupervisorEngineer, supervisorInfo?.UserId, supervisorInfo?.FullName, supervisorInfo?.AvatarUrl, supervisorInfo?.OrganizationCode);

        ProjectUserModel? advisor = null;
        var advisorInfo = usersInfos?.Where(x => x?.Id == project.Advisor).FirstOrDefault();
        if (advisorInfo != null)
            advisor = new ProjectUserModel(project.Advisor, advisorInfo?.UserId, advisorInfo?.FullName, advisorInfo?.AvatarUrl, advisorInfo?.OrganizationCode);

        ProjectUserModel? projectManager = null;
        var projectManagerInfo = usersInfos?.Where(x => x?.Id == project.ProjectManager).FirstOrDefault();
        if (projectManagerInfo != null)
            projectManager = new ProjectUserModel(project.ProjectManager, projectManagerInfo?.UserId, projectManagerInfo?.FullName, projectManagerInfo?.AvatarUrl, projectManagerInfo?.OrganizationCode);

        ProjectUserModel? planningAssistant = null;
        var planningAssistantInfo = usersInfos?.Where(x => x?.Id == project.PlanningAssistant).FirstOrDefault();
        if (planningAssistantInfo != null)
            planningAssistant = new ProjectUserModel(project.PlanningAssistant, planningAssistantInfo?.UserId, planningAssistantInfo?.FullName, planningAssistantInfo?.AvatarUrl, planningAssistantInfo?.OrganizationCode);
        var status = new ProjectStatusModel((int)project.Status, project.Status.GetEnumDescription());

#pragma warning disable CS8604 // Possible null reference argument.
        var response = new GetProjectByIdResponse()
        {
            Id = project.Id,
            ProjectTypeId = project.ProjectTypeId,
            ProjectTypeTitle = project.ProjectType?.ProjectTypeTitle,
            ProjectName = project.ProjectName,
            ProjectEnName = project.ProjectEnName,
            ProjectCode = project.ProjectCode,
            Prefix = project.Prefix,
            EmployerId = project.EmployerId,
            EmployerName = usersInfos?.Where(x => x?.Id == project.EmployerId).Select(x => x?.FullName).FirstOrDefault(),
            CostCenterId = project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
            CostCenterName = project.ProjectCostCenters.FirstOrDefault()?.CostCenter?.CostCenterName,
            SupervisorEngineer = project.SupervisorEngineer,
            SupervisorEngineerName = usersInfos?.Where(x => x?.Id == project.SupervisorEngineer).Select(x => x?.FullName).FirstOrDefault(),
            AdvisorId = project.Advisor,
            AdvisorName = usersInfos?.Where(x => x?.Id == project.Advisor).Select(x => x?.FullName).FirstOrDefault(),
            ProjectManagerId = project.ProjectManager,
            ProjectManagerName = usersInfos?.Where(x => x?.Id == project.ProjectManager).Select(x => x?.FullName).FirstOrDefault(),
            PlanningAssistantId = project.PlanningAssistant,
            PlanningAssistantName = usersInfos?.Where(x => x?.Id == project.PlanningAssistant).Select(x => x?.FullName).FirstOrDefault(),
            ImplementationAssistants = UserImplementationsData != null && UserImplementationsData.Count > 0 ? UserImplementationsData : [],
            TechnicalAssistants = UserTechnicalsData != null && UserTechnicalsData.Count > 0 ? UserTechnicalsData : [],
            ProjectStatus = status,
            CollectiveService = project.CollectiveService,
            IsActive = project.IsActive,
            HasProduct = project.HasProduct,
            OrganizationId = project.OrganizationId,
            IsOrganizationUnit = project.IsOrganizationUnit,
            Contractual = project.Contractual,
            ProjectType = project.ProjectType.Adapt<ProjectTypeModel>(),
            CostCenter = project.ProjectCostCenters.FirstOrDefault()?.CostCenter.Adapt<ProjectCostCenterModel>(),
            CostCenterList = project.ProjectCostCenters.Select(x => new ProjectCostCenterModel(
                x.CostCenterId,
                x.CostCenter?.CostCenterName,
                x.CostCenter?.CostCenterCode
            )).ToList(),
            Employer = employer != null ? employer : null,
            Supervisor = supervisor != null ? supervisor : null,
            Advisor = advisor != null ? advisor : null,
            ProjectManager = projectManager != null ? projectManager : null,
            PlanningAssistant = planningAssistant != null ? planningAssistant : null,
            ApprovedBudget = project.ApprovedBudget,
            CompanyId = project.CompanyId,
            Description = project.Description,
            DescriptionEn = project.DescriptionEn,
            CompanyNameFa = company?.NameFa,
            CreatorId = project.CreatorId,
            PreferentialReferenceCode = project.PreferentialReferenceCode,
            Status = project.Status,
            Category = project.ProjectCategories.Select(x => new GetProjectsCategoryModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.Category.CategoryName,
                CategoryCode = x.Category.CategoryCode
            }).ToList()
        };
#pragma warning restore CS8604 // Possible null reference argument.
        return response;
    }
    private GetSummarizedProjectByIdResponse SummarizedProjectDataCollector(Project project, List<GetUserInfo>? usersInfos, Company? company)
    {
        var userImplementationsData = new List<ImplementationModel>();
        foreach (var item in project.ProjectImplementationAssistants)
            userImplementationsData.Add(new ImplementationModel(item.ImplementationAssistantUserId,
                usersInfos?.Where(x => x?.Id == item.ImplementationAssistantUserId).Select(x => x?.FullName).FirstOrDefault()));

        var userTechnicalsData = new List<TechnicalModel>();
        foreach (var item in project.ProjectTechnicalAssistants)
            userTechnicalsData.Add(new TechnicalModel(item.TechnicalAssistantUserId,
                usersInfos?.Where(x => x?.Id == item.TechnicalAssistantUserId).Select(x => x?.FullName).FirstOrDefault()));

        var projectManagerInfo = usersInfos?.Where(x => x?.Id == project.ProjectManager).FirstOrDefault();
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
        var projectManager = new ProjectUserModel(project.ProjectManager != 0 || project.ProjectManager != null ? project.ProjectManager : null, projectManagerInfo?.UserId, projectManagerInfo?.FullName, projectManagerInfo?.AvatarUrl, projectManagerInfo?.OrganizationCode);
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'

        var planningAssistantInfo = usersInfos?.Where(x => x?.Id == project.PlanningAssistant).FirstOrDefault();
        var planningAssistant = new ProjectUserModel(project.PlanningAssistant != 0 || project.PlanningAssistant != null ? project.PlanningAssistant : null, planningAssistantInfo?.UserId, planningAssistantInfo?.FullName, planningAssistantInfo?.AvatarUrl, planningAssistantInfo?.OrganizationCode);

        return new GetSummarizedProjectByIdResponse(project.Id, project.ProjectName, project.ProjectCode, project.ProjectCostCenters.FirstOrDefault()?.CostCenter.Adapt<ProjectCostCenterModel>(),
            projectManager, planningAssistant, userImplementationsData, userTechnicalsData, project.Contractual, project.CompanyId, company?.NameFa);
    }
    //دریافت تمامی شناسه های کاربران برای متادیتا لیستی از پروژه ها
    private List<long> IdCollectors(List<Project> projects)
    {
        var dataResult = new List<long>();
        if (projects is not null)
            if (projects!.Count > 0)
            {
                dataResult.AddRange(projects!.NullListed(x => x.EmployerId));
                dataResult.AddRange(projects!.Where(x => x.SupervisorEngineer is not null && x.SupervisorEngineer > 0).Select(x => (long)x.SupervisorEngineer!).ToList());
                dataResult.AddRange(projects!.Where(x => x.Advisor is not null && x.Advisor > 0).Select(x => (long)x.Advisor!).ToList());
                dataResult.AddRange(projects!.Where(x => x.ProjectManager > 0).Select(x => (long)x.ProjectManager!).ToList());
                dataResult.AddRange(projects!.Where(x => x.PlanningAssistant is not null && x.PlanningAssistant > 0).Select(x => (long)x.PlanningAssistant!).ToList());
            }

        return dataResult.Where(x => x != 0).Distinct().ToList();
    }
    private List<long> IdCollectors(List<GetProjectsModel> projects)
    {
        var dataResult = new List<long>();
        if (projects is not null && projects.Count > 0)
        {
            dataResult.AddRange(projects!.Where(x => x.EmployerId is not null).NullListed(x => x.EmployerId));
            dataResult.AddRange(projects!.Where(x => x.SupervisorEngineer is not null && x.SupervisorEngineer > 0).Select(x => (long)x.SupervisorEngineer!).ToList());
            dataResult.AddRange(projects!.Where(x => x.AdvisorId is not null && x.AdvisorId > 0).Select(x => (long)x.AdvisorId!).ToList());
            dataResult.AddRange(projects!.Where(x => x.ProjectManagerId is not null && x.ProjectManagerId > 0).Select(x => (long)x.ProjectManagerId!).ToList());
            dataResult.AddRange(projects!.Where(x => x.PlanningAssistantId is not null && x.PlanningAssistantId > 0).Select(x => (long)x.PlanningAssistantId!).ToList());
        }

        return dataResult.Where(x => x != 0).Distinct().ToList();
    }
    // سازنده لیستی از دیتای خروجی پروژه ها به همراه اطلاعات کابران
    private async Task<List<GetProjectsModel>> ProjectsDataCollector(List<Project> projects, List<GetUserInfo?>? usersInfos, List<Company>? companies, List<ViewCity>? cities)
    {
        var dataResult = new List<GetProjectsModel>();

        foreach (var project in projects)
        {
            var employerInfo = usersInfos?.Where(x => x?.Id == project.EmployerId).FirstOrDefault();
            var employer = new ProjectUserModel(project.EmployerId, employerInfo?.UserId, employerInfo?.FullName, employerInfo?.AvatarUrl, employerInfo?.OrganizationCode);

            var supervisorInfo = usersInfos?.Where(x => x?.Id == project.SupervisorEngineer).FirstOrDefault();
            var supervisor = new ProjectUserModel(project.SupervisorEngineer, supervisorInfo?.UserId, supervisorInfo?.FullName, supervisorInfo?.AvatarUrl, supervisorInfo?.OrganizationCode);

            var advisorInfo = usersInfos?.Where(x => x?.Id == project.Advisor).FirstOrDefault();
            var advisor = new ProjectUserModel(project.Advisor, advisorInfo?.UserId, advisorInfo?.FullName, advisorInfo?.AvatarUrl, advisorInfo?.OrganizationCode);

            var projectManagerInfo = usersInfos?.Where(x => x?.Id == project.ProjectManager).FirstOrDefault();
            var projectManager = new ProjectUserModel(project.ProjectManager, projectManagerInfo?.UserId, projectManagerInfo?.FullName, projectManagerInfo?.AvatarUrl, projectManagerInfo?.OrganizationCode);

            var planningAssistantInfo = usersInfos?.Where(x => x?.Id == project.PlanningAssistant).FirstOrDefault();
            var planningAssistant = new ProjectUserModel(project.PlanningAssistant, planningAssistantInfo?.UserId, planningAssistantInfo?.FullName, planningAssistantInfo?.AvatarUrl, planningAssistantInfo?.OrganizationCode);

            var city = cities?.FirstOrDefault(x => x.Id == project.CityId);

            var company = companies?.Where(x => x.Id == project.CompanyId).FirstOrDefault();

            var result = new GetProjectsModel
            {
                Id = project.Id,
                ProjectTypeId = project.ProjectType?.Id,
                ProjectTypeTitle = project.ProjectType?.ProjectTypeTitle,
                ProjectName = project.ProjectName,
                ProjectEnName = project.ProjectEnName,
                ProjectCode = project.ProjectCode,
                EmployerId = project.EmployerId,
                EmployerName = usersInfos?.Where(x => x?.Id == project.EmployerId).Select(x => x?.FullName).FirstOrDefault() ?? "",
                CostCenterId = project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                CostCenterName = project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                CostCenterIds = project.ProjectCostCenters.Select(cc => cc.CostCenterId).ToList(),
                CostCenterNames = project.ProjectCostCenters.Select(cc => cc.CostCenter?.CostCenterName).JoinListComma(),
                SupervisorEngineer = project.SupervisorEngineer,
                SupervisorEngineerName = usersInfos?.Where(x => x?.Id == project.SupervisorEngineer).Select(x => x?.FullName).FirstOrDefault() ?? "",
                AdvisorId = project.Advisor,
                AdvisorName = usersInfos?.Where(x => x?.Id == project.Advisor).Select(x => x?.FullName).FirstOrDefault() ?? "",
                ProjectManagerId = project.ProjectManager,
                ProjectManagerName = usersInfos?.Where(x => x?.Id == project.ProjectManager).Select(x => x?.FullName).FirstOrDefault() ?? "",
                PlanningAssistantId = project.PlanningAssistant,
                PlanningAssistantName = usersInfos?.Where(x => x?.Id == project.PlanningAssistant).Select(x => x?.FullName).FirstOrDefault() ?? "",
                Status = project.Status,
                Contractual = project.Contractual,
                CollectiveService = project.CollectiveService,
                IsActive = project.IsActive,
                ApprovedBudget = project.ApprovedBudget,
                CompanyId = project.CompanyId,
                CompanyName = company?.NameFa,
                CityId = project.CityId,
                CityName = city?.Name,
                Description = project.Description,
                DescriptionEn = project.DescriptionEn,
                Created = project.Created,
                CreatorId = project.CreatorId,
                HasProduct = project.HasProduct,
                IsOrganizationUnit = project.IsOrganizationUnit,
                OrganizationId = project.OrganizationId,
                Category = project.ProjectCategories.Select(x => new GetProjectsCategoryModel
                {
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.CategoryName,
                    CategoryCode = x.Category.CategoryCode
                }).ToList()
            };

            dataResult.Add(result);
        }

        return dataResult;
    }
    private List<GetProjectsModel> ProjectsDataCollector(List<GetProjectsModel> projects, List<GetUserInfo?>? usersInfos, List<Company>? companies, List<ViewCity>? cities)
    {
        projects.ForEach(project =>
        {
            var company = companies?.Where(x => x.Id == project.CompanyId).FirstOrDefault();

            project.EmployerName = usersInfos?.Where(x => x?.Id == project.EmployerId).Select(x => x?.FullName).FirstOrDefault() ?? "";
            project.SupervisorEngineerName = usersInfos?.Where(x => x?.Id == project.SupervisorEngineer).Select(x => x?.FullName).FirstOrDefault() ?? "";
            project.AdvisorName = usersInfos?.Where(x => x?.Id == project.AdvisorId).Select(x => x?.FullName).FirstOrDefault() ?? "";
            project.ProjectManagerName = usersInfos?.Where(x => x?.Id == project.ProjectManagerId).Select(x => x?.FullName).FirstOrDefault() ?? "";
            project.PlanningAssistantName = usersInfos?.Where(x => x?.Id == project.PlanningAssistantId).Select(x => x?.FullName).FirstOrDefault() ?? "";
            project.CityName = cities?.FirstOrDefault(x => x.Id == project.CityId)?.Name;
            project.CompanyName = company?.NameFa;

        });

        return projects;
    }
    //دریافت تمامی شناسه های کاربران برای متادیتا لیستی از پروژه ها در سرویس GetsContractedProject
    private List<long> IdsCollector(List<Project> projects)
    {
        var dataResult = new List<long>();
        if (projects is not null)
            if (projects!.Count > 0)
            {
                dataResult.AddRange(projects!.NullListed(x => x.EmployerId));
                dataResult.AddRange(projects!.Where(x => x.ProjectManager > 0).Select(x => (long)x.ProjectManager!).ToList());
                dataResult.AddRange(projects!.Where(x => x.PlanningAssistant is not null && x.PlanningAssistant > 0).Select(x => (long)x.PlanningAssistant!).ToList());

                foreach (var item in projects)
                {
                    dataResult.AddRange(item.ProjectTechnicalAssistants.Select(x => x.TechnicalAssistantUserId).ToList());
                    dataResult.AddRange(item.ProjectImplementationAssistants.Select(x => x.ImplementationAssistantUserId).ToList());
                }
            }

        return dataResult.Where(x => x != 0).ToList();
    }
    //سازنده لیستی از دیتای خروجی پروژه ها به همراه اطلاعات کابران GetsContractedProject
    private List<GetsContractedProjectResponseModel> ProjectDataCollectorForGetContractedProject(
        List<Project> projects,
        List<GetUserInfo?>? metaInfos,
        List<Company>? companies)
    {
        var dataResult = new List<GetsContractedProjectResponseModel>();
        foreach (var project in projects)
        {
            var employerInfo = metaInfos?.Where(x => x?.Id == project.EmployerId).FirstOrDefault();
            var employer = new ProjectUserModel(project.EmployerId, employerInfo?.UserId, employerInfo?.FullName, employerInfo?.AvatarUrl, employerInfo?.OrganizationCode);

            var projectManagerInfo = metaInfos?.Where(x => x?.Id == project.ProjectManager).FirstOrDefault();
            var projectManager = new ProjectUserModel(project.ProjectManager == 0 ? null : project.ProjectManager, projectManagerInfo?.UserId, projectManagerInfo?.FullName, projectManagerInfo?.AvatarUrl, projectManagerInfo?.OrganizationCode);

            var planningAssistantInfo = metaInfos?.Where(x => x?.Id == project.PlanningAssistant).FirstOrDefault();
            var planningAssistant = new ProjectUserModel(project.PlanningAssistant == 0 ? null : project.PlanningAssistant, planningAssistantInfo?.UserId, planningAssistantInfo?.FullName, planningAssistantInfo?.AvatarUrl, planningAssistantInfo?.OrganizationCode);

            var company = companies?.Where(x => x.Id == project.CompanyId).FirstOrDefault();

            var tecnicalAssistants = new List<UserProjectTechnicalAssistantModel>();
            if (project.ProjectTechnicalAssistants.Count > 0)
                foreach (var tecnical in project.ProjectTechnicalAssistants)
                {
                    var tecnicalAssistantInfo = metaInfos?.Where(x => x?.Id == tecnical.TechnicalAssistantUserId).FirstOrDefault();
                    var tecnicalAssistant = new UserProjectTechnicalAssistantModel(tecnical.TechnicalAssistantUserId, tecnicalAssistantInfo?.FullName);
                    tecnicalAssistants.Add(tecnicalAssistant);
                }

            var ImplementationAssistants = new List<UserProjectImplementationAssistantModel>();
            if (project.ProjectImplementationAssistants.Count > 0)
                foreach (var Implementation in project.ProjectImplementationAssistants)
                {
                    var ImplementationAssistantInfo = metaInfos?.Where(x => x?.Id == Implementation.ImplementationAssistantUserId).FirstOrDefault();
                    var ImplementationAssistant = new UserProjectImplementationAssistantModel(Implementation.ImplementationAssistantUserId, ImplementationAssistantInfo?.FullName);
                    ImplementationAssistants.Add(ImplementationAssistant);
                }

            List<ProjectCategoryModel>? Category = [];
            if (project.ProjectCategories.HasItems())
                foreach (var item in project.ProjectCategories)
                    Category.Add(new ProjectCategoryModel(item.CategoryId, item.Category.CategoryName, item.Category.CategoryCode));

            var warehouseIds = new List<long>();
            if (project.ProjectCostCenters.Any() && project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterWarehouses.Any())
                foreach (var item in project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterWarehouses)
                    warehouseIds.Add(item.WarehouseId);

            var result = new GetsContractedProjectResponseModel(
                project.Id,
                project.ProjectName,
                project.ProjectCode,
                Category is null ? null : Category.Select(x => x.Id.ToString()).JoinList(),
                Category is null ? null : Category.Select(x => x.CategoryName.ToString()).JoinList(),
                project.ProjectCostCenters.FirstOrDefault()?.CostCenterId,
                project.ProjectCostCenters.FirstOrDefault()?.CostCenter.CostCenterName,
                projectManager.Id,
                projectManager.FullName,
                employer.Id,
                employer.FullName,
                planningAssistant.Id,
                planningAssistant.FullName,
                tecnicalAssistants!,
                ImplementationAssistants!,
                project.Status.GetEnumDescription(),
                project.Contractual,
                project.CollectiveService,
                project.HasProduct,
                project.IsActive,
                project.CompanyId,
                company?.NameFa,
                Category,
                warehouseIds);

            dataResult.Add(result);
        }

        return dataResult;
    }

    private async Task<bool> CheckCostCenterDependencies(long projectId, long costCenterId, CT ct)
    {
        //TODO:
        // 1. Check Request Goods Supplies (Using already injected _requestGoodsSupplyRepo)
        // Adjust the lambda expression based on your actual RequestGoodsSupply entity properties
        /*
        var hasGoodsSupplies = await _requestGoodsSupplyRepo.AnyAsync(
            x => x.ProjectId == projectId && x.CostCenterId == costCenterId && !x.IsDeleted, ct);
        if (hasGoodsSupplies) return true;
        */

        // 2. Check Project Operations / Contracts
        // If you don't have these repositories injected into ProjectLogic, you have two choices:
        // Choice A: Inject them into the ProjectLogic constructor just like _requestGoodsSupplyRepo
        // Choice B: Use _mediator to send a Query that checks the database in their respective logic classes

        /*
        var hasContracts = await _mediator.Send(new CheckContractDependencyQuery(projectId, costCenterId), ct);
        if (hasContracts) return true;

        var hasOperations = await _mediator.Send(new CheckOperationDependencyQuery(projectId, costCenterId), ct);
        if (hasOperations) return true;
        */

        // For now, returns false so your code compiles and you can test the deletion flow.
        // Uncomment and adjust the checks above based on your actual database tables.
        await Task.CompletedTask;
        return false;
    }


    // متدی برای جنریت یونیک پراجکت کد
    private async Task<Result<string>> GenerateUniqueProjectCode(
        string employerCode,
        string? costCenterCode,
        long? companyId, CT ct)
    {
        var last = await _mediator.Send(new GetLastProjectCodeQuery(employerCode, costCenterCode), ct);
        if (last.IsFailure)
            return Result.Failure<string>(last.Error!)!;

        var next = last.Value;

        for (var attempt = 0; attempt < 5; attempt++, next++)
        {
            var created = await _mediator.Send(
                new CreateProjectCodeCommand(employerCode, costCenterCode, next), ct);
            if (created.IsFailure)
                return Result.Failure<string>(created.Error!)!;

            // costCenterId = null -> checks the whole company, matching the unique index
            var exists = await _mediator.Send(
                new GetProjectByCodeQuery(created.Value!, null, companyId), ct);

            if (!(exists is { IsSuccess: true, Value: not null }))
                return created.Value!;
        }

        return Result.Failure<string>(ProjectErrors.CodeIsDuplicate)!;
    }
}
