


(function (ng) {
    'use strict';

    ng.module('schoolApp.student', [
        'ngRoute',
        'schoolApp.student.controllers',
        'schoolApp.student.models',
        'schoolApp.course.models'
    ])
        .config([
            '$routeProvider',
            function ($routes) {
                $routes.when('/student/create', {
                    templateUrl: 'template/student/create.html',
                    controller: 'StudentCreateController',
                    controllerAs: 'StudentCreate',
                    resolve: {
                        courses: [
                            'CourseProvider',
                            function (Course) {
                                return Course.all();
                            }
                        ]
                    }
                });

                $routes.when('/student/:id', {
                    templateUrl: 'template/student/view.html',
                    controller: 'StudentViewController',
                    controllerAs: 'StudentView',
                    resolve: {
                        student: [
                            '$route',
                            'StudentProvider',
                            function ($route, Student) {
                                return Student.one($route.current.params.id);
                            }
                        ]
                    }
                });

                $routes.when('/student/edit/:id', {
                    templateUrl: 'template/student/edit.html',
                    controller: 'StudentEditController',
                    controllerAs: 'StudentEdit',
                    resolve: {
                        student: [
                            '$route',
                            'StudentProvider',
                            function ($route, Student) {
                                return Student.one($route.current.params.id);
                            }
                        ],
                        courses: [
                            'CourseProvider',
                            function (Course) {
                                return Course.all();
                            }
                        ]
                    }
                });
            }
        ]);

})(angular);