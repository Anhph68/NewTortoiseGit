//var o;
$.extend($.fn.DataTable.defaults, {
    "paginationType": "full_numbers",
    "language": {
        "search": "Tìm kiếm: ",
        "decimal": ",",
        "thousands": ".",
        "lengthMenu": "Hiển thị _MENU_ kết quả/trang",
        "emptyTable": "Không tìm thấy kết quả phù hợp",
        "info": "Đang xem _START_ đến _END_ trong tổng số _TOTAL_ kết quả",
        "infoEmpty": "Không tìm thấy kết quả nào phù hợp",
        "infoFiltered": "(được lọc từ _MAX_ cuộc)",
        "paginate": {
            "first": '<span class="glyphicon glyphicon-fast-backward"></span>',
            "previous": '<span class="glyphicon glyphicon-backward"></span>',
            "next": '<span class="glyphicon glyphicon-forward"></span>',
            "last": '<span class="glyphicon glyphicon-fast-forward"></span>'
        }
    }
});

function g(o, u) {
    return o.DataTable({
        "bServerSide": true,
        "sAjaxSource": u,
        //"bProcessing": true,
        "columns": [
            { "data": "STT", "width": "10px" },
            { "data": "MaCuoc", "visible": false },
            { "data": "TenCuoc" },
            { "data": "DonVi" },
            { "data": "SoQuyetDinh" },
            { "data": "NgayKyQD", "sortable": false },
            {
                "targets": -1,
                "sortable": false,
                "data": null,
                "defaultContent": '<button class="btn btn-sm btn-primary mgr2 form-group edit" data-toggle="tooltip" data-placement="top" title="Thay đổi thông tin"><span class="glyphicon glyphicon-pencil"></span></button><button class="btn btn-sm btn-danger delete form-group" data-toggle="tooltip" data-placement="top" title="Xóa"><span class="glyphicon glyphicon-remove"></span></button>'
            }
        ],
        "fnServerParams": function (aoData) {
            aoData.push({ "name": "Year", "value": "2012"});
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