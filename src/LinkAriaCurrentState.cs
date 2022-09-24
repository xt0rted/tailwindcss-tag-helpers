namespace Tailwind.Css.TagHelpers;

public enum LinkAriaCurrentState
{
    True = 0,

    /// <summary>
    /// A <c>page</c> token used to indicate a link within a set of pagination links, where the link is visually styled to represent the currently-displayed page.
    /// </summary>
    Page = 1,

    /// <summary>
    /// A <c>step</c> token used to indicate a link within a step indicator for a step-based process, where the link is visually styled to represent the current step.
    /// </summary>
    Step = 2,
}
