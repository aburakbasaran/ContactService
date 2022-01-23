using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ContactService.SourceGenerator.ApiGenerator
{
    [ExcludeFromCodeCoverage]
    internal sealed class ApiGeneratorSyntaxReceiver : ISyntaxReceiver
    {
        private const string CommandSelector = "ICommandHandler";
        private const string QuerySelector = "IQueryHandler";
        public List<ClassDeclarationSyntax> Commands { get; } = new();
        public List<ClassDeclarationSyntax> Queries { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (IsClassOrRecordDeclarationSyntax(syntaxNode))
            {
                ClassDeclarationSyntax tds = (ClassDeclarationSyntax)syntaxNode;
                if (tds.BaseList != null)
                {
                    BaseListSyntax baselist = tds.BaseList;
                    foreach (BaseTypeSyntax entry in baselist.Types)
                    {
                        if (entry is SimpleBaseTypeSyntax { Type: GenericNameSyntax type })
                        {
                            switch (type.Identifier.ValueText)
                            {
                                case CommandSelector when type.TypeArgumentList.Arguments.Count == 2:
                                    Commands.Add(tds);
                                    return;
                                case QuerySelector when type.TypeArgumentList.Arguments.Count == 2:
                                    Queries.Add(tds);
                                    return;
                            }
                        }
                    }
                }
            }
        }

        private static bool IsClassOrRecordDeclarationSyntax(SyntaxNode syntaxNode)
        {
            return syntaxNode is ClassDeclarationSyntax || syntaxNode is RecordDeclarationSyntax;
        }
    }
}