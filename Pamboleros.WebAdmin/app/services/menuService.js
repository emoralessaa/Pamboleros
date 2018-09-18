'use strict';
app.factory('menuService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {

	var serviceBase = ngAuthSettings.apiServiceBaseUri;
	var menuService = {};

	var _getMenu = function (rolId) {

		return $http.get(serviceBase + 'api/menu/getMenuByRol?RoleId=' + rolId).then(function (results) {
			return results;			
		});
	};

	menuService.getMenu = _getMenu;

	return menuService;

}]);