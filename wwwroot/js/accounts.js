function Create(urlAction) {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    $.ajax({
        type: "POST",
        url: urlAction,
        data: {
            __RequestVerificationToken: token,
            Name: $("#accountName").val(),
            Amount: $("ammount").val()
        },
        dataType: "json",
        contentType: "application/json"
    });
}