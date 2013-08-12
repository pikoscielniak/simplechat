$.connection.hub.error(function (err) {
    alert(err);
});

var monitor = $.connection.monitor;

$.connection.hub.start();

var app = angular.module('signalrMonitor', []);

app.controller('mainController', ['$scope', function ($scope) {

    $scope.logs = [];

    function onNewEvent(eventType, connectionId) {
        var obj = {
            eventType: eventType,
            connectionId: connectionId
        };
        $scope.logs.push(obj);
            $scope.$apply(function () {
        });
    }

    monitor.client.newEvent = onNewEvent;

}]);

