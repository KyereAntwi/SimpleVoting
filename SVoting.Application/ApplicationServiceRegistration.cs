
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SVoting.Application;

public static class ApplicationServiceRegistration
{
	public static IServiceCollection AddApplicationService(this IServiceCollection services)
	{
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

		return services;
	}
}

