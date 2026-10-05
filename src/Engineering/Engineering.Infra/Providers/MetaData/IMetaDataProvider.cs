using Engineering.Application.WebServices.MetaDataServices.Banks.Models.GetBankById;
using Engineering.Application.WebServices.MetaDataServices.Banks.Models.GetsBankById;
using Engineering.Application.WebServices.MetaDataServices.Cities.Models.GetCityById;
using Engineering.Application.WebServices.MetaDataServices.Cities.Models.GetProvinceById;
using Engineering.Application.WebServices.MetaDataServices.Cities.Models.GetsCityById;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetCompanyById;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetFilteredCompaniesByIds;
using Engineering.Application.WebServices.MetaDataServices.Consultants.Models.GetConsultantById;
using Engineering.Application.WebServices.MetaDataServices.ContractorEmployees.Models.GetById;
using Engineering.Application.WebServices.MetaDataServices.Contractors.Models.CreateContractor;
using Engineering.Application.WebServices.MetaDataServices.Contractors.Models.GetContractorById;
using Engineering.Application.WebServices.MetaDataServices.Contractors.Models.UpdateContractor;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.GetsCostCategoryByCodes;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.GetsCostCategoryById;
using Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.GetsCostGroupByCodes;
using Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.GetsCostGroupById;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetCitiesByCodes;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetsCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.Directors.Models.GetDirectorById;
using Engineering.Application.WebServices.MetaDataServices.Employees.Models.GetEmployeeById;
using Engineering.Application.WebServices.MetaDataServices.Employers.Models.GetEmployerById;
using Engineering.Application.WebServices.MetaDataServices.Experts.Models.GetExpertById;
using Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators.Models.GetMachineryOperators;
using Engineering.Application.WebServices.MetaDataServices.MachineryInquiryOfficers.Models.GetMachineryInquiryOfficers;
using Engineering.Application.WebServices.MetaDataServices.Managers.Models.GetManagerById;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Models.GetMeasureunitById;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Models.GetsMeasureunitById;
using Engineering.Application.WebServices.MetaDataServices.MetaDataServices.Models.GetCostCategoryById;
using Engineering.Application.WebServices.MetaDataServices.MetaDataServices.Models.GetCostGroupById;
using Engineering.Application.WebServices.MetaDataServices.Planers.Models.GetPlanerById;
using Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetFilteredSkillByIds;
using Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetFilteredSkills;
using Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetSkillByCode;
using Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetSkillById;
using Engineering.Application.WebServices.MetaDataServices.Skills.Models.GetsSkillById;
using Engineering.Application.WebServices.MetaDataServices.Supervisors.Models.GetSupervisorById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.CreateThirdParty;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredByIds;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredThirdParties;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredThirdPartiesForSnap;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsThirdPartyById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsThirdPartyByUserId;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsTransportationThirdParty;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetThirdPartyById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetWithSelectedSkillId;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetWithSkillOnlyByIds;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.RemoveThirdParty;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.UpdateThirdParty;
using Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Models.AddSkillForThirdParty;
using Engineering.Application.WebServices.MetaDataServices.ThirdPartySkills.Models.GetThirdPartiesSkills;

namespace Engineering.Infra.Providers.MetaData;

public interface IMetaDataProvider
{
    // دریافت اطلاعات کاربران 
    [Post("/thirdparty/getByIds/")]
    Task<GetsThirdPartyByIdResponse> GetsThirdPartyById(
        [Body] GetsThirdPartyByIdRequest request, CT ct);

    // دریافت اطلاعات کاربران 
    [Post("/thirdparty/getWithSelectedSkillId/")]
    Task<GetWithSelectedSkillIdResponse> GetWithSelectedSkillId(
        [Body] GetWithSelectedSkillIdRequest request, CT ct);

    // دریافت اطلاعات کاربران 
    [Post("/thirdparty/getWithSkillOnlyByIds/")]
    Task<GetWithSkillOnlyByIdsResponse> GetWithSkillOnlyByIds(
        [Body] GetWithSkillOnlyByIdsRequest request, CT ct);

    // دریافت اطلاعات کاربران 
    [Post("/thirdparty/getFilteredByIds/")]
    Task<GetFilteredByIdsResponse> GetFilteredByIds(
        [Body] GetFilteredByIdsRequest request, CT ct);

    // دریافت اطلاعات کاربران 
    [Get("/thirdparty/getById/")]
    Task<GetThirdPartyByIdResponse> GetThirdPartyById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات کاربران 
    [Post("/thirdpartyskill/addSkillForThirdParty/")]
    Task<AddSkillForThirdPartyResponse> AddSkillForThirdParty(
        [Body] AddSkillForThirdPartyRequest request, CT ct);

    [Post("/thirdpartyskill/GetThirdPartiesSkills/")]
    Task<GetThirdPartiesSkillsResponse> GetThirdPartiesSkills(
        [Body] GetThirdPartiesSkillsRequest request, CT ct);

