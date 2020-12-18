using Gherkin.Ast;

namespace Sour.Core.Tests.Unit.Utilities.Builders
{
    public class StepBuilder
    {
        private Location _location = A.Location;
        private StepArgument _stepArgument = null;
        private string _keyword = "keyword";
        private string _text = "text";
        
        public Step Build() => new(_location, _keyword, _text, _stepArgument);

        public static implicit operator Step(StepBuilder builder)
            => builder.Build();

        private StepBuilder()
        {
        }

        public StepBuilder WithText(string text)
        {
            _text = text;
            return this;
        }

        public StepBuilder WithKeyword(string keyword)
        {
            _keyword = keyword;
            return this;
        }

        public static StepBuilder Create => new();
    }
}