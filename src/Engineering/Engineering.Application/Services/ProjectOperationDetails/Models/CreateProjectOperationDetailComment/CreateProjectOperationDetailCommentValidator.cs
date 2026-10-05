namespace Engineering.Application.Services.ProjectOperationDetails.Models.CreateProjectOperationDetailComment;

public class CreateProjectOperationDetailCommentValidator : AbstractValidator<CreateProjectOperationDetailCommentRequest>
{
    public CreateProjectOperationDetailCommentValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().WithError(ProjectOperationDetailCommentErrors.UnValidProjectOperationDetailId);
        RuleFor(oo => oo.Comment).NotNull().WithError(ProjectOperationDetailCommentErrors.UnValidComment);
    }
}
