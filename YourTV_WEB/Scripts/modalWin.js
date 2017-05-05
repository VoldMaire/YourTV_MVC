$(function () {
    $.ajaxSetup({ cache: false });
    $(".playlist-create").click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#dialogContent').html(data);
            $('#modDialog').modal('show');
        });
    });
});