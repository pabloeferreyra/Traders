function Create(urlAction) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        type: "POST",
        url: urlAction,
        data: {
            __RequestVerificationToken: token,
            Code: $("#ClientCode").val(),
            Email: $("#ClientEmail").val()
        },
        dataType: "json",
        contentType: "application/json"
    });
}

$("#ClientCode").blur(function () {
    if ($("#ClientCode").val() == '' || $("#ClientCode").val() <= '0') {
        $("#ClientCodeVal").text('Por favor ingresar un valor mayor a 0.');
        $("#btnCreateClient").prop('disabled', true);
    }
    else if ($("#ClientCode").val() == cuc) {
        $("#ClientCodeVal").text('cliente ya existente, por favor corregir');
        $("#btnCreateClient").prop('disabled', true);
    }
    else {
        $("#btnCreateClient").prop('disabled', false);
    }
});

$("#ClientEmail").blur(function () {
    if ($("#ClientEmail").val() == '') {
        $("#ClientEmailVal").text('Por favor ingresar un valor mayor a 0.');
        $("#btnCreateClient").prop('disabled', true);
    }
    else {
        $("#btnCreateClient").prop('disabled', false);
    }
});