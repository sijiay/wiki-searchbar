$(document).ready(function () {
   
    $("#text_name").keyup(function () {
        var newText = $(this).val()
        document.getElementById("outbox").innerHTML = newText;
        $('#outbox').empty();
        json();
    });
    //var output = $("#outbox").val();
})

function json() {
    var num = $("#text_name").val();

    $.ajax({
        type: "POST",
        url: "WebService1.asmx/searchPrefixForSuggestions", 
        data:"{phrase:'" + num + "'}",   
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            var list = msg.d;
            for (var i = 0; i < list.length; i++) {
                $('#outbox').append("<p>" + list[i] + "</p>");
            }
        },
        error: function (msg) {
        }
    });
}
