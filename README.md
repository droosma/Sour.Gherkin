# Sour.Gherkin

A [dotnet tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) for generating markdown from [gherkin](https://cucumber.io/docs/gherkin/) feature files

*This project is really beta usage at your own risk.*

## Install

To install run the following command: `dotnet tool install --global Sour.Gherkin`

## Usage

As the tool recursively searches for `.feature` files running `sour` after installing the tool in the root of you project should generate the markdown files by default in `docs/spec`.

To view options run `sour --help`
