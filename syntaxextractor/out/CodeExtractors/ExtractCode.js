"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.extractCodeFilesContent = exports.text = void 0;
const fs = require("fs");
const path = require("path");
const vscode = require("vscode");
const codeAnalyzer_1 = require("../codeAnalyzer");
exports.text = '';
function extractCodeFilesContent(fileType) {
    const cSharpFiles = (0, codeAnalyzer_1.getAllCodeFilesOfType)(fileType);
    let combinedContent = '';
    for (const filePath of cSharpFiles) {
        const fileContent = fs.readFileSync(filePath, 'utf-8');
        console.log(`Processing file: ${filePath}`); // Log the file path
        console.log(`File content length: ${fileContent.length}`); // Log the content size
        combinedContent += fileContent + '\n';
    }
    // Add the list of .cs files at the end of the combined content
    combinedContent += '\n\nLoaded .cs files:\n';
    for (const filePath of cSharpFiles) {
        combinedContent += filePath + '\n';
    }
    exports.text = combinedContent;
    // Save the combined content to a text file in the syntaxExtractorText folder
    const workspaceFolder = vscode.workspace.workspaceFolders[0].uri.fsPath;
    const outputPath = path.join(workspaceFolder, 'syntaxExtractorText');
    // Ensure the syntaxExtractorText folder exists
    fs.mkdirSync(outputPath, { recursive: true });
    // Get the current date and time as a formatted string
    const currentDate = new Date();
    const formattedDate = `${currentDate.getFullYear()}-${(currentDate.getMonth() + 1).toString().padStart(2, '0')}-${currentDate.getDate().toString().padStart(2, '0')}_${currentDate.getHours().toString().padStart(2, '0')}-${currentDate.getMinutes().toString().padStart(2, '0')}-${currentDate.getSeconds().toString().padStart(2, '0')}`;
    const outputFile = path.join(outputPath, `CSharpContent_${formattedDate}.txt`);
    fs.writeFileSync(outputFile, combinedContent, { encoding: 'utf-8' });
}
exports.extractCodeFilesContent = extractCodeFilesContent;
//# sourceMappingURL=ExtractCode.js.map