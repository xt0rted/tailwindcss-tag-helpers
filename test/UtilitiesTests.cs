namespace Tailwind.Css.TagHelpers;

public class UtilitiesTests
{
    [Theory]
    [InlineData("class1 class2 class3")]
    [InlineData("class1\tclass2\tclass3")]
    [InlineData("class1\nclass2\nclass3")]
    [InlineData("class1\fclass2\fclass3")]
    [InlineData("class1\rclass2\rclass3")]
    [InlineData("class1\t\n\f\rclass2\t\n\f\rclass3\t\n\f\r")]
    public void SplitsClassList_handles_a_list_of_classes(string classList)
    {
        // Given / When
        var result = Utilities.SplitClassList(classList);

        // Then
        result.ShouldNotBeNull();
        result.Length.ShouldBe(3);
        result.ShouldBe(new[]
            {
                "class1",
                "class2",
                "class3",
            });
    }
}
