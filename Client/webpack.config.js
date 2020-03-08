var path = require("path"),
  webpack = require("webpack"),
  MiniCssExtractPlugin = require("mini-css-extract-plugin"),
  FileManagerPlugin = require("filemanager-webpack-plugin"),
  pkg = require("../package.json"),
  local = require("../local.json");

// variables
var isProduction =
  process.argv.indexOf("-p") >= 0 || process.env.NODE_ENV === "production";
var sourcePath = path.join(__dirname, "./js/src");
pkg = Object.assign({}, pkg, local);

var commonConfig = {
  context: sourcePath,
  target: "web",
  mode: isProduction ? "production" : "development",
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        exclude: [/node_modules/, /_Development/],
        use: {
          loader: "babel-loader",
          options: {
            presets: ["@babel/preset-env", "@babel/preset-react"]
          }
        }
      },
      {
        test: /.jsx?$/,
        exclude: [/node_modules/, /_Development/],
        use: {
          loader: "babel-loader",
          options: {
            presets: ["@babel/preset-env", "@babel/preset-react"]
          }
        }
      }
    ]
  },
  externals: {
    jquery: "jQuery"
  },
  plugins: [
    new webpack.EnvironmentPlugin({
      NODE_ENV: "development", // use 'development' unless process.env.NODE_ENV is defined
      DEBUG: false
    }),
    new webpack.ProvidePlugin({
      $: "jquery",
      jQuery: "jquery",
      "window.jQuery": "jquery"
    }),
    new MiniCssExtractPlugin({
      filename: "../module.css"
    })
  ]
};

var mainAppConfig = Object.assign({}, commonConfig, {
  context: path.join(__dirname, "./js"),
  entry: {
    flickrgallery: "./main.jsx"
  },
  output: {
    path: isProduction
      ? path.resolve(__dirname, "../Server/FlickrGallery/js")
      : pkg.dnn.pathsAndFiles.devSitePath +
        "\\DesktopModules\\MVC\\Connect\\FlickrGallery\\js",
    filename: "[name].js"
  },
  resolve: {
    extensions: [".js", ".ts", ".tsx"],
    mainFields: ["module", "browser", "main"],
    alias: {
      app: path.resolve(__dirname, "src/app/")
    }
  }
});

var outPath = isProduction
  ? path.resolve(__dirname, "../Server/FlickrGallery")
  : pkg.dnn.pathsAndFiles.devSitePath +
    "\\DesktopModules\\MVC\\Connect\\FlickrGallery";
var cssConfig = Object.assign({}, commonConfig, {
  context: path.join(__dirname, "./css"),
  entry: "./module.less",
  output: {
    path: outPath,
    filename: "module.css.js"
  },
  module: {
    rules: [
      {
        test: /\.less$/,
        use: [MiniCssExtractPlugin.loader, "css-loader", "less-loader"]
      }
    ]
  },
  plugins: [
    new MiniCssExtractPlugin({
      filename: "module.css"
    }),
    new FileManagerPlugin({
      onEnd: {
        delete: [outPath + "/module.css.js"]
      }
    })
  ]
});

module.exports = [mainAppConfig, cssConfig];
