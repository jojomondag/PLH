import * as vscode from 'vscode';

const ec = require('./ExtractCode');
const omnisharpClient = require('omnisharp-client');

export async function cleanUpCSharpFiles(option: string,  codeLensProvider: any) {
  let cleanedUpContent = '';

  if (option === "1. Function names only") {
    vscode.window.showInformationMessage("We will clean up the C# files in the project to only include function names.");
    //The extractCodeContent function returns a string with the content of all the files in the currently opened Project.
    const file = ec.extractCodeContent('.cs', option);
    vscode.window.showInformationMessage("we will extract this " + file);

    // Extract method names using OmniSharp
    const client = new omnisharpClient.OmnisharpClient();
    const response = await client.codeStructure({ fileName: file });
    const codeLenses = getCodeLensesFromResponse(response, file);
    vscode.languages.registerCodeLensProvider({ language: 'csharp', scheme: 'file' }, codeLenses);
    vscode.window.showInformationMessage("Cleaned up content: " + cleanedUpContent);

    // Save the cleaned up content to a file
    const fileName = option + `${ec.getCurrentDateTimeString()}_CleanedUpCodeContent.txt`;
    ec.saveContentToFile(cleanedUpContent, fileName);

    // Also copy to clipboard
    ec.copyToClipboard(cleanedUpContent);
  }
}

function getCodeLensesFromResponse(response: any, file: string) {
  let codeLenses: vscode.CodeLens[] = [];

  // TODO: Replace this part with the usage of codeLenses array or any other data structure you prefer
  // Extract method names using the code structure data you have stored earlier
  for (const method of response.QuickFixes) {
    const methodName = method.Text.split('(')[0];
    const line = method.Line;
    const codeLens = createCodeLens(file, methodName, line);
    codeLenses.push(codeLens);
  }

  return {
    provideCodeLenses(document: vscode.TextDocument, token: vscode.CancellationToken): vscode.CodeLens[] {
      return codeLenses;
    }
  };
}

function createCodeLens(file: string, methodName: string, line: number): vscode.CodeLens {
  const range = new vscode.Range(line, 0, line, 0);
  const command = {
    title: 'Rename method',
    command: '',
    arguments: [file, methodName, range]
  };
  const codeLens = new vscode.CodeLens(range, command);
  return codeLens;
}

module.exports = {
  cleanUpCSharpFiles
};