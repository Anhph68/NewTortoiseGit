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