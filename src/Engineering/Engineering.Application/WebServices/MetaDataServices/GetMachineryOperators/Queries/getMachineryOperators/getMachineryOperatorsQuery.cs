namespace Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators.Queries.GetMachineryOperators;

public record GetMachineryOperatorsQuery(long? ThirdPartyId,
                                         string? FullName,
                                         string? OrganizationCode,
                                         string? FilterData,
                                         List<MachineryOperatorAppointment>? OrderBy,
                                         int PageIndex,
                                         int PageSize) : IQuery<DataResult<List<OfficerModel?>?>?>;
