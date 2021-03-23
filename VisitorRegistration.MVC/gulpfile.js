const gulp = require('gulp');
//const gulpif = require('gulpif');
const { series } = require('gulp');

const rename = require('gulp-rename');
const lec = require('gulp-line-ending-corrector');
const sass = require('gulp-sass');
const minifyCSS = require('gulp-minify-css');

var minimist = require('minimist');
var log = require('fancy-log');
var es = require('event-stream');

var knownOptions = {
    string: 'files',
    string: 'outdir'
};

var options = minimist(process.argv.slice(2), knownOptions);

gulp.task('sass-compile', function () {
    var files = options.files.split(";");
    log(files);
    return gulp.src(files)
        .pipe(sass().on('error', sass.logError))
        .pipe(lec())
        .pipe(rename({ dirname: '' }))
        .pipe(gulp.dest(options.outdir));
});

gulp.task('css-minify', function () {
    var files = options.files.split(";");
    log(files);
    return gulp.src(files)
        .pipe(minifyCSS())
        .pipe(lec())
        .pipe(rename({ suffix: '.min' }))
        .pipe(gulp.dest('.'));
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

gulp.task('copy-libs', function () {
    return gulp.src(['node_modules/bootstrap-icons/font/fonts/**/*']).pipe(gulp.dest('wwwroot/css/fonts'));
});
