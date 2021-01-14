using System;

using FluentAssertions;

using Gherkin.Ast;

using Sour.Core.Tests.Unit.Utilities;
using Sour.Export.Markdown;

using Xunit;

namespace Sour.Core.Tests.Unit
{
    public class StepTests
    {
        [Fact]
        public void AsMarkdown_GivenAStep_ReturnsStepContentAsMarkdown()
        {
            Step step = A.Step;

            var result = step.AsMarkdown();

            result.Should().Contain($"_{step.Keyword}_");
            result.Should().Contain(step.Text);
            result.Should().StartWith(">");
        }

        [Fact]
        public void AsMarkdown_GivenAStep_ReturnsStepEndingWithDoubleSpace()
        {
            Step step = A.Step;

            var result = step.AsMarkdown();

            result.Should().EndWith("  " + Environment.NewLine);
        }

        [Fact]
        public void AsMarkdown_GivenKeywordWithWhitespace_removesWhitespace()
        {
            const string trimmedKeyword = "keyword";
            Step step = A.Step.WithKeyword($"  {trimmedKeyword}  ");

            var result = step.AsMarkdown();

            result.Should().Contain($"_{trimmedKeyword}_");
        }

        [Fact]
        public void AsMarkdown_GivenTextWithWhitespace_removesWhitespace()
        {
            const string trimmedText = "text";
            Step step = A.Step.WithText($"  {trimmedText}  ");

            var result = step.AsMarkdown();

            result.Should().Contain($"{trimmedText}");
        }
    }
}