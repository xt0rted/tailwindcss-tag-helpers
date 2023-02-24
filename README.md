# <img src="assets/logo.svg" align="left" height="45"> tailwindcss-tag-helpers

[![CI build status](https://github.com/xt0rted/tailwindcss-tag-helpers/workflows/CI/badge.svg)](https://github.com/xt0rted/tailwindcss-tag-helpers/actions?query=workflow%3ACI)
[![NuGet Package](https://img.shields.io/nuget/v/TailwindCssTagHelpers?logo=nuget)](https://www.nuget.org/packages/TailwindCssTagHelpers)
[![GitHub Package Registry](https://img.shields.io/badge/github-package_registry-yellow?logo=nuget)](https://nuget.pkg.github.com/xt0rted/index.json)
[![Project license](https://img.shields.io/github/license/xt0rted/tailwindcss-tag-helpers)](LICENSE)

ASP.NET tag helpers to make working with [Tailwind CSS](https://tailwindcss.com/) and [Tailwind UI](https://tailwindui.com/) easier.

Included tag helpers:

Name | Description
:-- | :--
[LinkAriaCurrentStateTagHelper](#linkariacurrentstatetaghelper) | Adds the `aria-current` attribute to links that are processed by `LinkTagHelper` and include an `aria-current-state` attribute
[LinkTagHelper](#linktaghelper) | Applies css classes based on the `href` value and current url
[ValidationStatusTagHelper](#validationstatustaghelper) | Applies css classes based on the validation status of a model value

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

Name | Default value | Description
:-- | :-- | :--
`IncludeComments` | `false` | Add html comments before the target tag with base, current, and default classes to help make development/debugging easier.

## Usage

### LinkAriaCurrentStateTagHelper

Adds the [`aria-current="*"`](https://www.w3.org/TR/wai-aria-1.1/#aria-current) attribute to links that are processed by the `LinkTagHelper` and include an `aria-current-state="*"` attribute.

```html
<a
  asp-area="" asp-controller="Home" asp-action="Index"
  class="px-3 py-2 text-sm font-medium rounded-md"
  default-class="text-gray-300 hover:bg-gray-700 hover:text-white"
  current-class="text-white bg-gray-900"
  aria-current-state="Page"
>
  Home
</a>
```

Will output:

```html
<a
  href="/"
  class="px-3 py-2 text-sm font-medium rounded-md text-white bg-gray-900"
  aria-current="page"
>
  Home
</a>
```

#### Attributes

Name | Value | Description
:-- | :-- | :--
`aria-current-state` | `True`, `Page` (default), `Step`, `False`, `Location`, `Date`, `Time` | The value to use for the `aria-current` attribute.

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

Will output:

```html
<a
  href="/"
  class="px-3 py-2 text-sm font-medium rounded-md text-white bg-gray-900"
>
  Home
  <span
    class="ml-auto inline-block py-0.5 px-3 text-xs rounded-full bg-gray-50"
  >
    5
  </span>
</a>
```

#### Attributes

Name | Value | Description
:-- | :-- | :--
`current-class` | `string` | The css classes to apply if the link matches the current url.
`default-class` | `string` | The css classes to apply if the link doesn't match the current url.
`match` | `Full` (default) or `Base` | The method to use when matching the link to the current url.

### ValidationStatusTagHelper

The validation status tag helper will check the validation status of the model value passed to the `asp-for` attribute and apply one of two sets of css classes.

The `default-class` list will be applied if the field is valid, and the `error-class` list will be applied if the field is invalid.

```html
<input
  type="email"
  asp-for="Input.Email"
  class="block w-full rounded-md pl-10 sm:text-sm transition"
  default-class="border-gray-300 focus:border-blue-400 focus:ring-blue-400"
  error-class="border-red-300 text-red-900 placeholder-red-300 focus:border-red-500 focus:outline-none focus:ring-red-500"
>
```

This tag helper target element is `*` so it can be used with any element, including web components and other tag helpers.

```html
<heroicon-solid
  icon="AtSymbol"
  class="h-5 w-5"
  asp-for="Input.Email"
  default-class="text-gray-400"
  error-class="text-red-400"
/>
```

#### Attributes

Name | Value | Description
:-- | :-- | :--
`asp-for` | `ModelExpression` | An expression to be evaluated against the current model.
`default-class` | `string` | The classes to apply when the form field doesn't have any errors.
`error-class` | `string` | `default-class` | `string` | The classes to apply when the form field doesn't have any errors.

## Development

This project uses the [run-script](https://github.com/xt0rted/dotnet-run-script) dotnet tool to manage its build and test scripts.
To use this you'll need to run `dotnet tool install` and then `dotnet r` to see the available commands or look at the `scripts` section in the [global.json](global.json).
