(function ($, w) {
	$(function () {
	    var getForm = function () {
	        return $("#content form");
	    };

	    getForm().find("input[data-date]").removeClass("hasDatepicker");
	    getForm().find("input[data-date]").datepicker();
	    getForm().find("a.date").off().on("click", function (e) {
	        e.preventDefault();

	        var input = $(this).parent().find("input[data-date]");

	        if (input.datepicker("widget").is(":visible")) {
	            input.datepicker("hide");
	        }
	        else {
	            input.datepicker("show");
	        }
	    });
	    getForm().find(".clear_date").off().on("click", function (e) {
	        e.preventDefault();

	        $(this).parent().find("input[data-date]").datepicker("setDate", null);
	    });
	});
})(jQuery, window);