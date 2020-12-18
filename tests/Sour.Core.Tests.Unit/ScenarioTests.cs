using System;

using FluentAssertions;

using Gherkin.Ast;

using Sour.Core.Tests.Unit.Utilities;

using Xunit;

namespace Sour.Core.Tests.Unit
{
    public class ScenarioTests
    {
        [Fact]
        public void AsMarkdown_GivenScenarioWithDescription_IncludesDescriptionInOutput()
        {
            const string description = "description";
            Scenario scenario = A.Scenario.WithDescription(description);

            var result = scenario.AsMarkdown();

            result.Should().Contain($"{Environment.NewLine}" +
                                    $"{description}" +
                                    $"{Environment.NewLine}");
        }

        [Fact]
        public void METHOD_CONDITION_RESULT()
        {
            Scenario scenario = A.Scenario;

            var result = scenario.AsMarkdown();

            result.Should().Contain($"{Environment.NewLine}" +
                                    $"## {scenario.Name}" +
                                    $"{Environment.NewLine}");
        }
    }
}