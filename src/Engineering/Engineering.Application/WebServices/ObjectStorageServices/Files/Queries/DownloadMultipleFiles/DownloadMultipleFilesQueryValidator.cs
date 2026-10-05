
namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadMultipleFiles;

public class DownloadMultipleFilesQueryValidator : AbstractValidator<DownloadMultipleFilesQuery>
{
    public DownloadMultipleFilesQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
    }
}
