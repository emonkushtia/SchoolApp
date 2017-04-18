
(function () {
    'use strict';

    angular
       .module('schoolApp.course.controllers')
       .controller('CourseListController', ['$route', '$location', 'CourseProvider', CourseListController]);

    function CourseListController($route, Location, Course) {

        var vm = this;
        vm.courses = [];
        vm.deleteCourse = deleteCourse;

        load();

        function load() {
            vm.courses = $route.current.locals.courses.data.list;
        }

        function deleteCourse(id) {
            Course.remove(id).then(function () {
                var stu = _(vm.courses).findWhere({ id: +id });
                var itemIndex = _(vm.courses).indexOf(stu);
                if (itemIndex >= 0) {
                    vm.courses.splice(itemIndex, 1);
                }
                alert('Course has been deleted.');
            });
        }
    }


})();
