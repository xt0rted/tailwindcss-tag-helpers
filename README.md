# tailwindcss-tag-helpers

ASP.NET tag helpers to make working with [Tailwind CSS](https://tailwindcss.com/) and [Tailwind UI](https://tailwindui.com/) easier.

## Installation

```terminal
dotnet add package TailwindCssTagHelper
```

## Setup

In your `_ViewImports.cshtml` add:

```html
@addTagHelper *, TailwindCssTagHelper
```

In your `Startup.cs` add:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTailwindCss(Configuration);
}
```

In your `appsettings.json` add:

```json
{
  "TailwindCss": {
    "IncludeComments": true
  }
}
```
