'use strict';
app.controller('homeController', ['$scope', function ($scope) {
	$scope.templates =
		[{ name: 'menu.html', url: '/app/views/menu.html' },
		{ name: 'template2.html', url: '/app/views/signup.html' }];
	$scope.menuTemplate = $scope.templates[0];
	$scope.pageTemplate = $scope.templates[1];
}]);