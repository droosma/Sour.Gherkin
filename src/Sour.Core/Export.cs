using Gherkin.Ast;

namespace Sour.Core
{
    public interface Export
    {
        public string From(GherkinDocument document);
    }
}