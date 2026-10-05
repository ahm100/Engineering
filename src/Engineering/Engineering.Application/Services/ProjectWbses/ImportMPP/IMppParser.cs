namespace Engineering.Application.Services.ProjectWbses.ImportMPP;

public interface IMppParser
{
    Task<Result<MppImportModel>> ParseAsync(
            Stream file,
            CT ct);
}
