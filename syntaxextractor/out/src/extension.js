"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.deactivate = exports.activate = void 0;
const vscode = require("vscode");
const buttonActions = require("./buttonActions");
const init = require("./initActions");
function activate(context) {
    const treeDataProvider = new ButtonsTreeDataProvider(context);
    vscode.window.createTreeView('syntaxExtractorCustomView', { treeDataProvider });
    init.initialFunction();
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
    context.subscriptions.push(disposable1, disposable2, disposable3, disposable4);
}
exports.activate = activate;
class ButtonsTreeDataProvider {
    constructor(context) {
        this.context = context;
        this._onDidChangeTreeData = new vscode.EventEmitter();
        this.onDidChangeTreeData = this._onDidChangeTreeData.event;
    }
    getTreeItem(element) {
        return element;
    }
    getChildren(element) {
        if (!element) {
            return Promise.resolve(this.getButtons());
        }
        return Promise.resolve([]);
    }
    getButtons() {
        const labels = ['1. Function names only', '2. Function names and parameters', '3. Function names, parameters, and return types', '4. Access modifiers, static keyword, function names, parameters, and return types'];
        return labels.map((label) => new ButtonTreeItem(label, `syntaxextractor.button${labels.indexOf(label) + 1}`));
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
function deactivate() { }
exports.deactivate = deactivate;
//# sourceMappingURL=extension.js.map