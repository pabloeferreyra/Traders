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