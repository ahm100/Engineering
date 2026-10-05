using Engineering.Application.Services.Projects.Models.UpdateProjectProduct;

namespace Engineering.Application.Services.Projects.Models.CreateProjectProduct;

public class UpdateProjectProductRequestValidator : AbstractValidator<UpdateProjectProductRequest>
{
    public UpdateProjectProductRequestValidator()
    {
        RuleFor(oo => oo.ProjectId).IsPositive(ProjectCmts.ProjectProduct);
    }
}

public class UpdateProjectProductModelValidator : AbstractValidator<UpdateProjectProductModel>
{
    public UpdateProjectProductModelValidator()
    {
        RuleFor(oo => oo.RequestQuantity).IsPositive(ProjectCmts.TotalQuantity);
    }
}
