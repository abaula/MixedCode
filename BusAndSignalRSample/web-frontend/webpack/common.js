const pug = require('./pug');
const images = require('./images');
const fonts = require('./fonts');
const merge = require('webpack-merge');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = function(env, paths) {
    return merge([
        {
            entry: {
                'index': paths.source + '/ts/index.tsx'
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
                path: paths.build,
                filename: 'js/[name].js'
            },
            plugins: [
                new HtmlWebpackPlugin({
                    filename: 'index.html',
                    chunks: ['index', 'common'],
                    template: paths.source + '/pug/index.pug'
                }),
                new webpack.optimize.CommonsChunkPlugin({
                    name: 'common'
                }),
                new webpack.DefinePlugin({
                    __CORS_DEV__: JSON.stringify(JSON.parse(env.cors_dev || 'false'))
                }),
                new CopyWebpackPlugin([{
                    from: paths.webconfig
                  }])
            ],
            devServer: {
                inline: true
            }
        },
        fonts(),
        pug(),
        images()
    ]);
};