"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.cleanUpCSharpFiles = void 0;
const vscode = require("vscode");
const ec = require('./ExtractCode');
const omnisharpClient = require('omnisharp-client');
async function cleanUpCSharpFiles(option, codeLensProvider) {
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
exports.cleanUpCSharpFiles = cleanUpCSharpFiles;
function getCodeLensesFromResponse(response, file) {
    let codeLenses = [];
    // TODO: Replace this part with the usage of codeLenses array or any other data structure you prefer
    // Extract method names using the code structure data you have stored earlier
    for (const method of response.QuickFixes) {
        const methodName = method.Text.split('(')[0];
        const line = method.Line;
        const codeLens = createCodeLens(file, methodName, line);
        codeLenses.push(codeLens);
    }
    return {
        provideCodeLenses(document, token) {
            return codeLenses;
        }
    };
}
function createCodeLens(file, methodName, line) {
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
//# sourceMappingURL=Cleanup.js.map