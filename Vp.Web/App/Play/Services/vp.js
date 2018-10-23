angular.module("vpPlay")
    .factory("vp", ["$http", "$q", function ($http, $q) {
        return {
            getGames: function () {
                var deferred = $q.defer();
                $http.get($.url("vpMachine/Games"))
                    .success(deferred.resolve)
                    .error(deferred.reject);
                return deferred.promise;
            },
            dealHand: function () {
                var deferred = $q.defer();
                $http.get($.url("vpMachine/DealHand"))
                    .success(deferred.resolve)
                    .error(deferred.reject);
                return deferred.promise;
            },
            checkHand: function (query) {
                var deferred = $q.defer();
                $http.post($.url("vpMachine/CheckHand"), query)
                    .success(deferred.resolve)
                    .error(deferred.reject);
                return deferred.promise;
            },
            getResults: function (query) {
                var deferred = $q.defer();
                $http.post($.url("vpMachine/GetResults"),query)
                    .success(deferred.resolve)
                    .error(deferred.reject);
                return deferred.promise;
            },
        }
    }]);