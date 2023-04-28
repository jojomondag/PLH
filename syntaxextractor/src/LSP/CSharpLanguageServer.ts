import { TextDocument, TextEdit, Position } from 'vscode-languageserver';
import { CSharpSyntaxTree, CSharpParser } from 'dotnet-roslyn';

interface Workspace {
  // Add any necessary properties here
}

interface DocumentInfo {
  version: number;
  syntaxTree: CSharpSyntaxTree;
}

// Map of document URIs to document information
const documents: Map<string, DocumentInfo> = new Map();

// Create a new workspace object
const workspace: Workspace = {
  // Add any necessary properties here
};

const languageServer = {
  // Handle the "textDocument/didOpen" notification
  async handleTextDocumentDidOpen(params: { textDocument: { uri: string; version: number; }; }): Promise<void> {
    const { uri, version } = params.textDocument;
    const textDocument = documents.get(uri);

    if (!textDocument) {
      // Parse the C# code and create a syntax tree
      const parser = new CSharpParser();
      const syntaxTree = parser.parse(params.textDocument.text);

      // Add the document to the map of documents
      documents.set(uri, { version, syntaxTree });
    }
  },

  // Handle the "textDocument/didChange" notification
  async handleTextDocumentDidChange(params: { textDocument: { uri: string; version: number; }; contentChanges: { range: { start: Position; end: Position; }; text: string; }[]; }): Promise<void> {
    const { uri, version } = params.textDocument;
    const textDocument = documents.get(uri);

    if (textDocument && textDocument.version === version) {
      // Apply the changes to the syntax tree
      const parser = new CSharpParser();
      const newText = params.contentChanges[0].text;
      const updatedSyntaxTree = parser.parse(newText, textDocument.syntaxTree);

      // Update the document in the map of documents
      documents.set(uri, { version, syntaxTree: updatedSyntaxTree });
    }
  },

  // Handle the "textDocument/didClose" notification
  async handleTextDocumentDidClose(params: { textDocument: { uri: string; }; }): Promise<void> {
    const { uri } = params.textDocument;
    documents.delete(uri);
  },

  // Handle the "textDocument/hover" request
  async handleTextDocumentHover(params: { textDocument: { uri: string; }; position: Position; }): Promise<{ contents: string[]; }> {
    const { uri, position } = params.textDocument;
    const textDocument = documents.get(uri);

    if (textDocument) {
      // Get the node at the specified position in the syntax tree
      const node = textDocument.syntaxTree.getNodeAt(position.line, position.character);

      if (node) {
        // Return information about the node as a hover tooltip
        return {
          contents: [node.kindName]
        };
      }
    }

    return { contents: [] };
  },

  // Add any additional language server methods here
};

export default languageServer;
