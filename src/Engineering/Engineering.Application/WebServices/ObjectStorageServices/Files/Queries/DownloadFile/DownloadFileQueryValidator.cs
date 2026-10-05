
namespace Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadFile;

public class DownloadFileQueryValidator : AbstractValidator<DownloadFileQuery>
{
    public DownloadFileQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull()
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}