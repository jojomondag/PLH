using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;

namespace SynEx.Logic
{
    internal class CodeExtractor
    {
        public static List<string> ExtractFunctionNames(List<string> csFiles)
        {
            return ProcessFiles(csFiles, root => root.DescendantNodes().OfType<MethodDeclarationSyntax>().Select(fd => fd.Identifier.ValueText));
        }

        public static List<string> ExtractClassNames(List<string> csFiles)
        {
            return ProcessFiles(csFiles, root => root.DescendantNodes().OfType<ClassDeclarationSyntax>().Select(cd => cd.Identifier.ValueText));
        }

        public static List<string> ExtractClassAndFunctionNames(List<string> csFiles)
        {
            return ProcessFiles(csFiles, root => root.DescendantNodes().OfType<MethodDeclarationSyntax>().Select(fd => fd.Identifier.ValueText).Concat(root.DescendantNodes().OfType<ClassDeclarationSyntax>().Select(cd => cd.Identifier.ValueText)));
        }

        private static List<string> ProcessFiles(List<string> csFiles, Func<SyntaxNode, IEnumerable<string>> syntaxProcessor)
        {
            List<string> extractedData = new();

            foreach (string file in csFiles)
            {
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(file));
                SyntaxNode root = syntaxTree.GetRoot();

                extractedData.AddRange(syntaxProcessor(root));
            }

            return extractedData;
        }
    }
}