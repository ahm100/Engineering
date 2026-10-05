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
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.GetCostCategoryById;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.GetsCostCategoryByCodes;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Models.GetsCostCategoryById;
using Engineering.Application.WebServices.MetaDataServices.CostGroups.Models.GetCostGroupById;
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

namespace Engineering.Application.Abstractions.Interfaces;

public interface IMetaDataService
{
    // دریافت کاربران باشناسه 
    Task<GetsThirdPartyByIdResponse?> GetsThirdPartyById(GetsThirdPartyByIdRequest request, CT ct);
    Task<GetWithSelectedSkillIdResponse?> GetWithSelectedSkillId(GetWithSelectedSkillIdRequest request, CT ct);

    // دریافت کاربران باشناسه 
    Task<GetWithSkillOnlyByIdsResponse?> GetWithSkillOnlyByIds(GetWithSkillOnlyByIdsRequest request, CT ct);

    // دریافت کاربران درخواست دهنده شرح عملیات 
    Task<GetFilteredByIdsResponse> GetFilteredByIds(GetFilteredByIdsRequest request, CT ct);
    Task<GetThirdPartiesSkillsResponse> GetThirdPartiesSkills(GetThirdPartiesSkillsRequest request, CT ct);
    Task<GetThirdPartyByIdResponse> GetThirdPartyById(GetThirdPartyByIdRequest request, CT ct);

    Task<AddSkillForThirdPartyResponse> AddSkillForThirdParty(AddSkillForThirdPartyRequest request, CT ct);

    Task<GetFilteredSkillByIdsResponse> GetFilteredSkillByIds(GetFilteredSkillByIdsRequest request, CT ct);



    // دریافت واحد اندازه گیری
    Task<GetMeasureunitByIdResponse?> GetMeasureunitById(GetMeasureunitByIdRequest request, CT ct);

    // دریافت واحدهای اندازه گیری
    Task<GetsMeasureunitByIdResponse?> GetsMeasureunitById(GetsMeasureunitByIdRequest request, CT ct);

    // دریافت شهر
    Task<GetCityByIdResponse?> GetCityById(GetCityByIdRequest request, CT ct);

    // دریافت استان
    Task<GetProvinceByIdResponse?> GetProvinceById(GetProvinceByIdRequest request, CT ct);

    // دریافت شهر
    Task<CreateContractorResponse?> CreateContractor(CreateContractorRequest request, CT ct);

    // دریافت شهر
    Task<UpdateContractorResponse?> UpdateContractor(UpdateContractorRequest request, CT ct);

    // دریافت شهرها
    Task<GetsCityByIdResponse?> GetsCityById(GetsCityByIdRequest request, CT ct);

    // دریافت شهرها
    Task<GetCitiesByCodesResponse?> GetCitiesByCodes(GetCitiesByCodesRequest request, CT ct);

    // دریافت شهر
    Task<GetBankByIdResponse?> GetBankById(GetBankByIdRequest request, CT ct);

    // دریافت شهرها
    Task<GetsBankByIdResponse?> GetsBankById(GetsBankByIdRequest request, CT ct);

    // دریافت شهر
    Task<GetCostCategoryByIdResponse?> GetCostCategoryById(GetCostCategoryByIdRequest request, CT ct);

    // دریافت شهرها
    Task<GetsCostCategoryByIdResponse?> GetsCostCategoryById(GetsCostCategoryByIdRequest request, CT ct);

    // دریافت شهر
    Task<GetCostGroupByIdResponse?> GetCostGroupById(GetCostGroupByIdRequest request, CT ct);

    // دریافت شهرها
    Task<GetsCostGroupByIdResponse?> GetsCostGroupById(GetsCostGroupByIdRequest request, CT ct);

    // دریافت شهرها
    Task<GetsCostGroupByCodesResponse?> GetsCostGroupByCodes(GetsCostGroupByCodesRequest request, CT ct);

    // دریافت شهرها
    Task<GetsCostCategoryByCodesResponse?> GetsCostCategoryByCodes(GetsCostCategoryByCodesRequest request, CT ct);

    // دریافت مهارت
    Task<GetSkillByIdResponse?> GetSkillById(GetSkillByIdRequest request, CT ct);

    // دریافت مهارت
    Task<GetSkillByCodeResponse?> GetSkillByCode(GetSkillByCodeRequest request, CT ct);

