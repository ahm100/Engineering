namespace Engineering.Application.Services.Seasons.Models.GetSeasonById;

public record GetSeasonByIdRequest(
    long Id
     ) : IHttpRequest;