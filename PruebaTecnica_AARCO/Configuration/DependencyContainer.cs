using MediatR;

namespace PruebaTecnica_AARCO.Configuration
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(IInPutPort<,>).Assembly);

            return services;
        }
    }
}
