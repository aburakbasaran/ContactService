using System;
using System.Diagnostics.CodeAnalysis;

namespace ContactService.SourceGenerator.ApiGenerator
{
    [ExcludeFromCodeCoverage]
    public class GeneratorAttribute : Attribute
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string NameSpace { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public string Route { get; set; }
        public bool HasNoParameter { get; set; }
        public bool RequestFromBody { get; set; }
    }
}
