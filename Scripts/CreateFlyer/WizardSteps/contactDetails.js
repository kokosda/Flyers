(function ($, w) {
    var masterPage = w.masterPage;
    var shouldSubscribeToEvents = (!masterPage) || (!masterPage.createFlyer.wizardSteps.contactDetails);

    var func = function (e, step) {
        var name = "contactDetails";

        if (step && step.toLowerCase() != name.toLowerCase()) {
            return;
        }
        
        masterPage = w.masterPage;
        var createFlyer = masterPage.createFlyer;
        var wizardStep = createFlyer.wizardStep;

        var contactDetailsConstructor = function () {
            var self = this;
            var init = function () {
                var form = $("#content form");
                var validationOptions = {
                    form: form,
                    rules: {},
                    messages: {},
                    submitHandler: wizardStep.submitHandler,
                    keepInputsDisabledAfterSubmit: true
                };
                var rules = validationOptions.rules;
                var messages = validationOptions.messages;
                var inputNext = form.find("input[type='submit']");

                form.find("input").each(function () {
                    var element = $(this);
                    var clientName = element.data("clientname");
                    var name = element.attr("name");

                    switch (clientName) {
                        case "Name":
                            rules[name] = {
                                required: true
                            };
                            messages[name] = {
                                required: "Name is required"
                            }
                            break;
                        case "Phone":
                            rules[name] = {
                                required: true,
                                pattern: /((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}/
                            };
                            messages[name] = {
                                required: "Phone is required.",
                                pattern: "Invalid Phone. Use ###-###-#### format."
                            };
                            element.mask("999-999-9999", { autoclear: false });
                            break;
                        case "Email":
                            rules[name] = {
                                required: true,
                                email: true,
                                pattern: /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/
                            };
                            messages[name] = {
                                required: "Email is required.",
                                email: "Email is not valid.",
                                pattern: "Email is not valid."
                            };
                            break;
                    }
                }).on("keyup", function () {
                    if (!form.validate().checkForm()) {
                        inputNext.attr("disabled", "disabled");
                    }
                    else {
                        inputNext.removeAttr("disabled");
                    }
                });

                masterPage.assignValidation(validationOptions);
            };

            self.init = init;
            init();
        };

        var cd = masterPage.createFlyer.wizardSteps.contactDetails;

        if (!cd) {
            masterPage.createFlyer.wizardSteps.contactDetails = new contactDetailsConstructor();
        }
        else {
            cd.init();
        }
    };

    if (shouldSubscribeToEvents) {
        $(func);
        $(document).on("setContent", func);
    }
})(jQuery, window);