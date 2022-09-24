namespace Tailwind.Css.TagHelpers;

public class TagOptions
{
    /// <summary>
    /// Add html comments before the target tag with base, current, and default classes to help make development/debugging easier.
    /// </summary>
    /// <remarks>This is off by default.</remarks>
    public bool IncludeComments { get; set; }
}
