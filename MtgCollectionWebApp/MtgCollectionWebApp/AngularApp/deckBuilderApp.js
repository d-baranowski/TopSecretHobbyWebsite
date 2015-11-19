var app = angular.module("myApp", ["angular.filter", "angularUtils.directives.dirPagination", "ngDraggable"]);

app.controller("myCtrl", function ($scope, $http) {

    //Retrieve Data
    $http.get("/api/CardsApi/").success(function (response) {
        $scope.data = response;
    });
    $http.get("/api/DeckApi/").success(function (response) {
        $scope.decks = response;
    });

    $scope.refreshDecks = function () {
        $http.get("/api/DeckApi/").success(function (response) {
            $scope.decks = response;
            $scope.apply();
        });
    }

    //Rate Card
    $scope.rateCard = function (name, rating, entry) {
        entry["Rating"] = rating;
        $.ajax({
            url: "/api/RatingApi",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{\"RatingId\": \"0\",\"UserId\": \"0\",\"RatingValue\": \"" + rating + "\", \"RatingCardName\": \"" + name + "\"}"
        }).done(function (data) {
        }).fail(function (err) {
            alert("Sorry I failed you :(");
        }).success(function (result) {
        });
    }

    //Quantity
    function changeQuantity(cardId, value) {
        $.ajax({
            url: "/api/Quantity/" + cardId + "?value=" + value,
            type: "PUT",
        }).done(function (data) {
        }).fail(function (err) {
            alert("Sorry It looks like you need to log in again");
            window.location = "/Account/LogIn";
        }).success(function (result) {
        });
    }
    $scope.addOne = function (entry) {
        changeQuantity(entry["Card"]["CardId"], +1);
        entry["Quantity"] = entry["Quantity"] + 1;
    }
    $scope.subOne = function (entry) {
        changeQuantity(entry["Card"]["CardId"], -1);
        if (entry["Quantity"] !== 0) {
            entry["Quantity"] = entry["Quantity"] - 1;
        }
    }

    //Filters
    //Legality
    $scope.getLegality = function (item, format) {
        if (item["Card"]["CardLegalities"] !== "") {
            var obj = angular.fromJson(item["Card"]["CardLegalities"]);
            for (var i = 0; i < obj.length; i++) {
                if (obj[i]["format"] === format) {
                    if (obj[i]["legality"] === "Legal") {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    $scope.legalityIncludes = [];
    $scope.legalityToggler = function (id) {
        var btn = $("#" + id);
        btn.toggleClass("down");
        var state = btn.attr("class");


        if (state.indexOf("down") > -1) {
            $scope.legalityIncludes.push(btn.attr("value"));
        } else {
            var i = $.inArray(btn.attr("value"), $scope.legalityIncludes);
            $scope.legalityIncludes.splice(i, 1);
        }
        $scope.$apply();
    }
    $scope.legalityFilter = function (entry) {
        if ($scope.legalityIncludes.length > 0) {
            var inc = 0;
            for (var item in $scope.legalityIncludes) {
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
    $scope.rarityToggler = function (id) {
        var btn = $("#" + id);
        btn.toggleClass("down");
        var state = btn.attr("class");


        if (state.indexOf("down") > -1) {
            $scope.rarityIncludes.push(btn.attr("value"));
        } else {
            var i = $.inArray(btn.attr("value"), $scope.rarityIncludes);
            $scope.rarityIncludes.splice(i, 1);
        }
        $scope.$apply();
    }
    $scope.rarityFilter = function (entry) {
        if ($scope.rarityIncludes.length > 0) {
            if ($.inArray(entry["Card"]["CardRarity"], $scope.rarityIncludes) < 0) {
                return;
            }
        }
        return true;
    }

    //Color
    $scope.colorIncludes = [];
    $scope.colorToggler = function (id) {
        var btn = $("#" + id);
        btn.toggleClass("down");
        var state = btn.attr("class");


        if (state.indexOf("down") > -1) {
            $scope.colorIncludes.push(btn.attr("value"));
        } else {
            var i = $.inArray(btn.attr("value"), $scope.typeIncludes);
            $scope.colorIncludes.splice(i, 1);
        }
        $scope.$apply();

    }
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
    $scope.typeToggler = function (id) {
        var btn = $("#" + id);
        btn.toggleClass("down");
        var state = btn.attr("class");


        if (state.indexOf("down") > -1) {
            $scope.typeIncludes.push(btn.attr("value"));
        } else {
            var i = $.inArray(btn.attr("value"), $scope.typeIncludes);
            $scope.typeIncludes.splice(i, 1);
        }
        $scope.$apply();

    }
    $scope.typeFilter = function (entry) {
        //Filter out Planes and Schemes due to image incompatibilities 
        if (entry["Card"]["CardTypes"].toString().indexOf("Plane") !== -1 || entry["Card"]["CardTypes"].toString().indexOf("Scheme") !== -1) {
            return false;
        }
        if ($scope.typeIncludes.length > 0) {
            var fits = 0;
            for (item in $scope.typeIncludes) {
                if (entry["Card"]["CardTypes"].toString().indexOf($scope.typeIncludes[item]) !== -1) {
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
    $scope.costToggler = function (id) {
        var btn = $("#" + id);
        btn.toggleClass("down");
        var state = btn.attr("class");


        if (state.indexOf("down") > -1) {
            $scope.costIncludes.push(btn.attr("value"));
        } else {
            var i = $.inArray(btn.attr("value"), $scope.costIncludes);
            $scope.costIncludes.splice(i, 1);
        }
        $scope.$apply();
    }
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
    $scope.ownedToggler = function (id) {
        var btn = $("#" + id);
        btn.toggleClass("down");
        var state = btn.attr("class");

        if (state.indexOf("down") > -1) {
            $scope.owned = 1;
        } else {
            $scope.owned = 0;
        }
        $scope.$apply();
    }
    $("#owned").on("switchChange.bootstrapSwitch", function (event, state) {
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


    //Show variations
    $scope.myUnique = "Card.CardName";
    $scope.uniqueToggler = function (id) {
        var btn = $("#" + id);
        btn.toggleClass("down");
        var state = btn.attr("class");

        if (state.indexOf("down") > -1) {
            $scope.myUnique = "";
            $scope.$apply();
        } else {
            $scope.myUnique = "Card.CardName";
            $scope.$apply();
        }
        $scope.$apply();
    }

    //Order By
    $scope.orderBtns = ["ordName", "ordRating", "ordSet", "ordArtist", "ordManaCost", "ordType"];
    $scope.predicate = 'Card.CardName';
    $scope.reverse = false;
    $scope.order = function (predicate, id) {
        var btn;
        for (var item in $scope.orderBtns) {
            btn = $("#" + $scope.orderBtns[item]);
            var state = btn.attr("class");
            btn.children("span").remove();
            if (state.indexOf("down") > -1) {
                btn.toggleClass("down");
            }
        }
        btn = $("#" + id);
        btn.toggleClass("down");
        $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
        $scope.predicate = predicate;
        var html = btn.html();
        if ($scope.reverse == true) {
            btn.append(" <span class='glyphicon glyphicon-arrow-up'></span>");
        } else {
            btn.children("span").remove();
            btn.append(" <span class='glyphicon glyphicon-arrow-down'></span>");
        }
    };
    $scope.displayCardDetailsModal = function (entry) {
        if (!$scope.dragged) {
            $('#cardDetailsModalTitle').html(entry["Card"]["CardName"]);
            $scope.modalDescription = entry["Card"]["CardText"];
            $scope.apply();
            $('#cardDetailsModal').modal();
        }
        $scope.dragged = false;
    }

    //Utilities
    $scope.range = function (n) {
        return new Array(n);
    };

    //Deck Menu Navigation
    $scope.deckMenuPath = "deckList";
    $scope.setDeckMenuPath = function (path) {
        $scope.deckMenuPath = path;
        $scope.apply();
    };

    //Create Deck
    $scope.createDeck = function (name, description) {
        $.ajax({
            url: "/api/DeckApi",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{\"DeckId\": \"0\",\"OwnerId\": \"0\",\"DeckName\": \"" + name + "\", \"DeckDesc\": \"" + description + "\"}"
        }).done(function (data) {
            $scope.refreshDecks();
            $scope.deckMenuPath = "deckList";
            $scope.apply();
        }).fail(function (err) {
            alert("Sorry I failed you :(");
        }).success(function (result) {
        });
    }


    $scope.deckOpenFlag = false;
    //Open deck
    $scope.openDeck = function (deckId) {
        $scope.deckOpenFlag = true;
        var result = $.grep($scope.decks, function (deck) { return deck.Deck.DeckId === deckId; });
        if (result.length === 0) {
            // not found
        } else if (result.length === 1) {
            $scope.activeDeck = result[0].Deck;
            $http.get("/api/DeckApi/getDeckCardViewModels?id=" + $scope.activeDeck.DeckId).success(function (response) {
                $scope.mainDeck = response;
            });
            $http.get("/api/DeckApi/getSideBoardViewModels?id=" + $scope.activeDeck.DeckId).success(function (response) {
                $scope.sideboard = response;
            });

            $scope.deckMenuPath = "deckBox";
            $scope.apply();
        }
    }

    //Close deck
    $scope.closeDeck = function() {
        $scope.deckOpenFlag = false;
        $scope.sideboard = null;
        $scope.mainDeck = null;
        $scope.activeDeck = null;
        $scope.deckMenuPath = "deckList";
        $scope.apply();
    }

    //Drag and Drop Support
    $scope.dragged = false;
    $scope.onDropToMainDeck = function (data, evt) {
        if ($scope.deckOpenFlag) {
            $http.post("/api/DeckApi/addCardToMainDeck", { "deckId": $scope.activeDeck.DeckId, "cardId": data.Card.CardId, "quantity": 1 }).success(function (response) {
                $http.get("/api/DeckApi/getDeckCardViewModels?id=" + $scope.activeDeck.DeckId).success(function (response) {
                    $scope.mainDeck = response;
                });
            });
        }
    }

    $scope.onDropToSideBoard = function (data, evt) {
        if ($scope.deckOpenFlag) {
            $http.post("/api/DeckApi/addCardToSideboard", { "deckId": $scope.activeDeck.DeckId, "cardId": data.Card.CardId, "quantity": 1 }).success(function (response) {
                $http.get("/api/DeckApi/getSideBoardViewModels?id=" + $scope.activeDeck.DeckId).success(function (response) {
                    $scope.sideboard = response;
                });
            });
        }
    }

    $scope.onDragStart = function () {
        if ($scope.deckOpenFlag) {
            $scope.dragged = true;
            $(".showOnDrag").css("border-color", "silver");
            $(".showOnDrag").css("color", "silver");
            //$(".hideOnDrag").hide();
        }
    }

    $scope.onDragStop = function () {
        if ($scope.deckOpenFlag) {
            $(".showOnDrag").css("border-color", "transparent");
            $(".showOnDrag").css("color", "transparent");
            //$(".hideOnDrag").show();
        }
    }


    $scope.addOneToSideboard = function (data) {
        $http.post("/api/DeckApi/addCardToSideboard", { "deckId": $scope.activeDeck.DeckId, "cardId": data.card.CardId, "quantity": 1 }).success(function (response) {
            $http.get("/api/DeckApi/getSideBoardViewModels?id=" + $scope.activeDeck.DeckId).success(function (response) {
                $scope.sideboard = response;
            });
        });
    }

    $scope.remCardFromSideboard = function(data) {
        $http.post("/api/DeckApi/deleteCardFromSideboard", { "deckId": $scope.activeDeck.DeckId, "cardId": data.card.CardId, "quantity": 1 }).success(function (response) {
            $http.get("/api/DeckApi/getSideBoardViewModels?id=" + $scope.activeDeck.DeckId).success(function (response) {
                $scope.sideboard = response;
            });
        });
    }

    $scope.addOneToMainDeck = function (data) {
        $http.post("/api/DeckApi/addCardToMainDeck", { "deckId": $scope.activeDeck.DeckId, "cardId": data.card.CardId, "quantity": 1 }).success(function (response) {
            $http.get("/api/DeckApi/getDeckCardViewModels?id=" + $scope.activeDeck.DeckId).success(function (response) {
                $scope.mainDeck = response;
            });
        });
    }

    $scope.remOneFromMainDeck = function (data) {
        $http.post("/api/DeckApi/deleteCardFromMainDeck", { "deckId": $scope.activeDeck.DeckId, "cardId": data.card.CardId, "quantity": 1 }).success(function (response) {
            $http.get("/api/DeckApi/getDeckCardViewModels?id=" + $scope.activeDeck.DeckId).success(function (response) {
                $scope.mainDeck = response;
            });
        });
    }
});

