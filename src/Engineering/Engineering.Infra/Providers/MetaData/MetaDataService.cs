using Engineering.Application.Abstractions.Interfaces;
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

namespace Engineering.Infra.Providers.MetaData;

public class MetaDataService : IMetaDataService
{
    private readonly IMetaDataProvider _metaDataProvider;

    public MetaDataService(IMetaDataProvider metaDataProvider)
    {
        _metaDataProvider = metaDataProvider;
    }

    public async Task<GetsThirdPartyByIdResponse?> GetsThirdPartyById(GetsThirdPartyByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetsThirdPartyById(request, ct);
    }

    public async Task<GetWithSelectedSkillIdResponse?> GetWithSelectedSkillId(GetWithSelectedSkillIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetWithSelectedSkillId(request, ct);
    }

    public async Task<GetWithSkillOnlyByIdsResponse?> GetWithSkillOnlyByIds(GetWithSkillOnlyByIdsRequest request, CT ct)
    {
        return await _metaDataProvider.GetWithSkillOnlyByIds(request, ct);
    }

    public async Task<GetMeasureunitByIdResponse?> GetMeasureunitById(GetMeasureunitByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetMeasureunitById(request.Id, ct);
    }

    public async Task<GetsMeasureunitByIdResponse?> GetsMeasureunitById(GetsMeasureunitByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetsMeasureunitById(request, ct);
    }

    public async Task<CreateContractorResponse?> CreateContractor(CreateContractorRequest request, CT ct)
    {
        return await _metaDataProvider.CreateContractor(request, ct);
    }

    public async Task<UpdateContractorResponse?> UpdateContractor(UpdateContractorRequest request, CT ct)
    {
        return await _metaDataProvider.UpdateContractor(request, ct);
    }

    public async Task<GetCurrencyByIdResponse?> GetCurrencyById(GetCurrencyByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetCurrencyById(request.Id, ct);
    }

    public async Task<GetsCurrencyByIdResponse?> GetsCurrencyById(GetsCurrencyByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetsCurrencyById(request, ct);
    }

    public async Task<GetCityByIdResponse?> GetCityById(GetCityByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetCityById(request.Id, ct);
    }

    public async Task<GetProvinceByIdResponse?> GetProvinceById(GetProvinceByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetProvinceById(request.Id, ct);
    }

    public async Task<GetsCityByIdResponse?> GetsCityById(GetsCityByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetsCityById(request, ct);
    }

    public async Task<GetCitiesByCodesResponse?> GetCitiesByCodes(GetCitiesByCodesRequest request, CT ct)
    {
        return await _metaDataProvider.GetCitiesByCodes(request, ct);
    }

    public async Task<GetBankByIdResponse?> GetBankById(GetBankByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetBankById(request.Id, ct);
    }

    public async Task<GetsBankByIdResponse?> GetsBankById(GetsBankByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetsBankById(request, ct);
    }

    public async Task<GetCostCategoryByIdResponse?> GetCostCategoryById(GetCostCategoryByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetCostCategoryById(request.Id, ct);
    }

    public async Task<GetsCostCategoryByIdResponse?> GetsCostCategoryById(GetsCostCategoryByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetsCostCategoryById(request, ct);
    }

    public async Task<GetCostGroupByIdResponse?> GetCostGroupById(GetCostGroupByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetCostGroupById(request.Id, ct);
    }

    public async Task<GetsCostGroupByIdResponse?> GetsCostGroupById(GetsCostGroupByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetsCostGroupById(request, ct);
    }

    public async Task<GetsCostGroupByCodesResponse?> GetsCostGroupByCodes(GetsCostGroupByCodesRequest request, CT ct)
    {
        return await _metaDataProvider.GetsCostGroupByCodes(request, ct);
    }

    public async Task<GetsCostCategoryByCodesResponse?> GetsCostCategoryByCodes(GetsCostCategoryByCodesRequest request, CT ct)
    {
        return await _metaDataProvider.GetsCostCategoryByCodes(request, ct);
    }

    public async Task<GetSkillByIdResponse?> GetSkillById(GetSkillByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetSkillById(request.Id, ct);
    }

    public async Task<GetSkillByCodeResponse?> GetSkillByCode(GetSkillByCodeRequest request, CT ct)
    {
        return await _metaDataProvider.GetSkillByCode(request.Code, ct);
    }

    public async Task<GetsSkillByIdResponse?> GetsSkillById(GetsSkillByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetsSkillById(request, ct);
    }

    public async Task<GetFilteredSkillsResponse?> GetFilteredSkills(GetFilteredSkillsRequest request, CT ct)
    {
        return await _metaDataProvider.GetFilteredSkills(request.ThirdPartyId, request.Name, request.Code, request.FilterData, request.IsActive, request.PageIndex, request.PageSize, ct);
    }

    public async Task<GetFilteredThirdPartiesResponse?> GetFilteredThirdParties(GetFilteredThirdPartiesRequest request, CT ct)
    {
        return await _metaDataProvider.GetFilteredThirdParties(request, ct);
    }

