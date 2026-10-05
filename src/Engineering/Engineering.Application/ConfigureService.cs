using Engineering.Application.Configs;
using Engineering.Application.ContractorServices;
using Engineering.Application.Extensions.BackgroundTask;
using Engineering.Application.RequestGoodsSupplyDetailManagements;
using Engineering.Application.Services.Actions;
using Engineering.Application.Services.Adjustments;
using Engineering.Application.Services.Advertisements;
using Engineering.Application.Services.BillOfLadings;
using Engineering.Application.Services.Branchs;
using Engineering.Application.Services.CabinTypes;
using Engineering.Application.Services.Categories;
using Engineering.Application.Services.ConsumableVolumes;
using Engineering.Application.Services.ConsumptionStandards;
using Engineering.Application.Services.ContractorContracts;
using Engineering.Application.Services.ContractorMachineries;
using Engineering.Application.Services.ContractorServices;
using Engineering.Application.Services.ContractorStatusStatements;
using Engineering.Application.Services.Contracts;
using Engineering.Application.Services.CostCenterAuthorizedRoles;
using Engineering.Application.Services.CostCenterAuthorizedUsers;
using Engineering.Application.Services.CostCenterInformedUsers;
using Engineering.Application.Services.CostCenters;
using Engineering.Application.Services.CostCenterTypes;
using Engineering.Application.Services.CostCenterVirtualGroups;
using Engineering.Application.Services.CostCenterWarehouses;
using Engineering.Application.Services.CostOvers;
using Engineering.Application.Services.DailyProjectOperations;
using Engineering.Application.Services.Dashboard;
using Engineering.Application.Services.EmployerContracts;
using Engineering.Application.Services.EmployerEmployees;
using Engineering.Application.Services.EmployerStatusStatements;
using Engineering.Application.Services.EngineeringConfigs;
using Engineering.Application.Services.EngineeringDocs;
using Engineering.Application.Services.FiduciaryProductManages;
using Engineering.Application.Services.FiduciaryProducts;
using Engineering.Application.Services.FixAssetMachineries;
using Engineering.Application.Services.GoodsManagerAssignments;
using Engineering.Application.Services.Machineries;
using Engineering.Application.Services.MachineriesGroups;
using Engineering.Application.Services.MachineryReservations;
using Engineering.Application.Services.MachineTypes;
using Engineering.Application.Services.MessengerChannelHistories;
using Engineering.Application.Services.Messengers;
using Engineering.Application.Services.OperationInfoDependencies;
using Engineering.Application.Services.OperationInfoGroupRelations;
using Engineering.Application.Services.OperationInfoGroups;
using Engineering.Application.Services.OperationInfos;
using Engineering.Application.Services.OperationInfoSeasons;
using Engineering.Application.Services.OperationInfoServices;
using Engineering.Application.Services.OperationLocations;
using Engineering.Application.Services.ProcesVerbal;
using Engineering.Application.Services.ProjectAssistants;
using Engineering.Application.Services.ProjectCostCenterRequests;
using Engineering.Application.Services.ProjectOperationDependencies;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts;
using Engineering.Application.Services.ProjectOperationDetailDeductions;
using Engineering.Application.Services.ProjectOperationDetailInspections;
using Engineering.Application.Services.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailSchedulings;
using Engineering.Application.Services.ProjectOperations;
using Engineering.Application.Services.ProjectOperationTemporaryDailies;
using Engineering.Application.Services.ProjectOperationWbses;
using Engineering.Application.Services.ProjectRisks;
using Engineering.Application.Services.SubProjects;
using Engineering.Application.Services.Projects;
using Engineering.Application.Services.ProjectServices;
using Engineering.Application.Services.ProjectSessionRecords;
using Engineering.Application.Services.ProjectTypes;
using Engineering.Application.Services.ProjectWarehouses;
using Engineering.Application.Services.ProjectWbses;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Application.Services.PublicGroups;
using Engineering.Application.Services.RequestContractorInquiries;
using Engineering.Application.Services.RequestContractors;
using Engineering.Application.Services.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails;
using Engineering.Application.Services.RequestMachineries;
using Engineering.Application.Services.RequestMachineryBills;
using Engineering.Application.Services.RequestMachineryManagements;
using Engineering.Application.Services.RequestMachineryStatusStatements;
using Engineering.Application.Services.RequestRewards;
using Engineering.Application.Services.Seasons;
using Engineering.Application.Services.ServiceInfos;
using Engineering.Application.Services.SessionRecords;
using Engineering.Application.Services.ShippingCosts;
using Engineering.Application.Services.Tasks;
using Engineering.Application.Services.TelegramChats;
using Engineering.Application.Services.TelegramMessageHistorys;
using Engineering.Application.Services.TransportationContractorMachines;
using Engineering.Application.Services.TransportationContractorPersonnels;
using Engineering.Application.Services.TransportationContractors;
using Engineering.Application.Services.TransportationRequests;
using Engineering.Application.Services.Transportations;
using Engineering.Application.Services.Trips;
using Engineering.Application.Services.WbsTemplates;
using Engineering.Application.Services.WorkflowRequests;
using Gita.Backend.Shared.Application;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Engineering.Application;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        Assembly[] assemblies,
        IConfiguration configuration)
    {
        services.AddSharedApplicationServices(
            assemblies,
            configuration,
            false
        );

        var configs = configuration.RegisterAndGetConfiguration<MessageSenderConfig>(services, "MessageSenderConfig");

        AddServiceScopes(services);
        return services;
    }

    private static void AddServiceScopes(IServiceCollection services)
    {
        services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        services.AddHostedService<BackgroundWorkerService>();

        services.AddScoped<ProjectWbstImporter>();

        services.AddScoped<ICategoryLogic, CategoryLogic>();
        services.AddScoped<IBranchLogic, BranchLogic>();
        services.AddScoped<IOperationLocationLogic, OperationLocationLogic>();
        services.AddScoped<ISeasonLogic, SeasonLogic>();
        services.AddScoped<IOperationInfoLogic, OperationInfoLogic>();
        services.AddScoped<IConsumptionStandardLogic, ConsumptionStandardLogic>();
        services.AddScoped<ICostCenterLogic, CostCenterLogic>();
        services.AddScoped<ICostCenterVirtualGroupLogic, CostCenterVirtualGroupLogic>();
        services.AddScoped<ICostCenterWarehouseLogic, CostCenterWarehouseLogic>();
        services.AddScoped<IProjectWarehouseLogic, ProjectWarehouseLogic>();
        services.AddScoped<IInformedUserLogic, InformedUserLogic>();
        services.AddScoped<IAuthorizedRoleLogic, AuthorizedRoleLogic>();
        services.AddScoped<IAuthorizedUserLogic, AuthorizedUserLogic>();
        services.AddScoped<IEContractHeaderLogic, EContractHeaderLogic>();
        services.AddScoped<IProjectOperationLogic, ProjectOperationLogic>();
        services.AddScoped<IProjectOperationTemporaryDailyLogic, ProjectOperationTemporaryDailyLogic>();
        services.AddScoped<IProjectLogic, ProjectLogic>();
        services.AddScoped<IProjectTypeLogic, ProjectTypeLogic>();
        services.AddScoped<IProjectServiceLogic, ProjectServiceLogic>();
        services.AddScoped<IProjectCostCenterRequestLogic, ProjectCostCenterRequestLogic>();
        services.AddScoped<ICostCenterTypeLogic, CostCenterTypeLogic>();
        services.AddScoped<IProjectAssistantsLogic, ProjectAssistantsLogic>();
        services.AddScoped<IMachineriesGroupLogic, MachineriesGroupLogic>();
        services.AddScoped<IMachineryLogic, MachineryLogic>();
        services.AddScoped<IContractorMachineryLogic, ContractorMachineryLogic>();
        services.AddScoped<IFixAssetMachineryLogic, FixAssetMachineryLogic>();
        services.AddScoped<IMachineryReservationLogic, MachineryReservationLogic>();
        services.AddScoped<ICostOverLogic, CostOverLogic>();
        services.AddScoped<WorkflowIntegrationLogic>();
        services.AddScoped<WorkflowOutboxOperations>();
        services.AddScoped<IWorkflowEntityHandler, CostOverWorkflowHandler>();
        services.AddScoped<IServiceInfoLogic, ServiceInfoLogic>();
        services.AddScoped<IOperationInfoDependencyLogic, OperationInfoDependencyLogic>();
        services.AddScoped<IOperationInfoServiceLogic, OperationInfoServiceLogic>();
        services.AddScoped<IOperationInfoSeasonLogic, OperationInfoSeasonLogic>();
        services.AddScoped<IOperationInfoGroupLogic, OperationInfoGroupLogic>();
        services.AddScoped<IOperationInfoGroupRelationLogic, OperationInfoGroupRelationLogic>();
        services.AddScoped<IProjectOperationDependencyLogic, ProjectOperationDependencyLogic>();
        services.AddScoped<IProjectOperationDetailLogic, ProjectOperationDetailLogic>();
        services.AddScoped<IProjectOperationDetailInspectionLogic, ProjectOperationDetailInspectionLogic>();
        services.AddScoped<IProjectOperationDetailDeductionLogic, ProjectOperationDetailDeductionLogic>();
        services.AddScoped<IProjectOperationDetailSchedulingLogic, ProjectOperationDetailSchedulingLogic>();
        services.AddScoped<IProjectOperationDetailContractorServicesLogic, ProjectOperationDetailContractorServicesLogic>();
        services.AddScoped<IEmployerStatusStatementLogic, EmployerStatusStatementLogic>();


        services.AddScoped<IProjectOperationWbsLogic, ProjectOperationWbsLogic>();

        services.AddScoped<IMessengerChannelHistoryLogic, MessengerChannelHistoryLogic>();
        services.AddScoped<IMessengerLogic, MessengerLogic>();

        services.AddScoped<IContractorLogic, ContractorLogic>();

        services.AddScoped<ITransportationLogic, TransportationLogic>();
        services.AddScoped<ITransportationRequestLogic, TransportationRequestLogic>();
        services.AddScoped<ITransportationContractorLogic, TransportationContractorLogic>();
        services.AddScoped<ITransportationContractorPersonnelLogic, TransportationContractorPersonnelLogic>();
        services.AddScoped<ITransportationContractorMachineLogic, TransportationContractorMachineLogic>();
        services.AddScoped<IShippingCostLogic, ShippingCostLogic>();

        services.AddScoped<ITripLogic, TripLogic>();

        services.AddScoped<IMachineTypeLogic, MachineTypeLogic>();
        services.AddScoped<ICabinTypeLogic, CabinTypeLogic>();
        services.AddScoped<IWbsTemplateLogic, WbsTemplateLogic>();
        services.AddScoped<IProjectWbsLogic, ProjectWbsLogic>();

        services.AddScoped<IProjectRiskLogic, ProjectRiskLogic>();
        services.AddScoped<ISubProjectLogic, SubProjectLogic>();

        services.AddScoped<IBillOfLadingLogic, BillOfLadingLogic>();
        services.AddScoped<ITelegramChatLogic, TelegramChatLogic>();
        services.AddScoped<ITelegramMessageHistoryLogic, TelegramMessageHistoryLogic>();

        services.AddScoped<IRequestContractorLogic, RequestContractorLogic>();
        services.AddScoped<IEngineeringConfigLogic, EngineeringConfigLogic>();
        services.AddScoped<IRequestContractorInquiryLogic, RequestContractorInquiryLogic>();

        #region ContractorContracts

        services.AddScoped<IContractorContractLogic, ContractorContractLogic>();

        services.AddScoped<IContractorStatusStatementLogic, ContractorStatusStatementLogic>();

        #endregion

        services.AddScoped<IConsumableVolumeLogic, ConsumableVolumeLogic>();
        services.AddScoped<IDailyProjectOperationLogic, DailyProjectOperationLogic>();

        #region RequestRewards

        services.AddScoped<IRequestRewardLogic, RequestRewardLogic>();

        #endregion

        #region FiduciaryProducts

        services.AddScoped<IFiduciaryProductLogic, FiduciaryProductLogic>();
        services.AddScoped<IFiduciaryProductManageLogic, FiduciaryProductManageLogic>();

        #endregion

        #region  RequestGoodsSupplies

        services.AddScoped<IRequestGoodsSupplyLogic, RequestGoodsSupplyLogic>();
        services.AddScoped<IRequestGoodsSupplyDetailLogic, RequestGoodsSupplyDetailLogic>();
        services.AddScoped<IRequestGoodsSupplyManagementLogic, RequestGoodsSupplyManagementLogic>();

        #endregion

        #region RequestMachineries

        services.AddScoped<IRequestMachineryLogic, RequestMachineryLogic>();
        services.AddScoped<IRequestMachineryBillLogic, RequestMachineryBillLogic>();
        services.AddScoped<IRequestMachineryManagementLogic, RequestMachineryManagementLogic>();
        services.AddScoped<IRequestMachineryStatusStatementLogic, RequestMachineryStatusStatementLogic>();

        #endregion

        #region PublicGroups

        services.AddScoped<IPublicGroupLogic, PublicGroupLogic>();

        #endregion

        #region Actions

        services.AddScoped<IActionLogic, ActionLogic>();

        #endregion

        services.AddScoped<IEmployerEmployeeLogic, EmployerEmployeeLogic>();

        services.AddScoped<IDashboardLogic, DashboardLogic>();

        services.AddScoped<IProjectOperationDetailContractorExpertLogic, ProjectOperationDetailContractorExpertLogic>();

        services.AddScoped<IAdvertisementLogic, AdvertisementLogic>();
        services.AddScoped<IGoodsManagerAssignmentLogic, GoodsManagerAssignmentLogic>();
        services.AddScoped<IContractLogic, ContractLogic>();

        services.AddScoped<IEngineeringDocLogic, EngineeringDocLogic>();

        services.AddScoped<IProcesVerbalLogic, ProcesVerbalLogic>();
        services.AddScoped<ISessionRecordLogic, SessionRecordLogic>();

        // tasks
        services.AddScoped<ITaskLogic, TaskLogic>();
        
        services.AddScoped<IAdjustmentLogic, AdjustmentLogic>();
    }
}
