const path = require('path');

const config = {
    entry: {
        // bundle all Angular2 scripts and serve them from a single file
        'angular2': [
            'rxjs',
            'reflect-metadata',
            'angular2/core',
            'angular2/router',
            'angular2/http'
        ],
        'app': './app/app'
    },
    output: {
        path: __dirname + '/build',
        publicPath: '/build',
        filename: '[name].bundle.js',
        sourceMapFilename: '[file].js.map',
        chunkFilename: '[id].chunk.js'
    },
    resolve: {
        extensions: ['.webpack.js', '.web.js', '.ts', '.tsx', '.js', '.css', '.html']
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