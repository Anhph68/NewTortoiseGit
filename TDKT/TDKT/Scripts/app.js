$(function () {
    $('ul.nav.main-menu').metisMenu();

    $(".sidebar-minified i").click(function (e) {
        e.preventDefault(),
        $li = $('.navbar-static-side'),
        console.log($li);
        $li.hasClass("minified") ? (
            //$li.find(".toggle-icon").removeClass("fa-angle-down").addClass("fa-angle-left"),
            $li.removeClass("minified"), $('#page-wrapper').removeClass("expand")
        ) : (
            //$li.find(".toggle-icon").removeClass("fa-angle-left").addClass("fa-angle-down"),
            $li.addClass("minified"), $('#page-wrapper').addClass("expand")
        )
        //$li.find(".sub-menu").slideToggle(300)
    })

});

$.fn.clickToggle = function (e, t) {
    return this.each(function () {
        var o = !1; $(this).bind("click", function () {
            return o ? (o = !1, t.apply(this, arguments)) : (o = !0, e.apply(this, arguments))
        })
    })
};

//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
$(function () {
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
            $('#page-wrapper').removeClass("expand");
        }
    })
})