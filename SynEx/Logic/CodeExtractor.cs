using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace SynEx.Logic
{
    internal class CodeExtractor
    {
        public static void ExtractFunctionNames()
        {
            var solutionPath = Directory.GetCurrentDirectory();
            var workspace = MSBuildWorkspace.Create();
            var solution = workspace.OpenSolutionAsync(solutionPath).Result;
            var extractedCodePath = Path.Combine(solutionPath, "ExtractedCode");
            Directory.CreateDirectory(extractedCodePath);

            foreach (var project in solution.Projects)
            {
                foreach (var document in project.Documents)
                {
                    if (document.FilePath.EndsWith(".cs"))
                    {
                        var tree = document.GetSyntaxTreeAsync().Result;
                        var root = tree.GetCompilationUnitRoot();
                        var methodDeclarations = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
                        var functionNames = methodDeclarations.Select(md => md.Identifier.ValueText);

                        var fileName = Path.GetFileNameWithoutExtension(document.FilePath) + ".txt";
                        var filePath = Path.Combine(extractedCodePath, fileName);

                        using (var writer = new StreamWriter(filePath))
                        {
                            foreach (var functionName in functionNames)
                            {
                                writer.WriteLine(functionName);
                            }
                        }
                    }
                }
            }
        }
    }
}
