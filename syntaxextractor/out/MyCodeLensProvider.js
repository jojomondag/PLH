"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MyCodeLensProvider = void 0;
const vscode = require("vscode");
class MyCodeLensProvider {
    constructor() {
        this.methodNames = [];
    }
    async provideCodeLenses(document, token) {
        const codeLenses = [];
        // Get the code structure for the current document
        const codeStructure = await vscode.commands.executeCommand('vscode.executeCodeLensProvider', document.uri);
        if (codeStructure) {
            // Process the code structure data here
            // Extract method names or other information
            // and store it in methodNames array
            this.methodNames = this.extractMethodNames(codeStructure);
        }
        return codeLenses;
    }
    extractMethodNames(codeLenses) {
        const methodNames = [];
        // Extract method names from codeLenses and store them in methodNames array
        return methodNames;
    }
}
exports.MyCodeLensProvider = MyCodeLensProvider;
//# sourceMappingURL=MyCodeLensProvider.js.map