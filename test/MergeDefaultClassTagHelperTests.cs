namespace Tailwind.Css.TagHelpers;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

public class MergeDefaultClassTagHelperTests : TagHelperTestBase
{
    [Fact]
    public void Should_set_to_defaults_when_class_is_empty()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "div");
        var output = MakeTagHelperOutput(
            tagName: "div");

        var options = Options.Create(new TagOptions());
        var helper = new MergeDefaultClassTagHelper(options)
        {
            DefaultClass = "bg-white text-black",
        };

        // When
        helper.Process(context, output);

        // Then
        output.Attributes["class"].ShouldNotBeNull();

        var classList = output.Attributes["class"].Value as HtmlString;
        classList.ShouldNotBeNull();
        classList.Value.ShouldBe("bg-white text-black");
    }

    [Fact]
    public void Should_merge_defaults_into_existing_list()
    {
        // Given
        var context = MakeTagHelperContext(
            tagName: "div",
            new TagHelperAttributeList
            {
                { "class", "flex flex-col" },
            });
        var output = MakeTagHelperOutput(
            tagName: "div",
            new TagHelperAttributeList
            {
                { "class", "flex flex-col" },
            });

        var options = Options.Create(new TagOptions());
        var helper = new MergeDefaultClassTagHelper(options)
        {
            DefaultClass = "bg-white text-black",
        };

        // When
        helper.Process(context, output);

        // Then
        output.Attributes["class"].ShouldNotBeNull();

        var classList = output.Attributes["class"].Value as HtmlString;
        classList.ShouldNotBeNull();
        classList.Value.ShouldBe("flex flex-col bg-white text-black");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("flex flex-col")]
    public void Should_not_emit_comments_when_setting_is_disabled(string? defaultClasses)
    {
        // Given
        var attributeList = new TagHelperAttributeList();

        if (defaultClasses is not null)
        {
            attributeList.Add("class", defaultClasses);
        }

        var context = MakeTagHelperContext(
            tagName: "div",
            attributeList);
        var output = MakeTagHelperOutput(
            tagName: "div",
            attributeList);

        var options = Options.Create(
            new TagOptions
            {
                IncludeComments = false,
            });
        var helper = new MergeDefaultClassTagHelper(options)
        {
            DefaultClass = "bg-white underline",
        };

        // When
        helper.Process(context, output);

        // Then
        output.PreElement.GetContent().ShouldBeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("flex flex-col")]
    public void Should_emit_comments_when_setting_is_enabled(string? defaultClasses)
    {
        // Given
        var attributeList = new TagHelperAttributeList();

        if (defaultClasses is not null)
        {
            attributeList.Add("class", defaultClasses);
        }

        var context = MakeTagHelperContext(
            tagName: "div",
            attributeList);
        var output = MakeTagHelperOutput(
            tagName: "div",
            attributeList);

        var options = Options.Create(
            new TagOptions
            {
                IncludeComments = true,
            });
        var helper = new MergeDefaultClassTagHelper(options)
        {
            DefaultClass = "bg-white text-black",
        };

        // When
        helper.Process(context, output);

        // Then
        output.PreElement.GetContent().ShouldBe(
            $"""
            <!--
              Base: {defaultClasses}
              Default: bg-white text-black
            -->

            """);
    }
}
