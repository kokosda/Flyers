(function ($, w) {
    var masterPage = w.masterPage;
    var shouldSubscribeToEvents = (!masterPage) || (!masterPage.createFlyer.wizardSteps.flyerTitle);

    var func = function (e, step) {
        var name = "flyerTitle";

        if (step && step.toLowerCase() != name.toLowerCase()) {
            return;
        }
        
        masterPage = w.masterPage;
        var createFlyer = masterPage.createFlyer;
        var wizardStep = createFlyer.wizardStep;

        var flyerTitleConstructor = function () {
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
                        case "FlyerTitle":
                            rules[name] = {
                                required: true
                            };
                            messages[name] = {
                                required: "Flyer title is required"
                            }
                            break;
                        case "EmailSubject":
                            rules[name] = {
                                required: true
                            };
                            messages[name] = {
                                required: "Email subject is required."
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

        var ft = masterPage.createFlyer.wizardSteps.flyerTitle;

        if (!ft) {
            masterPage.createFlyer.wizardSteps.flyerTitle = new flyerTitleConstructor();
        }
        else {
            ft.init();
        }
    };

    if (shouldSubscribeToEvents) {
        $(func);
        $(document).on("setContent", func);
    }
})(jQuery, window);