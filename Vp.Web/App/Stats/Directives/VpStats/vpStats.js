angular.module("vp")
.directive("vpStats", function (vpRepository) {
    return {
        restrict: 'E',
        templateUrl: $.url("app/Stats/directives/vpStats/vpStats.html"),
        scope: {
        },
        link: function ($scope) {

            $scope.pickLists = {};
            $scope.inputs = {};

            vpRepository.getGames().then(function (data) { $scope.pickLists.games = data; });
            
            $scope.$watch("inputs.game", function () {
                if ($scope.inputs.game != null)
                    vpRepository.getConfigs($scope.inputs.game.Id).then(function (data) { $scope.pickLists.configs = data; });
            });

            $scope.$watch("inputs.config", function () {
                if ($scope.inputs.config != null)
                    vpRepository.getResultsSummary($scope.inputs.config.Id).then(function (data) { $scope.resultsSummary = data; buildChart(); });
            });

            $scope.rawChartOptions = {
                yaxes: [{ min: 0 }, { min: 0, max: 100, position: "right" }],
                legend: { position: "nw" },
                zoom: {
                    interactive: true
                },
                pan: {
                    interactive: true
                }
            };

            $scope.breakdownChartOptions = {
                yaxes: [{ min: 0 }, { min: 0, max: 100, position: "right" }],
                legend: { position: "nw" },
                series: {
                    bars: {
                        show: true
                    }
                },
                bars: {
                    align: "center",
                    barWidth: 0.5
                },
                zoom: {
                    interactive: true
                },
                pan: {
                    interactive: true
                }
            };

            function buildChart() {
                var lines = [
                    {
                        label: $scope.inputs.game.Name + " $" + $scope.inputs.config.DollarsPerCredit + " " + $scope.inputs.config.HandsPlayed + " hands",
                        color: "#6666AA",
                        lines: { fill: true, fillColor: "rgba(119,119,187,0.6)" },
                        yaxis: 1,
                        data: []
                    },
                ];

                angular.forEach($scope.resultsSummary.rawNumbers, function (result) {
                    lines[0].data.push([result.EndCredits, result.Percent]);
                });

                $scope.rawChartData = lines;

                var breakdown = [
                    {
                        label: $scope.inputs.game.Name + " $" + $scope.inputs.config.DollarsPerCredit + " " + $scope.inputs.config.HandsPlayed + " hands",
                        color: "#AA6666",
                        lines: { fill: true, fillColor: "rgba(187,119,119,0.6)" },
                        yaxis: 1,
                        data: []
                    },
                ];

                var ticks = [];

                angular.forEach($scope.resultsSummary.breakdown, function (result) {
                    var tickId = ticks.length;
                    ticks.push([tickId, result.Tier.toString()]);
                    breakdown[0].data.push([tickId, result.Percent]);
                });

                $scope.breakdownChartOptions.xaxis = { ticks: ticks };

                $scope.breakdownChartData = breakdown;
            }
        }
    };
})