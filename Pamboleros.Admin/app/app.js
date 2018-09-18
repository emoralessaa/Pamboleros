var app = angular.module('PambolerosApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

	$routeProvider.when("/home", {
		controller: "homeController",
		templateUrl: "/app/views/home.html"
	});

	$routeProvider.when("/login", {
		controller: "loginController",
		templateUrl: "/app/views/login.html"
	});

	$routeProvider.when("/signup", {
		controller: "signupController",
		templateUrl: "/app/views/signup.html"
	});

	//$routeProvider.when("/menu", {
	//	controller: "menuController",
	//	templateUrl: "/app/views/menu.html"
	//});

	$routeProvider.otherwise({ redirectTo: "/login" });
});

var serviceBase = 'http://localhost:60202/';
app.constant('ngAuthSettings', {
	apiServiceBaseUri: serviceBase,
	clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
	$httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
	authService.fillAuthData();
}]);

//app.config(['$locationProvider', function ($locationProvider) {

//	//if (window.history && window.history.pushState) {
//	//	$locationProvider.html5Mode({
//	//		enabled: true,
//	//		requireBase: true,
//	//		rewriteLinks: true
//	//	}).hashPrefix('!');
//	//}
//	//else {
//		$locationProvider.html5Mode(false).hashPrefix('!');
//	//}
//}]);