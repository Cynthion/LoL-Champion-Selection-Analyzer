const webpack = require('webpack');
const path = require('path');
const helpers = require('./helpers');

var CopyWebpackPlugin = require('copy-webpack-plugin');

const config = {
    devtool: 'source-map',

    entry: {
        'polyfills': './src/polyfills.ts',
        'vendor': './src/vendor.ts',
        'app': './src/app/app',
    },
    output: {
        path: helpers.root('src/app/dist'),
        filename: '[name].js',
        sourceMapFilename: '[file].map',
        chunkFilename: '[id].chunk.js'
    },
    resolve: {
        // Array of extensions that should be used to resolve modules
        extensions: ['.ts', '.tsx', '.js', '.json', '.css', '.html'],

        // Array of directory names to be resolved to the current directory
        modules: [helpers.root('src'), 'node_modules']
    },
    module: {
        rules: [
            // Support for .ts and .tsx files
            { 
                test: /\.ts(x?)$/, 
                exclude: [/\.(spec|e2e)\.ts$/],
                use: ['awesome-typescript-loader', 'angular2-template-loader']
            },
            // Support for .json files
            {
                test: /\.json$/,
                loader: 'json-loader'
            },
            // Support for .html and .css as raw text
            {
                test: /\.html$/,
                loader: 'raw-loader',
                exclude: [helpers.root('app/index.html')]
            }
        ]
    },
    plugins: [
        // Plugin: CommonsChunkPlugin
        // Description: Shares common code between the pages.
        // It identifies common modules and put them into a commons chunk.
        //
        // See: https://webpack.github.io/docs/list-of-plugins.html#commonschunkplugin
        // See: https://github.com/webpack/docs/wiki/optimization#multi-page-app
        new webpack.optimize.CommonsChunkPlugin({ name: ['vendor', 'polyfills'], minChunks: Infinity }),
        // Plugin: CopyWebpackPlugin
        // Description: Copy files and directories in webpack.
        //
        // Copies project static assets.
        //
        // See: https://www.npmjs.com/package/copy-webpack-plugin
        new CopyWebpackPlugin([{ from: 'src/assets', to: 'assets' }]),
    ]
    // we need this due to problems with es6-shim
    // node: {
    //     global: true,
    //     progress: false,
    //     crypto: 'empty',
    //     module: false,
    //     clearImmediate: false,
    //     setImmediate: false
    // }
};

// TODO used?
// config.target = 'electron-renderer';
module.exports = config;