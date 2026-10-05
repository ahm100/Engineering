using Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Commands.CreateRequestMachineryStatusStatement;

public class CreateRequestMachineryStatusStatementCommandHandler : ICommandHandler<CreateRequestMachineryStatusStatementCommand, RequestMachineryStatusStatement>
{
    private readonly ILogger<CreateRequestMachineryStatusStatementCommandHandler> _logger;
    private readonly IRequestMachineryStatusStatementRepository _repository;

    public CreateRequestMachineryStatusStatementCommandHandler(
        ILogger<CreateRequestMachineryStatusStatementCommandHandler> logger,
        IRequestMachineryStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestMachineryStatusStatement?>> Handle(CreateRequestMachineryStatusStatementCommand request, CT ct)
    {
        try
        {
            var entity = new RequestMachineryStatusStatement(request.ContractorId, request.FromDate, request.ToDate,
                request.PaymentDate, request.TotalRequestedCount, request.TotalFinalPrice, request.ContractorPrice,
                request.BankAccountId, request.IBAN, request.Description, request.CostCategoryId, request.CostGroupId,
                request.DocumentTypeId, request.PreferentialTypeId, request.CompanyId);
            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestMachineryStatusStatement>(SharedErrors.UnknownError);
        }
    }
}
