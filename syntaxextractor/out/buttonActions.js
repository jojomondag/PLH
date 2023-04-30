"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.button4 = exports.button3 = exports.button2 = exports.button1 = void 0;
const Cleanup_1 = require("./CodeExtractors/Cleanup");
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
//# sourceMappingURL=buttonActions.js.map