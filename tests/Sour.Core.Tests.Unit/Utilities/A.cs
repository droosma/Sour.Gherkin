using Gherkin.Ast;

using Sour.Core.Tests.Unit.Utilities.Builders;

namespace Sour.Core.Tests.Unit.Utilities
{
    public static class A
    {
        public static Location Location => new();
        public static StepBuilder Step => StepBuilder.Create;
        public static ScenarioBuilder Scenario => ScenarioBuilder.Create;
    }
}