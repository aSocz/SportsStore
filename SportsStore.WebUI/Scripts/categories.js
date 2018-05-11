$(function () {
    $(".delete-button").click(function () {
        $(this).hide();
        $(this).siblings(".form-alert").css('visibility', 'visible');
        $(this).siblings(".form-submit").show();
    });
});
