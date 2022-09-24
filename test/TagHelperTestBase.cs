namespace Tailwind.Css.TagHelpers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

public abstract class TagHelperTestBase
{
    protected static ViewContext MakeViewContext(string? requestPath = null)
    {
        var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
        if (requestPath is not null)
        {
            actionContext.HttpContext.Request.Path = new PathString(requestPath);
        }

        var metadataProvider = new EmptyModelMetadataProvider();
        var viewData = new ViewDataDictionary(metadataProvider, new ModelStateDictionary());

        return new ViewContext(
            actionContext,
            A.Fake<IView>(),
            viewData,
            A.Fake<ITempDataDictionary>(),
            TextWriter.Null,
            new HtmlHelperOptions());
    }

    protected static TagHelperContext MakeTagHelperContext(string tagName, TagHelperAttributeList? attributes = null)
    {
        attributes ??= new TagHelperAttributeList();

        return new TagHelperContext(
            tagName,
            allAttributes: attributes,
            items: new Dictionary<object, object>(),
            uniqueId: Guid.NewGuid().ToString("N"));
    }

    protected static TagHelperOutput MakeTagHelperOutput(string tagName, TagHelperAttributeList? attributes = null)
    {
        attributes ??= new TagHelperAttributeList();

        return new TagHelperOutput(
            tagName,
            attributes,
            getChildContentAsync: (_, __) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
    }
}
