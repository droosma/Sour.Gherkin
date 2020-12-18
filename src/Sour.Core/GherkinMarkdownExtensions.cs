using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gherkin.Ast;

using Sour.Core.Utilities;

namespace Sour.Core
{
    internal static class GherkinMarkdownExtensions
    {
        public static string AsMarkdown(this Step step)
        {
            var builder = new StringBuilder();
            builder.AppendLine($">_{step.Keyword.Trim()}_ {step.Text.Replace("<", "[").Replace(">", "]")}  ");
            
            if (step.Argument is DataTable dataTable)
            {
                dataTable.Rows.Aggregate(builder, (_, row) => builder.AppendLine("> " + row.GenerateRow(ExtractColumnWith(dataTable))));
            }

            return builder.ToString();

            int[] ExtractColumnWith(IHasRows table)
            {
                var columnWith = ColumnWith(table.Rows.First());
                foreach(var row in table.Rows.Skip(1))
                {
                    var rowWiths = ColumnWith(row);

                    for(var i = 0;i < columnWith.Length;i++)
                    {
                        columnWith[i] = rowWiths[i] > columnWith[i] ? rowWiths[i] : columnWith[i];
                    }
                }

                return columnWith;

                static int[] ColumnWith(TableRow row) 
                    => row.Cells.Select(cell => Math.Max(3, cell.Value.Length)).ToArray();
            }
        }

        public static string AsMarkdown(this IEnumerable<Examples> examples)
        {
            var builder = new StringBuilder();
            examples.Aggregate(builder, (_, example) => builder.Append(example.AsMarkdown()));
            return builder.ToString();
        }

        private static int[] ExtractColumnWith(this Examples examples)
        {
            const int minColumnWith = 3;
            var columnWith = examples.TableHeader
                                     .Cells
                                     .Select(cell => Math.Max(minColumnWith, cell.Value.Length))
                                     .ToArray();
            examples.TableBody.Aggregate(columnWith, (current, tableRow) => CalculateColumnWithForRow(tableRow, current));

            return columnWith;

            static int CalculateColumnWith(IReadOnlyList<int> current, int index, TableCell cell) 
                => Math.Max(current[index], cell.Value.Length);

            int[] CalculateColumnWithForRow(TableRow tableRow, IReadOnlyList<int> current) 
                => tableRow.Cells.Select((cell, index) => CalculateColumnWith(current, index, cell)).ToArray();
        }
        
        public static string AsMarkdown(this Examples examples)
        {
            var builder = new StringBuilder();
            builder.Append(examples.Keyword.AsMarkdownHeader(3, true));

            var columnWith = examples.ExtractColumnWith();

            builder.AppendLine(">" + examples.TableHeader.GenerateRow(columnWith));
            builder.AppendLine(">" + GenerateSeparationRow());
            foreach(var row in examples.TableBody)
            {
                builder.AppendLine(">" + row.GenerateRow(columnWith));
            }

            builder.AppendLine(">");
            return builder.ToString();

            string GenerateSeparationRow() 
                => string.Join(string.Empty, examples.TableHeader.Cells.Select((cell, index) => $"| {new string('-', columnWith[index])} ")) + "|";
        }

        private static string GenerateRow(this TableRow row, IReadOnlyList<int> columnWith)
        {
            var cellArray = row.Cells.ToArray();

            var rowBuilder = new StringBuilder();
            for (var index = 0; index < cellArray.Length; index++)
            {
                rowBuilder.Append($"| {cellArray[index].Value.PadRight(columnWith[index])} ");
            }

            return rowBuilder + "|  ";
        }

        public static string AsMarkdown(this Scenario scenario)
        {
            var builder = new StringBuilder();

            builder.Append(scenario.Name.AsMarkdownHeader(2));

            if(!scenario.Description.IsEmpty())
            {
                builder.AppendLine(scenario.Description.Cleanup());
            }

            scenario.Steps.Aggregate(builder, (_, step) => builder.Append(AsMarkdown(step)));

            if(scenario.Examples.Any())
                builder.Append(scenario.Examples.AsMarkdown());

            return builder.ToString();
        }

        private static string Cleanup(this string description) 
            => string.Join($"  {Environment.NewLine}", description.Split(Environment.NewLine).Select(s => s.Trim()));

        public static string AsMarkdown(this Background background)
        {
            var builder = new StringBuilder();

            background.Steps.Aggregate(builder, (s, step) => builder.Append(AsMarkdown(step)));

            return builder.ToString();
        }

        public static string AsMarkdown(this GherkinDocument document)
            => document.Feature.AsMarkdown();

        public static string AsMarkdown(this Feature feature)
        {
            var builder = new StringBuilder();

            builder.Append(feature.Name.AsMarkdownHeader(1));

            if(!feature.Description.IsEmpty())
            {
                builder.AppendLine(feature.Description.Cleanup());
            }

            foreach(var child in feature.Children)
            {
                switch(child)
                {
                    case Background background:
                        builder.Append(background.AsMarkdown());
                        break;
                    case Scenario scenario:
                        builder.Append(scenario.AsMarkdown());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(child), $"the child type {child.GetType().Name} currently not supported");
                }
            }

            return builder.ToString();
        }


        private static string AsMarkdownHeader(this string value, int index, bool blockQuote = false)
        {
            var linePrefix = blockQuote ? ">" : string.Empty;
            var builder = new StringBuilder();
            builder.AppendLine(linePrefix);
            builder.AppendLine($"{linePrefix}{new string('#', index)} {value}");
            builder.AppendLine(linePrefix);
            return builder.ToString();
        }
    }
}