"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.initialFunction = exports.workspaceFolders = void 0;
const vscode = require("vscode");
const ExtractCode_1 = require("./CodeExtractors/ExtractCode");
exports.workspaceFolders = vscode.workspace.workspaceFolders;
function initialFunction() {
    if (exports.workspaceFolders && exports.workspaceFolders.length > 0) {
        const projectPath = exports.workspaceFolders[0].uri.fsPath;
        vscode.window.showInformationMessage(`Current project path: ${projectPath}`);
        // Call the extractCodeFilesContent with type .cs
        (0, ExtractCode_1.extractCodeFilesContent)('.cs');
    }
    else {
        vscode.window.showInformationMessage('No workspace folder is currently open.');
    }
}
exports.initialFunction = initialFunction;
//# sourceMappingURL=initActions.js.map