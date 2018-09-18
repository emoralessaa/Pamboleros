//'use strict';
//app.controller('homeController', ['$scope', function ($scope) {
//	$scope.templates =
//		[{ name: 'menu.html', url: '/app/views/menu.html' },
//		{ name: 'template2.html', url: '/app/views/signup.html' }];
//	$scope.menuTemplate = $scope.templates[0];
//	$scope.pageTemplate = $scope.templates[1];
//}]);

'use strict';
app.controller('homeController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

	$scope.templates =
		[{ name: 'menu.html', url: '/app/views/menu.html' },
		{ name: 'template2.html', url: '/app/views/signup.html' }];
	$scope.menuTemplate = $scope.templates[0];
	$scope.pageTemplate = $scope.templates[1];

	$scope.logOut = function () {
		authService.logOut();
		$location.path('/login');
	}

	$scope.reloadRoute = function () {
		$route.reload();
	}

	$scope.authentication = authService.authentication;
}]);