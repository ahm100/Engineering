using Engineering.Application.Abstractions.Data.Actions;
using Engineering.Application.Abstractions.Data.Adjustments;
using Engineering.Application.Abstractions.Data.Advertisements;
using Engineering.Application.Abstractions.Data.BillOfLadings;
using Engineering.Application.Abstractions.Data.Branchs;
using Engineering.Application.Abstractions.Data.Categories;
using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Abstractions.Data.ContractorMachineries;
using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Application.Abstractions.Data.EmployerEmployees;
using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Application.Abstractions.Data.EngineeringDocs;
using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using Engineering.Application.Abstractions.Data.Machineries;
using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Abstractions.Data.Messengers;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using Engineering.Application.Abstractions.Data.OperationLocations;
using Engineering.Application.Abstractions.Data.ProcesVerbals;
using Engineering.Application.Abstractions.Data.ProjectCostCenterRequests;
using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Abstractions.Data.ProjectOperationDetails.ConsumableVolume;
using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Documents;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Histories;
using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;
using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Application.Abstractions.Data.ReviewReports;
using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Abstractions.Data.ServiceInfos;
using Engineering.Application.Abstractions.Data.SessionRecords;
using Engineering.Application.Abstractions.Data.Synonyms.FinanicalPeriod;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Currencies;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Organizations;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Categories;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Groups;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Packages;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.WarehouseAssets;
using Engineering.Application.Abstractions.Data.Tasks;
using Engineering.Application.Abstractions.Data.TelegramChats;
using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Abstractions.Data.Trips;
using Engineering.Application.Abstractions.Data.WbsTemplates;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationById;
using Engineering.Application.Services.ProjectWbses.Scheduling;
using Engineering.Persistence.Contract;
using Engineering.Persistence.Repositories;
using Engineering.Persistence.Repositories.Actions;
using Engineering.Persistence.Repositories.Adjustments;
using Engineering.Persistence.Repositories.Advertisements;
using Engineering.Persistence.Repositories.BillOfLadings;
using Engineering.Persistence.Repositories.Branchs;
using Engineering.Persistence.Repositories.Categories;
using Engineering.Persistence.Repositories.ContractorContractHeaders;
using Engineering.Persistence.Repositories.ContractorContracts;
using Engineering.Persistence.Repositories.ContractorEmployees;
using Engineering.Persistence.Repositories.ContractorMachineries;
using Engineering.Persistence.Repositories.ContractorStatusStatements;
using Engineering.Persistence.Repositories.Contracts;
using Engineering.Persistence.Repositories.CostCenters;
using Engineering.Persistence.Repositories.CostOvers;
using Engineering.Persistence.Repositories.DailyProjectOperations;
using Engineering.Persistence.Repositories.EmployerContracts;
using Engineering.Persistence.Repositories.EmployerEmployees;
using Engineering.Persistence.Repositories.EmployerStatusStatements;
using Engineering.Persistence.Repositories.EngineeringConfigs;
using Engineering.Persistence.Repositories.EngineeringDocs;
using Engineering.Persistence.Repositories.FiduciaryProducts;
using Engineering.Persistence.Repositories.FixAssetMachineries;
using Engineering.Persistence.Repositories.Machineries;
using Engineering.Persistence.Repositories.MachineTypes;
using Engineering.Persistence.Repositories.Messengers;
using Engineering.Persistence.Repositories.OperationInfos;
using Engineering.Persistence.Repositories.OperationInfos.ConsumptionStandards;
using Engineering.Persistence.Repositories.OperationLocations;
using Engineering.Persistence.Repositories.ProcesVerbals;
using Engineering.Persistence.Repositories.ProjectOperationDetails;
using Engineering.Persistence.Repositories.ProjectOperationDetails.ConsumableVolume;
using Engineering.Persistence.Repositories.ProjectOperationDetails.Users;
using Engineering.Persistence.Repositories.ProjectOperations;
using Engineering.Persistence.Repositories.Projects;
using Engineering.Persistence.Repositories.Projects.ProjectCalendars;
using Engineering.Persistence.Repositories.Projects.WBS;
using Engineering.Persistence.Repositories.RequestContractors;
using Engineering.Persistence.Repositories.RequestGoodsSupplies;
using Engineering.Persistence.Repositories.RequestGoodsSupplies.Documents;
using Engineering.Persistence.Repositories.RequestGoodsSupplies.Histories;
using Engineering.Persistence.Repositories.RequestMachineries;
using Engineering.Persistence.Repositories.RequestMachineryStatusStatementDetails;
using Engineering.Persistence.Repositories.RequestMachineryStatusStatements;
using Engineering.Persistence.Repositories.RequestRewards;
using Engineering.Persistence.Repositories.ReviewReports;
using Engineering.Persistence.Repositories.Seasons;
using Engineering.Persistence.Repositories.ServiceInfos;
using Engineering.Persistence.Repositories.SessionRecords;
using Engineering.Persistence.Repositories.ShippingCosts;
using Engineering.Persistence.Repositories.Synonyms.FinancialPeriod;
using Engineering.Persistence.Repositories.Synonyms.Meta.Currencies;
using Engineering.Persistence.Repositories.Synonyms.Meta.Organizations;
using Engineering.Persistence.Repositories.Synonyms.MetaEntities;
using Engineering.Persistence.Repositories.Synonyms.Warehouse.Categories;
using Engineering.Persistence.Repositories.Synonyms.Warehouse.Groups;
using Engineering.Persistence.Repositories.Synonyms.Warehouse.WarehouseAssets;
using Engineering.Persistence.Repositories.Tasks;
using Engineering.Persistence.Repositories.TelegramChats;
using Engineering.Persistence.Repositories.TransportationContractorMachines;
using Engineering.Persistence.Repositories.TransportationContractorPersonnels;
using Engineering.Persistence.Repositories.TransportationContractors;
using Engineering.Persistence.Repositories.Transportations;
using Engineering.Persistence.Repositories.Trips;
using Engineering.Persistence.Repositories.WbsTemplates;
using Gita.Backend.Shared.Application.Abstractions.Data;
using Gita.Backend.Shared.Persistence.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuditableEntityInterceptor = Engineering.Persistence.Interceptors.AuditableEntityInterceptor;

