namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;

using Tailwind.Css.TagHelpers;

public static class TailwindCssExtensions
{
    public static IServiceCollection AddTailwindCssTagHelpers(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.Configure<TagOptions>(configuration.GetSection("TailwindCss"));

        return services;
    }

    public static IServiceCollection AddTailwindCssTagHelpers(this IServiceCollection services, Action<TagOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureOptions);

        services.Configure(configureOptions);

        return services;
    }
}
