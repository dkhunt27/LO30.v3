'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('lo30PlayerStatsTeamController',
  [
    '$scope',
    'alertService',
    'externalLibService',
    'dataServicePlayerStatsTeam',
    function ($scope, alertService, externalLibService, dataServicePlayerStatsTeam) {
      var _ = externalLibService._;

      $scope.sortAscFirst = function (column) {
        if ($scope.sortOn === column) {
          $scope.sortDirection = !$scope.sortDirection;
        } else {
          $scope.sortOn = column;
          $scope.sortDirection = false;
        }
      };

      $scope.sortDescFirst = function (column) {
        if ($scope.sortOn === column) {
          $scope.sortDirection = !$scope.sortDirection;
        } else {
          $scope.sortOn = column;
          $scope.sortDirection = true;
        }
      };

      $scope.sortAscOnly = function (column) {
        $scope.sortOn = column;
        $scope.sortDirection = false;
      };

      $scope.sortDescOnly = function (column) {
        $scope.sortOn = column;
        $scope.sortDirection = true;
      };

      $scope.initializeScopeVariables = function () {

        $scope.data = {
          playerStatsTeam: []
        };

        $scope.events = {
          playerStatsTeamProcessing: false,
          playerStatsTeamProcessed: false,
        };

        $scope.user = {
        };
      };

      $scope.getPlayerStatsTeam = function (playerId, playoffs) {
        var retrievedType = "PlayerStatsTeam";

        $scope.events.playerStatsTeamProcessing = true;
        $scope.events.playerStatsTeamProcessed = false;
        $scope.data.playerStatsTeam = [];

        dataServicePlayerStatsTeam.listByPlayerId(playerId).$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item, index) {
                if (item.playoffs === playoffs) {
                  $scope.data.playerStatsTeam.push(item);
                }
              });

              $scope.events.playerStatsTeamProcessing = false;
              $scope.events.playerStatsTeamProcessed = true;

              alertService.successRetrieval(retrievedType, $scope.data.playerStatsTeam.length);
            } else {
              // results not successful
              alertService.errorRetrieval(retrievedType, result.reason);
            }
          }
        );
      };

      $scope.setWatches = function () {
      };

      $scope.activate = function () {
        $scope.initializeScopeVariables();
        $scope.setWatches();
        $scope.getPlayerStatsTeam($scope.playerId, $scope.playoffs);
      };

      $scope.activate();
    }
  ]
);

