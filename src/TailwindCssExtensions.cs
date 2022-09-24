namespace Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;

using Tailwind.Css.TagHelpers;

public static class TailwindCssExtensions
{
    public static IServiceCollection AddTailwindCssTagHelpers(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        services.Configure<TagOptions>(configuration.GetSection("TailwindCss"));

        return services;
    }

    public static IServiceCollection AddTailwindCssTagHelpers(this IServiceCollection services, Action<TagOptions> configureOptions)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configureOptions is null) throw new ArgumentNullException(nameof(configureOptions));

        services.Configure(configureOptions);

        return services;
    }
}
