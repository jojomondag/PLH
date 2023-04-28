/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ([
/* 0 */,
/* 1 */
/***/ ((module) => {

module.exports = require("vscode");

/***/ }),
/* 2 */
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.initialFunction = exports.workspaceFolders = void 0;
const vscode = __webpack_require__(1);
const ExtractCode_1 = __webpack_require__(7);
exports.workspaceFolders = vscode.workspace.workspaceFolders;
function initialFunction() {
    if (exports.workspaceFolders && exports.workspaceFolders.length > 0) {
        const projectPath = exports.workspaceFolders[0].uri.fsPath;
        vscode.window.showInformationMessage(`Current project path: ${projectPath}`);
        // Call the extractCodeFilesContent with type .cs
        (0, ExtractCode_1.extractCodeFilesContent)('.cs');
    }
    else {
        vscode.window.showInformationMessage('No workspace folder is currently open.');
    }
}
exports.initialFunction = initialFunction;


/***/ }),
/* 3 */
/***/ ((module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.button4 = exports.button3 = exports.button2 = exports.button1 = void 0;
async function button1() {
    console.log('Button2');
}
exports.button1 = button1;
function button2() {
    console.log('Button2');
}
exports.button2 = button2;
function button3() {
    console.log('Button3');
}
exports.button3 = button3;
function button4() {
    console.log('Button3');
}
exports.button4 = button4;
module.exports = {
    button1,
    button2,
    button3,
    button4,
};


/***/ }),
/* 4 */
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.getAllCodeFilesOfType = void 0;
const vscode = __webpack_require__(1);
const initActions_1 = __webpack_require__(2);
const path = __webpack_require__(5);
const fs = __webpack_require__(6);
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


/***/ }),
/* 5 */
/***/ ((module) => {

module.exports = require("path");

/***/ }),
/* 6 */
/***/ ((module) => {

module.exports = require("fs");

/***/ }),
/* 7 */
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.extractCodeFilesContent = exports.text = void 0;
const fs = __webpack_require__(6);
const path = __webpack_require__(5);
const vscode = __webpack_require__(1);
const codeAnalyzer_1 = __webpack_require__(4);
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
const initActions_1 = __webpack_require__(2);
const buttonActions = __webpack_require__(3);
function activate(context) {
    const treeDataProvider = new ButtonsTreeDataProvider(context);
    vscode.window.createTreeView('syntaxExtractorCustomView', { treeDataProvider });
    let disposable1 = vscode.commands.registerCommand('syntaxextractor.button1', () => {
        (0, initActions_1.initialFunction)();
        buttonActions.button1();
    });
    let disposable2 = vscode.commands.registerCommand('syntaxextractor.button2', () => {
        (0, initActions_1.initialFunction)();
        buttonActions.button2();
    });
    let disposable3 = vscode.commands.registerCommand('syntaxextractor.button3', () => {
        (0, initActions_1.initialFunction)();
        buttonActions.button3();
    });
    let disposable4 = vscode.commands.registerCommand('syntaxextractor.button4', () => {
        (0, initActions_1.initialFunction)();
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