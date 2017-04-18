
(function (ng) {
    'use strict';

    ng.module('schoolApp.course', [
        'ngRoute',
        'schoolApp.course.controllers',
        'schoolApp.course.models'
    ])
        .config([
            '$routeProvider',
            function ($routes) {
                $routes.when('/course/create', {
                    templateUrl: 'template/course/create.html',
                    controller: 'CourseCreateController',
                    controllerAs: 'CourseCreate'
                });
            }
        ]);

})(angular);