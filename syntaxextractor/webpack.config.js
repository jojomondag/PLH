const path = require('path');
// eslint-disable-next-line @typescript-eslint/naming-convention
const { CleanWebpackPlugin } = require('clean-webpack-plugin');

module.exports = {
  entry: './src/index.ts',
  devtool: 'inline-source-map',
  mode: 'development',
  plugins: [
    new CleanWebpackPlugin(),
  ],
  resolve: {
    extensions: ['.tsx', '.ts', '.js'],
  },
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },
      {
        test: /omnisharp-client[\\/]lib[\\/]drivers[\\/](http|stdio|websocket)\.(d\.ts|d)?$/,
        use: 'ignore-loader',
      },
    ],
  },
  output: {
    filename: 'extension.js',
    path: path.resolve(__dirname, 'dist'),
    libraryTarget: 'commonjs2',
    devtoolModuleFilenameTemplate: '../[resource-path]',
  },
  externals: {
    vscode: 'commonjs vscode',
  },
  target: 'node',
  stats: {
    warningsFilter: /export .* was not found in/,
  },
};
