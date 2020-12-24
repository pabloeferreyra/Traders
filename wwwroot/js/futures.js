$(function () {
    var tdate = new Date();
    var dd = tdate.getDate(); //yields day
    var MM = tdate.getMonth() + 1; //yields month
    var yyyy = tdate.getFullYear(); //yields year
    var h = tdate.getHours();
    var m = tdate.getMinutes();
    if (h < 10) {
        h = '0' + h;
    }

    if (m < 10) {
        m = '0' + m;
    }

    if (dd < 10) {
        dd = '0' + dd;
    }

    if (MM < 10) {
        MM = '0' + MM;
    }
    var currentDate = yyyy + "-" + MM + "-" + dd;

    $("#startDate").blur(function () {
        if ($("#startDate").val() < currentDate) {
            $("#startDateVal").text('la fecha no puede ser anterior a la actual.');
            $("#btnCreateFuture").prop('disabled', true);
        }
        else {
            $("#btnCreateFuture").prop('disabled', false);
        }
    });
})
function Create(urlAction) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        type: "POST",
        url: urlAction,
        data: {
            __RequestVerificationToken: token,
            ClientId: $("#clientId :selected").val(),
            StartDate: $("#startDate").val(),
            ParticipationId: $("#clientId :selected").val(),
            Capital: $("#capital").val()
        },
        dataType: "json",
        contentType: "application/json"
    });
}
