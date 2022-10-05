namespace Sample;

using System.Text.Json;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

public static class IHtmlHelperExtensions
{
    private static readonly JsonSerializerOptions serializerOptions = new()
    {
        WriteIndented = true,
    };

    public static IHtmlContent Dump<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, TResult source)
    {
        ArgumentNullException.ThrowIfNull(htmlHelper);

        var json = JsonSerializer.Serialize(source, serializerOptions);

        return new HtmlString(json);
    }
}
