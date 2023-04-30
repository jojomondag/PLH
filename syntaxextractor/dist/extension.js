/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ([
/* 0 */,
/* 1 */
/***/ ((module) => {

module.exports = require("vscode");

/***/ }),
/* 2 */
/***/ ((module, exports, __webpack_require__) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.button4 = exports.button3 = exports.button2 = exports.button1 = void 0;
const Cleanup_1 = __webpack_require__(3);
async function button1() {
    console.log('Button1');
    (0, Cleanup_1.cleanUpCSharpFiles)('1. Function names only'); // call decideAction with the corresponding parameter
}
exports.button1 = button1;
async function button2() {
    console.log('Button2');
    (0, Cleanup_1.cleanUpCSharpFiles)('2. Function names and parameters'); // call decideAction with the corresponding parameter
}
exports.button2 = button2;
async function button3() {
    console.log('Button3');
    (0, Cleanup_1.cleanUpCSharpFiles)('3. Function names, parameters, and return types'); // call decideAction with the corresponding parameter
}
exports.button3 = button3;
async function button4() {
    console.log('Button4');
    (0, Cleanup_1.cleanUpCSharpFiles)('4. Access modifiers, static keyword, function names, parameters, and return types'); // call decideAction with the corresponding parameter
}
exports.button4 = button4;
module.exports = {
    button1,
    button2,
    button3,
    button4,
};


/***/ }),
/* 3 */
/***/ ((module, exports, __webpack_require__) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.cleanUpCSharpFiles = void 0;
const ec = __webpack_require__(4);
const vscode = __webpack_require__(1);
const roslynSyntaxExtractor = __webpack_require__(9);
async function cleanUpCSharpFiles(option) {
    let cleanedUpContent = '';
    if (option === "1. Function names only") {
        vscode.window.showInformationMessage("We will clean up the C# files in the project to only include function names.");
        //The extractCodeContent function returns a string with the content of all the files in the currently opened Project.
        const file = ec.extractCodeContent('.cs', option);
        vscode.window.showInformationMessage("we will extract this " + file);
        // Extract method names using RoslynSyntaxExtractor
        cleanedUpContent = roslynSyntaxExtractor.Start(1, file);
        vscode.window.showInformationMessage("Cleaned up content: " + cleanedUpContent);
        // Save the cleaned up content to a file
        const fileName = option + `${ec.getCurrentDateTimeString()}_CleanedUpCodeContent.txt`;
        ec.saveContentToFile(cleanedUpContent, fileName);
        // Also copy to clipboard
        ec.copyToClipboard(cleanedUpContent);
    }
}
exports.cleanUpCSharpFiles = cleanUpCSharpFiles;
module.exports = {
    cleanUpCSharpFiles
};


/***/ }),
/* 4 */
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.getCurrentDateTimeString = exports.copyToClipboard = exports.saveContentToFile = exports.extractCodeContent = void 0;
// Import dependencies
const fs = __webpack_require__(5);
const path = __webpack_require__(6);
const vscode = __webpack_require__(1);
// Import custom module
const codeAnalyzer_1 = __webpack_require__(7);
// The goal of the extractCodeContent function is to loop through all the C# files in the project and extract the code into a single file
function extractCodeContent(fileType, extraText) {
    // Get all the code files of a given file type
    const codeFiles = (0, codeAnalyzer_1.getAllCodeFilesOfType)(fileType);
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
exports.extractCodeContent = extractCodeContent;
// This function saves the given content to a file with a given name
function saveContentToFile(content, fileName) {
    // Get the workspace folder and create the output folder if it doesn't exist
    const workspaceFolder = vscode.workspace.workspaceFolders[0].uri.fsPath;
    const outputPath = path.join(workspaceFolder, 'syntaxExtractorText');
    fs.mkdirSync(outputPath, { recursive: true });
    // Save the content to the output file and return the file path
    const outputFile = path.join(outputPath, fileName);
    fs.writeFileSync(outputFile, content, { encoding: 'utf-8' });
    return outputFile;
}
exports.saveContentToFile = saveContentToFile;
// This function copies the given text to the clipboard
async function copyToClipboard(textToCopy) {
    try {
        await vscode.env.clipboard.writeText(textToCopy);
        console.log('Copied text to clipboard');
    }
    catch (err) {
        console.log(`Failed to copy text to clipboard: ${err}`);
    }
}
exports.copyToClipboard = copyToClipboard;
// This function returns the current date and time as a formatted string
function getCurrentDateTimeString() {
    const currentDate = new Date();
    const formattedDate = `${currentDate.getFullYear()}-${(currentDate.getMonth() + 1).toString().padStart(2, '0')}-${currentDate.getDate().toString().padStart(2, '0')}_${currentDate.getHours().toString().padStart(2, '0')}-${currentDate.getMinutes().toString().padStart(2, '0')}-${currentDate.getSeconds().toString().padStart(2, '0')}`;
    return formattedDate;
}
exports.getCurrentDateTimeString = getCurrentDateTimeString;
function cleanup(cleanedUpContent) {
    // Save the cleaned up content to a file
    const fileName = `${getCurrentDateTimeString()}_CleanedUpCodeContent.txt`;
    const filePath = saveContentToFile(cleanedUpContent, fileName);
    // Copy the cleaned up content to the clipboard
    copyToClipboard(cleanedUpContent);
}


/***/ }),
/* 5 */
/***/ ((module) => {

module.exports = require("fs");

/***/ }),
/* 6 */
/***/ ((module) => {

module.exports = require("path");

/***/ }),
/* 7 */
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.loadAllTextFiles = exports.getAllCodeFilesOfType = void 0;
const vscode = __webpack_require__(1);
const initActions_1 = __webpack_require__(8);
const path = __webpack_require__(6);
const fs = __webpack_require__(5);
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
//Create a function that loads all textfiles from syntaxExtractorText folder In the main workspaceFolders and stores in a list
function loadAllTextFiles() {
    if (initActions_1.workspaceFolders && initActions_1.workspaceFolders.length > 0) {
        const projectPath = initActions_1.workspaceFolders[0].uri.fsPath;
        const textFolderPath = path.join(projectPath, 'syntaxExtractorText');
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
                    if (filePath.endsWith('.txt')) {
                        fileList.push(filePath);
                    }
                }
            }
        };
        walk(textFolderPath);
        return fileList;
    }
    else {
        vscode.window.showErrorMessage('No workspace folder is currently open.');
        return [];
    }
}
exports.loadAllTextFiles = loadAllTextFiles;


