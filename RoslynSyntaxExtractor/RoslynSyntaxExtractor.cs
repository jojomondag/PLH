using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynSyntaxExtractor
{
    public class Extractor
    {
        public static string Start(int choice, string codeContent)
        {
            bool displayParameters = choice >= 2;
            bool displayReturnTypes = choice >= 3;
            bool displayModifiers = choice >= 4;

            List<string> results = new List<string>();

            // Parse the C# source code
            SyntaxTree tree = CSharpSyntaxTree.ParseText(codeContent);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            // Extract method declarations
            var methodDeclarations = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

            foreach (var methodDeclaration in methodDeclarations)
            {
                string methodName = methodDeclaration.Identifier.ToString();
                string result = "";

                if (displayModifiers)
                {
                    string modifiers = string.Join(" ", methodDeclaration.Modifiers.Select(m => m.ToString()));
                    result += $"{modifiers} ";
                }

                result += methodName;

                if (displayParameters)
                {
                    var parameters = string.Join(", ", methodDeclaration.ParameterList.Parameters);
                    result += $"({parameters})";
                }

                if (displayReturnTypes)
                {
                    string returnType = methodDeclaration.ReturnType.ToString();
                    result += $": {returnType}";
                }

                results.Add($"- {result}");
            }

            return string.Join(Environment.NewLine, results);
        }
    }
}
