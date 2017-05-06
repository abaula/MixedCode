/// <binding BeforeBuild="clean:wwwroot, build:increaseBuildNumber, build:html, build:js, build:css, copy:csslibs, copy:fonts" Clean="clean:wwwroot" />
"use strict";

var fs = require("fs");
var gulp = require("gulp");
var concat = require("gulp-concat");
var del = require("del");
var header = require("gulp-header");
var less = require("gulp-less");
var minifycss = require("gulp-minify-css");
var replace = require("gulp-replace");
var webpack = require('webpack-stream');
var rename = require("gulp-rename");

var settings = {
    appName: "async-tree",
    appVersion: "1.0.0",
    url: "https://github.com/abaula/MixedCode/ReactRedux",
    copyright: "Copyright 2017 / Anton Baula, anton.baula@gmail.com",
    path: {
        build: {
            root: "wwwroot",
            html: "wwwroot",
            js: "wwwroot/js",
            style: "wwwroot/css",
            fonts: "wwwroot/fonts"
        },
        src: {
            html: "src/*.html",
            indextsx: "src/ts/index.tsx",
            style: "src/css/*.less",
            buildNumber: "BuildNumber.txt",
            fonts: "src/resources/font-awesome-4.7.0/fonts/*",
            styleLibs: ["src/resources/font-awesome-4.7.0/css/font-awesome.css"]
        }
    }
};

function getAppVersion()
{
    var buildNumber = fs.readFileSync(settings.path.src.buildNumber);
    return settings.appVersion + "." + buildNumber;
}

gulp.task("build:increaseBuildNumber", function ()
{
    var buildNumber = parseInt(fs.readFileSync(settings.path.src.buildNumber));
    fs.writeFileSync(settings.path.src.buildNumber, ++buildNumber);
});

gulp.task("clean:wwwroot", function ()
{
    return del(settings.path.build.root + "/**/*");
});

gulp.task("build:html", function ()
{
    var appVersion = getAppVersion();

    return gulp.src(settings.path.src.html)
        .pipe(header(fs.readFileSync("Copyrights/Html.txt"),
            {
                version: appVersion,
                appName: settings.appName,
                url: settings.url,
                copyright: settings.copyright
            }))
        .pipe(replace("{#version#}", appVersion))
        .pipe(gulp.dest(settings.path.build.html));
});

gulp.task("build:js", function ()
{
    var appVersion = getAppVersion();

    return gulp.src(settings.path.src.indextsx)
        .pipe(webpack(require('./webpack.config.js')))
        .pipe(header(fs.readFileSync("Copyrights/Js.txt"),
            {
                version: appVersion,
                appName: settings.appName,
                url: settings.url,
                copyright: settings.copyright
            }))
        .pipe(rename({
            basename: "app.min." + appVersion,
        }))
        .pipe(gulp.dest(settings.path.build.js));        
});

gulp.task("build:css", function ()
{
    var appVersion = getAppVersion();

    return gulp.src(settings.path.src.style)
        .pipe(less())
        //.pipe(minifycss())
        .pipe(concat("app.min." + appVersion + ".css"))
        .pipe(header(fs.readFileSync("Copyrights/Js.txt"),
        {
            version: appVersion,
            appName: settings.appName,
            url: settings.url,
            copyright: settings.copyright
        }))
        .pipe(gulp.dest(settings.path.build.style));
});

gulp.task("copy:csslibs", function ()
{
    return gulp.src(settings.path.src.styleLibs)
        .pipe(gulp.dest(settings.path.build.style));
});

gulp.task("copy:fonts", function ()
{
    return gulp.src(settings.path.src.fonts)
        .pipe(gulp.dest(settings.path.build.fonts));
});
