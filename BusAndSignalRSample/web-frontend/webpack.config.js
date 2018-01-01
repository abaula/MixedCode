const path = require('path');

const common = require('./webpack/common');
const merge = require('webpack-merge');
const devserver = require('./webpack/devserver');
const sass = require('./webpack/sass');
const css = require('./webpack/css');
const extractCSS = require('./webpack/css.extract');
const uglifyJS = require('./webpack/js.uglify');


const PATHS = {
    source: path.join(__dirname, 'src'),
    build: path.join(__dirname, 'dist'),
    webconfig: path.join(__dirname, 'web.config'),
};

module.exports = function(env) {
    if (env.production){
        return merge([
            common(env, PATHS),
            extractCSS(),
            uglifyJS()
        ]);
    }
    if (env.development){
        return merge([
            common(env, PATHS),
            extractCSS()
        ])
    }
    if (env.devserver){
        return merge([
            common(env, PATHS),
            devserver(),
            sass(),
            css()
        ])
    }
};