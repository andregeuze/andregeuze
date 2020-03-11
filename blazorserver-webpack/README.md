# Blazor Server and dependency management with NPM and Webpack

###### A 30 minute read

---

## Basic project setup

- Run `mkdir blazorserver-webpack`

- Run `cd blazorserver-webpack`

- Run `dotnet new blazorserver`

- Rename wwwroot to Client

- Delete from Client/css:

  - bootstrap
  - open-iconic

- Edit Client/css/site.css, remove the first line:
  `@import url('open-iconic/font/css/open-iconic-bootstrap.min.css');`

- Create new files in Client:

  - index.js

    - Add content:
      ```javascript
      require("bootstrap");
      require("bootstrap/dist/css/bootstrap.css");
      require("./css/site.css");
      ```

  - host_template.ejs

    - Copy contents from Pages/\_Host.cshtml into this file
    - Remove these two lines from the `<head>` section:
      ```html
      <link rel="stylesheet" href="css/bootstrap/bootstrap.min.css" />
      <link href="css/site.css" rel="stylesheet" />
      ```

- Create `.gitignore` file with this content:

  ```
  node_modules/
  wwwroot/
  publish/
  _Host.cshtml
  ```

## Setup NPM and install packages

- Run `npm init`

- Install NPM dev dependencies with this oneliner:

  `npm i -D clean-webpack-plugin css-loader html-webpack-plugin mini-css-extract-plugin style-loader webpack webpack-cli jquery popper.js`

- Install npm dependencies with this oneliner:

  `npm i bootstrap open-iconic`

- Edit package json, replace the scripts with the following scripts block:

  ```json
  "scripts": {
    "release": "webpack --mode=production",
    "publish": "npm run release && dotnet publish -c Release -o publish",
    "production": "npm run publish && cd publish && dotnet blazorserver-webpack.exe",
    "start": "npm run release && dotnet run"
  },
  ```

## Setup Webpack

- Create a webpack configuration file `webpack.config.js` with the following content:

  ```javascript
  const path = require("path");
  const { CleanWebpackPlugin } = require("clean-webpack-plugin");
  const HtmlWebpackPlugin = require("html-webpack-plugin");
  const MiniCssExtractPlugin = require("mini-css-extract-plugin");
  module.exports = {
    entry: "./Client/index.js",
    output: {
      path: path.resolve(__dirname, "wwwroot"),
      publicPath: "/",
      filename: "[name].[chunkhash].js"
    },
    resolve: {
      extensions: [".js"]
    },
    module: {
      rules: [
        { test: /\.css$/, use: [MiniCssExtractPlugin.loader, "css-loader"] }
      ]
    },
    plugins: [
      new CleanWebpackPlugin(),
      new MiniCssExtractPlugin({
        filename: "[name].[chunkhash].css"
      }),
      new HtmlWebpackPlugin({
        template: "./Client/host_template.ejs",
        favicon: "./Client/favicon.ico",
        filename: "../Pages/_Host.cshtml"
      })
    ]
  };
  ```

## Done!

Run `npm start` and enjoy.
