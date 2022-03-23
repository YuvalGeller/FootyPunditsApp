using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using FootballDataApi.Models;

namespace FootyPunditsApp.DataTests
{
    class TestMatch
    {
        public static string GetMatches()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(FootyPunditsApp.App)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("FootyPunditsApp.DataTests.matches.json");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }
    }
}
