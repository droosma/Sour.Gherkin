using Gherkin.Ast;

namespace Sour.Core
{
    public static class Markdown
    {
        public static string From(GherkinDocument document)
            => document.AsMarkdown();
    }
}