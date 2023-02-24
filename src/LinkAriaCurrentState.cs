namespace Tailwind.Css.TagHelpers;

public enum LinkAriaCurrentState
{
    /// <summary>
    /// Represents the current item within a set.
    /// </summary>
    /// <value>true</value>
    True = 0,

    /// <summary>
    /// Represents the current page within a set of pages such as the link to the current document in a breadcrumb.
    /// </summary>
    /// <value>page</value>
    Page = 1,

    /// <summary>
    /// Represents the current step within a process such as the current step in an enumerated multi step checkout flow .
    /// </summary>
    /// <value>step</value>
    Step = 2,

    /// <summary>
    /// Does not represent the current item within a set.
    /// </summary>
    /// <value>false</value>
    False = 3,

    /// <summary>
    /// Represents the current location within an environment or context such as the image that is visually highlighted as the current component of a flow chart.
    /// </summary>
    /// <value>location</value>
    Location = 4,

    /// <summary>
    /// Represents the current date within a collection of dates such as the current date within a calendar.
    /// </summary>
    /// <value>date</value>
    Date = 5,

    /// <summary>
    /// Represents the current time within a set of times such as the current time within a timetable.
    /// </summary>
    /// <value>time</value>
    Time = 6,
}
