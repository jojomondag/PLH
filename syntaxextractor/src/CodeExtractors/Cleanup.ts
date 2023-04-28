import * as ec from './ExtractCode';
import * as vscode from 'vscode';

export async function cleanUpCSharpFiles(option: string) {
    let cleanedUpContent = '';
  
    if (option === "1. Function names only") {
      vscode.window.showInformationMessage("We will clean up the C# files in the project to only include function names.");
      const file = ec.extractCodeContent('.cs', option);
      vscode.window.showInformationMessage("we will extract this " + file);
  
      //implement the extracion logic here.

      // Save the cleaned up content to a file
      const fileName = option + `${ec.getCurrentDateTimeString()}_CleanedUpCodeContent.txt`;
      ec.saveContentToFile(cleanedUpContent, fileName);
  
      // Also copy to clipboard
      ec.copyToClipboard(cleanedUpContent + " fefefefe");
    }
  }