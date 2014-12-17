

function s(o, d, u, i) {
    o.on('click', d, function (e) {
        e.preventDefault();
        var data = o.row($(this).parents('tr')).data();
        $.get(u, { key: data[i] }, function (msg) {
            $('body').append(msg);
        });
    });
}