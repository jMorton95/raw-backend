namespace RAWAPI.Features;

public interface IEndpoint
{
    static abstract void Map(IEndpointRouteBuilder app);
}