
namespace Engineering.Application.Services.RequestMachineryStatusStatements.Queries.GetRequestMachineryStatusStatementById;

public class GetRequestMachineryStatusStatementByIdQueryValidator : AbstractValidator<GetRequestMachineryStatusStatementByIdQuery>
{
    public GetRequestMachineryStatusStatementByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(RequestMachineryStatusStatementErrors.InValidId)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
