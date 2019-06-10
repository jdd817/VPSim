angular.module("vpPlay")
.directive("vpMachine", function (vp) {
    return {
        restrict: 'E',
        templateUrl: $.url("app/Play/directives/vpMachine/vpMachine.html"),
        scope: {
        },
        link: function ($scope) {
            $scope.vp = {
                gameState: 1,
                bet: 1,
                hands: 1,
                credits: 0,
                holds: [false, false, false, false, false],
                correctHolds: [false, false, false, false, false],
                hand: ['TS', 'JS', 'QS', 'KS', 'AS'],
                tierCredits: 0,
                dollarsPerTierCredit: 10,
                dollarsPerCredit: 2,
                mistakes: []                
            };
            $scope.payoutVisibility = { visibility: "hidden" };

            vp.getGames().then(function (data) { $scope.vp.gameList = data; $scope.vp.game = $scope.vp.gameList[0]; });

            $scope.addHundo = function () {
                if ($scope.vp.gameState == 1)
                    $scope.vp.credits += 100 / $scope.vp.dollarsPerCredit;
            }

            $scope.addThow = function () {
                if ($scope.vp.gameState == 1)
                    $scope.vp.credits += 1000 / $scope.vp.dollarsPerCredit;
            }

            $scope.maxBet = function () {
                if ($scope.vp.gameState == 1) {
                    $scope.vp.bet = 5;
                    $scope.vp.hands = 50;
                    $scope.deal();
                }
            };

            $scope.bet = function () {
                if ($scope.vp.gameState == 1) {
                    $scope.vp.bet++;
                    if ($scope.vp.bet > 5)
                        $scope.vp.bet = 1;
                }
            };

            $scope.changeDenom = function () {
                var oldDenom = $scope.vp.dollarsPerCredit;
                switch ($scope.vp.dollarsPerCredit) {
                    case 0.05: $scope.vp.dollarsPerCredit = 0.10; break;
                    case 0.10: $scope.vp.dollarsPerCredit = 0.25; break;
                    case 0.25: $scope.vp.dollarsPerCredit = 0.50; break;
                    case 0.50: $scope.vp.dollarsPerCredit = 1; break;
                    case 1: $scope.vp.dollarsPerCredit = 2; break;
                    case 2: $scope.vp.dollarsPerCredit = 5; break;
                    case 5: $scope.vp.dollarsPerCredit = 10; break;
                    case 10: $scope.vp.dollarsPerCredit = 25; break;
                    case 25: $scope.vp.dollarsPerCredit = 0.05; break;
                }
                $scope.vp.credits = ($scope.vp.credits * oldDenom) / $scope.vp.dollarsPerCredit;
            };

            $scope.addHand = function () {
                if ($scope.vp.gameState == 1) {
                    $scope.vp.hands++;
                    if ($scope.vp.hands > 50)
                        $scope.vp.hands = 50;
                }
            };

            $scope.hold = function (cardIndex) {
                if ($scope.vp.gameState == 2)
                    $scope.vp.holds[cardIndex] = !$scope.vp.holds[cardIndex];
            }

            $scope.deal = function () {
                if ($scope.vp.gameState == 1) {
                    if ($scope.vp.credits < $scope.vp.bet * $scope.vp.hands)
                        return;
                    $scope.vp.credits -= $scope.vp.bet * $scope.vp.hands;
                    $scope.vp.tierCredits += ($scope.vp.bet * $scope.vp.hands)/($scope.vp.dollarsPerTierCredit / $scope.vp.dollarsPerCredit);
                    $scope.vp.holds = [false, false, false, false, false];
                    $scope.vp.correctHolds = [false, false, false, false, false];
                    $scope.vp.gameState = 2;
                    
                    vp.dealHand().then(function (hand) {
                        $scope.vp.hand = hand;
                        vp.checkHand(
                       {
                           Hand: $scope.vp.hand,
                           Bet: $scope.vp.bet,
                           PayTable: $scope.vp.game.Paytable
                       }).then(function (result) {
                           $scope.vp.handResult = result;
                           if (result.Payout == 0)
                               $scope.payoutVisibility = { visibility: "hidden" };
                           else
                               $scope.payoutVisibility = null;
                       });
                    });
                }
                else if ($scope.vp.gameState == 2) {
                    $scope.vp.originalHand = $scope.vp.hand;
                    var heldCards = [];
                    for (var i = 0; i < 5; i++)
                        if ($scope.vp.holds[i])
                            heldCards.push($scope.vp.hand[i]);
                    
                    vp.getResults(
                        {
                            Hand: $scope.vp.hand,
                            HeldCards: heldCards,
                            Bet: $scope.vp.bet,
                            Hands: $scope.vp.hands,
                            PayTable: $scope.vp.game.Paytable
                        }).then(function (result) {
                            $scope.vp.hand = result.Hand.Hand;
                            $scope.vp.handResult = result.Hand;
                            $scope.vp.additionalHands = result.Results;
                            $scope.vp.credits += result.CreditsPayed;
                            $scope.vp.gameState = 1;
                            $scope.vp.paid = result.CreditsPayed;
                            angular.forEach(result.CorrectPlayIndexes, function (idx) { $scope.vp.correctHolds[idx] = true; });

                            if (result.Payout == 0)
                                $scope.payoutVisibility = { visibility: "hidden" };
                            else
                                $scope.payoutVisibility = null;

                            var madeMistake = false;
                            for (var i = 0; i < 5; i++)
                                if ($scope.vp.holds[i] != $scope.vp.correctHolds[i])
                                    madeMistake = true;

                            if (madeMistake) {
                                $scope.vp.mistakes.push({
                                    hand: $scope.vp.originalHand,
                                    holds: $scope.vp.holds,
                                    correctHolds: $scope.vp.correctHolds
                                });
                            }
                        });
                }
            }
        }
    };
})