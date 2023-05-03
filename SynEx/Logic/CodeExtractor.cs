using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SynEx.Data;

namespace SynEx.Logic
{
    internal class CodeExtractor
    {
        public delegate IEnumerable<string> SyntaxProcessor(SyntaxNode root);

        public static void ExtractFunctionNames()
        {
            ProcessFiles(root => root.DescendantNodes().OfType<MethodDeclarationSyntax>().Select(fd => fd.Identifier.ValueText), "FunctionNames");
        }
        public static void ExtractClassNames()
        {
            ProcessFiles(root => root.DescendantNodes().OfType<ClassDeclarationSyntax>().Select(cd => cd.Identifier.ValueText), "ClassNames");
        }
        public static void ExtractClassAndFunctionNames()
        {
            ProcessFiles(root => root.DescendantNodes().OfType<MethodDeclarationSyntax>().Select(fd => fd.Identifier.ValueText).Concat(root.DescendantNodes().OfType<ClassDeclarationSyntax>().Select(cd => cd.Identifier.ValueText)),"ClassAndFunctionNames");
        }

        private static void ProcessFiles(SyntaxProcessor syntaxProcessor, string fileNamePrefix)
        {
            string solutionPath = DataManager.GetSolutionPath();
            string folderPath = Path.Combine(solutionPath, "ExtractedSyntax");
            Directory.CreateDirectory(folderPath);

            List<string> csFiles = DataManager.GetCsFiles();

            List<string> extractedData = new List<string>();

            foreach (string file in csFiles)
            {
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(file));
                SyntaxNode root = syntaxTree.GetRoot();

                extractedData.AddRange(syntaxProcessor(root));
            }

            FileDataCreator(extractedData, folderPath, fileNamePrefix);
        }

        private static void FileDataCreator(List<string> extractedData, string folderPath, string fileNamePrefix)
        {
            string dateTimeString = DataManager.GetCurrentDateTimeString();
            string fileName = $"{fileNamePrefix}_{dateTimeString}.txt";
            string filePath = Path.Combine(folderPath, fileName);

            using (var writer = new StreamWriter(filePath))
            {
                foreach (string data in extractedData)
                {
                    writer.WriteLine(data);
                }
            }

            string dataText = File.ReadAllText(filePath);
            ClipboardManager.SetTextToClipboard(dataText);
        }
    }
}
