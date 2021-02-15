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
