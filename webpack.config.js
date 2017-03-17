const path = require('path');

const config = {
    entry: {
        app: './src/app.js'
    },
    output: {
        path: __dirname + '/build',
        publicPath: '/build',
        filename: '[name].bundle.js',
        sourceMapFilename: '[file].js.map',
        chunkFilename: '[id].chunk.js'
    },
    resolve: {
        extensions: ['', '.webpack.js', '.web.js', '.ts', '.tsx', '.js', '.css', '.html']
    },
    module: {
        rules: [
            { 
                test: /\.ts(x?)$/, 
                exclude: /node_modules/,
                use: 'ts-loader' 
            }
        ]
    },
    plugins: [

    ]
};

module.exports = config;