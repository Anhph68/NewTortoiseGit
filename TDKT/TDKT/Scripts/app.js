$(function () {
    $('#side-menu').metisMenu();

    function e() {
        $(window).width() < 977 ? $(".navbar-static-side").hasClass("minified") && ($(".navbar-static-side").removeClass("minified"), $(".navbar-static-side").addClass("init-minified")) : $(".navbar-static-side").hasClass("init-minified") && $(".navbar-static-side").removeClass("init-minified").addClass("minified")
    }

    //    //$(".js-toggle-minified").clickToggle(function () {
    //    //    console.log("OK");
    //    //    $(".navbar-static-side").addClass("minified"), $("#page-wrapper").addClass("expanded"), $(".navbar-static-side .nav-second-level").css("display", "none"), $(".sidebar-minified").find("i.fa-angle-left").toggleClass("fa-angle-right"), $(".navbar-static-side .text").css("display", "none")
    //    //}, function () {
    //    //    $(".navbar-static-side").removeClass("minified"), $("#page-wrapper").removeClass("expanded"), $(".sidebar-minified").find("i.fa-angle-left").toggleClass("fa-angle-right")
    //    //});

    if ($(".main-menu .js-sub-menu-toggle").click(function (e) {
        e.preventDefault(),
        $li = $(this).parents("li"),
        $li.hasClass("active") ? ($li.find(".toggle-icon").removeClass("fa-angle-down").addClass("fa-angle-left"), $li.removeClass("active")) : ($li.find(".toggle-icon").removeClass("fa-angle-left").addClass("fa-angle-down"), $li.addClass("active")), $li.find(".sub-menu").slideToggle(300)

    }), $(".js-toggle-minified").clickToggle(function () {
        $(".left-sidebar").addClass("minified"), $(".content-wrapper").addClass("expanded"), $(".left-sidebar .sub-menu").css("display", "none").css("overflow", "hidden"), $(".sidebar-minified").find("i.fa-angle-left").toggleClass("fa-angle-right")
    }, function () {
        $(".left-sidebar").removeClass("minified"), $(".content-wrapper").removeClass("expanded"), $(".sidebar-minified").find("i.fa-angle-left").toggleClass("fa-angle-right")
    }), $(".main-nav-toggle").clickToggle(function () {
        $(".left-sidebar").slideDown(300)
    }, function () {
        $(".left-sidebar").slideUp(300)
    }), $mainContentCopy = $(".main-content").clone(), $('.searchbox input[type="search"]').keydown(function () {
        var e = $(this);
        setTimeout(function () {
            var t = e.val();
            if (t.length > 2) {
                var o = new RegExp(t, "i"),
                    n = [];
                $(".widget-header h3").each(function () {
                    var e = $(this).text().match(o);
                    "" != e && null != e && n.push($(this).parents(".widget"))
    }), n.length > 0 ? ($(".main-content .widget").hide(), $.each(n, function (e, t) {
                    t.show()
    })) : console.log("widget not found")
    } else $(".main-content .widget").show()
    }, 0)
    }), $(".widget .btn-remove").click(function (e) {
        e.preventDefault(), $(this).parents(".widget").fadeOut(300, function () {
            $(this).remove()
    })
    }), $(".widget .btn-toggle-expand").clickToggle(function (e) {
        e.preventDefault(), $(this).parents(".widget").find(".widget-content").slideUp(300), $(this).find("i.fa-chevron-up").toggleClass("fa-chevron-down")
    }, function (e) {
        e.preventDefault(), $(this).parents(".widget").find(".widget-content").slideDown(300), $(this).find("i.fa-chevron-up").toggleClass("fa-chevron-down")
    }), $(".widget .btn-focus").clickToggle(function (e) {
        e.preventDefault(), $(this).find("i.fa-eye").toggleClass("fa-eye-slash"), $(this).parents(".widget").find(".btn-remove").addClass("link-disabled"), $(this).parents(".widget").addClass("widget-focus-enabled"), $('<div id="focus-overlay"></div>').hide().appendTo("body").fadeIn(300)
    }, function (e) {
        e.preventDefault(), $theWidget = $(this).parents(".widget"), $(this).find("i.fa-eye").toggleClass("fa-eye-slash"), $theWidget.find(".btn-remove").removeClass("link-disabled"), $("body").find("#focus-overlay").fadeOut(function () {
            $(this).remove(), $theWidget.removeClass("widget-focus-enabled")
    })
    }), $(window).bind("resize", e), $("body").tooltip({
        selector: "[data-toggle=tooltip]",
        container: "body"
    }), $(".alert .close").click(function (e) {
        e.preventDefault(), $(this).parents(".alert").fadeOut(300)
    }), $(".btn-help").popover({
        container: "body",
        placement: "top",
        html: !0,
        title: '<i class="fa fa-book"></i> Help',
        content: "Help summary goes here. Options can be passed via data attributes <code>data-</code> or JavaScript. Please check <a href='http://getbootstrap.com/javascript/#popovers'>Bootstrap Doc</a>"
    }), $(".demo-popover1 #popover-title").popover({
        html: !0,
        title: '<i class="fa fa-cogs"></i> Popover Title',
        content: "This popover has title and support HTML content. Quickly implement process-centric networks rather than compelling potentialities. Objectively reinvent competitive technologies after high standards in process improvements. Phosfluorescently cultivate 24/365."
    }), $(".demo-popover1 #popover-hover").popover({
        html: !0,
        title: '<i class="fa fa-cogs"></i> Popover Title',
        trigger: "hover",
        content: "Activate the popover on hover. Objectively enable optimal opportunities without market positioning expertise. Assertively optimize multidisciplinary benefits rather than holistic experiences. Credibly underwhelm real-time paradigms with."
    }), $(".demo-popover2 .btn").popover(), $(".widget-header-toolbar .btn-ajax").click(function (e) {
        e.preventDefault(), $theButton = $(this), $.ajax({
        url: "php/widget-ajax.php",
        type: "POST",
        dataType: "json",
        cache: !1,
        beforeSend: function () {
                $theButton.prop("disabled", !0), $theButton.find("i").removeClass().addClass("fa fa-spinner fa-spin"), $theButton.find("span").text("Loading...")
    },
        success: function (e) {
                setTimeout(function () {
                    t($theButton, e.msg)
    }, 1e3)
    },
        error: function (e, t, o) {
                console.log("AJAX ERROR: \n" + o)
    }
    })
    }), $(".widget-header .multiselect").length > 0 && $(".widget-header .multiselect").multiselect({
        dropRight: !0,
        buttonClass: "btn btn-warning btn-sm"
    }), $(".today-reminder").length > 0) {
        var i, a = 0,
            l = new Audio;
        l.src = navigator.userAgent.match("Firefox/") ? "assets/audio/bell-ringing.ogg" : "assets/audio/bell-ringing.mp3", o()
    }
    $(".bs-switch").length > 0 && $(".bs-switch").bootstrapSwitch(), $(".demo-only-page-blank").length > 0 && $(".content-wrapper").css("min-height", $(".wrapper").outerHeight(!0) - $(".top-bar").outerHeight(!0));
    var s = new Tour({
        basePath: "/dashboard/kingadmin-v1.2/",
        steps: [{
            element: "#tour-searchbox",
            title: 'Bootstrap Tour <span class="badge element-bg-color-blue">New</span>',
            content: "<p>Hello there, this tour can <strong>guide new user</strong> or show new feature/update on your website. Oh and why don't you try search <em>'sales'</em> here to see Widget Live Search on action.</p><p><em>To navigate the site please follow the tour first or click \"End tour\".</em></p>",
            placement: "bottom"
        }, {
            element: "#sales-stat-tab",
            title: "Dynamic Content",
            content: "You can select statistic view based on predefined period here."
        }, {
            element: "#tour-focus",
            title: "Focus Mode",
            content: "Focus your attention and hide all irrelevant contents.",
            placement: "left"
        }, {
            element: ".del-switcher-toggle",
            title: "Color Skins",
            content: "Open the hidden panel here by clicking this icon to apply built-in skins.",
            placement: "left",
            backdrop: !0
        }, {
            element: "#start-tour",
            title: "Start/Restart Tour",
            content: "Click this link later if you need to <strong>restart the tour</strong>.",
            placement: "bottom",
            backdrop: !0
        }],
        template: "<div class='popover tour'> <div class='arrow'></div> <h3 class='popover-title'></h3><div class='popover-content'></div><div class='popover-navigation'><button class='btn btn-default' data-role='prev'>� Prev</button><button class='btn btn-primary' data-role='next'>Next �</button><button class='btn btn-default' data-role='end'>End tour</button></div></div>",
        onEnd: function () {
            $("#start-tour").prop("disabled", !1)
        }
    });
    $(".top-general-alert").length > 0 && null == localStorage.getItem("general-alert") && ($(".top-general-alert").delay(800).slideDown("medium"), $(".top-general-alert .close").click(function () {
        $(this).parent().slideUp("fast"), localStorage.setItem("general-alert", "closed")
    })), s.init(), $("#start-tour").click(function () {
        s.ended() ? s.restart() : s.start(), $(this).prop("disabled", !0)
    }), $btnGlobalvol = $(".btn-global-volume"), $theIcon = $btnGlobalvol.find("i"), n($theIcon, localStorage.getItem("global-volume")), $btnGlobalvol.click(function () {
        var e = localStorage.getItem("global-volume");
        null == e || "1" == e ? localStorage.setItem("global-volume", 0) : localStorage.setItem("global-volume", 1), n($theIcon, localStorage.getItem("global-volume"))
    })

});

$.fn.clickToggle = function (e, t) {
    return this.each(function () {
        var o = !1;
        $(this).bind("click", function () {
            return o ? (o = !1, t.apply(this, arguments)) : (o = !0, e.apply(this, arguments))
        })
    })
}

//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
$(function () {
    $(window).bind("load resize", function () {
        width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
        //console.log(width);
        if (width < 768) {
            $('div.sidebar-collapse').addClass('collapse'), $('div.sidebar-minified').addClass('collapse')
        } else {
            $('div.sidebar-collapse').removeClass('collapse'), $('div.sidebar-minified').removeClass('collapse')
        }
    })
})
