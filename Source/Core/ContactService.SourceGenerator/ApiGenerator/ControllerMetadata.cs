using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ContactService.SourceGenerator.ApiGenerator
{
    [ExcludeFromCodeCoverage]
    internal sealed class ControllerMetadata
    {
        public ControllerMetadata()
        {
            Namespaces = new();
            DefaultAttributes = new();
            Actions = new List<ActionMetadata>();
        }

        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Summary { get; set; }
        public HashSet<string> Namespaces { get; set; }
        public HashSet<string> DefaultAttributes { get; set; }

        public IList<ActionMetadata> Actions { get; set; }
    }
}