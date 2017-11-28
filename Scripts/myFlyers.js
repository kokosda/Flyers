(function ($, w) {
    $(function () {
        var divAccountFlyers = $(".account-flyers:first");

        divAccountFlyers.find("[id*='hlViewThisFlyer'],[id*='hlTitle']").on("click", function (e) {
            if ($(this).attr("href") && $(this).attr("href").length > 0) {
                masterPage.showFlyerPreview(e, $(this).attr("href"));
            }
        });
        divAccountFlyers.find("[id*='lbCreateCopy']").on("click", function (e) {
            if (!$(this).hasClass("gray")) {
                if (!confirm("Are you sure, you want to create the copy of this flyer?")) {
                    e.preventDefault();
                }
            }
        });
        divAccountFlyers.find("[id*='lbDeleteThisFlyer']").on("click", function (e) {
            if (!$(this).hasClass("gray")) {
                if (!confirm("Are you sure you want to delete this flyer?")) {
                    e.preventDefault();
                }
            }
        });
        divAccountFlyers.find("[id*='lbEditAndResend']").on("click", function (e) {
            if (!$(this).hasClass("gray")) {
                $(this).addClass("gray");
                setTimeout(function (el) {
                    el.removeClass("gray");
                }, 500, $(this));
            }
            else {
                e.preventDefault();
            }
        });
	});
})(jQuery, window);