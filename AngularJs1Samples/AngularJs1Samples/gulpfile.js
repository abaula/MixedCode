/// <binding BeforeBuild="clean:wwwroot, copyAppJsScripts, copyAppHtmlFiles, copyAppStyles, copyLibJsScripts" Clean="clean:wwwroot" />
/*
    Можно было бы сделать и через конфиг, но для этого проекта хватит и простого копирования директорий один к одному.
    Необходимо следить за именами файлов js в проектах, чтобы они сортировались в нужном порядке.
*/
var fs = require("fs");
var path = require("path");
var dir = require("node-dir");
var gulp = require("gulp");
var concat = require("gulp-concat");
var uglify = require("gulp-uglify");
var del = require("del");
var header = require("gulp-header");
var less = require("gulp-less");
var minifycss = require("gulp-minify-css");

// информация о версии
var appVersion = fs.readFileSync("version.txt");
// папка с библиотечными скриптами
var libsPath = "bower_components";
// папка с исходными скриптами
var scriptsPath = "scripts";
// папка куда копируются готовые скрипты
var outputPath = "wwwroot";


function replaceCoreFolder(dir, newCoreFolder)
{
    var dirArr = dir.split(path.sep);
    dirArr[0] = newCoreFolder;
    return dirArr.join(path.sep);
}

gulp.task("copyAppJsScripts", function()
{
    dir.subdirs(scriptsPath, function(err, subdirs)
    {
        if (err)
            throw err;

        subdirs.forEach(function(subdir)
        {
            gulp.src(path.join(subdir, "/*.js"))
                .pipe(uglify())
                .pipe(concat("app.min.js"))
                .pipe(header(fs.readFileSync("copyright.js.txt"), { version: appVersion }))
                .pipe(gulp.dest(replaceCoreFolder(subdir, outputPath)));
        });
    });
});

gulp.task("clean:wwwroot", function()
{
    return del("wwwroot/**/*");
});

gulp.task("copyAppHtmlFiles", function()
{
    return gulp.src(path.join(scriptsPath, "/**/*.html"))
        .pipe(header(fs.readFileSync("copyright.html.txt"), { version: appVersion }))
        .pipe(gulp.dest(outputPath));
});

gulp.task("copyAppStyles", function()
{
    dir.subdirs(scriptsPath, function(err, subdirs)
    {
        if (err)
            throw err;

        subdirs.forEach(function(subdir)
        {
            gulp.src(path.join(subdir, "/*.less"))
                .pipe(less())
                .pipe(minifycss())
                .pipe(concat("app.min.css"))
                .pipe(header(fs.readFileSync("copyright.js.txt"), { version: appVersion }))
                .pipe(gulp.dest(replaceCoreFolder(subdir, outputPath)));
        });
    });
});

gulp.task("copyLibJsScripts", function()
{
    var stream = gulp.src(path.join(libsPath, "/**/*.min.js"));

    dir.subdirs(outputPath, function (err, subdirs)
    {
        if (err)
            throw err;

        subdirs.forEach(function (subdir)
        {
            if(path.basename(subdir) === "js")
                stream.pipe(gulp.dest(subdir));
        });
    });
});