(function ($, w) {
    var masterPage = w.masterPage;
    var shouldSubscribeToEvents = (!masterPage) || (!masterPage.createFlyer.wizardSteps.price);

    var func = function (e, step) {
        var name = "price";

        if (step && step.toLowerCase() != name.toLowerCase()) {
            return;
        }
        
        masterPage = w.masterPage;
        var createFlyer = masterPage.createFlyer;
        var wizardStep = createFlyer.wizardStep;

        var priceConstructor = function () {
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
                        case "Price":
                            rules[name] = {
                                number: true
                            };
                            messages[name] = {
                                number: "Invalid price, please don't use alphabets and special symbols."
                            }
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

                var getDdlRentPeriod = function () {
                    return form.find("[data-clientname='RentPeriod']");
                };

                form.find("input[name='priceRent']").on("change", function () {
                    var input = $(this);
                    var value = input.val().toLowerCase();
                    
                    if (value == "price") {
                        getDdlRentPeriod().hide();
                    }
                    else if (value == "rent") {
                        getDdlRentPeriod().show();
                    }
                });
            };

            self.init = init;
            init();
        };

        var p = masterPage.createFlyer.wizardSteps.priceConstructor;

        if (!p) {
            masterPage.createFlyer.wizardSteps.price = new priceConstructor();
        }
        else {
            p.init();
        }
    };

    if (shouldSubscribeToEvents) {
        $(func);
        $(document).on("setContent", func);
    }
})(jQuery, window);