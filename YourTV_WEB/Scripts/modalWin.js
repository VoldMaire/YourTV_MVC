
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

    $(function () {
        $.ajaxSetup({ cache: false });
        $("#confirm-creating").click(function (e) {

            e.preventDefault();
            $.get(this.href, function (data) {
                if (this.href.indexOf('AddingPlaylist') + 1) {
                    $('#dialogContent').html(data);
                    $('#playlist-create').modal('show');
                }
            });
        });
    });

