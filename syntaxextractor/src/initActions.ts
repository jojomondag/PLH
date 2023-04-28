import * as vscode from 'vscode';
import { extractCodeFilesContent } from './CodeExtractors/ExtractCode';

export const workspaceFolders = vscode.workspace.workspaceFolders;

export function initialFunction(): void {
    if (workspaceFolders && workspaceFolders.length > 0) {
        const projectPath = workspaceFolders[0].uri.fsPath;
        vscode.window.showInformationMessage(`Current project path: ${projectPath}`);

        // Call the extractCodeFilesContent with type .cs
        extractCodeFilesContent('.cs');

    } else {
        vscode.window.showInformationMessage('No workspace folder is currently open.');
    }
}