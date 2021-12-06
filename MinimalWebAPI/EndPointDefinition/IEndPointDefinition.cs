namespace MinimalWebAPI.EndPointDefinition
{
    public interface IEndPointDefinition
    {
        void DefineServices(IServiceCollection services);
        void DefineEndPoints(WebApplication app);
    }
}
