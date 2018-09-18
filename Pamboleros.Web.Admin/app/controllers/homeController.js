'use strict';
app.controller('homeController', ['$scope', '$location', 'authService', 'menuService', function ($scope, $location, authService, menuService) {

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


	$scope.menu = [];
	$scope.message = "";
	var rolID = authService.authentication.RoleId;
	 

	menuService.getMenu(rolID.toString()).then(function (response) {

		response.data.forEach(ArrayElements);

	},
		function (err) {
			$scope.message = err.error_description;
		});

	function ArrayElements(element, index, array) {
		if (element.menuLevel == 0) {
			$scope.menu = $scope.menu.concat([{
				MenuId: element.menuId,
				MenuName: element.menuName,
				MenuHref: element.menuHref,
				MenuLevel: element.menuLevel,
				MenuIdRoot: element.menuIdRoot,
				MenuStat: element.menuStat,
				MenuIcon: element.menuIcon,
				MenuChilds: element.menuChilds
			}]);
		}
		if (element.menuLevel == 1) {

			const menuRoot = $scope.menu.find(NodeMenu => NodeMenu.MenuId === element.menuIdRoot);

			if (menuRoot['MenuDetails'] == null) {
				$scope.menu.find(NodeMenu => NodeMenu.MenuId === element.menuIdRoot)['MenuDetails'] = [{
					MenuId: element.menuId,
					MenuName: element.menuName,
					MenuHref: element.menuHref,
					MenuLevel: element.menuLevel,
					MenuIdRoot: element.menuIdRoot,
					MenuStat: element.menuStat,
					MenuIcon: element.menuIcon,
					MenuChilds: element.menuChilds,
					MenuDetails: null
				}];
			} else {
				$scope.menu.find(NodeMenu => NodeMenu.MenuId === element.menuIdRoot)['MenuDetails'] =
					$scope.menu.find(NodeMenu => NodeMenu.MenuId === element.menuIdRoot)['MenuDetails'].concat([{
						MenuId: element.menuId,
						MenuName: element.menuName,
						MenuHref: element.menuHref,
						MenuLevel: element.menuLevel,
						MenuIdRoot: element.menuIdRoot,
						MenuStat: element.menuStat,
						MenuIcon: element.menuIcon,
						MenuChilds: element.menuChilds,
						MenuDetails: null
					}]);
			}


		}
	}
}]);