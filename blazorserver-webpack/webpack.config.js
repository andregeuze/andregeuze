const path = require("path");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const TerserJSPlugin = require('terser-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const OptimizeCSSAssetsPlugin = require('optimize-css-assets-webpack-plugin');
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
    optimization: {
        minimizer: [new TerserJSPlugin({}), new OptimizeCSSAssetsPlugin({})],
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
