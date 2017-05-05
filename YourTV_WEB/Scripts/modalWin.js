
$(function () {
    $.ajaxSetup({ cache: false });
    $("#btn-create-playlist").click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#dialogContent').html(data);
            $('#playlist-create').modal('show');
        });
    });
});
