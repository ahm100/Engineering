namespace Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryInquiryOperators;

public record GetRequestMachineryInquiryOperatorsQuery(long MachineryId,
                                                       long MachinertGroupId
                                                       ) : IQuery<List<GetRequestMachineryInquiryOperatorsQueryModel>>;
