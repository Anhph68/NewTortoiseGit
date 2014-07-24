$(function () {
    var menu = $('ul.nav.main-menu').metisMenu();

    $(".sidebar-minified i").click(function (e) {
        e.preventDefault(),
        $li = $('.navbar-static-side');
        $li.hasClass("minified")? (
            $li.removeClass("minified"),
            $('#page-wrapper').removeClass("expand"),
            $(this).removeClass('fa-angle-right'),
            $('#todayPicker').removeClass('collapse')
        ) : (
            $li.addClass("minified"),
            $('#page-wrapper').addClass("expand"),
            $(this).addClass('fa-angle-right'),
            menu.find('li.active').toggleClass('active').children('ul').collapse('toggle'),
            $('.nav-second-level').removeAttr('style'),
            $('#todayPicker').addClass('collapse')
        )
        setTimeout(function () { $(window).resize(); }, 300)
    });

    $(window).bind("load resize", function () {
        width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.sidebar-collapse').addClass('collapse').removeClass('in');
            $('div.sidebar-minified').hide();
            $('.navbar-static-side').removeClass('minified');
            $('#todayPicker').addClass('collapse')
        } else {
            $('div.sidebar-collapse').removeClass('collapse');
            $('.sidebar-collapse').removeAttr('style');
            $('div.sidebar-minified').show();
            $('.navbar-static-side').hasClass("minified") ? $('#page-wrapper').addClass("expand") : $('#page-wrapper').removeClass("expand"), 
            $('.navbar-static-side').hasClass("minified") ? $('#todayPicker').addClass('collapse') : $('#todayPicker').removeClass('collapse')
        }
    })
});

var option = {
    todayBtn: "linked",
    language: "vi",
    todayHighlight: true,
    //startDate: "01/01/2014",
    //endDate: "31/12/2014",
    weekStart: 1,
    autoclose: true
};

function formatDate(d) {
    date = new Date(d)
    var dd = date.getDate();
    var mm = date.getMonth() + 1;
    var yyyy = date.getFullYear();
    if (dd < 10) { dd = '0' + dd }
    if (mm < 10) { mm = '0' + mm };
    return d = dd + '/' + mm + '/' + yyyy
}

function loadSuccess(st) {
    setTimeout(function () {
        toastr.success(st);
    }, 500);
}