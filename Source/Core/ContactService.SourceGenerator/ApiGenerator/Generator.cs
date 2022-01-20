using System;

namespace ContactService.SourceGenerator.ApiGenerator
{
    public class GeneratorAttribute : Attribute
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public string Route { get; set; }
        public bool HasNoParameter { get; set; }
        public bool RequestFromBody { get; set; }
    }
}
