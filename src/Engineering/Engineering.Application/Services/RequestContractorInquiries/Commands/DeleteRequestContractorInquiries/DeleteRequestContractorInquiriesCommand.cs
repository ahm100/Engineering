using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Application.Services.RequestContractorInquiries.Commands.DeleteRequestContractorInquiries;

public record DeleteRequestContractorInquiriesCommand(RequestContractor RequestContractor) : ICommand<bool?>;
