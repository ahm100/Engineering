using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.Seasons.Models.GetByNames;

public record GetByNamesResponse(
List<Season> Data
    );
