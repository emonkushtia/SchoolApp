
(function () {
    'use strict';

    angular
       .module('schoolApp.student.controllers')
       .controller('StudentListController', ['$route', '$location', 'StudentProvider', StudentListController]);

    function StudentListController($route, $location, Student) {
        var sortBy = 'name',
            descending = true;
        var vm = this;
        vm.students = [];
        vm.totalItems = 0;
        vm.currentPage = 1;
        vm.pageSize = 10;
        vm.pageChanged = pageChanged;
        vm.pageSizeChanged = pageSizeChanged;

        vm.deleteStudent = deleteStudent;

        function load() {
            vm.students = $route.current.locals.students.data.list;
            vm.totalItems = $route.current.locals.students.data.count;
            vm.pageSize = $route.current.params.pageSize || 10;
            vm.currentPage = $route.current.params.page || 1;
        }

        function deleteStudent(id) {
            Student.remove(id).then(function () {
                var stu = _(vm.students).findWhere({ id: +id });
                var itemIndex = _(vm.students).indexOf(stu);
                if (itemIndex >= 0) {
                    vm.students.splice(itemIndex, 1);
                }
                alert('Student has been deleted.');
            });
        }

        function goTo(page, pageSize) {
            $location.path('/students/' + sortBy + '/' + (descending ? 'desc' : 'asc') + '/' + page + '/' + pageSize);

        }

        function pageChanged() {
            goTo(vm.currentPage, vm.pageSize);
        }

        function pageSizeChanged() {
            vm.currentPage = 1;
            goTo(vm.currentPage, vm.pageSize);
        }

        load();
    }


})();
