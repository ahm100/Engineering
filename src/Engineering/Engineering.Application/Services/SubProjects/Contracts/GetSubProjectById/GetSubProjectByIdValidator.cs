namespace Engineering.Application.Services.SubProjects.Contracts.GetSubProjectById;

public class GetSubProjectByIdValidator : AbstractValidator<GetSubProjectByIdRequest>
{
    public GetSubProjectByIdValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
