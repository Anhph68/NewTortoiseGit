$(function () {
    var n = $("ul.nav.main-menu").metisMenu();
    $(".sidebar-minified i").click(function (t) {
        t.preventDefault();
        $li = $(".navbar-static-side");
        $li.hasClass("minified") ? ($li.removeClass("minified"), $("#page-wrapper").removeClass("expand"), $(this).removeClass("fa-angle-right"), $(".bottom-side").show(), $('#todayPicker').removeClass('collapse')) : ($li.addClass("minified"), $("#page-wrapper").addClass("expand"), $(this).addClass("fa-angle-right"), n.find("li.active").toggleClass("active").children("ul").collapse("toggle"), $(".nav-second-level").removeAttr("style"), $(".bottom-side").hide(), $('#todayPicker').addClass('collapse'));
        setTimeout(function () { $(window).resize() }, 300)
    });
    $(window).bind("load resize", function () {
        width = this.window.innerWidth > 0 ? this.window.innerWidth : this.screen.width; width < 768 ? ($("div.sidebar-collapse").addClass("collapse").removeClass("in"), $("div.sidebar-minified").hide(), $(".navbar-static-side").removeClass("minified"), $(".bottom-side").hide(), $('#todayPicker').addClass('collapse')) : ($("div.sidebar-collapse").removeClass("collapse"), $(".sidebar-collapse").removeAttr("style"), $("div.sidebar-minified").show(), $(".navbar-static-side").hasClass("minified") ? $("#page-wrapper").addClass("expand") : $("#page-wrapper").removeClass("expand"), $(".navbar-static-side").hasClass("minified") ? $(".bottom-side").hide() : $(".bottom-side").show(), $('.navbar-static-side').hasClass("minified") ? $('#todayPicker').addClass('collapse') : $('#todayPicker').removeClass('collapse'))
    })
});
//function formatDate(d) {
//    date = new Date(d)
//    var dd = date.getDate();
//    var mm = date.getMonth() + 1;
//    var yyyy = date.getFullYear();
//    if (dd < 10) { dd = '0' + dd }
//    if (mm < 10) { mm = '0' + mm };
//    return d = dd + '/' + mm + '/' + yyyy
//}
