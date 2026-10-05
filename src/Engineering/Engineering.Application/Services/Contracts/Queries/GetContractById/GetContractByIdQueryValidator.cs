namespace Engineering.Application.Services.Contracts.Queries.GetContractById;

public class GetContractByIdQueryValidator : AbstractValidator<GetContractByIdQuery>
{
    public GetContractByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
