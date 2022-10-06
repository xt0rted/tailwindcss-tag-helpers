namespace Tailwind.Css.TagHelpers;

using System.Text.Encodings.Web;

// https://github.com/dotnet/aspnetcore/blob/d14dd282af26d6cab4617c7533a210ed550a9134/src/WebEncoders/src/Testing/HtmlTestEncoder.cs
public sealed class HtmlTestEncoder : HtmlEncoder
{
    /// <inheritdoc />
    public override int MaxOutputCharactersPerInputCharacter => 1;

    /// <inheritdoc />
    public override string Encode(string value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));

        if (value.Length == 0)
        {
            return string.Empty;
        }

        return $"HtmlEncode[[{value}]]";
    }

    /// <inheritdoc />
    public override void Encode(TextWriter output, char[] value, int startIndex, int characterCount)
    {
        if (output is null) throw new ArgumentNullException(nameof(output));
        if (value is null) throw new ArgumentNullException(nameof(value));

        if (characterCount == 0)
        {
            return;
        }

        output.Write("HtmlEncode[[");
        output.Write(value, startIndex, characterCount);
        output.Write("]]");
    }

    /// <inheritdoc />
    public override void Encode(TextWriter output, string value, int startIndex, int characterCount)
    {
        if (output is null) throw new ArgumentNullException(nameof(output));
        if (value is null) throw new ArgumentNullException(nameof(value));

        if (characterCount == 0)
        {
            return;
        }

        output.Write("HtmlEncode[[");
        output.Write(value.AsSpan(startIndex, characterCount));
        output.Write("]]");
    }

    /// <inheritdoc />
    public override bool WillEncode(int unicodeScalar) => false;

    /// <inheritdoc />
    public override unsafe int FindFirstCharacterToEncode(char* text, int textLength) => -1;

    /// <inheritdoc />
    public override unsafe bool TryEncodeUnicodeScalar(
        int unicodeScalar,
        char* buffer,
        int bufferLength,
        out int numberOfCharactersWritten)
    {
        if (buffer is null)
        {
            throw new ArgumentNullException(nameof(buffer));
        }

        numberOfCharactersWritten = 0;
        return false;
    }
}
