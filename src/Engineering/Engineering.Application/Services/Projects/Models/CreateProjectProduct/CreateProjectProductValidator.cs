namespace Engineering.Application.Services.Projects.Models.CreateProjectProduct;

public class CreateProjectProductRequestValidator : AbstractValidator<CreateProjectProductRequest>
{
    public CreateProjectProductRequestValidator()
    {
        RuleFor(oo => oo.ProjectId).IsPositive(ProjectCmts.ProjectId);
    }
}
public class CreateProjectProductModelValidator : AbstractValidator<CreateProjectProductModel>
{
    public CreateProjectProductModelValidator()
    {
        RuleFor(oo => oo.RequestQuantity).IsPositive(ProjectCmts.TotalQuantity);
    }
}
