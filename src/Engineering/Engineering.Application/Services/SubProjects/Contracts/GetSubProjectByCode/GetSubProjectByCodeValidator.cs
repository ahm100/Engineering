namespace Engineering.Application.Services.SubProjects.Contracts.GetSubProjectByCode;

public class GetSubProjectByCodeValidator : AbstractValidator<GetSubProjectByCodeRequest>
{
    public GetSubProjectByCodeValidator()
    {
        RuleFor(oo => oo.Code)
            .HasMaxLength(GlobalCmts.Code, 150);
    }
}
