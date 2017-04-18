


(function (ng,_) {
    'use strict';

    ng.module('seliseSchool.course.controllers').controller('CourseCreateController', CourseCreateController);
    CourseCreateController.$inject = ['$scope', '$route', '$location', 'CourseProvider'];

    function CourseCreateController($scope, $route, $location, CourseProvider) {

        var vm = this;
        vm.course = {};
        vm.submitForm = submitForm;
        vm.goToList = goToList;

        load();

        function load() {

        }

        function submitForm() {
            if ($scope.courseForm.$invalid) {
                return;
            }
            CourseProvider.create(vm.course).then(function () {
                alert('Course has been created successfully.');
                vm.goToList();
            }, function (response) {
            });
        }

        function goToList() {
            $location.path('/courses');
        }
    }


})(angular, _);
