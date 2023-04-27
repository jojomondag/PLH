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
exports.initialFunction = void 0;
const vscode = __webpack_require__(1);
//This works but you have to open a folder in the test enviroment so you can test it
function initialFunction() {
    const workspaceFolders = vscode.workspace.workspaceFolders;
    if (workspaceFolders && workspaceFolders.length > 0) {
        const projectPath = workspaceFolders[0].uri.fsPath;
        console.log(`Current project path: ${projectPath}`);
    }
    else {
        console.log('No workspace folder is currently open.');
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
    // Call the initial function after creating the tree view
    (0, initActions_1.initialFunction)();
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