using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetsTransportationThirdParty;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;

namespace Engineering.Application.Abstractions.Data;

public interface IViewThirdPartyRepository : IBaseRepository<ViewThirdParty>
{
    Task<ViewThirdParty?> GetById(
        long id,
        CT ct);

    Task<ViewThirdParty?> GetByDefaultPhoneNo(
        string defaultPhoneNo,
        CT ct);

    Task<ViewThirdParty?> GetThirdPartyWithRelationsById(
        long id,
        CT ct);

    Task<string> CodeCreator(CT ct);

    Task<ViewThirdParty?> GetDefaultPhoneNoAndAddressById(
        long id,
        CT ct);

    Task<ViewThirdParty?> GetByIdWithSkills(
        long id,
        CT ct);

    Task<ViewThirdParty?> GetThirdPartyWithAllRelationsById(
        long id,
        CT ct);

    Task<ViewThirdParty?> GetByIdWithSpecialRelation(
        long id,
        CT ct);

    Task<(List<ViewThirdParty> Data, int RowCount)> GetThirdPartiesWithSelectedSkillIds(
        List<long> ids,
        List<long> skillIds,
        string? filterData,
        CT ct);

    Task<List<ViewThirdParty>> GetByIds(
        List<long> ids,
        CT ct);

    Task<List<ViewThirdParty>> GetByCodes(
        List<string> codes,
        CT ct);

    Task<List<ViewThirdParty>> GetByUserIds(
        List<long> ids,
        CT ct);

    Task<List<ViewThirdParty>> GetByNationalCodes(
        List<string> nationalCodes,
        CT ct);

    Task<List<ViewThirdParty>> GetByDefaultPhoneNos(
        List<string> defaultPhoneNos,
        CT ct);

    Task<(List<ViewThirdParty> Data, int RowCount)> GetThirdPartiesByIds(
        List<long> ids,
        bool? ignoreQuery,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsTransportationThirdPartyModel> Data, int RowCount)> GetsTransportationThirdParty(
        List<long>? ids,
        string? filterData,
        int pageIndex,
        int pageSize,
        bool? ignoreQuery,
        CT ct);

    Task<(List<ViewThirdParty> Data, int RowCount)> GetThirdPartiesByIdsIncludeCompany(
        List<long> ids,
        bool? ignoreQuery,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ViewThirdParty> Data, int RowCount)> GetFilteredThirdParties(
        List<long>? ids,
        string? filterData,
        bool? isActive,
        bool? isIndividual,
        List<long>? skillIds,
        List<long>? guildTypeIds,
        string? defaultPhoneNo,
        string? iBAN,
        string[]? orderBy,
        Guid[]? preferentialReferenceCodes,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ViewThirdParty> Data, int RowCount)> GetFilteredThirdPartiesForSnap(
        List<string>? organizationCodes,
        List<string>? defaultPhoneNumbers,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ViewThirdParty> Data, int RowCount)> GetThirdPartiesBySkillCode(
        long? thirdPartyId,
        string? fullName,
        string? organizationCode,
        int skillCode,
        string? filterData,
        bool? isActive,
        int pageIndex,
        int pageSize,
        bool? isUserNullable,
        CT ct);

    Task<(List<ViewThirdParty> Data, int RowCount)> GetFltrThirdParties(
        List<long> ids,
        string? filterData,
        int pageIndex,
        int pageSize,
        CancellationToken ct);
}