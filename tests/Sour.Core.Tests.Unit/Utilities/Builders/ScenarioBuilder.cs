using Gherkin.Ast;

namespace Sour.Core.Tests.Unit.Utilities.Builders
{
    public class ScenarioBuilder
    {
        private string _description = string.Empty;
        private readonly string _keyword = "keyword";
        private readonly string _name = "scenario name";
        private readonly Step[] _steps = {A.Step};
        private readonly Tag[] _tags = {};
        private readonly Examples[] _examples = {};
        private readonly Location _location = A.Location;

        private ScenarioBuilder()
        {
        }

        public static ScenarioBuilder Create => new();

        public Scenario Build()
            => new(_tags, _location, _keyword, _name, _description, _steps, _examples);

        public static implicit operator Scenario(ScenarioBuilder builder)
            => builder.Build();

        public ScenarioBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }
    }
}