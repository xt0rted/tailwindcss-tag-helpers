namespace Tailwind.Css.TagHelpers;

using System.Runtime.Serialization;

[Serializable]
public class ParentContextNotFoundException : Exception
{
    public ParentContextNotFoundException()
        : this("No context was set in the parent element")
    {
    }

    public ParentContextNotFoundException(string? message)
        : base(message)
    {
    }

    public ParentContextNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected ParentContextNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
