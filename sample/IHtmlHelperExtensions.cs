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

    public static IHtmlContent ErrorMessage<TModel>(this IHtmlHelper<TModel> htmlHelper, string expression)
    {
        ArgumentNullException.ThrowIfNull(htmlHelper);

        htmlHelper.ViewContext.ModelState.TryGetValue(expression, out var entry);

        if (entry?.Errors.Count > 0)
        {
            var error = entry.Errors.FirstOrDefault(e => !string.IsNullOrEmpty(e.ErrorMessage)) ?? entry.Errors[0];

            return new HtmlString(error.ErrorMessage);
        }

        return HtmlString.Empty;
    }

    public static bool HasError<TModel>(this IHtmlHelper<TModel> htmlHelper, string expression)
    {
        ArgumentNullException.ThrowIfNull(htmlHelper);

        htmlHelper.ViewContext.ModelState.TryGetValue(expression, out var input);

        return input?.Errors.Count > 0;
   }
}
