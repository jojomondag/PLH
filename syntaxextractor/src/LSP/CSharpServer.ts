import { createServer, ProposedFeatures } from 'vscode-languageserver';
import { TextDocument } from 'vscode-languageserver-textdocument';
import languageServer from './CSharpLanguageServer';

const server = createServer({
  ...languageServer,
  documentSelector: [{ language: 'csharp', scheme: 'file' }]
}, (socket) => {
  return createServerSocketTransport(socket);
});

server.listen().then(() => {
  console.log('C# language server listening on port ' + server.address().port);
});
