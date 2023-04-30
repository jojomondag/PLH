"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const edge = require("edge-js");
const roslynSyntaxExtractor = edge.func({
    assemblyFile: __dirname + '/RoslynSyntaxExtractor.dll',
    typeName: 'RoslynSyntaxExtractor.RoslynSyntaxExtractor',
    methodName: 'Start'
});
exports.default = roslynSyntaxExtractor;
//# sourceMappingURL=roslynSyntaxExtractor.js.map