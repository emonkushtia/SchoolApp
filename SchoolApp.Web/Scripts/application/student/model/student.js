

(function (_, ng) {
    'use strict';

    function StudentProvider() {
        var url = 'api/students';

        this.$get = [
            '$http', function ($http) {
                return {
                    all: function (sortBy, sortOrder, page, pageSize) {
                        var offset = (page - 1) * pageSize,
                            sort = (sortBy || '') + (sortOrder ? ' ' + sortOrder : '');
                        return $http.get(url, {
                            params: {
                                offset: offset,
                                limit: pageSize,
                                sort: sort
                            }
                        });
                    },
                    create: function (student) {
                        return $http.post(url, student);
                    },
                    one: function (id) {
                        return $http.get(url + '/' + id);
                    },
                    remove: function (id) {
                        return $http.delete(url + '/' + id);
                    }
                };
            }
        ];
    }

    ng.module('seliseSchool.student.models').provider('StudentProvider', [StudentProvider]);

})(_, angular);