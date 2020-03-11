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
