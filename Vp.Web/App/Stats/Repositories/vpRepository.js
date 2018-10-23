angular.module("vp")
    .factory("vpRepository", ["$http", "$q", function ($http, $q) {
        return {
            getGames: function () {
                var deferred = $q.defer();
                $http.get($.url("vp/Games"))
                    .success(deferred.resolve)
                    .error(deferred.reject);
                return deferred.promise;
            },
            getConfigs: function (gameId) {
                var deferred = $q.defer();
                $http.get($.url("vp/Games/" + gameId + "/Configs"))
                    .success(deferred.resolve)
                    .error(deferred.reject);
                return deferred.promise;
            },
            getResults: function (configId) {
                var deferred = $q.defer();
                $http.get($.url("vp/Configs/" + configId + "/Results"))
                    .success(deferred.resolve)
                    .error(deferred.reject);
                return deferred.promise;
            },
            getResultsSummary: function (configId) {
                var deferred = $q.defer();
                $http.get($.url("vp/Configs/" + configId + "/ResultsSummary"))
                    .success(deferred.resolve)
                    .error(deferred.reject);
                return deferred.promise;
            }
        }
    }]);