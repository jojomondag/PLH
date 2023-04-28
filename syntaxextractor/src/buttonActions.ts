import { cleanUpCSharpFiles } from './CodeExtractors/Cleanup';

export async function button1() {
  console.log('Button1');
  cleanUpCSharpFiles('1. Function names only'); // call decideAction with the corresponding parameter
}
export async function button2() {
  console.log('Button2');
  cleanUpCSharpFiles('2. Function names and parameters'); // call decideAction with the corresponding parameter
}
export async function button3() {
  console.log('Button3');
  cleanUpCSharpFiles('3. Function names, parameters, and return types'); // call decideAction with the corresponding parameter
}
export async function button4() {
  console.log('Button4');
  cleanUpCSharpFiles('4. Access modifiers, static keyword, function names, parameters, and return types'); // call decideAction with the corresponding parameter
}
module.exports = {
  button1,
  button2,
  button3,
  button4,
};