    // دریافت اطلاعات کاربران 
    [Post("/skill/getFilteredByIds/")]
    Task<GetFilteredSkillByIdsResponse> GetFilteredSkillByIds(
        [Body] GetFilteredSkillByIdsRequest request, CT ct);


    [Post("/thirdparty/createContractor/")]
    Task<CreateContractorResponse> CreateContractor(
        [Body] CreateContractorRequest request, CT ct);

    [Put("/thirdparty/update/")]
    Task<UpdateContractorResponse> UpdateContractor(
        [Body] UpdateContractorRequest request, CT ct);

    // دریافت اطلاعات واحد اندازه گیری 
    [Get("/measureunit/getById/")]
    Task<GetMeasureunitByIdResponse> GetMeasureunitById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات واحدهای اندازه گیری 
    [Post("/measureunit/getByIds/")]
    Task<GetsMeasureunitByIdResponse> GetsMeasureunitById(
        [Body] GetsMeasureunitByIdRequest request, CT ct);

    // دریافت اطلاعات ارز 
    [Get("/currency/getById/")]
    Task<GetCurrencyByIdResponse> GetCurrencyById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات ارزها 
    [Post("/currency/getByIds/")]
    Task<GetsCurrencyByIdResponse> GetsCurrencyById(
        [Body] GetsCurrencyByIdRequest request, CT ct);

    // دریافت اطلاعات شهر 
    [Get("/city/getById/")]
    Task<GetCityByIdResponse> GetCityById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات استان 
    [Get("/province/getById/")]
    Task<GetProvinceByIdResponse> GetProvinceById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات شهرها 
    [Post("/city/getByIds/")]
    Task<GetsCityByIdResponse> GetsCityById(
        [Body] GetsCityByIdRequest request, CT ct);

    // دریافت اطلاعات شهرها 
    [Post("/city/getByCodes/")]
    Task<GetCitiesByCodesResponse> GetCitiesByCodes(
        [Body] GetCitiesByCodesRequest request, CT ct);

    // دریافت اطلاعات شهر 
    [Get("/Bank/getById/")]
    Task<GetBankByIdResponse> GetBankById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات شهرها 
    [Post("/Bank/getByIds/")]
    Task<GetsBankByIdResponse> GetsBankById(
        [Body] GetsBankByIdRequest request, CT ct);

    // دریافت اطلاعات شهر 
    [Get("/CostCategory/GetCostCategoryById/")]
    Task<GetCostCategoryByIdResponse> GetCostCategoryById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات شهرها 
    [Post("/CostCategory/GetsCostCategory/")]
    Task<GetsCostCategoryByIdResponse> GetsCostCategoryById(
        [Body] GetsCostCategoryByIdRequest request, CT ct);

    // دریافت اطلاعات شهر 
    [Get("/CostGroup/GetCostGroupById/")]
    Task<GetCostGroupByIdResponse> GetCostGroupById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات شهرها 
    [Post("/CostGroup/GetsCostGroup/")]
    Task<GetsCostGroupByIdResponse> GetsCostGroupById(
        [Body] GetsCostGroupByIdRequest request, CT ct);

    // دریافت اطلاعات شهرها 
    [Post("/costGroup/GetsCostGroupByCodes/")]
    Task<GetsCostGroupByCodesResponse> GetsCostGroupByCodes(
        [Body] GetsCostGroupByCodesRequest request, CT ct);

    // دریافت اطلاعات شهرها 
    [Post("/costCategory/GetsCostCategoryByCodes/")]
    Task<GetsCostCategoryByCodesResponse> GetsCostCategoryByCodes(
        [Body] GetsCostCategoryByCodesRequest request, CT ct);

    // دریافت اطلاعات مهارت 
    [Get("/skill/getById/")]
    Task<GetSkillByIdResponse> GetSkillById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات مهارت 
    [Get("/skill/getByCode/")]
    Task<GetSkillByCodeResponse> GetSkillByCode(
        [AliasAs("Code")] string Code, CT ct);

    // دریافت اطلاعات مهارت ها 
    [Post("/skill/getByIds/")]
    Task<GetsSkillByIdResponse> GetsSkillById(
        [Body] GetsSkillByIdRequest request, CT ct);

    // دریافت اطلاعات مهارت ها 
    [Get("/skill/getFiltered/")]
    Task<GetFilteredSkillsResponse> GetFilteredSkills(
        [AliasAs("ThirdPartyId")] string? ThirdPartyId, [AliasAs("Name")] string? Name,
        [AliasAs("Code")] string? Code, [AliasAs("FilterData")] string? FilterData,
        [AliasAs("IsActive")] bool? IsActive, [AliasAs("PageIndex")] int PageIndex, [AliasAs("PageSize")] int PageSize, CT ct);

    // دریافت اطلاعات واحد 
    [Post("/thirdParty/getFiltered/")]
    Task<GetFilteredThirdPartiesResponse> GetFilteredThirdParties(
        [Body] GetFilteredThirdPartiesRequest request, CT ct);

