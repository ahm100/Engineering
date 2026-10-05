using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractorInquiries.Commands.SetContractorInquiryConfirmedUser;

public record SetContractorInquiryConfirmedUserCommand(
    RequestContractorInquiry RequestContractorInquiry,
    long ConfirmedUser) : ICommand<RequestContractorInquiry>;
