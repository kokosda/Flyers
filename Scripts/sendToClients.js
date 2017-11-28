(function ($, w) {
    $(function () {
        var form = $("#content form");
        var rules = {};
        var messages = {};

        var validationOptions = {
            form: form,
            rules: rules,
            messages: messages,
            keepInputsDisabledAfterSubmit: true,
            submitHandler: function (form) {
                masterPage.setPopupControlHeaderHtml("genericprogress", "Operation is in progress. Please wait for completion.");
                masterPage.handleClickOnPopupControls(null, "genericprogress");

                var inputSubmit = $(form).find("input[type='submit'][data-serverbutton='true']");

                __doPostBack(inputSubmit.attr("name"), "");
            }
        };

        form.find("input,textarea").each(function () {
            var element = $(this);
            var name = element.attr("name");
            var clientName = element.data("clientname");

            if (typeof clientName == "string") {
                switch (clientName) {
                    case "EmailSubject":
                        rules[name] = {
                            required: true
                        };
                        messages[name] = {
                            required: "Email Subject is required.",
                        };
                        break;
                    case "EmailAddresses":
                        rules[name] = {
                            required: true
                        };
                        messages[name] = {
                            required: "Email Addresses are required."
                        };
                        break;
                }
            }
        });

        masterPage.assignValidation(validationOptions);
	});
})(jQuery, window);