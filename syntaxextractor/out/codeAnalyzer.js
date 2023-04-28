"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.getAllCodeFilesOfType = void 0;
const vscode = require("vscode");
const initActions_1 = require("./initActions");
const path = require("path");
const fs = require("fs");
function printFilenames(fileList) {
    for (const file of fileList) {
        console.log(path.basename(file));
    }
}
function getAllCodeFilesOfType(fileType) {
    if (initActions_1.workspaceFolders && initActions_1.workspaceFolders.length > 0) {
        const projectPath = initActions_1.workspaceFolders[0].uri.fsPath;
        const parentProjectPath = path.dirname(projectPath);
        //Create a list to hold all text/code files in the parent project folder and its subdirectories
        let fileList = [];
        //Loop through all files in the parent project folder and its subdirectories
        const walk = (dir) => {
            const files = fs.readdirSync(dir);
            for (const file of files) {
                const filePath = path.join(dir, file);
                const stat = fs.statSync(filePath);
                if (stat.isDirectory()) {
                    walk(filePath);
                }
                else {
                    //Check if the file is a text/code file of the specified type
                    if (filePath.endsWith(fileType)) {
                        fileList.push(filePath);
                    }
                }
            }
        };
        walk(parentProjectPath);
        return fileList;
    }
    else {
        vscode.window.showErrorMessage('No workspace folder is currently open.');
        return [];
    }
}
exports.getAllCodeFilesOfType = getAllCodeFilesOfType;
//# sourceMappingURL=codeAnalyzer.js.map