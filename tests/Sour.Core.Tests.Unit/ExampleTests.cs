using FluentAssertions;

using Sour.Core.Tests.Unit.Utilities;
using Sour.Export.Markdown;

using Xunit;

namespace Sour.Core.Tests.Unit
{
    public class ExampleTests
    {
        private readonly MarkdownExport _markdownExport;

        public ExampleTests()
        {
            _markdownExport = new MarkdownExport();
        }

        [Theory]
        [InlineData("Background")]
        [InlineData("ScenarioOutline")]
        [InlineData("SingleScenario")]
        [InlineData("SingleScenarioWithTable")]
        public void Scenario(string scenario, string language = "en")
        {
            var expectedOutput = Resource.ReadResource($"{scenario}.md");
            var stream = Resource.LoadAsStream($"{scenario}.feature");
            var document = ParseUtils.Parse(stream, language);

            var markdown = _markdownExport.From(document);

            markdown.Should().Be(expectedOutput);
        }
    }
}