// webpack.config.js

// module.exports is needed for modules in Node.js, webpack runs in Node.js.
module.exports = {
    // configure the module and loaders
    module: {
        rules: [
            // for .js -> use babel-loader
            { test: /\.css$/, use: ['style-loader', 'css-loader'] },
            { test: /\.js$/, use: ['babel-loader'] },
        ]
    }
};