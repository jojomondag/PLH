import * as fs from 'fs';
import * as path from 'path';
import * as vscode from 'vscode';
import { getAllCodeFilesOfType } from '../codeAnalyzer';

export let text: string = '';

export function extractCodeFilesContent(fileType: string): void {
    const cSharpFiles = getAllCodeFilesOfType(fileType);

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

    text = combinedContent;

    // Save the combined content to a text file in the syntaxExtractorText folder
    const workspaceFolder = vscode.workspace.workspaceFolders![0].uri.fsPath;
    const outputPath = path.join(workspaceFolder, 'syntaxExtractorText');
    
    // Ensure the syntaxExtractorText folder exists
    fs.mkdirSync(outputPath, { recursive: true });

    // Get the current date and time as a formatted string
    const currentDate = new Date();
    const formattedDate = `${currentDate.getFullYear()}-${(currentDate.getMonth() + 1).toString().padStart(2, '0')}-${currentDate.getDate().toString().padStart(2, '0')}_${currentDate.getHours().toString().padStart(2, '0')}-${currentDate.getMinutes().toString().padStart(2, '0')}-${currentDate.getSeconds().toString().padStart(2, '0')}`;

    const outputFile = path.join(outputPath, `CSharpContent_${formattedDate}.txt`);
    fs.writeFileSync(outputFile, combinedContent, { encoding: 'utf-8' });
}