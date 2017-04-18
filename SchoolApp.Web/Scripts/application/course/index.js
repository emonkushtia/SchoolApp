
(function (ng) {
    'use strict';

    ng.module('seliseSchool.course', [
        'ngRoute',
        'seliseSchool.course.controllers',
        'seliseSchool.course.models'
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