$.extend($.fn.DataTable.defaults, {
    "bAutoWidth": false,
    "bServerSide": true,
    "bProcessing": true,
    "paginationType": "full_numbers",
    "language": {
        "processing": '<div id="blurringTextG"><div id="blurringTextG_1" class="blurringTextG">Đ</div><div id="blurringTextG_2" class="blurringTextG">a</div><div id="blurringTextG_3" class="blurringTextG">n</div><div id="blurringTextG_4" class="blurringTextG">g</div><div id="blurringTextG_5" class="blurringTextG">&nbsp;</div><div id="blurringTextG_6" class="blurringTextG">l</div><div id="blurringTextG_7" class="blurringTextG">ấ</div><div id="blurringTextG_8" class="blurringTextG">y</div><div id="blurringTextG_9" class="blurringTextG">&nbsp;</div><div id="blurringTextG_10" class="blurringTextG">d</div><div id="blurringTextG_11" class="blurringTextG">ữ</div><div id="blurringTextG_12" class="blurringTextG">&nbsp;</div><div id="blurringTextG_13" class="blurringTextG">l</div><div id="blurringTextG_14" class="blurringTextG">i</div><div id="blurringTextG_15" class="blurringTextG">ệ</div><div id="blurringTextG_16" class="blurringTextG">u</div><div id="blurringTextG_17" class="blurringTextG">.</div><div id="blurringTextG_18" class="blurringTextG">.</div><div id="blurringTextG_19" class="blurringTextG">.</div></div>',
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
        "sAjaxSource": u,
        "bStateSave": true,
        "columns": [
            { "data": "col0", "width": "10px", "class": "center" },
            { "data": "col1", "visible": false, "searchable": false },
            { "data": "col2" },
            { "data": "col3" },
            { "data": "col4" },
            { "data": "col5" },
            { "targets": -1, "class": "center", "sortable": false, "searchable": false, "data": null, "defaultContent": '<span class="glyphicon glyphicon-chevron-right" style="color: #428bca;" title="Chọn cuộc kiểm toán để cập nhập số liệu"></span>' }
        ],
        "fnServerParams": function (aoData) {
            aoData.push({ "name": "Year", "value": $("span#year").text() });
            aoData.push({ "name": "DonVi", "value": $('#DonVi').val() });
            aoData.push({ "name": "Status", "value": 3 });
            aoData.push({ "name": "LinhVuc", "value": $('#LinhVuc').val() });
        }
    }).on('mouseover', 'tr[role = "row"] td', function () {
        $('#myData tbody tr').removeClass('highlight');
        $(this).closest('tr').addClass('highlight');
    });
}
var option = {
    todayBtn: "linked",
    language: "vi",
    todayHighlight: true,
    weekStart: 1,
    autoclose: true,
    calendarWeeks: false,
    daysOfWeekDisabled: [0]
};
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