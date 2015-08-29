//AJAX function
function startAjax(i, val) {
    $.ajax({
        url: 'http://localhost:59756/api/Quantity/' + i + "?value=" + val,
        type: "PUT",
    }).done(function (data) {
    }).fail(function (err) {
        alert('fail')
    }).success(function (result) {
    });

}