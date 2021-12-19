namespace Tailwind.Css.TagHelpers;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

public class LinkTagHelperTests
{
    [Fact]
    public void Should_do_nothing_if_options_are_not_set()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
                { "href", "/foo" },
            });
        var viewContext = MakeViewContext("/foo");

        var helper = new LinkTagHelper
        {
            ViewContext = viewContext,
        };

        // When
        helper.Process(context, output);

        // Then
        output.Attributes["class"].ShouldNotBeNull();

        var classList = output.Attributes["class"].Value as string;
        classList.ShouldNotBeNull();
        classList.ShouldBe("text-black");
    }

    [Fact]
    public void Should_add_default_classes_when_not_the_current_url()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
                { "href", "/foo" },
            });
        var viewContext = MakeViewContext("/bar");

        var helper = new LinkTagHelper
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            ViewContext = viewContext,
        };

        // When
        helper.Process(context, output);

        // Then
        output.Attributes["class"].ShouldNotBeNull();

        var classList = output.Attributes["class"].Value as HtmlString;
        classList.ShouldNotBeNull();
        classList.Value.ShouldBe("text-black bg-white underline");
    }

    [Fact]
    public void Should_add_current_classes_when_the_current_url()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
            });
        var output = MakeTagHelperOutput(
            tagName: "a",
            new TagHelperAttributeList
            {
                { "class", "text-black" },
                { "href", "/bar" },
            });
        var viewContext = MakeViewContext("/bar");

        var helper = new LinkTagHelper
        {
            CurrentClass = "bg-orange no-underline",
            DefaultClass = "bg-white underline",
            ViewContext = viewContext,
        };

        // When
        helper.Process(context, output);

        // Then
        output.Attributes["class"].ShouldNotBeNull();

        var classList = output.Attributes["class"].Value as HtmlString;
        classList.ShouldNotBeNull();
        classList.Value.ShouldBe("text-black bg-orange no-underline");
    }

    private static ViewContext MakeViewContext(string? requestPath = null)
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

    private static TagHelperContext MakeTagHelperContext(string tagName, TagHelperAttributeList? attributes = null)
    {
        attributes ??= new TagHelperAttributeList();

        return new TagHelperContext(
            tagName,
            allAttributes: attributes,
            items: new Dictionary<object, object>(),
            uniqueId: Guid.NewGuid().ToString("N"));
    }

    private static TagHelperOutput MakeTagHelperOutput(string tagName, TagHelperAttributeList? attributes = null)
    {
        attributes ??= new TagHelperAttributeList();

        return new TagHelperOutput(
            tagName,
            attributes,
            getChildContentAsync: (_, __) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));
    }
}
