namespace Tailwind.Css.TagHelpers;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

public class FormFieldValidationStatusTagHelperTests : TagHelperTestBase
{
    [Fact]
    public void Should_not_throw_when_for_is_null()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "input",
            new TagHelperAttributeList());
        var output = MakeTagHelperOutput(
            tagName: "input",
            new TagHelperAttributeList());

        var options = Options.Create(new TagOptions());
        var helper = new FormFieldValidationStatusTagHelper(options)
        {
            For = null,
        };

        // When
        var result = () => helper.Process(context, output);

        // Then
        result.ShouldNotThrow();
    }

    [Fact]
    public void Should_return_default_list_when_for_control_not_found_in_modelstate()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "input",
            new TagHelperAttributeList
            {
                { "class", "flex" },
            });
        var output = MakeTagHelperOutput(
            tagName: "input",
            new TagHelperAttributeList
            {
                { "class", "flex" },
            });

        var helper = GetTagHelper("Email");

        // When
        helper.Process(context, output);

        // Then
        var classList = output.Attributes["class"].Value as HtmlString;
        classList.ShouldNotBeNull();
        classList.Value.ShouldBe("flex default");
    }

    [Fact]
    public void Should_return_default_list_when_for_control_found_in_modelstate_but_has_no_errors()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "input",
            new TagHelperAttributeList
            {
                { "class", "flex" },
            });
        var output = MakeTagHelperOutput(
            tagName: "input",
            new TagHelperAttributeList
            {
                { "class", "flex" },
            });

        var helper = GetTagHelper("Email");

        // When
        helper.Process(context, output);

        // Then
        var classList = output.Attributes["class"].Value as HtmlString;
        classList.ShouldNotBeNull();
        classList.Value.ShouldBe("flex default");
    }

    [Fact]
    public void Should_return_error_list_when_for_control_found_in_modelstate_and_has_errors()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "input",
            new TagHelperAttributeList
            {
                { "class", "flex" },
            });
        var output = MakeTagHelperOutput(
            tagName: "input",
            new TagHelperAttributeList
            {
                { "class", "flex" },
            });

        var helper = GetTagHelper("Password");

        // When
        helper.Process(context, output);

        // Then
        var classList = output.Attributes["class"].Value as HtmlString;
        classList.ShouldNotBeNull();
        classList.Value.ShouldBe("flex error");
    }

    [Fact]
    public void Should_write_debug_comment_when_enabled()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "input",
            new TagHelperAttributeList
            {
                { "class", "flex" },
            });
        var output = MakeTagHelperOutput(
            tagName: "input",
            new TagHelperAttributeList
            {
                { "class", "flex" },
            });

        var helper = GetTagHelper("Email", includeComments: true);

        // When
        helper.Process(context, output);

        // Then
        RenderedContent(output.PreElement).ShouldBe(
            """
            <!--
              For: Email
              Base: flex
              Default: default
              Error: error
            -->

            """);
    }

    private FormFieldValidationStatusTagHelper GetTagHelper(
        string name,
        bool includeComments = false)
    {
        var modelState = DefaultModelState();

        var metadataProvider = new EmptyModelMetadataProvider();
        const bool model = false;
        var modelExplorer = metadataProvider.GetModelExplorerForType(typeof(bool), model);
        var modelExpression = new ModelExpression(name, modelExplorer);

        var viewContext = MakeViewContext(model, metadataProvider, modelState);

        var options = Options.Create(new TagOptions { IncludeComments = includeComments });

        return new FormFieldValidationStatusTagHelper(options)
        {
            ViewContext = viewContext,
            For = modelExpression,
            DefaultClass = "default",
            ErrorClass = "error",
        };
    }
}
