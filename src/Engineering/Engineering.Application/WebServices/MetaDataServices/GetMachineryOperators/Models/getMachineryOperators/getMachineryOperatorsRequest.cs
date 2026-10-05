namespace Engineering.Application.WebServices.MetaDataServices.GetMachineryOperators.Models.GetMachineryOperators;

public record GetMachineryOperatorsRequest(long? ThirdPartyId,
                                           string? FullName,
                                           string? OrganizationCode,
                                           string? FilterData,
                                           List<MachineryOperatorAppointment>? OrderBy,
                                           int PageIndex,
                                           int PageSize);