    // دریافت اطلاعات واحد 
    [Post("/thirdParty/GetFilteredThirdPartiesForSnap/")]
    Task<GetFilteredThirdPartiesForSnapResponse> GetFilteredThirdPartiesForSnap(
        [Body] GetFilteredThirdPartiesForSnapRequest request, CT ct);

    // دریافت اطلاعات واحد 
    [Post("/thirdParty/getByUserIds/")]
    Task<GetsThirdPartyByUserIdResponse> GetsThirdPartyByUserId(
        [AliasAs("ids")] List<long> Ids, [AliasAs("PageIndex")] int PageIndex, [AliasAs("PageSize")] int PageSize, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Get("/thirdparty/getEmployees/")]
    Task<GetEmployeeByIdResponse> GetEmployeeById(
        [AliasAs("ThirdPartyId")] long Id, [AliasAs("PageIndex")] int PageIndex, [AliasAs("PageSize")] int PageSize, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Get("/thirdparty/getContractors/")]
    Task<GetContractorByIdResponse> GetContractorById(
        [AliasAs("ThirdPartyId")] long Id, [AliasAs("PageIndex")] int PageIndex, [AliasAs("PageSize")] int PageSize, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Get("/thirdparty/getContractorEmployees/")]
    Task<ContractorEmployeesByIdResponse> GetContractorEmployees(
        [AliasAs("ContractorId")] long ContractorId, [AliasAs("PageIndex")] int PageIndex, [AliasAs("PageSize")] int PageSize, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Get("/thirdparty/getSupervisors/")]
    Task<GetSupervisorByIdResponse> GetSupervisorById(
        [AliasAs("ThirdPartyId")] long Id, [AliasAs("PageIndex")] int PageIndex, [AliasAs("PageSize")] int PageSize, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Get("/thirdparty/getConsultants/")]
    Task<GetConsultantByIdResponse> GetConsultantById(
        [AliasAs("ThirdPartyId")] long Id, [AliasAs("PageIndex")] int PageIndex, [AliasAs("PageSize")] int PageSize, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Get("/thirdparty/getEmployers/")]
    Task<GetEmployerByIdResponse> GetEmployerById(
        [AliasAs("ThirdPartyId")] long Id, [AliasAs("PageIndex")] int PageIndex, [AliasAs("PageSize")] int PageSize, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Get("/thirdparty/getExperts/")]
    Task<GetExpertByIdResponse> GetExpertById(
        [AliasAs("ThirdPartyId")] long Id, [AliasAs("PageIndex")] int PageIndex, [AliasAs("PageSize")] int PageSize, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Get("/thirdparty/getDirectors/")]
    Task<GetDirectorByIdResponse> GetDirectorById(
        [AliasAs("ThirdPartyId")] long Id, [AliasAs("PageIndex")] int PageIndex, [AliasAs("PageSize")] int PageSize, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Get("/thirdparty/getManagers/")]
    Task<GetManagerByIdResponse> GetManagerById(
        [AliasAs("ThirdPartyId")] long Id, [AliasAs("PageIndex")] int PageIndex, [AliasAs("PageSize")] int PageSize, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Get("/thirdparty/getPlaners/")]
    Task<GetPlanerByIdResponse> GetPlanerById(
        [AliasAs("ThirdPartyId")] long Id, [AliasAs("PageIndex")] int PageIndex, [AliasAs("PageSize")] int PageSize, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Post("/thirdparty/getMachineryInquiryOfficers/")]
    Task<GetMachineryInquiryOfficersResponse> GetMachineryInquiryOfficers(
        [Body] GetMachineryInquiryOfficersRequest request, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Post("/thirdparty/getMachineryOperators/")]
    Task<GetMachineryOperatorsResponse> GetMachineryOperators(
        [Body] GetMachineryOperatorsRequest request, CT ct);

    // دریافت اطلاعات کاربربه صورت فیلتر شده 
    [Post("/thirdparty/getFilteredUsers/")]
    Task<GetFilteredUsersResponse> GetFilteredUsers(
        [Body] GetFilteredUsersRequest request, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Get("/company/getById/")]
    Task<GetCompanyByIdResponse> GetCompanyById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات کاربر مطلع 
    [Post("/company/getFilteredByIds/")]
    Task<GetFilteredCompaniesByIdsResponse> GetFilteredCompaniesByIds(
        [Body] GetFilteredCompaniesByIdsRequest request, CT ct);

    [Post("/thirdparty/GetsTransportationThirdParty/")]
    Task<GetsTransportationThirdPartyResponse> GetsTransportationThirdParty([Body] GetsTransportationThirdPartyRequest request, CancellationToken ct);

    [Post("/thirdparty/create")]
    Task<Result<CreateThirdPartyResponse?>> CreateThirdParty(CreateThirdPartyRequest request, CancellationToken ct);

    [Put("/thirdparty/update")]
    Task<Result<UpdateThirdPartyResponse?>> UpdateThirdParty(UpdateThirdPartyRequest request, CancellationToken ct);

    [Delete("/thirdparty/remove")]
    Task<Result<RemoveThirdPartyResponse?>> RemoveThirdParty([AliasAs("Id")] long Id, CancellationToken ct);


}