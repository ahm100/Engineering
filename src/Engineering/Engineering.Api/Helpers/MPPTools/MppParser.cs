using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Errors.WebServices;

namespace Engineering.Api.Helpers.MppTools;

public class MppParser(ILogger<MppParser> logger) : IMppParser
{
    public async Task<Result<MppImportModel>> ParseAsync(Stream file, CT ct)
    {
        try
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms, ct);
            ms.Position = 0;

            return await Task.Run(() =>
            {
                var project = new Aspose.Tasks.Project(ms);
                var columns = MppImporterHelpers.ParseCustomColumns(project);
                var byFieldId = columns.ToDictionary(c => c.FieldId);

                var model = new MppImportModel
                {
                    ProjectName = project.RootTask.Name,
                    StartDate = MppImporterHelpers.NormalizeDate(project.Get(Aspose.Tasks.Prj.StartDate)),
                    FinishDate = MppImporterHelpers.NormalizeDate(project.Get(Aspose.Tasks.Prj.FinishDate)),
                    StatusDate = MppImporterHelpers.NormalizeDate(project.Get(Aspose.Tasks.Prj.StatusDate)),
                    CustomColumns = columns,
                    Calendars = MppImporterHelpers.ParseCalendars(project, ct),
                    Tasks = MppImporterHelpers.ParseTasks(project, byFieldId, ct),
                    Dependencies = MppImporterHelpers.ParseDependencies(project, ct)
                };
                return Result.Success(model);
            }, ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "MPP parse failed");
            return Result.Failure<MppImportModel>(GlobalErrors.ErrorOnReadFile)!;
        }
    }
}