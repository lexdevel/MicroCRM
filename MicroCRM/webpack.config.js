const path = require("path");
const webpack = require("webpack");

module.exports = {
    entry: {
        "app": "./client-app/main.ts",
        "polyfills": "./client-app/polyfills.ts",
        "bootstrap": "./client-app/bootstrap.ts"
    },
    output: {
        path: path.resolve(__dirname, "./wwwroot/dist"),
        publicPath: "/dist/",
        filename: "[name].bundle.js"
    },
    resolve: {
        extensions: [
            ".ts",
            ".js"
        ]
    },
    module: {
        rules: [
            {
                test: /\.ts$/,
                use: [
                    {
                        loader: "ts-loader"
                    },
                    "angular2-template-loader"
                ]
            },
            {
                test: /\.html$/,
                use: [
                    {
                        loader: "html-loader"
                    }
                ]
            },
            {
                test: /\.css$/,
                use: ["to-string-loader", "style-loader", "css-loader"]
            }
        ]
    },
    devtool: "source-map",
    plugins: [
        new webpack.ProvidePlugin({ $: "jquery", jQuery: "jquery", Popper: "popper.js" }),
        new webpack.ContextReplacementPlugin(/angular(\\|\/)core/, "./client-app", {}),
        new webpack.optimize.CommonsChunkPlugin({ name: ["app", "polyfills", "bootstrap"] }),
        new webpack.optimize.UglifyJsPlugin({ sourceMap: true })
    ]
};
