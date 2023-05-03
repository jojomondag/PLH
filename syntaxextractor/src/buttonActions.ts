import { MyCodeLensProvider } from './MyCodeLensProvider';
import { cleanUpCSharpFiles } from './Cleanup';

const codeLensProvider = new MyCodeLensProvider();

export async function button1(command : string, codeLensProvider: MyCodeLensProvider) {
  console.log('Button1');
  cleanUpCSharpFiles(command, codeLensProvider);
}

export async function button2(codeLensProvider: MyCodeLensProvider) {
  console.log('Button2');
  cleanUpCSharpFiles('2. Function names and parameters', codeLensProvider);
}

export async function button3(codeLensProvider: MyCodeLensProvider) {
  console.log('Button3');
  cleanUpCSharpFiles('3. Function names, parameters, and return types', codeLensProvider);
}

export async function button4(codeLensProvider: MyCodeLensProvider) {
  console.log('Button4');
  cleanUpCSharpFiles('4. Access modifiers, static keyword, function names, parameters, and return types', codeLensProvider);
}