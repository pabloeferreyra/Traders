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
});
function Create(urlAction) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        type: "POST",
        url: urlAction,
        dataType: "json",
        contentType: "application/json",
        data: {
            __RequestVerificationToken: token,
            Code: $("#ClientCode").val(),
            Email: $("#ClientEmail").val(),
            StartDate: $("#startDate").val(),
            ParticipationId: $("#participationId :selected").val(),
            Capital: $("#capital").val()
        },
        complete: function (result) {
            toastr.success('Contrato creado correctamente', 'Correcto!');
        }
    });
}

function SendReport(urlAction) {
    $.ajax({
        type: "GET",
        url: urlAction,
        dataType: "json",
        contentType: "application/json",
        data: {
        },
        success: function (result) {
            toastr.success('Email enviado correctamente', 'Enviado!');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error('No se pudo enviar el email', 'Error');
        }
    });
}

$("#ClientCode").blur(function () {
    if ($("#ClientCode").val() == '' || $("#ClientCode").val() <= '0') {
        $("#ClientCodeVal").text('Por favor ingresar un valor mayor a 0.');
        $("#btnCreateFuture").prop('disabled', true);
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Common/ClientExist",
            dataType: "json",
            contentType: "application/json",
            data: {
                Code: $("#ClientCode").val(),
            },
            complete: function (msj) {
                value = msj.responseText;
                if (value == 'false') {
                    $("#btnCreateFuture").prop('disabled', false);
                }
                else {
                    $("#ClientCodeVal").text('Por favor ingresar un cliente Nuevo.');
                    toastr.error('Por favor ingresar un cliente Nuevo.', 'Error');
                    $("#btnCreateFuture").prop('disabled', true);
                }
            }
        });
    }
});

$("#ClientEmail").blur(function () {
    if ($("#ClientEmail").val() == '') {
        $("#ClientEmailVal").text('Por favor ingresar un email.');
        $("#btnCreateFuture").prop('disabled', true);
    }
    else {
        $("#btnCreateFuture").prop('disabled', false);
    }
});
