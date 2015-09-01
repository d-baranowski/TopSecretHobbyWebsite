var app = angular.module('myApp', []);
app.controller('myCtrl', function ($scope, $http) {

    $http.get("http://localhost:59756/api/CardsApi/").success(function (response) {
        $scope.data = response;
    });

    $scope.addOne = function (entry) {
        changeQuantity(entry["Card"]["CardId"], +1);
        entry["Quantity"] = entry["Quantity"] + 1;
    }

    $scope.subOne = function (entry) {
        changeQuantity(entry["Card"]["CardId"], -1);
        if (entry["Quantity"] != 0) {
            entry["Quantity"] = entry["Quantity"] - 1;
        }
    }

    function changeQuantity(cardId, value) {
        $.ajax({
            url: 'http://localhost:59756/api/Quantity/' + cardId + "?value=" + value,
            type: "PUT",
        }).done(function (data) {
        }).fail(function (err) {
            alert('fail')
        }).success(function (result) {
        });
    }

    $scope.getLegality = function (item, format) {
        if (item["Card"]["CardLegalities"] != "") {
            var obj = angular.fromJson(item["Card"]["CardLegalities"]);
            for (i = 0; i < obj.length; i++) {
                if (obj[i]["format"] === format) {
                    if (obj[i]["legality"] === "Legal") {
                        return true;
                    }
                } 
            }
        }
        return false;
    }

    //Filters
    //Legality
    $scope.legalityIncludes = [];
    $('.legality').on('switchChange.bootstrapSwitch', function (event, state) {
        if (state) {
            $scope.legalityIncludes.push(this.value);
        } else {
            var i = $.inArray(this.value, $scope.legalityIncludes);
            $scope.legalityIncludes.splice(i, 1);
        }
        $scope.$apply();
    });
    $scope.legalityFilter = function (entry) {
        if ($scope.legalityIncludes.length > 0) {
            var inc = 0;
            for (item in $scope.legalityIncludes) {
                if ($scope.getLegality(entry, $scope.legalityIncludes[item])) {
                    inc = 1;
                }
            }
            return inc === 1;
        }
        return true;
    }


    //Rarity
    $scope.rarityIncludes = [];
    $('.rarity').on('switchChange.bootstrapSwitch', function (event, state) {
        if (state) {
            $scope.rarityIncludes.push(this.value);
        } else {
            var i = $.inArray(this.value, $scope.rarityIncludes);
            $scope.rarityIncludes.splice(i, 1);
        }
        $scope.$apply();
    });

    $scope.rarityFilter = function (entry) {
        if ($scope.rarityIncludes.length > 0) {
            if ($.inArray(entry["Card"]["CardRarity"], $scope.rarityIncludes) < 0) {
                return;
            }    
        }
        return entry;
    }
    //Color
    $scope.colorIncludes = [];
    $('.color').on('switchChange.bootstrapSwitch', function (event, state) {
        if (state) {
            $scope.colorIncludes.push(this.value);
        } else {
            var i = $.inArray(this.value, $scope.colorIncludes);
            $scope.colorIncludes.splice(i, 1);
        }
        $scope.$apply();
    });
    $scope.colorFilter = function (entry) {
        if ($scope.colorIncludes.length > 0) {
            var fits = 1;
            for (item in $scope.colorIncludes) {
                if (entry["Card"]["CardColors"].toString().indexOf($scope.colorIncludes[item]) === -1) {
                    fits = 0;
                }
            }
            return fits === 1;
        }
        return true;
    }
    //Types
    $scope.typeIncludes = [];
    $('.type').on('switchChange.bootstrapSwitch', function (event, state) {
        if (state) {
            $scope.typeIncludes.push(this.value);
        } else {
            var i = $.inArray(this.value, $scope.typeIncludes);
            $scope.typeIncludes.splice(i, 1);
        }
        $scope.$apply();
    });
    $scope.typeFilter = function (entry) {
        if ($scope.typeIncludes.length > 0) {
            var fits = 0;
            for (item in $scope.typeIncludes) {
                if (entry["Card"]["CardTypes"].toString().indexOf($scope.typeIncludes[item]) != -1) {
                    fits = 1;
                }
            }
            return fits === 1;
        }
        return true;
    }

    //Cost
    $scope.costIncludes = [];
    $scope.lowerBoundry = [0, 2, 4, 6];
    $scope.upperBoundry = [1, 3, 5, 99];

    $('.cost').on('switchChange.bootstrapSwitch', function (event, state) {
        if (state) {
            $scope.costIncludes.push(this.value);
        } else {
            var i = $.inArray(this.value, $scope.costIncludes);
            $scope.costIncludes.splice(i, 1);
        }
        $scope.$apply();
    });
    $scope.costFilter = function (entry) {
        if ($scope.costIncludes.length > 0) {
            var fits = 0;
            for (item in $scope.costIncludes) {
                if (entry["Card"]["CardCmc"] >= $scope.lowerBoundry[$scope.costIncludes[item]]) {
                    if (entry["Card"]["CardCmc"] <= $scope.upperBoundry[$scope.costIncludes[item]]) {
                        fits = 1;
                    }
                }
            }
            return fits === 1;
        }
        return true;
            
    }

    //Owned
    $scope.owned = 0;
    $('#owned').on('switchChange.bootstrapSwitch', function (event, state) {
        if (state) {
            $scope.owned = 1;
        } else {
            $scope.owned = 0;
        }
        $scope.$apply();
    });
    $scope.ownedFilter = function (entry) {
        if ($scope.owned > 0) {
            return entry["Quantity"] > 0;
        }
        return true;           
    }

});
