

(function (ng, _) {
    'use strict';

    ng.module('schoolApp.student.controllers').controller('StudentCreateController', StudentCreateController);
    StudentCreateController.$inject = ['$scope', '$route', '$location', 'StudentProvider'];

    function StudentCreateController($scope, $route, $location, StudentProvider) {

        var vm = this;
        vm.student = {};
        vm.courses = [];
        vm.coursesList = [];
        vm.selectCourse = selectCourse;
        vm.submitForm = submitForm;
        vm.goToList = goToList;

        load();

        function load() {
            vm.courses = $route.current.locals.courses.data.list;
        }

        function selectCourse() {
            var selectedFilters = _(vm.courses).filter(function (item) { return item.selected; });
            vm.coursesList = _.pluck(selectedFilters, 'id');
        }

        function submitForm()
        {
            vm.student.coursesList = vm.coursesList;

            if ($scope.studentForm.$invalid) {
                return;
            }
            StudentProvider.create(vm.student).then(function () {
                alert('Student Successfully Added');
                vm.goToList();
                }, function (response) {
            });
        }

        function goToList() {
            $location.path('/students');
        }

        
    }

})(angular,_);
