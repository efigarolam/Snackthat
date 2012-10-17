/// <summary>
/// Validates the current form
/// </summary>
function validateForm() {
    $('.quantity').attr('disabled', true);

    $('input:checkbox').click(function () {
        if ($(this).is(':checked')) {
            var id = $(this).attr("value");
            var selector = ".id" + id;
            $(selector).attr("disabled", false);
        } else {
            var id = $(this).attr("value");
            var selector = ".id" + id;
            var selector2 = ".id" + id + " option[value=0]";
            $(selector).attr("disabled", true);
            $(selector2).attr("selected", true);
        }
    });
}