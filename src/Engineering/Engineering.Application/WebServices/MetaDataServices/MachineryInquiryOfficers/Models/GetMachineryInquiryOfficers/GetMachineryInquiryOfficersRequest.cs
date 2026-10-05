namespace Engineering.Application.WebServices.MetaDataServices.MachineryInquiryOfficers.Models.GetMachineryInquiryOfficers;

public record GetMachineryInquiryOfficersRequest(long? ThirdPartyId,
                                                 string? FullName,
                                                 string? OrganizationCode,
                                                 string? FilterData,
                                                 List<OperatorInquiryAppointment>? OrderBy,
                                                 int PageIndex,
                                                 int PageSize);
