using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace ContactService.SourceGenerator.ApiGenerator
{
    internal sealed class ActionMetadata
    {
        public string Name { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public string Route { get; set; }
        public string RequestTypeName { get; set; }
        public SyntaxTriviaList Summary { get; set; }
        public bool IsCommand { get; set; }

        public bool HasRequest { get; set; }
        public bool RequestFromBody { get; set; }
        public string RequestVariableName => IsCommand ? "command" : "query";
        public HashSet<string> Namespaces { get; set; }
        public HashSet<string> DefaultAttributes { get; set; }
    }
}