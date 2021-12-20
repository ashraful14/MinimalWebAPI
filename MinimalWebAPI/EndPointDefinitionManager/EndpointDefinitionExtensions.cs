namespace MinimalWebAPI.EndPointDefinitionManager
{
    public static class EndpointDefinitionExtensions 
    {
        public static void AddEndpointDefinitions(this IServiceCollection services, params Type[] scanMarkers)
        {
            var endpointDefinitions = new List<IEndPointDefinition>();
            foreach (var scanMarker in scanMarkers)
            {
                endpointDefinitions.AddRange(scanMarker.Assembly.ExportedTypes.Where(x => typeof(IEndPointDefinition)
                .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
               .Select(Activator.CreateInstance).Cast<IEndPointDefinition>());
            }
            foreach (var endpointDefinition in endpointDefinitions)
            {
                endpointDefinition.DefineServices(services);
            }
            services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndPointDefinition>);
        }

        public static void UseEndpointDefinitions(this WebApplication app)
        {
            var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndPointDefinition>>();

            foreach (var definition in definitions)
            {
                definition.DefineEndPoints(app);
            }
        }
    }
}
