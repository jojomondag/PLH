import * as vscode from 'vscode';
import * as buttonActions from './buttonActions';
import * as init from './initActions';
import { MyCodeLensProvider } from './MyCodeLensProvider';

export function activate(context: vscode.ExtensionContext) {
  init.initialFunction();

  const selector: vscode.DocumentSelector = { language: 'csharp', scheme: 'file' };

  const codeLensProvider = new MyCodeLensProvider();
  const codeLensProviderDisposable = vscode.languages.registerCodeLensProvider(selector, codeLensProvider);
  context.subscriptions.push(codeLensProviderDisposable);

  const buttonsDataProvider = new ButtonsTreeDataProvider();
  const buttonsTreeView = vscode.window.createTreeView('syntaxExtractorCustomView', {treeDataProvider: buttonsDataProvider});

  const disposable1 = vscode.commands.registerCommand('syntaxextractor.button1', () => {
    buttonActions.button1('1. Function names only', codeLensProvider);
  });
  const disposable2 = vscode.commands.registerCommand('syntaxextractor.button2', () => {
    buttonActions.button2(codeLensProvider);
  });
  const disposable3 = vscode.commands.registerCommand('syntaxextractor.button3', () => {
    buttonActions.button3(codeLensProvider);
  });
  const disposable4 = vscode.commands.registerCommand('syntaxextractor.button4', () => {
    buttonActions.button4(codeLensProvider);
  });

  context.subscriptions.push(disposable1, disposable2, disposable3, disposable4);
}

export function deactivate() {}

class ButtonsTreeDataProvider implements vscode.TreeDataProvider<ButtonTreeItem> {
  private _onDidChangeTreeData: vscode.EventEmitter<ButtonTreeItem | undefined | null | void> = new vscode.EventEmitter<ButtonTreeItem | undefined | null | void>();
  readonly onDidChangeTreeData: vscode.Event<ButtonTreeItem | undefined | null | void> = this._onDidChangeTreeData.event;

  constructor() {}

  getTreeItem(element: ButtonTreeItem): vscode.TreeItem {
    return element;
  }

  getChildren(element?: ButtonTreeItem): vscode.ProviderResult<ButtonTreeItem[]> {
    // Return the root level items if element is undefined
    if (!element) {
      const buttons: ButtonTreeItem[] = [
        new ButtonTreeItem('1. Function names only', 'syntaxextractor.button1'),
        new ButtonTreeItem('2. Function names and parameters', 'syntaxextractor.button2'),
        new ButtonTreeItem('3. Function names, parameters, and return types', 'syntaxextractor.button3'),
        new ButtonTreeItem('4. Access modifiers, static keyword, function names, parameters, and return types', 'syntaxextractor.button4')
      ];
      return buttons;
    }
    return undefined;
  }
}
class ButtonTreeItem extends vscode.TreeItem {
  constructor(label: string, command: string) {
    super(label, vscode.TreeItemCollapsibleState.None);
    this.command = {
      command: command,
      title: '',
      arguments: [label]
    };
  }
}