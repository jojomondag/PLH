"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.deactivate = exports.activate = void 0;
const vscode = require("vscode");
const buttonActions = require("./buttonActions");
const init = require("./initActions");
function activate(context) {
    init.initialFunction();
    // Create a new instance of the ButtonsTreeDataProvider
    const buttonsDataProvider = new ButtonsTreeDataProvider();
    // Create a new TreeView for the ButtonsTreeDataProvider
    const buttonsTreeView = vscode.window.createTreeView('syntaxExtractorCustomView', {
        treeDataProvider: buttonsDataProvider
    });
    // Register the command for each button
    const disposable1 = vscode.commands.registerCommand('syntaxextractor.button1', () => {
        buttonActions.button1();
    });
    const disposable2 = vscode.commands.registerCommand('syntaxextractor.button2', () => {
        buttonActions.button2();
    });
    const disposable3 = vscode.commands.registerCommand('syntaxextractor.button3', () => {
        buttonActions.button3();
    });
    const disposable4 = vscode.commands.registerCommand('syntaxextractor.button4', () => {
        buttonActions.button4();
    });
    context.subscriptions.push(disposable1, disposable2, disposable3, disposable4);
}
exports.activate = activate;
function deactivate() { }
exports.deactivate = deactivate;
class ButtonsTreeDataProvider {
    constructor() {
        this._onDidChangeTreeData = new vscode.EventEmitter();
        this.onDidChangeTreeData = this._onDidChangeTreeData.event;
    }
    getTreeItem(element) {
        return element;
    }
    getChildren(element) {
        // Return the root level items if element is undefined
        if (!element) {
            const buttons = [
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
    constructor(label, command) {
        super(label, vscode.TreeItemCollapsibleState.None);
        this.command = {
            command: command,
            title: '',
            arguments: [label]
        };
    }
}
//# sourceMappingURL=index.js.map