$(document).ready(function () {
    $('#pdiv').hide();
    $('#txtPromoCode').val('');
    var API = location.protocol + "//" + window.location.hostname + "/";
    function GetDropDownData() {
        $.ajax({
            type: "GET",
            url: API + "Home/GetPrices?promoCode=" + $('#txtPromoCode').val(),
            contentType: "application/json"
        }).done(function (res) {
            if (res.length) {
                $('#price').empty();
                $('.list').empty();
                $('.current').text('');
                $('#hosted_button_id').val('KYG4UW8YRTJA4');
                $.each(res, function (key, value) {
                    $('#price')
                        .append($("<option></option>")
                            .attr("value", value.Value)
                            .text(value.Text));
                    $('.list')
                        .append($("<li></li>")
                            .attr("class", "option")
                            .attr("data-value", value.Value)
                            .text(value.Text));
                });
                $('#lblText').text("");
            }
            else {
                $('#lblText').text("Invalid Promo Code");
                $('#hosted_button_id').val('VPMZ5KP8XQW44');
            }
        });
    }

    $("#btnPromo").click(function () {
        GetDropDownData();
    });
});