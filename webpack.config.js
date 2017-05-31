var path = require("path");
var webpack = require("webpack");
var CopyWebpackPlugin = require("copy-webpack-plugin");

function resolve(filePath) {
  return path.join(__dirname, filePath)
}

var babelOptions = {
  presets: [["es2015", {"modules": false}]],
  plugins: ["transform-runtime"]
}

module.exports = {
  devtool: "source-map",
  entry: resolve('./tic-tac-toe--fable.fsproj'),
  output: {
    filename: 'bundle.js',
    path: resolve('./build'),
  },
  devServer: {
    contentBase: resolve('./public'),
    port: 8080
  },
  plugins: [
    new CopyWebpackPlugin([
      { from: './public' }
    ])
  ],
  module: {
    rules: [
      {
        test: /\.fs(x|proj)?$/,
        use: {
          loader: "fable-loader",
          options: { babel: babelOptions }
        }
      },
      {
        test: /\.js$/,
        exclude: /node_modules[\\\/](?!fable-)/,
        use: {
          loader: 'babel-loader',
          options: babelOptions
        },
      },
      {
        test: /\.s(a|c)ss$/,
        use: [
          "style-loader",
          "css-loader",
          "sass-loader"
        ]
      },
      {
         test: /\.svg|\.png$/,
         loader: 'file-loader',
         query: {
           name: 'static/media/[name].[hash:8].[ext]'
         }
      }
    ]
  }
};
