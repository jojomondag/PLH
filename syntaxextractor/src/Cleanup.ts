const ec = require('./ExtractCode');
const vscode = require('vscode');
const roslynSyntaxExtractor = require('../lib/roslynSyntaxExtractor');

export async function cleanUpCSharpFiles(option: string) {
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
  
module.exports = {
  cleanUpCSharpFiles
};
