// Import dependencies
import * as fs from 'fs';
import * as path from 'path';
import * as vscode from 'vscode';

// Import custom module
import { getAllCodeFilesOfType } from './codeAnalyzer';

// The goal of the extractCodeContent function is to loop through all the C# files in the project and extract the code into a single file
export function extractCodeContent(fileType: string, extraText: string): string {
  // Get all the code files of a given file type
  const codeFiles = getAllCodeFilesOfType(fileType);

  let combinedContent = '';

  // Loop through each code file and extract its content
  for (const filePath of codeFiles) {
    const fileContent = fs.readFileSync(filePath, 'utf-8');
    console.log(`Processing file: ${filePath}`); // Log the file path
    console.log(`File content length: ${fileContent.length}`); // Log the content size
    combinedContent += fileContent + '\n';
  }

  // Add the list of code files at the end of the combined content
  combinedContent += '\n\nLoaded code files:\n';
  for (const filePath of codeFiles) {
    combinedContent += filePath + '\n';
  }
  // Save the combined content to a file
  const fileName = `${extraText}_CodeContent_${getCurrentDateTimeString()}.txt`;
  const filePath = saveContentToFile(combinedContent, fileName);

  return filePath;
}
// This function saves the given content to a file with a given name
export function saveContentToFile(content: string, fileName: string): string {
  // Get the workspace folder and create the output folder if it doesn't exist
  const workspaceFolder = vscode.workspace.workspaceFolders![0].uri.fsPath;
  const outputPath = path.join(workspaceFolder, 'syntaxExtractorText');
  fs.mkdirSync(outputPath, { recursive: true });

  // Save the content to the output file and return the file path
  const outputFile = path.join(outputPath, fileName);
  fs.writeFileSync(outputFile, content, { encoding: 'utf-8' });
  return outputFile;
}
// This function copies the given text to the clipboard
export async function copyToClipboard(textToCopy: string) {
    try {
      await vscode.env.clipboard.writeText(textToCopy);
      console.log('Copied text to clipboard');
    } catch (err) {
      console.log(`Failed to copy text to clipboard: ${err}`);
    }
}
  // This function returns the current date and time as a formatted string
 export function getCurrentDateTimeString(): string {
    const currentDate = new Date();
    const formattedDate = `${currentDate.getFullYear()}-${(currentDate.getMonth() + 1).toString().padStart(2, '0')}-${currentDate.getDate().toString().padStart(2, '0')}_${currentDate.getHours().toString().padStart(2, '0')}-${currentDate.getMinutes().toString().padStart(2, '0')}-${currentDate.getSeconds().toString().padStart(2, '0')}`;
    return formattedDate;
}