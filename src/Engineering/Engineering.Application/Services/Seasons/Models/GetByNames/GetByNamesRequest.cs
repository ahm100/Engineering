namespace Engineering.Application.Services.Seasons.Models.GetByNames;

public record FindSeasonByNamesOrCodesQuery(
    List<string> Names
    );