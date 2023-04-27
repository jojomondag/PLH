import * as vscode from 'vscode';

//This works but you have to open a folder in the test enviroment so you can test it

export function initialFunction(): void {
    const workspaceFolders = vscode.workspace.workspaceFolders;

    if (workspaceFolders && workspaceFolders.length > 0) {
        const projectPath = workspaceFolders[0].uri.fsPath;
        console.log(`Current project path: ${projectPath}`);
    } else {
        console.log('No workspace folder is currently open.');
    }
}
