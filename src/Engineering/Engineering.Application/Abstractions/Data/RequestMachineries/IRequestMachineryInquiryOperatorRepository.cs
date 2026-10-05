using Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryInquiryOperators;
using Engineering.Application.Services.RequestMachineryManagements.Queries.GetRequestMachineryOperators;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Abstractions.Data.RequestMachineries;

public interface IRequestMachineryInquiryOperatorRepository : IBaseRepository<RequestMachineryInquiryOperator>
{
    Task<RequestMachineryInquiryOperator?> GetByIdAsync(long id, CT ct);

    Task<RequestMachineryInquiryOperator?> GetByMachineryAsync(long machineryId, long operatorId, CT ct);

    Task<List<GetRequestMachineryInquiryOperatorsQueryModel>> GetFilteredAsync(long machineryId, long machineryGroupId, CT ct);

    Task<List<GetRequestMachineryOperatorsQueryModel>> GetFilteredOperatorAsync(long requestMachineryId, long machineryId, long machineryGroupId, CT ct);
}
