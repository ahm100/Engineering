namespace Engineering.Domain.Entities.EngineeringDocs.Enums
{
    public enum ProjectDocStatusEnum
    {
        [Description(ProjectCmts.Draft)]
        Draft = 1,

        [Description(ProjectCmts.Submitted)]
        Submitted = 2,

        [Description(ProjectCmts.UnderReview)]
        UnderReview = 3,

        [Description(ProjectCmts.Commented)]
        Commented = 4,

        [Description(ProjectCmts.Resubmitted)]
        Resubmitted = 5,

        [Description(ProjectCmts.Approved)]
        Approved = 6,

        [Description(ProjectCmts.Rejected)]
        Rejected = 7,

        [Description(ProjectCmts.Cancelled)]
        Cancelled = 8
    }
}


