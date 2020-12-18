using System;
using System.IO;
using System.Linq;

using CommandLine;

using Sour.Core;

namespace Sour.Gherkin
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                  .WithParsed(options =>
                              {
                                  Console.WriteLine($"Interpreting specs as {options.Language}");
                                  Console.WriteLine($"input path: '{options.ScanPath}'");
                                  var features = FileUtils.FindFeatures(options.ScanPath);
                                  Console.WriteLine(string.Join(Environment.NewLine, features));
                                  Console.WriteLine();
                                  Console.WriteLine($"output path: '{options.OutputPath}'");
                                  if(!Directory.Exists(options.OutputPath))
                                      Directory.CreateDirectory(options.OutputPath);

                                  var outputPaths = features.Select(feature =>
                                                                    {
                                                                        var document = ParseUtils.Parse(feature, options.Language);
                                                                        var markdown = Markdown.From(document);
                                                                        var fileName = Path.GetFileNameWithoutExtension(feature);
                                                                        var markdownPath = Path.Combine(options.OutputPath, $"{fileName}.md");
                                                                        File.WriteAllText(markdownPath, markdown);

                                                                        return markdownPath;
                                                                    });
                                  Console.WriteLine(string.Join(Environment.NewLine, outputPaths));
                              });
        }

        private class Options
        {
            [Option('i', "input-path", Required = false, HelpText = "Sets the path to scan for *.feature")]
            public string ScanPath { get; set; } = Environment.CurrentDirectory;

            [Option('o', "output-path", Required = false, HelpText = "Sets the output path for generated markdown")]
            public string OutputPath { get; set; } = Path.Combine(Environment.CurrentDirectory, "docs", "spec");

            [Option('l', "language", Required = false, HelpText = "ISO 3166 language code used to parse the Gherkin")]
            public string Language { get; set; } = "en";
        }
    }
}