using FluentAssertions;

using Sour.Core.Tests.Unit.Utilities;

using Xunit;

namespace Sour.Core.Tests.Unit
{
    public class ExampleTests
    {
        [Theory]
        [InlineData("Background")]
        [InlineData("ScenarioOutline")]
        [InlineData("SingleScenario")]
        [InlineData("SingleScenarioWithTable")]
        public void Scenario(string scenario)
        {
            var expectedOutput = Resource.ReadResource($"{scenario}.md");
            var stream = Resource.LoadAsStream($"{scenario}.feature");
            var document = ParseUtils.Parse(stream, "en");

            var markdown = Markdown.From(document);

            markdown.Should().Be(expectedOutput);
        }
    }
}