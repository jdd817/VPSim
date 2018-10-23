angular.module("vpPlay")
.directive("card", function (vp) {
    return {
        restrict: 'E',
        //templateUrl: $.url("app/Play/directives/vpMachine/vpMachine.html"),
        //template: '<div class="card">{{card}}</div>',
        scope: {
            card: '=',
            width: '=?',
            height:'=?'
        },
        link: function ($scope, $element) {
            if ($scope.width == null)
                $scope.width = 73;
            if ($scope.height == null)
                $scope.height = 98;

            $element.addClass("card");

            if ($scope.card.length == 2) {
                var suit = $scope.card.substring(1);
                var value = $scope.card.substring(0, 1);

                var vIndex = 0;
                if (suit == 'C') vIndex = 0;
                if (suit == 'S') vIndex = 1;
                if (suit == 'H') vIndex = 2;
                if (suit == 'D') vIndex = 3;

                var hIndex = 0;
                if (value == 'A') hIndex = 0;
                else if (value == 'T') hIndex = 9;
                else if (value == 'J') hIndex = 10;
                else if (value == 'Q') hIndex = 11;
                else if (value == 'K') hIndex = 12;
                else hIndex = parseInt(value) - 1;

                $element.css("width",($scope.width+2)+"px");
                $element.css("height",($scope.height+2)+"px");

                $element.css("background-size", ($scope.width * 13 + 1) + "px " + ($scope.height * 4) + "px");
                $element.css("background-position-x", (hIndex * -$scope.width-1) + "px");
                $element.css("background-position-y", (vIndex * -$scope.height-1) + "px");
            }
        }
    };
})