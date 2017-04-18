


(function (ng) {
    ng.module('seliseSchool', [
            'ngRoute',
            'ngAnimate',
            'seliseSchool.course',
            'seliseSchool.student'
    ])
        .config([
            '$routeProvider', function ($routeProvider) {
                $routeProvider.when('/', {
                    templateUrl: 'template/dashboard/dashboard.html'
                });

                $routeProvider.when('/students/:sortBy?/:sortOrder?/:page?/:pageSize?', {
                    templateUrl: 'template/student/studentList.html',
                    controller: 'StudentListController',
                    controllerAs: 'StudentList',
                    resolve: {
                        students: [
                            'StudentProvider',
                            '$route',
                            function (Student, $route) {
                                var params = $route.current.params;
                                params.sortBy = params.sortBy || 'name';
                                params.sortOrder = params.sortOrder || 'desc';
                                params.page = params.page || 1;
                                params.pageSize = params.pageSize || 10;
                                return Student.all(params.sortBy, params.sortOrder, params.page, params.pageSize);
                            }
                        ]
                    }
                });

                $routeProvider.when('/courses', {
                    templateUrl: 'template/course/courseList.html',
                    controller: 'CourseListController',
                    controllerAs: 'CourseList',
                    resolve: {
                        courses: [
                            'CourseProvider',
                            function (Course) {
                                return Course.all();
                            }
                        ]
                    }
                });

                $routeProvider.otherwise('/');
            }
        ]);
})(angular);