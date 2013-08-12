$.connection.hub.error(function (err) {
    alert(err);
});
$.connection.hub.logging = true;
var chat = $.connection.chat;
$.connection.hub.start(); //{transport: 'longPolling'}

var app = angular.module('signalrChat', []);

app.controller('mainController', ['$scope', function ($scope) {

    $scope.messages = [];

    $scope.joinRoom = function () {
        chat.server.joinRoom($scope.room);
    };

    function onNewMessage(message) {
        $scope.$apply(function () {
            $scope.messages.push(message);
        });
    }

    chat.client.newMessage = onNewMessage;

    $scope.sendMessage = function () {
        chat.server.sendMessage($scope.message);
    };

    $scope.sendMessageToRoom = function () {
        chat.server.sendMessageFromRoom($scope.room, $scope.message);
    };

}]);

