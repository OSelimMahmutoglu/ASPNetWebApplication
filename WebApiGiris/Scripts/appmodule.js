/// <reference path="anguler.js"/>
var app = angular.module("northapp", []);

app.factory("api", function ($http) {
    var apiurl = "http://localhost:54778/api/";
    return {
        getallcategories: function (success) {
            $http({
                url: apiurl + "category/",
                method: "GET",
                dataType: "JSON"
            }).then(function (response) {
                success(response.data);
            });
        }
    }
});