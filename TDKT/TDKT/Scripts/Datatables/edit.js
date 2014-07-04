toastr.options = {
    "closeButton": true,
    "debug": false,
    "positionClass": "toast-bottom-right",
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "3000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

function s(o, d, u, i) {
    return o.on('click', d, function () {
        console.log("OK");
        var data = o.row($(this).parents('tr')).data();
        $.get(u, { key: data[i] }, function (data) {
            $('body').after(data);
        });
    });
}