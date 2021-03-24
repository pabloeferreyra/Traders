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
            RetireCapital: $("#retireCapital").val(),
            FinishDate: $("#finishDate").val(),
            StartCurrency: $("#startCurrency").val(),
            RetireCurrency: $("#retireCurrency").val(),
            Client: {
                Name: $("#ClientName").val(),
                Email: $("#ClientEmail").val(),
                Dni: $("#ClientDni").val(),
                BirthDate: $("#ClientBirthDate").val(),
                Address: $("#ClientAddress").val(),
                City: $("#ClientCity").val(),
                Province: $("#ClientProvince").val(),
                Country: $("#ClientCountry").val(),
                Phone: $("#ClientPhone").val(),
                VirtualWallet1: $("#ClientVirtualW1").val(),
                VirtualWallet2: $("#ClientVirtualW2").val(),
                Pep: $("#ClientPep").val()
            }
        },
        success: function () {
            toastr.success('Contrato creado correctamente', 'Correcto!');
        },
        error: function () {
            toastr.error('Contrato con errores', 'Error!');
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
            toastr.success('Email enviado correctamente', 'Enviado!');
        }
    });
}

$("#ClientDni").blur(function () {
    if ($("#ClientDni").val() == '' || $("#ClientDni").val() <= '0') {
        $("#ClientDniVal").text('Por favor ingresar un valor mayor a 0.');
        $("#btnCreateFuture").prop('disabled', true);
    }
    else {
        var dateVal;
        $.ajax({
            type: "GET",
            url: "/Traders/Common/GetClient",
            dataType: "json",
            contentType: "application/json",
            data: ({ dni: $("#ClientDni").val() }),
            complete: function (msj) {
                if (msj.responseText.length > 4) {
                    $("#ClientId").val(msj.responseJSON.id).change();
                    $("#ClientName").val(msj.responseJSON.name).change();
                    $("#ClientEmail").val(msj.responseJSON.email).change();
                    $("#ClientBirthDate").val(msj.responseJSON.birthDate.substring(0, 10)).change();
                    $("#ClientAddress").val(msj.responseJSON.address).change();
                    $("#ClientCity").val(msj.responseJSON.city).change();
                    $("#ClientProvince").val(msj.responseJSON.province).change();
                    $("#ClientCountry").val(msj.responseJSON.country).change();
                    $("#ClientPhone").val(msj.responseJSON.phone).change();
                    $("#ClientVirtualW1").val(msj.responseJSON.virtualWallet1).change();
                    $("#ClientVirtualW2").val(msj.responseJSON.virtualWallet2).change();
                    $("#ClientPep").val(msj.responseJSON.pep).change();
                    $("#ClientPep").prop('disabled', true);
                }
            }
        });
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
    if ($("#refeer").val() != '') {
        $.ajax({
            type: "GET",
            url: "/Traders/Common/ClientExist",
            dataType: "json",
            contentType: "application/json",
            data: ({ email: $("#refeer").val() }),
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

$("#startCurrency").blur(function () {
    if ($("#startCurrency").val != '') {
        $.ajax({
            type: "GET",
            url: "/Traders/Common/CurrencyExists",
            dataType: "json",
            contentType: "application/json",
            data: ({ currency: $("#startCurrency").val() }),
            complete: function (msj) {
                value = msj.responseText;
                if (value == 'true') {
                    $("#btnCreateFuture").prop('disabled', false);
                    toastr.success('Moneda Aceptada', 'Correcto!');
                }
                else {
                    $("#startCurrencyVal").text('Por favor ingresar una Moneda valida.');
                    toastr.error('Por favor ingresar una moneda valida.', 'Error');
                    $("#btnCreateFuture").prop('disabled', true);
                }
            }
        });
    }
    else {
        if ($("#startCurrency").val == '') {
            $("#startCurrencyVal").text('Por favor ingresar una Moneda valida.');
            toastr.error('Por favor ingresar una moneda valida.', 'Error');
            $("#btnCreateFuture").prop('disabled', true);
        }
    }
});
$("#retireCurrency").blur(function () {
    if ($("#retireCurrency").val != '') {
        $.ajax({
            type: "GET",
            url: "/Traders/Common/CurrencyExists",
            dataType: "json",
            contentType: "application/json",
            data: ({ currency: $("#retireCurrency").val() }),
            complete: function (msj) {
                value = msj.responseText;
                if (value == 'true') {
                    $("#btnCreateFuture").prop('disabled', false);
                    toastr.success('Moneda Aceptada', 'Correcto!');
                }
                else {
                    $("#retireCurrencyVal").text('Por favor ingresar una Moneda valida.');
                    toastr.error('Por favor ingresar una moneda valida.', 'Error');
                    $("#btnCreateFuture").prop('disabled', true);
                }
            }
        });
    }
    else {
        if ($("#retireCurrency").val == '') {
            $("#retireCurrency").text('Por favor ingresar una Moneda valida.');
            toastr.error('Por favor ingresar una moneda valida.', 'Error');
            $("#btnCreateFuture").prop('disabled', true);
        }
    }
});


$("#startDate").blur(function () {
    $.ajax({
        type: "GET",
        url: "/Traders/Common/CalculateTime",
        dataType: "json",
        contentType: "application/json",
        data: ({ date: $("#startDate").val() }),
        complete: function (msj) {
            var date = new Date(msj.responseJSON);
            var mo = date.getMonth() + 1;
            var da = date.getDate();
            if (mo < 10) {
                mo = "0" + mo;
            }
            if (da < 10) {
                da = "0" + da;
            }
            var dt = date.getFullYear() + "-" + mo + "-" + da;
            $("#finishDate").val(dt);
                
        }
    });
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

$("#ClientName").blur(function () {
    if ($("#ClientName").val() == '') {
        $("#ClientNameVal").text('Por favor ingresar un nombre.');
        $("#btnCreateFuture").prop('disabled', true);
    }
    else {
        $("#btnCreateFuture").prop('disabled', false);
    }
});

$("#ClientAddress").blur(function () {
    if ($("#ClientAddress").val() == '') {
        $("#ClientAddressVal").text('Por favor ingresar un DNI/CUIT.');
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
