namespace Engineering.Application.WebServices.MetaDataServices.MachineryInquiryOfficers.Queries.GetMachineryInquiryOfficers;

public record GetMachineryInquiryOfficersQuery(long? ThirdPartyId,
                                               string? FullName,
                                               string? OrganizationCode,
                                               string? FilterData,
                                               List<OperatorInquiryAppointment>? OrderBy,
                                               int PageIndex,
                                               int PageSize) : IQuery<DataResult<List<Officer?>?>?>;
