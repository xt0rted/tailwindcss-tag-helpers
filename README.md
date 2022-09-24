# <img src="assets/logo.svg" align="left" height="45"> tailwindcss-tag-helpers

[![CI build status](https://github.com/xt0rted/tailwindcss-tag-helpers/workflows/CI/badge.svg)](https://github.com/xt0rted/tailwindcss-tag-helpers/actions?query=workflow%3ACI)
[![NuGet Package](https://img.shields.io/nuget/v/TailwindCssTagHelpers?logo=nuget)](https://www.nuget.org/packages/TailwindCssTagHelpers)
[![GitHub Package Registry](https://img.shields.io/badge/github-package_registry-yellow?logo=nuget)](https://nuget.pkg.github.com/xt0rted/index.json)
[![Project license](https://img.shields.io/github/license/xt0rted/tailwindcss-tag-helpers)](LICENSE)

ASP.NET tag helpers to make working with [Tailwind CSS](https://tailwindcss.com/) and [Tailwind UI](https://tailwindui.com/) easier.

## Installation

```terminal
dotnet add package TailwindCssTagHelpers
```

## Setup

In your `_ViewImports.cshtml` add:

```html
@addTagHelper *, TailwindCssTagHelpers
```

In your `Startup.cs` add:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTailwindCssTagHelpers(Configuration);
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

## Settings

Name | Default Value | Description
:-- | :-- | :--
`IncludeComments` | `false` | Add html comments before the target tag with base, current, and default classes to help make development/debugging easier.

## Usage

### LinkTagHelper

The link tag helper will compare the `href` to the current url and apply one of two sets of css classes.

The `default-class` list will be applied if the urls don't match, and the `current-class` list will be applied if the urls do match.

If an immediate child element has a `default-class` or `current-class` attribute it will also have its class lists merged.

The naming of these attributes aligns with the comments found in the Tailwind UI templates and the `-class` suffix allows the attributes to automatically work with [Headwind](https://marketplace.visualstudio.com/items?itemName=heybourn.headwind).

The matching method can be either `Full` (default) which ensures the link path and current path are the same, or `Base` which ensures the link path starts with, or is the same as, the current path.

> **Note**: Query string values are not used for either method of matching.

```html
<a
  asp-area="" asp-controller="Home" asp-action="Index"
  class="px-3 py-2 text-sm font-medium rounded-md"
  default-class="text-gray-300 hover:bg-gray-700 hover:text-white"
  current-class="text-white bg-gray-900"
  match="Base"
>
  Home
  <span
    class="ml-auto inline-block py-0.5 px-3 text-xs rounded-full"
    current-class="bg-gray-50"
    default-class="bg-gray-200 text-gray-600 group-hover:bg-gray-200"
  >
    5
  </span>
</a>
```
