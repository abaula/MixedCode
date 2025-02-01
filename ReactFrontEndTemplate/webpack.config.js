const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const merge = require('webpack-merge');
const pug = require('./webpack/pug');
const devserver = require('./webpack/devserver');
const sass = require('./webpack/sass');
const css = require('./webpack/css');
const extractCSS = require('./webpack/css.extract');
const uglifyJS = require('./webpack/js.uglify');
const images = require('./webpack/images');
var CopyWebpackPlugin = require('copy-webpack-plugin');

const PATHS = {
    source: path.join(__dirname, 'src'),
    build: path.join(__dirname, 'dist'),
    webconfig: path.join(__dirname, 'web.config'),
};

const common = env => merge([
    {
        entry: {
            'index': PATHS.source + '/ts/index.tsx'
        },
        module: {
            rules: [
              {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/
              }
            ]
          },
        resolve: {
            extensions: [".tsx", ".ts", ".js"]
          },
        output: {
            path: PATHS.build,
            filename: 'js/[name].js'
        },
        plugins: [
            new HtmlWebpackPlugin({
                filename: 'index.html',
                chunks: ['index', 'common'],
                template: PATHS.source + '/pug/index.pug'
            }),
            new webpack.optimize.CommonsChunkPlugin({
                name: 'common'
            }),
            new webpack.DefinePlugin({
                __CORS_DEV__: JSON.stringify(JSON.parse(env.cors_dev || 'false')),
                __CORS_TEST__: JSON.stringify(JSON.parse(env.cors_test || 'false')),
                __CORS_PRE__: JSON.stringify(JSON.parse(env.cors_pre || 'false'))
            }),
            new CopyWebpackPlugin([{
                from: PATHS.webconfig
              }])
        ],
        devServer: {
            inline: true
        }
    },
    pug(),
    images()
]);

module.exports = function(env) {
    if (env.production){
        return merge([
            common(env),
            extractCSS(),
            uglifyJS()
        ]);
    }
    if (env.development){
        return merge([
            common(env),
            extractCSS()
        ])
    }
    if (env.devserver){
        return merge([
            common(env),
            devserver(),
            sass(),
            css()
        ])
    }
};