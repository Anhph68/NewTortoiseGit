function u(o, u) {
    return o.DataTable({
        //"bAutoWidth": false,
        //"bServerSide": true,
        "sAjaxSource": u,
        //"bProcessing": true,
        "columns": [
            { "data": "col0", "width": "10px", "class": "center" },
            { "data": "col1", "visible": false, "searchable": false },
            { "data": "col2" },
            { "data": "col3" },
            { "data": "col4" },
            { "data": "col5", "searchable": false, "sortable": false },
            { "data": "col6" },
            {
                "targets": -1,
                "sortable": false,
                "data": null,
                "defaultContent": '<button class="btn btn-sm btn-warning resetPwd" data-toggle="tooltip" data-placement="top" title="Thiết lập lại mật khẩu"><span class="fa fa-key"></span></button><button class="btn btn-sm btn-primary edit" data-toggle="tooltip" data-placement="top" title="Thay đổi thông tin"><span class="glyphicon glyphicon-pencil"></span></button><button class="btn btn-sm btn-danger delete" data-toggle="tooltip" data-placement="top" title="Xóa"><span class="glyphicon glyphicon-trash"></span></button>'
            }
        ],
        "fnServerParams": function (aoData) {
            aoData.push({ "name": "DonVi", "value": $('#DonVi').val() });
        },
        "fnDrawCallback": function () {
            $('button.btn').tooltip({ 'delay': { show: 500 } });
        }
    }).on('mouseover', 'td', function () {
        $('#myData tbody tr').removeClass('highlight');
        $(this).closest('tr').addClass('highlight');
    });
}

function d(o, u, c) {
    return o.DataTable({
        //"bAutoWidth": false,
        //"bServerSide": true,
        "sAjaxSource": u,
        //"bProcessing": true,
        "columns": c, 
        "fnDrawCallback": function () {
            $('button.btn').tooltip({ 'delay': { show: 500 } });
        }
    }).on('mouseover', 'td', function () {
        $('#myData tbody tr').removeClass('highlight');
        $(this).closest('tr').addClass('highlight');
    });
}