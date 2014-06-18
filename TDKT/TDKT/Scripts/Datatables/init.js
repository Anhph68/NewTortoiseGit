var oTable;
$.extend($.fn.DataTable.defaults, {
    "paginationType": "full_numbers",
    "language": {
        "search": "Tìm kiếm: ",
        "decimal": ",",
        "thousands": ".",
        "lengthMenu": "Hiển thị _MENU_ kết quả/trang",
        "zeroRecords": "Không tìm thấy kết quả nào phù hợp",
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