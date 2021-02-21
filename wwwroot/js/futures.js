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
            Capital: $("#capital").val(),
            RefeerCode: $("#refeer").val(),
            RetireCapital: $("#retireCapital").val()
        },
        complete: function (result) {
            toastr.success('Contrato creado correctamente', 'Correcto!');
        }
    });
}

function Retire(urlAction) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        type: "POST",
        url: urlAction,
        dataType: "json",
        contentType: "application/json",
        data: {
            __RequestVerificationToken: token,
            ContractNumber: $("#ContractNumber").val(),
            Capital: $("#Capital").val(),
            RetireDate: $('#RetireDate').val,
            RetireCapital: $("#RetireCapital").val()
        },
        complete: function (result) {
            toastr.success('retiro realizado correctamente', 'Correcto!');
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
        $("#btnCreateFuture").prop('disabled', false);
    }
});

$("#FixRent").change(function () {
    if ($('#FixRent').is(":checked")) {
        $("#Participation").hide(1000);
        $('#fixRentP').show(1000);
        $('#Refeerer').show(1000);
    }
    else {
        $("#Participation").show(1000);
        $('#fixRentP').hide(1000);
        $('#Refeerer').hide(1000);
    }
});

$("#refeer").blur(function () {
    if ($("#refeer").val() <= '0') {
        $("#RefeerVal").text('Por favor ingresar un valor mayor a 0.');
        $("#btnCreateFuture").prop('disabled', true);
    }
    else if ($("#refeer").val() != '') {
        $.ajax({
            type: "POST",
            url: "/Traders/Common/ClientExist",
            dataType: "json",
            contentType: "application/json",
            data: $("#refeer").val(),
            complete: function (msj) {
                value = msj.responseText;
                if (value == 'true') {
                    $("#btnCreateFuture").prop('disabled', false);
                    toastr.success('Cliente aceptado', 'Correcto!');
                }
                else {
                    $("#RefeerVal").text('Por favor ingresar un cliente valido.');
                    toastr.error('Por favor ingresar un cliente valido.', 'Error');
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

function SearchFutures(urlAction) {
    var date = $("#DateContract").val();
    $.ajax({
        type: "POST",
        url: urlAction,
        data: {
            dateContract: date
        },
        success: function (result) {
            if (result.trim().length == 0) {
                toastr.success('no quedan contratos!', 'Todo listo!');
                $("#FuturesPartial").html(result);
            }
            $("#FuturesPartial").html(result);
        },
        error: function (req, status, error) {
        }
    });
}

function SearchAllFutures(urlAction) {
    $.ajax({
        type: "POST",
        url: urlAction,
        data: {
            dateTurn: null,
            medicId: null
        },
        success: function (result) {
            if (result.trim().length == 0) {
                toastr.success('no quedan contratos!', 'Todo listo!');
                $("#FuturesPartial").html(result);
            }
            $("#FuturesPartial").html(result);
        },
        error: function (req, status, error) {
        }
    });
}
