'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {

	$scope.savedSuccessfully = false;
	$scope.message = "";

	$scope.registration = {
		userName: "",
		password: "",
		confirmPassword: ""
	};

	$scope.signUp = function () {

		authService.saveRegistration($scope.registration).then(function (response) {

			$scope.savedSuccessfully = true;
			$scope.message = "El usuario ha sido creado correctamente, serás re-dirigido al inicio en 2 segundos...";
			startTimer();

		},
			function (response) {
				var errors = [];
				for (var key in response.data.modelState) {
					for (var i = 0; i < response.data.modelState[key].length; i++) {
						errors.push(response.data.modelState[key][i]);
					}
				}
				$scope.message = "Error al registrar al usuario debido a: " + errors.join(' ');
			});
	};

	var startTimer = function () {
		var timer = $timeout(function () {
			$timeout.cancel(timer);
			$location.path('/login');
		}, 2000);
	}

}]);