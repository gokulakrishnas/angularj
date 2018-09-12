
var register = angular
    .module("mymodule", [])
    .controller("mycontrol", function ($scope, $http) {

        var success = function (response) {
            $scope.Values = response.data;
        }
        var Error = function (response) {
            $scope.error = response.data;
        }
       
       

        $http({
            method: "POST",
            params: {
                username: $scope.username,
                password: $scope.password
            },
            url: "/api/Registers/Registering",
            dataType: 'json',
            headers: { "Content-Type": "application/json" }
        }).then(success, Error);
        });
   







//var register = angular
//    .module("mymodule", [])
//    .controller("mycontrol", function ($scope, $http) {

//        var success = function (response) {
//            $scope.Values = response.data;
//        }
//        var Error = function (response) {
//            $scope.error = response.data;
//        }

//        //$http({
//        //    method: 'GET',
//        //    url: 'api/Registers/Index',



//        //})

//        $http({
//            method: 'POST',
//            url: 'api/Registers/GetDatas'
//        })
//            .then(success, Error);
//    });


