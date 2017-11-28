(function ($, w) {
	$(function () {
	    var getForm = function () {
	        return $("#content form");
	    };

	    getForm().find("a.delete").on("click", function (e) {
	        if (confirm("Are you sure you want to delete this item?")) {
	            return true;
	        }
	        else {
	            e.preventDefault();
	            return false;
	        }
	    });
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

	    if ($("#editor").size()) {
	        var editor = ace.edit("editor");

	        editor.setTheme("ace/theme/crimson_editor");
	        editor.getSession().setMode("ace/mode/html");
	        editor.getSession().setValue($("textarea[name='markup']").val());
	        editor.getSession().on("change", function () {
	            $("textarea[name='markup']").val(editor.getSession().getValue());
	        });
	        editor.renderer.setShowGutter(false);
	        editor.setShowPrintMargin(false);
	    }
	});
})(jQuery, window);