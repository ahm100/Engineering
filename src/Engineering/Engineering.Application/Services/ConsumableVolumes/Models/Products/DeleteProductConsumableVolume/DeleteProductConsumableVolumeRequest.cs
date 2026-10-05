namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.DeleteProductConsumableVolume;

public record DeleteConsumableVolumeProductRequest(
    long Id
     ) : IHttpRequest;
