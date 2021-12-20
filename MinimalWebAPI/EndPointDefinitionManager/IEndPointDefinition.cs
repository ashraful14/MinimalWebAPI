namespace MinimalWebAPI.EndPointDefinitionManager
{
    public interface IEndPointDefinition
    {
        void DefineServices(IServiceCollection services);
        void DefineEndPoints(WebApplication app);
    }
}
