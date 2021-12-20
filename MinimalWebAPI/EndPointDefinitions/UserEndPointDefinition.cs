namespace MinimalWebAPI.EndPointDefinitions
{
    public class UserEndPointDefinition : IEndPointDefinition
    {
        public void DefineEndPoints(WebApplication app)
        {
            app.MapPost("/token", (UserInfo userInfo,IUserRepository service) =>
            {
                return service.GetToken(userInfo);
            });
        }

        public void DefineServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