namespace Engineering.Persistence;

public static class ConfigureService
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        Gita.Backend.Shared.Persistence.ConfigureServices.AddSharedPersistenceService(services, configuration);
        services.AddScoped<AuditableEntityInterceptor>();
        AddDbContext(services, configuration);
        AddServiceScopes(services);
        services.AddScoped<Engineering.Application.Abstractions.Data.WorkflowRequests.IWorkflowIntegrationRepository,
            Engineering.Persistence.Repositories.WorkflowRequests.WorkflowIntegrationRepository>();

        return services;
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddScoped<DbAuditLogInterceptor>();

        services.AddDbContext<EngineeringDBContext>((provider, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EngineeringDB")!,
                builder => builder
                    .MigrationsAssembly(typeof(EngineeringDBContext).Assembly.FullName)
            );
            options.AddInterceptors(provider.GetRequiredService<AuditableEntityInterceptor>(),
                                    provider.GetRequiredService<DbAuditLogInterceptor>());
        });
    }

    private static void AddServiceScopes(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #region Actions
        services.AddScoped<IActionRepository, ActionRepository>();
        #endregion

        #region BillOfLadings
        services.AddScoped<IBillOfLadingRepository, BillOfLadingRepository>();
        #endregion

        #region Branchs
        services.AddScoped<IBranchRepository, BranchRepository>();
        #endregion

        #region Categories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        #endregion

        #region ContractorContracts
        services.AddScoped<IContractorContractHeaderRepository, ContractorContractHeaderRepository>();

        services.AddScoped<IContractorContractRepository, ContractorContractRepository>();

        services.AddScoped<IContractorContractDetailRepository, ContractorContractDetailRepository>();

        services.AddScoped<IContractorContractDetailPriceRepository, ContractorContractDetailPriceRepository>();

        services.AddScoped<IContractorContractHistoryRepository, ContractorContractHistoryRepository>();

        services.AddScoped<IContractorContractHeaderHistoryRepository, ContractorContractHeaderHistoryRepository>();

        services.AddScoped<IContractorContractDetailServiceRepository, ContractorContractDetailServiceRepository>();

        services.AddScoped<IContractorContractDetailCostOverRepository, ContractorContractDetailCostOverRepository>();

        services.AddScoped<IContractorContractDetailPriceHistoryRepository, ContractorContractDetailPriceHistoryRepository>();

        services.AddScoped<IContractorContractHeaderVersionRepository, ContractorContractHeaderVersionRepository>();

        services.AddScoped<IContractorContractHeaderVersionRepository, ContractorContractHeaderVersionRepository>();

        #endregion

        #region ContractorEmployees
        services.AddScoped<IContractorEmployeeRepository, ContractorEmployeeRepository>();
        #endregion

        #region ContractorStatusStatements

        services.AddScoped<IContractorStatusStatementRepository, ContractorStatusStatementRepository>();

        services.AddScoped<IContractorStatusStatementDetailRepository, ContractorStatusStatementDetailRepository>();

        services.AddScoped<IContractorStatusStatementFineRepository, ContractorStatusStatementFineRepository>();

        services.AddScoped<IContractorStatusStatementProductRepository, ContractorStatusStatementProductRepository>();

        services.AddScoped<IContractorStatusStatementServiceRepository, ContractorStatusStatementServiceRepository>();

        services.AddScoped<IContractorStatusStatementRewardRepository, ContractorStatusStatementRewardRepository>();

        services.AddScoped<IContractorStatusStatementServiceThirdPartyRepository, ContractorStatusStatementServiceThirdPartyRepository>();

        services.AddScoped<IContractorStatusStatementServiceDailyRepository, ContractorStatusStatementServiceDailyRepository>();

        services.AddScoped<IContractorStatusStatementHistoryRepository, ContractorStatusStatementHistoryRepository>();

        services.AddScoped<IContractorStatusStatementDiscountRepository, ContractorStatusStatementDiscountRepository>();

        services.AddScoped<IContractorStatusStatementCostOverRepository, ContractorStatusStatementCostOverRepository>();

        services.AddScoped<IContractorStatusStatementPaymentRepository, ContractorStatusStatementPaymentRepository>();

        #endregion

        #region ContractorServices

        services.AddScoped<IContractorServicesRepository, ContractorServicesRepository>();

        #endregion

        #region CostCenters
        services.AddScoped<ICostCenterRepository, CostCenterRepository>();

        services.AddScoped<ICostCenterTypeRepository, CostCenterTypeRepository>();

        services.AddScoped<ICostCenterWarehouseRepository, CostCenterWarehouseRepository>();

        services.AddScoped<ICostCenterInformedUserRepository, CostCenterInformedUserRepository>();

        services.AddScoped<ICostCenterAuthorizedRoleRepository, CostCenterAuthorizedRoleRepository>();

        services.AddScoped<ICostCenterAuthorizedUserRepository, CostCenterAuthorizedUserRepository>();

        services.AddScoped<ICostCenterVirtualGroupAdminRepository, CostCenterVirtualGroupAdminRepository>();

        services.AddScoped<ICostCenterVirtualGroupRepository, CostCenterVirtualGroupRepository>();

        services.AddScoped<ICostCenterHistoryRepository, CostCenterHistoryRepository>();

        services.AddScoped<IViewPackageRepository, ViewPackageRepository>();
        #endregion

        #region CostOvers
        services.AddScoped<ICostOverRepository, CostOverRepository>();

        #endregion

        #region DailyProjectOperations
        services.AddScoped<IDailyProjectOperationDocumentRepository, DailyProjectOperationDocumentRepository>();

        services.AddScoped<IDailyProjectOperationExpertRepository, DailyProjectOperationExpertRepository>();

        services.AddScoped<IDailyProjectOperationMachineryRepository, DailyProjectOperationMachineryRepository>();

        services.AddScoped<IDailyProjectOperationProductRepository, DailyProjectOperationProductRepository>();

        services.AddScoped<IDailyProjectOperationRepository, DailyProjectOperationRepository>();

        services.AddScoped<IDailyProjectOperationHistoryRepository, DailyProjectOperationHistoryRepository>();

        services.AddScoped<IDailyProjectOperationServiceRepository, DailyProjectOperationServiceRepository>();

        services.AddScoped<IDailyProjectOperationRequestRewardRepository, DailyProjectOperationRequestRewardRepository>();
        #endregion

        #region EmployerContracts
        services.AddScoped<IEmployerContractHeadRepository, EmployerContractHeadRepository>();

        services.AddScoped<IEmployerContractRepository, EmployerContractRepository>();

        services.AddScoped<IEmployerDocRepository, EmployerDocRepository>();

        services.AddScoped<IEmployerDocUrlRepository, EmployerDocUrlRepository>();

        services.AddScoped<IEmployerConsiderationRepository, EmployerConsiderationRepository>();

        services.AddScoped<IEmployerConsiderationDepRepository, EmployerConsiderationDepRepository>();

        services.AddScoped<IEmployerCostOverRepository, EmployerCostOverRepository>();

        services.AddScoped<IEmployerCostOverImpactRepository, EmployerCostOverImpactRepository>();

        services.AddScoped<IEmployerOperationRepository, EmployerOperationRepository>();

        services.AddScoped<IEmployerOperationDetailRepository, EmployerOperationDetailRepository>();

        services.AddScoped<IEmployerContractHistoryRepository, EmployerContractHistoryRepository>();

        services.AddScoped<IEmployerOperationHistoryRepository, EmployerOperationHistoryRepository>();

        services.AddScoped<IEmployerOperationProductRepository, EmployerOperationProductRepository>();

        services.AddScoped<IEmployerOperationServiceRepository, EmployerOperationServiceRepository>();
        #endregion

        #region EmployerStatusStatements
        services.AddScoped<IEmployerStatusStatementRepository, EmployerStatusStatementRepository>();

        services.AddScoped<IEmployerStatusStatementHistoryRepository, EmployerStatusStatementHistoryRepository>();

        services.AddScoped<IEmployerStatusStatementProjectOperationRepository, EmployerStatusStatementProjectOperationRepository>();

        services.AddScoped<IEmployerStatusStatementProjectOperationDetailRepository, EmployerStatusStatementProjectOperationDetailRepository>();

        services.AddScoped<IEmployerStatusStatementProjectOperationDetailDailyRepository, EmployerStatusStatementProjectOperationDetailDailyRepository>();
        #endregion

        #region FiduciaryProducts
        services.AddScoped<IFiduciaryProductDetailRepository, FiduciaryProductDetailRepository>();

        services.AddScoped<IFiduciaryProductDetailReturnDocumentRepository, FiduciaryProductDetailReturnDocumentRepository>();

        services.AddScoped<IFiduciaryProductDetailReturnRepository, FiduciaryProductDetailReturnRepository>();

        services.AddScoped<IFiduciaryProductHistoryRepository, FiduciaryProductHistoryRepository>();

        services.AddScoped<IFiduciaryProductRepository, FiduciaryProductRepository>();

        services.AddScoped<IFiduciaryProductDetailManagementRepository, FiduciaryProductDetailManagementRepository>();

        services.AddScoped<IFiduciaryProductDetailHistoryRepository, FiduciaryProductDetailHistoryRepository>();
        #endregion

        #region Machineries
        services.AddScoped<IMachineriesGroupRepository, MachineriesGroupRepository>();

        services.AddScoped<IMachineryRepository, MachineryRepository>();
        #endregion

        #region FixAssetMachineries
        services.AddScoped<IFixAssetMachineryRepository, FixAssetMachineryRepository>();

        services.AddScoped<IFixAssetMachineryNotWorkRepository, FixAssetMachineryNotWorkRepository>();

        services.AddScoped<IMachineryReservationRepository, MachineryReservationRepository>();

        services.AddScoped<IFixAssetMachineryDocumentRepository, FixAssetMachineryDocumentRepository>();

        services.AddScoped<IFixAssetNotWorkDocumentRepository, FixAssetNotWorkDocumentRepository>();

        services.AddScoped<IFixAssetMachineryRateRepository, FixAssetMachineryRateRepository>();
        #endregion

        #region RequestContractor
        services.AddScoped<IRequestContractorRepository, RequestContractorRepository>();

        services.AddScoped<IRequestContractorHistoryRepository, RequestContractorHistoryRepository>();

        services.AddScoped<IRequestContractorInquiryRepository, RequestContractorInquiryRepository>();

        services.AddScoped<IRequestContractorInquiryDocumentRepository, RequestContractorInquiryDocumentRepository>();
        #endregion

        #region ContractorMachineries
        services.AddScoped<IContractorMachineryRepository, ContractorMachineryRepository>();
        #endregion

        #region MachineTypes

        services.AddScoped<IMachineTypeRepository, MachineTypeRepository>();

        services.AddScoped<ICabinTypeRepository, CabinTypeRepository>();

        #endregion

        #region OperationInfos
        services.AddScoped<IOperationInfoRepository, OperationInfoRepository>();

        services.AddScoped<IOperationInfoDependencyRepository, OperationInfoDependencyRepository>();

        services.AddScoped<IOperationInfoServiceRepository, OperationInfoServiceRepository>();

        services.AddScoped<IOperationInfoSeasonRepository, OperationInfoSeasonRepository>();

        services.AddScoped<IOperationInfoGroupRepository, OperationInfoGroupRepository>();

        services.AddScoped<IOperationInfoGroupRelationRepository, OperationInfoGroupRelationRepository>();

        services.AddScoped<IPublicGroupRepository, PublicGroupRepository>();

        services.AddScoped<IConsumptionStandardExpertRepository, ConsumptionStandardExpertRepository>();

        services.AddScoped<IConsumptionStandardProductRepository, ConsumptionStandardProductRepository>();

        services.AddScoped<IConsumptionStandardMachineryRepository, ConsumptionStandardMachineryRepository>();

        services.AddScoped<IOperationInfoHistoryRepository, OperationInfoHistoryRepository>();

        services.AddScoped<IOperationInfoActionRepository, OperationInfoActionRepository>();
        #endregion

        #region OperationLocations
        services.AddScoped<IOperationLocationRepository, OperationLocationRepository>();
        #endregion

        #region ProjectOperationDetails

        services.AddScoped<IProjectOperationDetailRepository, ProjectOperationDetailRepository>();

        services.AddScoped<IProjectOperationDetailHistoryRepository, ProjectOperationDetailHistoryRepository>();

        services.AddScoped<IProjectOperationDetailDeductionRepository, ProjectOperationDetailDeductionRepository>();

        services.AddScoped<IUserImplementationsRepository, UserImplementationsRepository>();

        services.AddScoped<IUserPlanersRepository, UserPlanersRepository>();

        services.AddScoped<IUserTechnicalsRepository, UserTechnicalsRepository>();

        services.AddScoped<IProjectOperationDetailContractorServiceRepository, ProjectOperationDetailContractorServiceRepository>();

        services.AddScoped<IProjectOperationDetailInspectionRepository, ProjectOperationDetailInspectionRepository>();

        services.AddScoped<IProjectOperationDetailContractorExpertRepository, ProjectOperationDetailContractorExpertRepository>();

        //ConsumableVolumes
        services.AddScoped<IConsumableVolumeExpertRepository, ConsumableVolumeExpertRepository>();

        services.AddScoped<IConsumableVolumeMachineryRepository, ConsumableVolumeMachineryRepository>();

        services.AddScoped<IConsumableVolumeProductRepository, ConsumableVolumeProductRepository>();
        #endregion

        #region ProjectOperations
        services.AddScoped<IProjectOperationRepository, ProjectOperationRepository>();

        services.AddScoped<IProjectOperationDependencyRepository, ProjectOperationDependencyRepository>();

        services.AddScoped<IProjectOperationTemporaryDailyRepository, ProjectOperationTemporaryDailyRepository>();

        services.AddScoped<IProjectOperationTemporaryDailyDocumentRepository, ProjectOperationTemporaryDailyDocumentRepository>();

        services.AddScoped<IProjectOperationHistoryRepository, ProjectOperationHistoryRepository>();

        services.AddScoped<IProjectOperationActionRepository, ProjectOperationActionRepository>();
        #endregion

        #region Projects
        services.AddScoped<IProjectRepository, ProjectRepository>();

        services.AddScoped<IProjectWarehouseRepository, ProjectWarehouseRepository>();

        services.AddScoped<IProjectHistoryRepository, ProjectHistoryRepository>();

        services.AddScoped<IProjectTechnicalAssistantRepository, ProjectTechnicalAssistantRepository>();

        services.AddScoped<IProjectImplementationAssistantRepository, ProjectImplementationAssistantRepository>();

        services.AddScoped<IProjectTypeRepository, ProjectTypeRepository>();

        services.AddScoped<IProjectServiceRepository, ProjectServiceRepository>();

        services.AddScoped<IProjectServiceDetailRepository, ProjectServiceDetailRepository>();

        services.AddScoped<IProjectProductRepository, ProjectProductRepository>();

        services.AddScoped<IProjectCostCenterRequestRepository, ProjectCostCenterRequestRepository>();
        #endregion

        #region RequestGoodsSupplies
        services.AddScoped<IRequestGoodsSupplyRepository, RequestGoodsSupplyRepository>();

        services.AddScoped<IRequestGoodsSupplyHistoryRepository, RequestGoodsSupplyHistoryRepository>();

        services.AddScoped<IRequestGoodsSupplyDetailRepository, RequestGoodsSupplyDetailRepository>();

        services.AddScoped<IRequestGoodsSupplyDetailHistoryRepository, RequestGoodsSupplyDetailHistoryRepository>();

        services.AddScoped<IRequestGoodsSupplyManagementRepository, RequestGoodsSupplyManagementRepository>();

        services.AddScoped<IRequestGoodsSupplyManagementHistoryRepository, RequestGoodsSupplyManagementHistoryRepository>();

        services.AddScoped<IRequestGoodsSupplyProductRepository, RequestGoodsSupplyProductRepository>();

        services.AddScoped<IRequestGoodsSupplyProductHistoryRepository, RequestGoodsSupplyProductHistoryRepository>();

        services.AddScoped<IRequestGoodsSupplyDetailDocumentRepository, RequestGoodsSupplyDetailDocumentRepository>();
        #endregion

        #region RequestMachineries
        services.AddScoped<IRequestMachineryRepository, RequestMachineryRepository>();

        services.AddScoped<IRequestMachineryDocumentRepository, RequestMachineryDocumentRepository>();

        services.AddScoped<IRequestMachineryBillDocumentRepository, RequestMachineryBillDocumentRepository>();

        services.AddScoped<IRequestMachineryProjectOperationRepository, RequestMachineryProjectOperationRepository>();

        services.AddScoped<IRequestMachineryProjectOperationDetailRepository, RequestMachineryProjectOperationDetailRepository>();

        services.AddScoped<IRequestMachineryInquiryRepository, RequestMachineryInquiryRepository>();

        services.AddScoped<IRequestMachineryInquiryOperatorRepository, RequestMachineryInquiryOperatorRepository>();

        services.AddScoped<IRequestMachineryHistoryRepository, RequestMachineryHistoryRepository>();

        services.AddScoped<IRequestMachineryAssignmentRepository, RequestMachineryAssignmentRepository>();

        services.AddScoped<IRequestMachineryInquiryDocumentRepository, RequestMachineryInquiryDocumentRepository>();

        services.AddScoped<IRequestMachineryStatusStatementRepository, RequestMachineryStatusStatementRepository>();

        services.AddScoped<IRequestMachineryStatusStatementDetailRepository, RequestMachineryStatusStatementDetailRepository>();

        services.AddScoped<IRequestMachineryBillRepository, RequestMachineryBillRepository>();
        #endregion

        #region RequestRewards
        services.AddScoped<IRequestRewardProductRepository, RequestRewardProductRepository>();

        services.AddScoped<IRequestRewardRepository, RequestRewardRepository>();

        services.AddScoped<IRequestRewardDocumentRepository, RequestRewardDocumentRepository>();

        services.AddScoped<IRequestRewardHistoryRepository, RequestRewardHistoryRepository>();

        services.AddScoped<IRequestRewardThirdPartyRepository, RequestRewardThirdPartyRepository>();
        #endregion

        #region Seasons
        services.AddScoped<ISeasonRepository, SeasonRepository>();
        #endregion

        #region ServiceInfos
        services.AddScoped<IServiceInfoRepository, ServiceInfoRepository>();
        #endregion

        #region Transportations
        services.AddScoped<ITransportationRepository, TransportationRepository>();

        services.AddScoped<ITransportationCargoPalletRepository, TransportationCargoPalletRepository>();

        services.AddScoped<ITransportationCargoRepository, TransportationCargoRepository>();

        services.AddScoped<ITransportationRequestRepository, TransportationRequestRepository>();

        services.AddScoped<ITransportationRequestDocumentRepository, TransportationRequestDocumentRepository>();

        services.AddScoped<ITransportationRequestCostCenterRepository, TransportationRequestCostCenterRepository>();

        services.AddScoped<ITransportationRequestProjectRepository, TransportationRequestProjectRepository>();

        services.AddScoped<ITransportationRequestProjectOperationRepository, TransportationRequestProjectOperationRepository>();

        services.AddScoped<ITransportationRequestProjectOperationDetailRepository, TransportationRequestProjectOperationDetailRepository>();

        services.AddScoped<ITransportationRequestHistoryRepository, TransportationRequestHistoryRepository>();

        services.AddScoped<ITransportationContractorRepository, TransportationContractorRepository>();

        services.AddScoped<ITransportationContractorPersonnelRepository, TransportationContractorPersonnelRepository>();

        services.AddScoped<IShippingCostRepository, ShippingCostRepository>();

        services.AddScoped<IShippingCostHistoryRepository, ShippingCostHistoryRepository>();

        services.AddScoped<ITransportationRequestWarehouseRepository, TransportationRequestWarehouseRepository>();

        services.AddScoped<ITransportationRequestDetailRepository, TransportationRequestDetailRepository>();

        services.AddScoped<ITransportationContractorMachineRepository, TransportationContractorMachineRepository>();

        services.AddScoped<ITransportationContractorPriceWeightRepository, TransportationContractorPriceWeightRepository>();

        services.AddScoped<ITransportationContractorPriceWeightHistoryRepository, TransportationContractorPriceWeightHistoryRepository>();

        #endregion

        #region Trips
        services.AddScoped<ITripRepository, TripRepository>();
        #endregion

        #region TelegramChat
        services.AddScoped<ITelegramChatRepository, TelegramChatRepository>();

        services.AddScoped<ITelegramMessageHistoryRepository, TelegramMessageHistoryHistoryRepository>();
        #endregion

        #region TelegramChat
        services.AddScoped<IMessengerRepository, MessengerRepository>();
        services.AddScoped<IMessengerChannelRepository, MessengerChannelRepository>();
        services.AddScoped<IMessengerChannelHistoryRepository, MessengerChannelHistoryRepository>();
        #endregion

        services.AddScoped<IViewProductRepository, ProductRepository>();

        services.AddScoped<IViewWarehouseRepository, WarehouseRepository>();

        services.AddScoped<IViewPackingRepository, ViewPackingRepository>();

        services.AddScoped<IViewThirdPartyRepository, ViewThirdPartyRepository>();

        services.AddScoped<IViewThirdPartyRepository, ViewThirdPartyRepository>();

        services.AddScoped<IViewAddressRepository, ViewAddressRepository>();

        services.AddScoped<IViewCityRepository, ViewCityRepository>();

        services.AddScoped<IViewLegalRepository, ViewLegalRepository>();

        services.AddScoped<IViewRegionRepository, ViewRegionRepository>();

        services.AddScoped<IViewInvoiceRepository, ViewInvoiceRepository>();

        services.AddScoped<IViewInvoiceProductRepository, ViewInvoiceProductRepository>();

        services.AddScoped<IViewInvoiceProductPriceRepository, ViewInvoiceProductPriceRepository>();

        services.AddScoped<IViewCurrencyRepository, ViewCurrencyRepository>();

        services.AddScoped<IMeasureUnitRepository, MeasureUnitRepository>();

        services.AddScoped<IMeasureUnitGroupRepository, MeasureUnitGroupRepository>();

        services.AddScoped<IViewWarehouseAssetRepository, ViewWarehouseAssetRepository>();

        services.AddScoped<IViewCategoryRepository, ViewCategoryRepository>();

        services.AddScoped<IViewGroupRepository, ViewGroupRepository>();

        services.AddScoped<IEngineeringConfigRepository, EngineeringConfigRepository>();

        services.AddScoped<IEngineeringConfigHistoryRepository, EngineeringConfigHistoryRepository>();

        services.AddScoped<IEngineeringCodingConfigRepository, EngineeringCodingConfigRepository>();

        services.AddScoped<IEmployerOperationProductHistoryRepository, EmployerOperationProductHistoryRepository>();

        services.AddScoped<IEmployerOperationServiceHistoryRepository, EmployerOperationServiceHistoryRepository>();

        services.AddScoped<IProjectThirdPartyRepository, ProjectThirdPartyRepository>();

        services.AddScoped<IEmployerEmployeeRepository, EmployerEmployeeRepository>();

        services.AddScoped<IProjectRiskRepository, ProjectRiskRepository>();
        services.AddScoped<ISubProjectRepository, SubProjectRepository>();

        services.AddScoped<IWbsTemplateRepository, WbsTemplateRepository>();

        services.AddScoped<IProjectScheduleColumnRepository, ProjectScheduleColumnRepository>();
        services.AddScoped<IProjectScheduleTaskValueRepository, ProjectScheduleTaskValueRepository>();
        services.AddScoped<IProjectWbsRepository, ProjectWbsRepository>();
        services.AddScoped<IProjectScheduleImportRepository, ProjectScheduleImportRepository>();
        services.AddScoped<IProjectScheduleTaskDependencyRepository, ProjectScheduleTaskDependencyRepository>();
        services.AddScoped<IProjectScheduleTaskOperationRepository, ProjectScheduleTaskOperationRepository>();
        services.AddScoped<IProjectScheduleTaskRepository, ProjectScheduleTaskRepository>();
        services.AddScoped<IProjectOperationWbsRepository, ProjectOperationWbsRepository>();
        services.AddScoped<ScheduleRecalculator>();

        services.AddScoped<IProcesVerbalsRepository, ProcesVerbalRepository>();
        services.AddScoped<IProcesVerbalItemRepository, ProcesVerbalItemRepository>();
        services.AddScoped<IProcesVerbalProductRepository, ProcesVerbalProductRepository>();

        services.AddScoped<ISessionRecordRepository, SessionRecordRepository>();
        services.AddScoped<ISessionRecordActionRepository, SessionRecordActionRepository>();

        services.AddScoped<IProjectCalendarRepository, ProjectCalendarRepository>();
        services.AddScoped<IProjectCalendarExceptionRepository, ProjectCalendarExceptionRepository>();
        services.AddScoped<IProjectCalendarWorkingDayRepository, ProjectCalendarWorkingDayRepository>();
        services.AddScoped<IProjectCalendarWorkingTimeRepository, ProjectCalendarWorkingTimeRepository>();

        services.AddScoped<IViewOrganizationRepository, ViewOrganizationRepository>();

        services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();

        services.AddScoped<IRequestGoodsSupplyDocumentRepository, RequestGoodsSupplyDocumentRepository>();

        services.AddScoped<IRequestGoodsSupplyTypeRepository, RequestGoodsSupplyTypeRepository>();

        services.AddScoped<IRequestGoodsSupplyTypeDocumentRepository, RequestGoodsSupplyTypeDocumentRepository>();

        services.AddScoped<IRequestGoodsSupplyTypeHistoryRepository, RequestGoodsSupplyTypeHistoryRepository>();

        services.AddScoped<IRequestGoodsSupplyTypeDetailRepository, RequestGoodsSupplyTypeDetailRepository>();

        services.AddScoped<IRequestGoodsSupplyTypeDetailDocumentRepository, RequestGoodsSupplyTypeDetailDocumentRepository>();

        services.AddScoped<IRequestGoodsSupplyTypeDetailHistoryRepository, RequestGoodsSupplyTypeDetailHistoryRepository>();

        services.AddScoped<IProjectCostCenterRepository, ProjectCostCenterRepository>();

        services.AddScoped<IGoodsManagerAssignmentRepository, GoodsManagerAssignmentRepository>();

        services.AddScoped<IGoodsManagerAssignmentHistoryRepository, GoodsManagerAssignmentHistoryRepository>();

        services.AddScoped<IContractRepository, ContractRepository>();

        services.AddScoped<IContractRepository, ContractRepository>();

        services.AddScoped<IContractTypeRepository, ContractTypeRepository>();

        services.AddScoped<IContractTypeDetailRepository, ContractTypeDetailRepository>();

        services.AddScoped<IContractFinancialInformationRepository, ContractFinancialInformationRepository>();

        services.AddScoped<IContractAdjustmentConfigurationRepository, ContractAdjustmentConfigurationRepository>();

        services.AddScoped<IContractChangeRepository, ContractChangeRepository>();

        services.AddScoped<IContractAdjustmentReferenceRepository, ContractAdjustmentReferenceRepository>();

        services.AddScoped<IContractAdjustmentIndexRepository, ContractAdjustmentIndexRepository>();

        services.AddScoped<IContractGuaranteeRepository, ContractGuaranteeRepository>();

        services.AddScoped<IReviewReportRepository, ReviewReportRepository>();


        #region EngineeringDocs
        services.AddScoped<IDisciplineRepository, DisciplineRepository>();
        services.AddScoped<IDisciplineDocRepository, DisciplineDocRepository>();
        services.AddScoped<IDisciplineDocTypeRepository, DisciplineDocTypeRepository>();
        services.AddScoped<IProjectDocRepository, ProjectDocRepository>();
        services.AddScoped<IProjectDocHistoryRepository, ProjectDocHistoryRepository>();
        services.AddScoped<IViewFinancialPeriodRepository, ViewFinancialPeriodRepository>();
        #endregion EngineeringDocs

        services.AddScoped<ITaskGroupRepository, TaskGroupRepository>();
        services.AddScoped<IUserTaskRepository, UserTaskRepository>();
        services.AddScoped<IAdjustmentIndexValueRepository, AdjustmentIndexValueRepository>();
        services.AddScoped<IAdjustmentIndexRepository, AdjustmentIndexRepository>();
        services.AddScoped<IAdjustmentReferenceRepository, AdjustmentReferenceRepository>();

    }
}
