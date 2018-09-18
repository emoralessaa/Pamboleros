'use strict';
app.controller('menuController', ['$scope', 'menuService', 'authService', function ($scope, menuService, authService) {

	$scope.menu = []; 
	$scope.message = "";
	$scope.rolID = authService.authentication.RoleId;


	menuService.getMenu($scope.rolID).then(function (response) {

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