    public async Task<GetFilteredThirdPartiesForSnapResponse?> GetFilteredThirdPartiesForSnap(GetFilteredThirdPartiesForSnapRequest request, CT ct)
    {
        return await _metaDataProvider.GetFilteredThirdPartiesForSnap(request, ct);
    }

    public async Task<GetsThirdPartyByUserIdResponse?> GetsThirdPartyByUserId(GetsThirdPartyByUserIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetsThirdPartyByUserId(request.Ids, request.PageIndex, request.PageSize, ct);
    }

    public async Task<GetEmployeeByIdResponse?> GetEmployeeById(GetEmployeeByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetEmployeeById(request.Id, 1, 10, ct);
    }

    public async Task<GetContractorByIdResponse?> GetContractorById(GetContractorByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetContractorById(request.Id, 1, 10, ct);
    }

    public async Task<GetSupervisorByIdResponse?> GetSupervisorById(GetSupervisorByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetSupervisorById(request.Id, 1, 10, ct);
    }

    public async Task<GetConsultantByIdResponse?> GetConsultantById(GetConsultantByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetConsultantById(request.Id, 1, 10, ct);
    }

    public async Task<GetEmployerByIdResponse?> GetEmployerById(GetEmployerByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetEmployerById(request.Id, 1, 10, ct);
    }

    public async Task<GetExpertByIdResponse?> GetExpertById(GetExpertByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetExpertById(request.Id, 1, 10, ct);
    }

    public async Task<GetDirectorByIdResponse?> GetDirectorById(GetDirectorByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetDirectorById(request.Id, 1, 10, ct);
    }

    public async Task<GetManagerByIdResponse?> GetManagerById(GetManagerByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetManagerById(request.Id, 1, 10, ct);
    }

    public async Task<GetPlanerByIdResponse?> GetPlanerById(GetPlanerByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetPlanerById(request.Id, 1, 10, ct);
    }

    public async Task<ContractorEmployeesByIdResponse?> GetContractorEmployees(ContractorEmployeesByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetContractorEmployees(request.ContractorId, request.PageIndex, request.PageSize, ct);
    }

    public async Task<GetMachineryInquiryOfficersResponse?> GetMachineryInquiryOfficers(GetMachineryInquiryOfficersRequest request, CT ct)
    {
        return await _metaDataProvider.GetMachineryInquiryOfficers(request, ct);
    }

    public async Task<GetMachineryOperatorsResponse?> GetMachineryOperators(GetMachineryOperatorsRequest request, CT ct)
    {
        return await _metaDataProvider.GetMachineryOperators(request, ct);
    }

    public async Task<GetFilteredByIdsResponse> GetFilteredByIds(GetFilteredByIdsRequest request, CT ct)
    {
        return await _metaDataProvider.GetFilteredByIds(request, ct);
    }

    public async Task<GetThirdPartiesSkillsResponse> GetThirdPartiesSkills(GetThirdPartiesSkillsRequest request, CT ct)
    {
        return await _metaDataProvider.GetThirdPartiesSkills(request, ct);
    }

    public async Task<AddSkillForThirdPartyResponse> AddSkillForThirdParty(AddSkillForThirdPartyRequest request, CT ct)
    {
        return await _metaDataProvider.AddSkillForThirdParty(request, ct);
    }

    public async Task<GetFilteredSkillByIdsResponse> GetFilteredSkillByIds(GetFilteredSkillByIdsRequest request, CT ct)
    {
        return await _metaDataProvider.GetFilteredSkillByIds(request, ct);
    }

    public async Task<GetThirdPartyByIdResponse> GetThirdPartyById(GetThirdPartyByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetThirdPartyById(request.Id, ct);
    }

    public async Task<GetFilteredUsersResponse?> GetFilteredUsers(GetFilteredUsersRequest request, CT ct)
    {
        return await _metaDataProvider.GetFilteredUsers(request, ct);
    }

    public async Task<GetFilteredCompaniesByIdsResponse?> GetFilteredCompaniesByIds(GetFilteredCompaniesByIdsRequest request, CT ct)
    {
        return await _metaDataProvider.GetFilteredCompaniesByIds(request, ct);
    }

    public async Task<GetCompanyByIdResponse?> GetCompanyById(GetCompanyByIdRequest request, CT ct)
    {
        return await _metaDataProvider.GetCompanyById(request.Id, ct);
    }
    public async Task<Result<GetsTransportationThirdPartyResponse?>> GetsTransportationThirdParty(GetsTransportationThirdPartyRequest request, CancellationToken ct)
    {
        return await _metaDataProvider.GetsTransportationThirdParty(request, ct);
    }

    public async Task<Result<CreateThirdPartyResponse?>> CreateThirdParty(CreateThirdPartyRequest request, CancellationToken ct)
    {
        return await _metaDataProvider.CreateThirdParty(request, ct);
    }

    public async Task<Result<UpdateThirdPartyResponse?>> UpdateThirdParty(UpdateThirdPartyRequest request, CancellationToken ct)
    {
        return await _metaDataProvider.UpdateThirdParty(request, ct);
    }

    public async Task<Result<RemoveThirdPartyResponse?>> RemoveThirdParty(RemoveThirdPartyRequest request, CancellationToken ct)
    {
        return await _metaDataProvider.RemoveThirdParty(request.Id, ct);
    }
}