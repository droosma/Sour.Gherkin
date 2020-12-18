using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sour.Core.Tests.Unit.Utilities
{
    internal static class Resource
    {
        public static StreamReader LoadAsStream(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            var resourcePath = resourceNames.Single(str => str.EndsWith(name));

            // TODO: [ Duncan Roosma 2020-12-15 ] Leaky
            var stream = assembly.GetManifestResourceStream(resourcePath);
            if (stream == null)
                throw new Exception($"unable to find {name} in {string.Join(", ", resourceNames)}");

            return new StreamReader(stream);
        }

        public static string ReadResource(string name) 
            => LoadAsStream(name).ReadToEnd();
    }
}