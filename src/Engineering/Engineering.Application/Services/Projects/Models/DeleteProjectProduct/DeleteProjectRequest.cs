namespace Engineering.Application.Services.Projects.Models.DeleteProjectProduct;

public record DeleteProjectProductRequest(
    List<long> Ids
     ) : IHttpRequest;
