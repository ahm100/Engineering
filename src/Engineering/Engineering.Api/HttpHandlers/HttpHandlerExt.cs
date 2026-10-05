using MediatR;

namespace Engineering.Api.HttpHandlers;

/// <summary>
/// 
/// </summary>
public static class HttpHandlerExt
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="endpoints"></param>
    /// <param name="template"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapHttpGet<TRequest>(this IEndpointRouteBuilder endpoints, string template,
        Func<MapHttpConfiguration>? config = null)
        where TRequest : IHttpRequest
    {
        var endpoint = endpoints.MapGet(template,
            async (IMediator mediator, [AsParameters] TRequest request, CT ct) =>
            await mediator.Send(request, ct));

        if (config is not null)
        {
            SetConfiguration(endpoint, config.Invoke());
        }

        return endpoints;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="endpoints"></param>
    /// <param name="template"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapHttpGet<TRequest, TResponse>(this IEndpointRouteBuilder endpoints, string template,
    Func<MapHttpConfiguration>? config = null)
    where TRequest : IHttpRequest
    {
        var endpoint = endpoints.MapGet(template,
            async (IMediator mediator, [AsParameters] TRequest request, CT ct) =>
            await mediator.Send(request, ct)).Produces<TResponse>();

        if (config is not null)
        {
            SetConfiguration(endpoint, config.Invoke());
        }

        return endpoints;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="endpoints"></param>
    /// <param name="template"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapHttpPost<TRequest>(this IEndpointRouteBuilder endpoints, string template, Func<MapHttpConfiguration>? config = null)
        where TRequest : IHttpRequest
    {
        var endpoint = endpoints.MapPost(template,
            async (IMediator mediator, [FromBody] TRequest request, CT ct) =>
            await mediator.Send(request, ct));

        if (config is not null)
        {
            SetConfiguration(endpoint, config.Invoke());
        }

        return endpoints;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="endpoints"></param>
    /// <param name="template"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapHttpPost<TRequest, TResponse>(this IEndpointRouteBuilder endpoints, string template, Func<MapHttpConfiguration>? config = null)
    where TRequest : IHttpRequest
    {
        var endpoint = endpoints.MapPost(template,
            async (IMediator mediator, [FromBody] TRequest request, CT ct) =>
            await mediator.Send(request, ct)).Produces<TResponse>();

        if (config is not null)
        {
            SetConfiguration(endpoint, config.Invoke());
        }

        return endpoints;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="endpoints"></param>
    /// <param name="template"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapHttpPut<TRequest>(this IEndpointRouteBuilder endpoints, string template,
        Func<MapHttpConfiguration>? config = null)
        where TRequest : IHttpRequest
    {
        var endpoint = endpoints.MapPut(template,
            async (IMediator mediator, [FromBody] TRequest request, CT ct) =>
            await mediator.Send(request, ct));

        if (config is not null)
        {
            SetConfiguration(endpoint, config.Invoke());
        }

        return endpoints;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="endpoints"></param>
    /// <param name="template"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapHttpPut<TRequest, TResponse>(this IEndpointRouteBuilder endpoints, string template,
    Func<MapHttpConfiguration>? config = null)
    where TRequest : IHttpRequest
    {
        var endpoint = endpoints.MapPut(template,
            async (IMediator mediator, [FromBody] TRequest request, CT ct) =>
            await mediator.Send(request, ct)).Produces<TResponse>();

        if (config is not null)
        {
            SetConfiguration(endpoint, config.Invoke());
        }

        return endpoints;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <param name="endpoints"></param>
    /// <param name="template"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapHttpDelete<TRequest>(this IEndpointRouteBuilder endpoints, string template,
        Func<MapHttpConfiguration>? config = null)
        where TRequest : IHttpRequest
    {
        var endpoint = endpoints.MapDelete(template,
            async (IMediator mediator, [AsParameters] TRequest request, CT ct) =>
            await mediator.Send(request, ct));

        if (config is not null)
        {
            SetConfiguration(endpoint, config.Invoke());
        }

        return endpoints;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="endpoints"></param>
    /// <param name="template"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapHttpDelete<TRequest, TResponse>(this IEndpointRouteBuilder endpoints, string template,
    Func<MapHttpConfiguration>? config = null)
    where TRequest : IHttpRequest
    {
        var endpoint = endpoints.MapDelete(template,
            async (IMediator mediator, [AsParameters] TRequest request, CT ct) =>
            await mediator.Send(request, ct)).Produces<TResponse>();

        if (config is not null)
        {
            SetConfiguration(endpoint, config.Invoke());
        }

        return endpoints;
    }

    private static void SetConfiguration(IEndpointConventionBuilder endpoint, MapHttpConfiguration configuration)
    {
        if (configuration.AllowAnonymous)
        {
            endpoint.AllowAnonymous();
        }
        else
        {
            if (configuration.Policy is null)
            {
                endpoint.RequireAuthorization();
            }
            else
            {
                configuration.Policy.Apply(endpoint);
            }
        }

        if (configuration.Description is not null)
        {
            endpoint.WithDescription(configuration.Description);
        }

        if (configuration.Name is not null)
        {
            endpoint.WithName(configuration.Name);
        }

        if (configuration.Summary is not null)
        {
            endpoint.WithSummary(configuration.Summary);
        }

        endpoint.WithOpenApi();
    }
}

