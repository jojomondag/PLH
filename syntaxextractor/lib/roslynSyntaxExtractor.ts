declare module 'roslynSyntaxExtractor' {
  function extractFunctionNames(code: string): string;
  export = extractFunctionNames;
}