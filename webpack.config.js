var path = require("path");
var webpack = require("webpack");
var MinifyPlugin = require("terser-webpack-plugin");
var CopyWebpackPlugin = require("copy-webpack-plugin");

function resolve(filePath) {
  return path.join(__dirname, filePath)
}

var babelOptions = {
    presets: [
        ["@babel/preset-env", {
            "targets": {
                "browsers": ["last 2 versions"]
            },
            "modules": false
        }]
    ]
};

var isProduction = process.argv.indexOf("-p") >= 0;
console.log("Bundling for " + (isProduction ? "production" : "development") + "...");

module.exports = {
  devtool: "source-map",
  entry: resolve('./src/tic-tac-toe--fable.fsproj'),
  output: {
    filename: '[name].js',
    path: resolve('./build'),
  },
  resolve: {
    modules: [resolve("./node_modules/")]
  },
  devServer: {
    contentBase: resolve('./public'),
    port: 8080
  },
  optimization: {
    splitChunks: {
        cacheGroups: {
            commons: {
                test: /node_modules/,
                name: "vendors",
                chunks: "all"
            }
        }
    },
    minimizer: isProduction ? [new MinifyPlugin()] : []
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
         test: /\.(svg|png)(\?[a-z0-9=&.]+)?$/,
         use: 'base64-inline-loader?limit=1000&name=[name].[ext]'
      }
    ]
  }
};