    // دریافت مهارت ها
    Task<GetsSkillByIdResponse?> GetsSkillById(GetsSkillByIdRequest request, CT ct);

    // دریافت مهارت ها
    Task<GetFilteredSkillsResponse?> GetFilteredSkills(GetFilteredSkillsRequest request, CT ct);

    // دریافت ارز
    Task<GetCurrencyByIdResponse?> GetCurrencyById(GetCurrencyByIdRequest request, CT ct);

    // دریافت ارزها
    Task<GetsCurrencyByIdResponse?> GetsCurrencyById(GetsCurrencyByIdRequest request, CT ct);

    // دریافت کاربران 
    Task<GetsThirdPartyByUserIdResponse?> GetsThirdPartyByUserId(GetsThirdPartyByUserIdRequest request, CT ct);

    // دریافت کاربران 
    Task<GetFilteredThirdPartiesResponse?> GetFilteredThirdParties(GetFilteredThirdPartiesRequest request, CT ct);

    Task<GetFilteredThirdPartiesForSnapResponse?> GetFilteredThirdPartiesForSnap(GetFilteredThirdPartiesForSnapRequest request, CT ct);

    // دریافت کاربران مطلع
    Task<GetEmployeeByIdResponse?> GetEmployeeById(GetEmployeeByIdRequest request, CT ct);

    // دریافت کاربران مطلع
    Task<GetContractorByIdResponse?> GetContractorById(GetContractorByIdRequest request, CT ct);

    // دریافت کاربران مطلع
    Task<ContractorEmployeesByIdResponse?> GetContractorEmployees(ContractorEmployeesByIdRequest request, CT ct);

    // دریافت کاربران مطلع
    Task<GetSupervisorByIdResponse?> GetSupervisorById(GetSupervisorByIdRequest request, CT ct);

    // دریافت کاربران مطلع
    Task<GetConsultantByIdResponse?> GetConsultantById(GetConsultantByIdRequest request, CT ct);

    // دریافت کاربران مطلع
    Task<GetEmployerByIdResponse?> GetEmployerById(GetEmployerByIdRequest request, CT ct);

    // دریافت کاربران مطلع
    Task<GetExpertByIdResponse?> GetExpertById(GetExpertByIdRequest request, CT ct);

    // دریافت کاربران مطلع
    Task<GetDirectorByIdResponse?> GetDirectorById(GetDirectorByIdRequest request, CT ct);

    // دریافت کاربران مطلع
    Task<GetManagerByIdResponse?> GetManagerById(GetManagerByIdRequest request, CT ct);

    // دریافت مسئول برنامه ریزی
    Task<GetPlanerByIdResponse?> GetPlanerById(GetPlanerByIdRequest request, CT ct);

    // دریافت متصدیان
    Task<GetMachineryInquiryOfficersResponse?> GetMachineryInquiryOfficers(GetMachineryInquiryOfficersRequest request, CT ct);

    // دریافت متصدیان
    Task<GetMachineryOperatorsResponse?> GetMachineryOperators(GetMachineryOperatorsRequest request, CT ct);

    // دریافت کاربران به صورت فیلتر شده
    Task<GetFilteredUsersResponse?> GetFilteredUsers(GetFilteredUsersRequest request, CT ct);

    // دریافت کمپانی های فیلتر شده به وسیله شناسه ها
    Task<GetFilteredCompaniesByIdsResponse?> GetFilteredCompaniesByIds(GetFilteredCompaniesByIdsRequest request, CT ct);
    // دریافت کمپانی با شناسه
    Task<GetCompanyByIdResponse?> GetCompanyById(GetCompanyByIdRequest request, CT ct);

    Task<Result<GetsTransportationThirdPartyResponse?>> GetsTransportationThirdParty(
      GetsTransportationThirdPartyRequest request, CancellationToken ct);

    Task<Result<CreateThirdPartyResponse?>> CreateThirdParty(
    CreateThirdPartyRequest request, CT ct);

    Task<Result<UpdateThirdPartyResponse?>> UpdateThirdParty(
        UpdateThirdPartyRequest request, CT ct);

    Task<Result<RemoveThirdPartyResponse?>> RemoveThirdParty(
        RemoveThirdPartyRequest request, CT ct);
}