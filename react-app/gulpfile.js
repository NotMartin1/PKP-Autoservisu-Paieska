const gulp = require('gulp');
const sass = require('gulp-sass')(require('sass'));
const del = require('del');

const exec = require('child_process').exec;

gulp.task('styles', () => {
  return gulp.src('src/scss/app.scss')
    .pipe(sass().on('error', sass.logError))
    .pipe(gulp.dest('src/css'));
});

gulp.task('clean', () => {
  return del([
      'src/css/app.css',
  ]);
});

gulp.task('start', function (cb) {
  const child = exec('npm start');
  child.stdout.pipe(process.stdout);
  child.stderr.pipe(process.stderr);
  cb();
});

gulp.task('watch', function () {
  gulp.watch('src/scss/**/*.scss', gulp.series(['clean', 'styles']));
});

gulp.task('default', gulp.series('clean', 'styles', 'start', 'watch'));
