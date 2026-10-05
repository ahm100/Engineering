namespace Engineering.Application.WebServices.MetaDataServices.MachineryInquiryOfficers.Queries.GetMachineryInquiryOfficers;

public class GetMachineryInquiryOfficersQueryValidator : AbstractValidator<GetMachineryInquiryOfficersQuery>
{
    public GetMachineryInquiryOfficersQueryValidator()
    {
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
    }
}
