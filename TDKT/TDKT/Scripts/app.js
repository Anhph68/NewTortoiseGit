$(function () {
    var menu = $('ul.nav.main-menu').metisMenu();

    $(".sidebar-minified i").click(function (e) {
        e.preventDefault(),
        $li = $('.navbar-static-side');
        $li.hasClass("minified")? (
            $li.removeClass("minified"),
            $('#page-wrapper').removeClass("expand"),
            $(this).removeClass('fa-angle-right')
        ) : (
            $li.addClass("minified"),
            $('#page-wrapper').addClass("expand"),
            $(this).addClass('fa-angle-right'),
            menu.find('li.active').toggleClass('active').children('ul').collapse('toggle'),
            $('.nav-second-level').removeAttr('style')
        )
    });

    $(window).bind("load resize", function () {
        width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        if (width < 768) {
            $('div.sidebar-collapse').addClass('collapse');
            $('div.sidebar-minified').hide();
            $('.navbar-static-side').removeClass('minified');
            
        } else {
            $('div.sidebar-collapse').removeClass('collapse');
            $('.sidebar-collapse').removeAttr('style');
            $('div.sidebar-minified').show();
            $('.navbar-static-side').hasClass("minified") ? $('#page-wrapper').addClass("expand") : $('#page-wrapper').removeClass("expand");
        }
    })
});