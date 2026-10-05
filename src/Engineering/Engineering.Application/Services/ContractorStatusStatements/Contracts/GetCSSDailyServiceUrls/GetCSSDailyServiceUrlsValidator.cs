namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSDailyServiceUrls;

public class GetCSSDailyServiceUrlsValidator : AbstractValidator<GetCSSDailyServiceUrlsRequest>
{
    public GetCSSDailyServiceUrlsValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}

