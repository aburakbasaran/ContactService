using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace ContactService.SourceGenerator.ApiGenerator
{
    [Microsoft.CodeAnalysis.Generator]
    [ExcludeFromCodeCoverage]
    internal sealed class ApiGenerator : ISourceGenerator
    {
        private const string GeneratorAttributeName = "Generator";
        private const string DefaultNamespace = "ContactService.API.Controllers";
        private readonly Dictionary<string, ControllerMetadata> _controllers = new();

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ApiGeneratorSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            ApiGeneratorSyntaxReceiver syntaxReceiver = (ApiGeneratorSyntaxReceiver)context.SyntaxReceiver;
            GenerateContoller(context, syntaxReceiver);
        }

        private void GenerateContoller(GeneratorExecutionContext context, ApiGeneratorSyntaxReceiver syntaxReceiver)
        {
            foreach (ClassDeclarationSyntax query in syntaxReceiver.Commands)
            {
                InitializeHandler(query, true);
            }

            foreach (ClassDeclarationSyntax query in syntaxReceiver.Queries)
            {
                InitializeHandler(query, false);
            }

            foreach (KeyValuePair<string, ControllerMetadata> controller in _controllers)
            {
                string controllerSource = DrawController(controller.Value);
                SourceText countrollerSourceText = SourceText.From(controllerSource, Encoding.UTF8);
                context.AddSource($"{controller.Key}Controller.cs", countrollerSourceText);
            }
        }

        private void InitializeHandler(BaseTypeDeclarationSyntax classDeclarationSyntax, bool isCommand)
        {
            SyntaxList<AttributeListSyntax> attributeListSyntaxes = classDeclarationSyntax.AttributeLists;
            IDictionary<string, string> allAttributes = FindAttributes(attributeListSyntaxes);
            if (allAttributes?.ContainsKey(GeneratorAttributeName) == false)
            {
                return;
            }

            GeneratorAttribute generatorAttributesArguments = GetAttributesArguments(FindMetadataAttribute(attributeListSyntaxes));

            if (!_controllers.ContainsKey(generatorAttributesArguments.ControllerName))
            {
                _controllers[generatorAttributesArguments.ControllerName] = new()
                {
                    Name = generatorAttributesArguments.ControllerName,
                    Namespace = DefaultNamespace,
                    Namespaces = new()
                    {
                        "using System.Threading",
                        "using System.Threading.Tasks",
                        "using Microsoft.AspNetCore.Mvc",
                        "using ContactService.Application.Controller"
                    },
                    DefaultAttributes = new()
                };
            }

            string fullString = classDeclarationSyntax.SyntaxTree.ToString();
            string code = classDeclarationSyntax.ToString();
            List<string> namespaces = fullString.Replace(code, string.Empty)
                .Split(new[] { "namespace" }, StringSplitOptions.RemoveEmptyEntries).First()
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Where(c => c.Contains("ContactService")).Select(c =>
                      c.Replace(Environment.NewLine, string.Empty).Replace(Environment.CommandLine, string.Empty))
                .ToList();

            allAttributes.Remove(GeneratorAttributeName);

            string requestName = FindRequest(classDeclarationSyntax);
            ActionMetadata actionMetadata = new()
            {
                Name = generatorAttributesArguments.ActionName,
                Route = generatorAttributesArguments.Route,
                HttpMethod = generatorAttributesArguments.HttpMethod,
                Namespaces = new(namespaces),
                RequestTypeName = requestName,
                IsCommand = isCommand,
                HasRequest = !generatorAttributesArguments.HasNoParameter,
                RequestFromBody = generatorAttributesArguments.RequestFromBody,
                Summary = classDeclarationSyntax.GetLeadingTrivia(),
                DefaultAttributes = new(allAttributes.Values)
            };
            _controllers[generatorAttributesArguments.ControllerName].Actions.Add(actionMetadata);
        }

        private static AttributeSyntax FindMetadataAttribute(SyntaxList<AttributeListSyntax> attributeListSyntaxes)
        {
            return attributeListSyntaxes.Where(c => c.Attributes.Count > 0)
                .SelectMany(c => c.Attributes).First(c => (c.Name as IdentifierNameSyntax).Identifier.Text == GeneratorAttributeName);
        }

        private static IDictionary<string, string> FindAttributes(SyntaxList<AttributeListSyntax> attributeListSyntaxes)
        {
            return attributeListSyntaxes.SelectMany(c => c.Attributes).Where(c => c.Name is IdentifierNameSyntax)
                .ToDictionary(c => (c.Name as IdentifierNameSyntax).Identifier.ValueText, c => c.ToFullString());
        }

        private static string FindRequest(BaseTypeDeclarationSyntax classDeclarationSyntax)
        {
            SimpleBaseTypeSyntax simpleBaseTypeSyntax = classDeclarationSyntax.BaseList.Types.OfType<SimpleBaseTypeSyntax>().First();
            GenericNameSyntax genericNameSyntax = simpleBaseTypeSyntax.Type as GenericNameSyntax;
            return genericNameSyntax.TypeArgumentList.Arguments.Select(l => l.GetText().ToString()).First();
        }

        private static GeneratorAttribute GetAttributesArguments(AttributeSyntax attributes)
        {
            if (attributes.ArgumentList == null || attributes.ArgumentList.Arguments.Count == 0)
            {
                return null;
            }

            GeneratorAttribute generatorAttribute = new();
            foreach (AttributeArgumentSyntax attributeArgumentSyntax in attributes.ArgumentList.Arguments)
            {
                string propertyName = attributeArgumentSyntax.NameEquals?.Name?.Identifier.ValueText;
                string propertyValue = propertyName.Equals(nameof(GeneratorAttribute.HttpMethod)) ?
                      ((IdentifierNameSyntax)((MemberAccessExpressionSyntax)attributeArgumentSyntax.Expression).Name)
                      .Identifier.Value.ToString()
                      :
                      ((LiteralExpressionSyntax)attributeArgumentSyntax.Expression).Token.ValueText;

                switch (propertyName)
                {
                    case nameof(GeneratorAttribute.ActionName):
                        generatorAttribute.ActionName = propertyValue;
                        break;
                    case nameof(GeneratorAttribute.ControllerName):
                        generatorAttribute.ControllerName = propertyValue;
                        break;
                    case nameof(GeneratorAttribute.Route):
                        generatorAttribute.Route = propertyValue;
                        break;
                    case nameof(GeneratorAttribute.HasNoParameter):
                        generatorAttribute.HasNoParameter = bool.TryParse(propertyValue, out bool hasRequestModel) && hasRequestModel;
                        break;
                    case nameof(GeneratorAttribute.RequestFromBody):
                        generatorAttribute.RequestFromBody = bool.TryParse(propertyValue, out bool requestFromBody) && requestFromBody;
                        break;
                    case nameof(GeneratorAttribute.HttpMethod):
                        Enum.TryParse(propertyValue, out HttpMethod httpMethod);
                        generatorAttribute.HttpMethod = httpMethod;
                        break;
                }
            }

            return generatorAttribute;
        }

        private static void DrawAction(SourceBuilder builder, ActionMetadata actionMetadata)
        {
            if (actionMetadata.Summary.Count > 0)
            {
                foreach (SyntaxTrivia summaryLine in actionMetadata.Summary)
                {
                    string summaryLineString = summaryLine.ToString().Trim();
                    builder.WriteLineIf(!string.IsNullOrWhiteSpace(summaryLineString), summaryLineString);
                }
            }

            builder.WriteLineIf(!string.IsNullOrWhiteSpace(actionMetadata.Route), $"[Route(\"{actionMetadata.Route}\")]")
                   .WriteLineIf(actionMetadata.HttpMethod != HttpMethod.None, $"[{ConvertHttpMethod(actionMetadata.HttpMethod)}]");
            if (actionMetadata.DefaultAttributes != null && actionMetadata.DefaultAttributes.Any())
            {
                foreach (string attribute in actionMetadata.DefaultAttributes)
                {
                    builder.WriteLine($"[{attribute}]");
                }
            }

            builder.Write("public async Task<IActionResult> ")
                    .Write(actionMetadata.Name)
                    .WriteOpeningBracket()
                    .WriteIf(actionMetadata.RequestFromBody, "[FromBody] ")
                    .WriteIf(actionMetadata.HasRequest, actionMetadata.RequestTypeName)
                    .WriteIf(actionMetadata.HasRequest, $" {actionMetadata.RequestVariableName}, CancellationToken cancellationToken")
                    .WriteIf(!actionMetadata.HasRequest, "CancellationToken cancellationToken")
                    .WriteClosingBracket()
                    .WriteOpeningCurlyBracket()
                    .WriteLineIf(!actionMetadata.HasRequest, $"return await Send(new {actionMetadata.RequestTypeName}(), cancellationToken);")
                    .WriteLineIf(actionMetadata.HasRequest, $"return await Send({actionMetadata.RequestVariableName}, cancellationToken);")
                    .WriteClosingCurlyBracket()
                    .WriteLine();
        }

        private static string DrawController(ControllerMetadata controllerMetadata)
        {
            using SourceBuilder builder = new();
            List<string> namepaces = controllerMetadata.Actions.SelectMany(c => c.Namespaces).ToList();
            namepaces.AddRange(controllerMetadata.Namespaces);
            namepaces = namepaces.Distinct().ToList();
            foreach (string item in namepaces)
            {
                builder.Write(item)
                    .Write(";")
                    .WriteLine();
            }

            builder.WriteLine(string.Empty)
                .Write("namespace ")
                .Write(controllerMetadata.Namespace)
                .WriteLine()
                .WriteOpeningCurlyBracket();
            foreach (string attribute in controllerMetadata.DefaultAttributes)
            {
                builder.WriteLine($"[{attribute}]");
            }

            builder.WriteLineIf(!string.IsNullOrWhiteSpace(controllerMetadata.Summary), controllerMetadata.Summary)
            .WriteLine($"public class {controllerMetadata.Name}Controller : BaseApiController")
            .WriteOpeningCurlyBracket();

            foreach (ActionMetadata action in controllerMetadata.Actions)
            {
                DrawAction(builder, action);
            }

            builder.WriteClosingCurlyBracket()
                   .WriteClosingCurlyBracket();

            return builder.ToString();
        }

        private static string ConvertHttpMethod(HttpMethod httpMethod)
        {
            return $"Http{httpMethod}";
        }
    }
}