(function ($, w) {
	$(function () {
		var masterPage = w.masterPage;
		var form = $(".content.form form")
		var validationOptions = masterPage.getLoginValidationOptions(form);

		masterPage.assignValidation(validationOptions);
	});
})(jQuery, window);