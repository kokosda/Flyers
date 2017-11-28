(function ($) {
    $(function () {
        $("#content a[data-clientname='Preview']").on("click", function (e) {
            masterPage.showFlyerPreview(e, $(this).attr("href"));
        });
	});
})(jQuery);