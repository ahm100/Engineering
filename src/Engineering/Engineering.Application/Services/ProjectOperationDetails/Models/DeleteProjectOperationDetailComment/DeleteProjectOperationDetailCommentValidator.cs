namespace Engineering.Application.Services.ProjectOperationDetails.Models.DeleteProjectOperationDetailComment;

public class DeleteProjectOperationDetailCommentValidator : AbstractValidator<DeleteProjectOperationDetailCommentRequest>
{
    public DeleteProjectOperationDetailCommentValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ProjectOperationDetailCommentErrors.UnValidProjectOperationDetailCommentId);
    }
}
