$(function () {
    var placeholder = $("#modal-placeholder");
    $(document).on('click', 'a[data-toggle="add-ajax-modal"]', function () {
        var url = $(this).data('url');
        $.ajax({
            url: url            
        }).done(function (result) {
            placeholder.html(result);
            placeholder.find('.modal').modal('show');
        });
    });
    $(document).on('click', 'button[data-toggle="add-ajax-modal"]', function () {
        var url = $(this).data('url');
        $.ajax({
            url: url,
            error: function (jqXHR, ) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'network error';
                } else if (jqXHR.status == 404) {
                    msg = 'not found';

                } else if (jqXHR.status == 401) {
                    msg = 'unauthorized';
                    //window.location.href = 'http://localhost:51056/Account/Login';
                } else if (jqXHR.status == 500) {
                    msg = 'intevral error';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
            }
        }).done(function (result) {
            placeholder.html(result);
            placeholder.find('.modal').modal('show');
        });
    });
    // edit as modal
    $(document).on('click', 'button[data-toggle="edit-ajax-modal"]', function () {
        var url = $(this).data('url');
        $.ajax({
            url: url,
            error: function (jqXHR) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'network error';
                } else if (jqXHR.status == 404) {
                    msg = 'not found';

                } else if (jqXHR.status == 401) {
                    msg = 'unauthorized';
                } else if (jqXHR.status == 500) {
                    msg = 'intevral error';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
            }
        }).done(function (result) {
            placeholder.html(result);
            placeholder.find('.modal').modal('show');
        });
    });
});
