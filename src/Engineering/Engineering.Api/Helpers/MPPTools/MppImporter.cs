using Aspose.Tasks;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Errors.WebServices;

namespace Engineering.Api.Helpers.MppTools;

public static class MppImporter
{
    public static Result<MppImportModel> Parse(Stream file, CT ct)
    {
        ct.ThrowIfCancellationRequested();

        if (!file.CanRead)
            return Result.Failure<MppImportModel>(GlobalErrors.ErrorOnReadFile)!;

        if (file.CanSeek)
            file.Position = 0;

        var project = new Project(file);

        var model = new MppImportModel
        {
            ProjectName = project.Get(Prj.Name),
            StartDate = MppImporterHelpers.NormalizeDate(project.Get(Prj.StartDate)),
            FinishDate = MppImporterHelpers.NormalizeDate(project.Get(Prj.FinishDate))
        };

        model.Calendars = MppImporterHelpers.ParseCalendars(project, ct);
        model.CustomColumns = MppImporterHelpers.ParseCustomColumns(project);

        var byFieldId = model.CustomColumns
            .GroupBy(x => x.FieldId)
            .ToDictionary(g => g.Key, g => g.First());

        model.Tasks = MppImporterHelpers.ParseTasks(project, byFieldId, ct);
        model.Dependencies = MppImporterHelpers.ParseDependencies(project, ct);

        return Result.Success(model);
    }
}