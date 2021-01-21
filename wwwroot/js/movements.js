function Create(urlAction) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        type: "POST",
        url: urlAction,
        data: {
            __RequestVerificationToken: token,
            UserGuid: $("#UserGuid").val(),
            AmountIn: $("#AmountIn").val(),
            BankAccountGuidIn: $("#BankAccountIn :selected").val(),
            AmountOut: $("#AmountOut").val(),
            BankAccountGuidIn: $("#BankAccountOut :selected").val(),
            Comission: $("#Comission").val()
        },
        dataType: "json",
        contentType: "application/json"
    });
}

$("#AmmountIn").blur(function () {
    if ($("#AmmountIn").val() == '' || $("#AmmountIn").val() <= '0') {
        $("#AmmountInVal").text('Por favor ingrese un valor mayor a 0.');
        $("#btnCreateMov").prop('disabled', true);
    }
    else {
        $("#btnCreateMov").prop('disabled', false);
    }
});
$("#AmmountOut").blur(function () {
    if ($("#AmmountOut").val() == '' || $("#AmmountOut").val() <= '0') {
        $("#AmmountOutVal").text('Por favor ingrese un valor mayor a 0.');
        $("#btnCreateMov").prop('disabled', true);
    }
    else {
        $("#btnCreateMov").prop('disabled', false);
    }
});

$("#Comission").blur(function () {
    if ($("#Comission").val() == '' || $("#AmmountIn").val() <= '0') {
        $("#ComissionVal").text('Por favor ingrese un valor mayor a 0.');
        $("#btnCreateMov").prop('disabled', true);
    }
    else {
        $("#btnCreateMov").prop('disabled', false);
    }
});