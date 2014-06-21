function u(o, u) {
    return o.DataTable({
        "bServerSide": true,
        "sAjaxSource": u,
        //"bProcessing": true,
        "columns": [
            { "data": "STT", "width": "10px" },
            { "data": "ID", "visible": false },
            { "data": "HoTen" },
            { "data": "TenDangNhap" },
            { "data": "DonVi", "sortable": false },
            {
                "targets": -1,
                "sortable": false,
                "data": null,
                "defaultContent": '<button class="btn btn-sm btn-primary mgr2 edit" data-toggle="tooltip" data-placement="top" title="Thay đổi thông tin"><span class="glyphicon glyphicon-pencil"></span></button><button class="btn btn-sm btn-danger delete" data-toggle="tooltip" data-placement="top" title="Xóa"><span class="glyphicon glyphicon-remove"></span></button>'
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