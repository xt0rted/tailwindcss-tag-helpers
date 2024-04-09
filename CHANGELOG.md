# Changelog

## Unreleased

## [0.6.0](https://github.com/xt0rted/tailwindcss-tag-helpers/compare/v0.5.0...v0.6.0) - 2024-04-09

- Dropped support for .NET 6
- Dropped support for .NET 7
- Added support for .NET 8
- Added `MergeDefaultClassTagHelper` which will merge the `default-class` attribute with the `class` attribute

## [0.5.0](https://github.com/xt0rted/tailwindcss-tag-helpers/compare/v0.4.0...v0.5.0) - 2023-02-24

- Dropped support for .NET Core 3.1
- Added support for .NET 7
- Added missing `aria-current` states: `false`, `location`, `date`, and `time`

## [0.4.0](https://github.com/xt0rted/tailwindcss-tag-helpers/compare/v0.3.0...v0.4.0) - 2022-10-13

### Fixed

- Fixed bug in `LinkTagHelper` that caused it to not match correctly when using `href="..."`

## [0.3.0](https://github.com/xt0rted/tailwindcss-tag-helpers/compare/v0.2.0...v0.3.0) - 2022-10-11

### Added

- Added `ValidationStatusTagHelper` which will allow styling elements based on a field's validation status (e.g. style a form input red when it contains errors)

### Fixed

- Base class extraction correctly handles `IHtmlContent` attribute types

## [0.2.0](https://github.com/xt0rted/tailwindcss-tag-helpers/compare/v0.1.0...v0.2.0) - 2022-09-24

> **Note**: This version drops support for .NET 5 which is no longer supported.

### Added

- Added `LinkAriaCurrentStateTagHelper` which will add an [`aria-current="*"`](https://www.w3.org/TR/wai-aria-1.1/#aria-current) attribute to links that are processed by `LinkTagHelper` and include an `aria-current-state="*"` attribute.

### Updated

- Immediate child elements of `LinkTagHelper` that have `default-class` or `current-class` attributes will also have their class lists merged.
- Switched from [actions/setup-dotnet](https://github.com/actions/setup-dotnet) to [xt0rted/setup-dotnet](https://github.com/xt0rted/setup-dotnet).
- Added [dotnet run-script](https://github.com/xt0rted/dotnet-run-script) for build and test scripts.

## [0.1.0](https://github.com/xt0rted/tailwindcss-tag-helpers/releases/tag/v0.1.0) - 2021-12-19

- Added `LinkTagHelper`
- Include `README.md` in the nupkg
- Target .NET Core 3.1, .NET 5, and .NET 6
