/// <binding ProjectOpened='watchSass' />

const gulp = require('gulp');
//const gulpif = require('gulpif');
const { series } = require('gulp');

const rename = require('gulp-rename');
const lec = require('gulp-line-ending-corrector');
const sass = require('gulp-sass');
const minifyCSS = require('gulp-minify-css');
//const browserSync = require('browser-sync').create();



var minimist = require('minimist');
var log = require('fancy-log');
var es = require('event-stream');


var knownOptions = {
    string: 'files',
    string: 'outdir'
};

var options = minimist(process.argv.slice(2), knownOptions);

gulp.task('sass-compile', function (done) {
    var files = options.files.split(";");
    log(files);
    files.forEach(function (file) {
        gulp.src(file)
            .pipe(sass().on('error', sass.logError))
            .pipe(lec())
            .pipe(rename({ dirname: '' }))
            .pipe(gulp.dest(options.outdir))
    });
    done();
});


// Compile SASS into CSS
function compileSass() {
    return gulp.src('Content/*.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(lec())
        .pipe(gulp.dest('wwwroot/css'))
        //.pipe(browserSync.stream());
}

function minify() {
    return gulp.src('wwwroot/css/*.css')
        .pipe(minifyCSS())
        .pipe(lec())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('wwwroot/css'))
}

function watchSass() {
    gulp.watch('Content/*.scss', compileSass);
}

function copyLibs() {
    return gulp.src(['node_modules/bootstrap-icons/font/fonts/**/*']).pipe(gulp.dest('wwwroot/css/fonts'));
}

exports.minify = minify;
exports.copyLibs = copyLibs;
exports.compileSass = compileSass;
exports.watchSass = watchSass;
exports.default = series(compileSass);
