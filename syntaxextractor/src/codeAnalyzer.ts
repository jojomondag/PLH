import * as vscode from 'vscode';
import { workspaceFolders } from './initActions';
import * as path from 'path';
import * as fs from 'fs';

export function getAllCodeFilesOfType(fileType: string): string[] {
  if (workspaceFolders && workspaceFolders.length > 0) {
    const projectPath = workspaceFolders[0].uri.fsPath;

    // Create a list to hold all text/code files in the parent project folder and its subdirectories
    let fileList: string[] = [];

    const walk = (dir: string) => {
      const files = fs.readdirSync(dir);
      for (const file of files) {
        const filePath = path.join(dir, file);
        const stat = fs.statSync(filePath);
        if (stat.isDirectory() && !filePath.includes('node_modules') && filePath !== projectPath) {
          walk(filePath);
        } else {
          if (filePath.endsWith(fileType)) {
            fileList.push(filePath);
          }
        }
      }
    };

    walk(projectPath);

    return fileList;
  } else {
    vscode.window.showErrorMessage('No workspace folder is currently open.');
    return [];
  }
}

//Create a function that loads all textfiles from syntaxExtractorText folder In the main workspaceFolders and stores in a list
export function loadAllTextFiles(): string[] {
  if (workspaceFolders && workspaceFolders.length > 0) {
    const projectPath = workspaceFolders[0].uri.fsPath;

    const textFolderPath = path.join(projectPath, 'syntaxExtractorText');

    // Create a list to hold all text/code files in the parent project folder and its subdirectories
    let fileList: string[] = [];

    // Loop through all files in the syntaxExtractorText folder and its subdirectories
    const walk = (dir: string, parentProjectPath: string, fileType: string) => {
      const files = fs.readdirSync(dir);
      for (const file of files) {
        const filePath = path.join(dir, file);
        const stat = fs.statSync(filePath);
        if (stat.isDirectory() && !filePath.includes('node_modules') && filePath !== parentProjectPath) {
          walk(filePath, parentProjectPath, fileType);
        } else {
          // Check if the file is a text/code file of the specified type
          if (filePath.endsWith('.txt')) {
            fileList.push(filePath);
          }
        }
      }
    };

    walk(textFolderPath, projectPath, '.txt');

    return fileList;
  } else {
    vscode.window.showErrorMessage('No workspace folder is currently open.');
    return [];
  }
}
