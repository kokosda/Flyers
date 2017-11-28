(function ($, w) {
	$(function () {
		var masterPage = w.masterPage;
		var form = $(".content.form form")
		var validationOptions = masterPage.getRecoverPasswordValidationOptions(form);

		masterPage.assignValidation(validationOptions);
        /*
		setTimeout(function (el) {
		    if (el.size()) {
		        if (el.parent().find(".error").size() == 1) {
		            el.parent().find("input[type='email']").removeClass("error");
		        }

		        el.remove();
		    }
		}, 5000, form.find("input[type='email']").parent().find("label.error"));*/
	});
})(jQuery, window);