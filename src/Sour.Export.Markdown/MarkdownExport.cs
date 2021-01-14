using Gherkin.Ast;

namespace Sour.Export.Markdown
{
    public class MarkdownExport : Core.Export
    {
        public string From(GherkinDocument document)
            => document.AsMarkdown();
    }
}