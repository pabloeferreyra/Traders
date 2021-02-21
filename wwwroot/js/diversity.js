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
            ClientCode: $("#ClientCode").val(),
            AmmountBTC: $("#AmmountBTC").val(),
            AmmountETH: $("#AmmountETH").val(),
            AmmountUSDT: $("#AmmountUSDT").val(),
        },
        complete: function (result) {
            toastr.success('Cliente cargado correctamente', 'Correcto!');
        }
    });
}

$("#ClientCode").blur(function () {
    if ($("#ClientCode").val() == '' || $("#ClientCode").val() <= '0') {
        $("#ClientCodeVal").text('Por favor ingresar un valor mayor a 0.');
        $("#btnCreateDiversity").prop('disabled', true);
    }
    else {
        $.ajax({
            type: "POST",
            url: "/Traders/Common/ClientExist",
            dataType: "json",
            contentType: "application/json",
            data: $("#ClientCode").val(),
            complete: function (msj) {
                value = msj.responseText;
                if (value == 'true') {
                    $("#btnCreateDiversity").prop('disabled', false);
                    toastr.success('Cliente aceptado', 'Correcto!');
                }
                else {
                    $("#ClientCodeVal").text('Por favor ingresar un cliente valido.');
                    toastr.error('Por favor ingresar un cliente valido.', 'Error');
                    $("#btnCreateDiversity").prop('disabled', true);
                }
            }
        });
    }
});