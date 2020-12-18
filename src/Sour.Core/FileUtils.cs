using System;
using System.Collections.Generic;
using System.IO;

namespace Sour.Core
{
    public static class FileUtils
    {
        public static IReadOnlyCollection<string> FindFeatures(string basePath,
                                                               string extension = "feature")
        {
            if(!Directory.Exists(basePath))
                throw new ArgumentException($"given path: '{basePath}' does not exist", nameof(basePath));

            return Directory.GetFiles(basePath, $"*.{extension}", SearchOption.AllDirectories);
        }
    }
}