/***/ }),
/* 8 */
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.initialFunction = exports.workspaceFolders = void 0;
const vscode = __webpack_require__(1);
const ExtractCode_1 = __webpack_require__(4);
exports.workspaceFolders = vscode.workspace.workspaceFolders;
function initialFunction() {
    if (exports.workspaceFolders && exports.workspaceFolders.length > 0) {
        const projectPath = exports.workspaceFolders[0].uri.fsPath;
        vscode.window.showInformationMessage(`Current project path: ${projectPath}`);
        // Call the extractCodeFilesContent with type .cs
        (0, ExtractCode_1.extractCodeContent)('.cs', "Project_Session_Start_Snapshot");
    }
    else {
        vscode.window.showInformationMessage('No workspace folder is currently open.');
    }
}
exports.initialFunction = initialFunction;


/***/ }),
/* 9 */
/***/ (() => {




/***/ })
/******/ 	]);
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		var cachedModule = __webpack_module_cache__[moduleId];
/******/ 		if (cachedModule !== undefined) {
/******/ 			return cachedModule.exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId](module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
var __webpack_exports__ = {};
// This entry need to be wrapped in an IIFE because it need to be isolated against other modules in the chunk.
(() => {
var exports = __webpack_exports__;

Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.deactivate = exports.activate = void 0;
const vscode = __webpack_require__(1);
const buttonActions = __webpack_require__(2);
const init = __webpack_require__(8);
function activate(context) {
    const treeDataProvider = new ButtonsTreeDataProvider(context);
    vscode.window.createTreeView('syntaxExtractorCustomView', { treeDataProvider });
    init.initialFunction();
    let disposable1 = vscode.commands.registerCommand('syntaxextractor.button1', () => {
        buttonActions.button1();
    });
    let disposable2 = vscode.commands.registerCommand('syntaxextractor.button2', () => {
        buttonActions.button2();
    });
    let disposable3 = vscode.commands.registerCommand('syntaxextractor.button3', () => {
        buttonActions.button3();
    });
    let disposable4 = vscode.commands.registerCommand('syntaxextractor.button4', () => {
        buttonActions.button4();
    });
    context.subscriptions.push(disposable1, disposable2, disposable3, disposable4);
}
exports.activate = activate;
class ButtonsTreeDataProvider {
    constructor(context) {
        this.context = context;
        this._onDidChangeTreeData = new vscode.EventEmitter();
        this.onDidChangeTreeData = this._onDidChangeTreeData.event;
    }
    getTreeItem(element) {
        return element;
    }
    getChildren(element) {
        if (!element) {
            return Promise.resolve(this.getButtons());
        }
        return Promise.resolve([]);
    }
    getButtons() {
        const labels = ['1. Function names only', '2. Function names and parameters', '3. Function names, parameters, and return types', '4. Access modifiers, static keyword, function names, parameters, and return types'];
        return labels.map((label) => new ButtonTreeItem(label, `syntaxextractor.button${labels.indexOf(label) + 1}`));
    }
}
class ButtonTreeItem extends vscode.TreeItem {
    constructor(label, command) {
        super(label, vscode.TreeItemCollapsibleState.None);
        this.command = {
            command: command,
            title: '',
            arguments: [label]
        };
    }
}
function deactivate() { }
exports.deactivate = deactivate;

})();

module.exports = __webpack_exports__;
/******/ })()
;
//# sourceMappingURL=extension.js.map