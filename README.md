# tailwindcss-tag-helpers

[![CI build status](https://github.com/xt0rted/tailwindcss-tag-helpers/workflows/CI/badge.svg)](https://github.com/xt0rted/tailwindcss-tag-helpers/actions?query=workflow%3ACI)
[![GitHub Package Registry](https://img.shields.io/badge/github-package_registry-yellow?logo=nuget)](https://nuget.pkg.github.com/xt0rted/index.json)
[![Project license](https://img.shields.io/github/license/xt0rted/tailwindcss-tag-helpers)](LICENSE)

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

## Usage

### LinkTagHelper

The link tag helper will compare the `href` to the current url and apply one of two sets of css classes.

The `default-class` list will be applied if the urls don't match, and the `current-class` list will be applied if the urls do match.

The naming of these attributes aligns with the comments found in the Tailwind UI templates and the `-class` suffix allows the attributes to automatically work with [Headwind](https://marketplace.visualstudio.com/items?itemName=heybourn.headwind).

```html
<a
  asp-area="" asp-controller="Home" asp-action="Index"
  class="px-3 py-2 text-sm font-medium rounded-md"
  default-class="text-gray-300 hover:bg-gray-700 hover:text-white"
  current-class="text-white bg-gray-900"
>
  Home
</a>
```
