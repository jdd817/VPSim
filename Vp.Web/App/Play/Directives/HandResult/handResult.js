angular.module("vpPlay")
.directive("handResult", function (vp) {
    return {
        restrict: 'E',
        templateUrl: $.url("app/Play/directives/handResult/handResult.html"),
        scope: {
            result:'='
        },
        link: function ($scope,$element) {
            if ($scope.result.Payout == 0)
                $scope.payoutVisibility = { visibility: "hidden" };
        }
    };
})