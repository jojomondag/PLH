import * as vscode from 'vscode';

export class MyCodeLensProvider implements vscode.CodeLensProvider {
  public methodNames: string[] = [];

  async provideCodeLenses(document: vscode.TextDocument, token: vscode.CancellationToken): Promise<vscode.CodeLens[]> {
    const codeLenses: vscode.CodeLens[] = [];

    // Get the code structure for the current document
    const codeStructure = await vscode.commands.executeCommand<vscode.CodeLens[]>('vscode.executeCodeLensProvider', document.uri);

    if (codeStructure) {
      // Process the code structure data here
      // Extract method names or other information
      // and store it in methodNames array
      this.methodNames = this.extractMethodNames(codeStructure);
    }

    return codeLenses;
  }

  private extractMethodNames(codeLenses: vscode.CodeLens[]): string[] {
    const methodNames: string[] = [];
    // Extract method names from codeLenses and store them in methodNames array
    return methodNames;
  }
}
