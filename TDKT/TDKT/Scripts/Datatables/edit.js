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

var option = {
    todayBtn: "linked",
    language: "vi",
    todayHighlight: true,
    startDate: "01/01/2014",
    endDate: "31/12/2014",
    weekStart: 1,
    autoclose: true
};

function s(o, d, u, i) {
    return o.on('click', d, function () {
        var data = o.row($(this).parents('tr')).data();
        $.get(u, { key: data[i] }, function (data) {
            $('body').after(data);
        });
    });
}

function loadSuccess(st) {
    $('#myModal').modal('hide');
    o.ajax.reload();
    setTimeout(function () {
        toastr.success(st);
    }, 300);
}