


(function (_, ng) {
    'use strict';

    ng.module('schoolApp.course.models').provider('CourseProvider', [CourseProvider]);

    function CourseProvider() {
        var url = 'api/courses';

        this.$get = [
            '$http', function ($http) {
                return {
                    all: function () {
                        return $http.get(url);
                    },
                    create: function (course) {
                        return $http.post(url, course);
                    },
                    remove: function (id) {
                        return $http.delete(url + '/' + id);
                    }
                };
            }
        ];
    }

})(_, angular);