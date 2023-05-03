using RoslynSyntaxExtractor;

namespace RoslynSyntaxExtractorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set your desired choice and pathToCsFiles here
            int choice = 4;
            string code = @"using System;

                            namespace HelloWorld
                            {
                                class Program
                                {
                                    static void Main(string[] args)
                                    {
                                        Console.WriteLine(""Hello, world!"");
                                    }
                                }
                            }";

            string result = Extractor.Start(choice, code);
            Console.WriteLine(result);
        }
    }
}
