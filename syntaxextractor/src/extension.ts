import * as vscode from 'vscode';
import { initialFunction } from './initActions';
import * as buttonActions from './buttonActions';

export function activate(context: vscode.ExtensionContext) {
	const treeDataProvider = new ButtonsTreeDataProvider(context);
	vscode.window.createTreeView('syntaxExtractorCustomView', { treeDataProvider });
	
	// Call the initial function after creating the tree view
  initialFunction();
	
	let disposable1 = vscode.commands.registerCommand('syntaxextractor.button1', () => {
	  buttonActions.button1();
	});
  
	let disposable2 = vscode.commands.registerCommand('syntaxextractor.button2', () => {
	  buttonActions.button2();
	});
  
	let disposable3 = vscode.commands.registerCommand('syntaxextractor.button3', () => {
	  buttonActions.button3();
	});
  let disposable4 = vscode.commands.registerCommand('syntaxextractor.button4', () => {
	  buttonActions.button4();
	});
  
	context.subscriptions.push(disposable1, disposable2, disposable3,disposable4);
}

class ButtonsTreeDataProvider implements vscode.TreeDataProvider<ButtonTreeItem> {
  private _onDidChangeTreeData: vscode.EventEmitter<ButtonTreeItem | null> = new vscode.EventEmitter<ButtonTreeItem | null>();
  readonly onDidChangeTreeData: vscode.Event<ButtonTreeItem | null> = this._onDidChangeTreeData.event;

  constructor(private context: vscode.ExtensionContext) {}

  getTreeItem(element: ButtonTreeItem): ButtonTreeItem {
    return element;
  }

  getChildren(element?: ButtonTreeItem): Thenable<ButtonTreeItem[]> {
    if (!element) {
      return Promise.resolve(this.getButtons());
    }
    return Promise.resolve([]);
  }

  private getButtons(): ButtonTreeItem[] {
    const labels = ['1. Function names only', '2. Function names and parameters', '3. Function names, parameters, and return types', '4. Access modifiers, static keyword, function names, parameters, and return types'];
    return labels.map((label) => new ButtonTreeItem(label, `syntaxextractor.button${labels.indexOf(label) + 1}`));
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

export function deactivate() {}