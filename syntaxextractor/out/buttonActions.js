"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.button4 = exports.button3 = exports.button2 = exports.button1 = void 0;
const MyCodeLensProvider_1 = require("./MyCodeLensProvider");
const Cleanup_1 = require("./Cleanup");
const codeLensProvider = new MyCodeLensProvider_1.MyCodeLensProvider();
async function button1(command, codeLensProvider) {
    console.log('Button1');
    (0, Cleanup_1.cleanUpCSharpFiles)(command, codeLensProvider);
}
exports.button1 = button1;
async function button2(codeLensProvider) {
    console.log('Button2');
    (0, Cleanup_1.cleanUpCSharpFiles)('2. Function names and parameters', codeLensProvider);
}
exports.button2 = button2;
async function button3(codeLensProvider) {
    console.log('Button3');
    (0, Cleanup_1.cleanUpCSharpFiles)('3. Function names, parameters, and return types', codeLensProvider);
}
exports.button3 = button3;
async function button4(codeLensProvider) {
    console.log('Button4');
    (0, Cleanup_1.cleanUpCSharpFiles)('4. Access modifiers, static keyword, function names, parameters, and return types', codeLensProvider);
}
exports.button4 = button4;
//# sourceMappingURL=buttonActions.js.map