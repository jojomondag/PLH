import * as vscode from 'vscode';
import { extractCodeContent } from './ExtractCode';

export const workspaceFolders = vscode.workspace.workspaceFolders;

export function initialFunction(): void {
    if (workspaceFolders && workspaceFolders.length > 0) {
        const projectPath = workspaceFolders[0].uri.fsPath;
        vscode.window.showInformationMessage(`Current project path: ${projectPath}`);

        // Call the extractCodeFilesContent with type .cs
        extractCodeContent('.cs', "Project_Session_Start_Snapshot");

    } else {
        vscode.window.showInformationMessage('No workspace folder is currently open.');
    }
}