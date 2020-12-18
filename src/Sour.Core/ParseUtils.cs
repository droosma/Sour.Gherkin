using System.IO;

using Gherkin;
using Gherkin.Ast;

namespace Sour.Core
{
    public static class ParseUtils
    {
        public static GherkinDocument Parse(string file, string languageCode)
        {
            using var featureFileReader = new StreamReader(file);
            
            return Parse(featureFileReader, languageCode);
        }

        public static GherkinDocument Parse(TextReader reader, string languageCode)
        {
            var parser = new Parser();
            var dialect = new GherkinDialectProvider(languageCode);
            var tokenScanner = new TokenScanner(reader);
            var tokenMatcher = new TokenMatcher(dialect);
            
            return parser.Parse(tokenScanner, tokenMatcher);
        }
    }
}