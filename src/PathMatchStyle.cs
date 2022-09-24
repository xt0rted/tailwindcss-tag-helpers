namespace Tailwind.Css.TagHelpers;

using Microsoft.AspNetCore.Http;

public enum PathMatchStyle
{
    /// <summary>
    /// Matches the current url to the tag's url using <see cref="PathString.Equals"/>.
    /// </summary>
    Full = 0,

    /// <summary>
    /// Matches the current url to the tag's url using <see cref="PathString.StartsWithSegments"/>.
    /// </summary>
    Base = 1,
}
