(function ($, w) {
    var masterPage = w.masterPage;
    var shouldSubscribeToEvents = (!masterPage) || (!masterPage.createFlyer.wizardSteps.uploadFlyer);

    var func = function (e, step) {
        var name = "uploadFlyer";

        if (step && step.toLowerCase() != name.toLowerCase()) {
            return;
        }
        
        masterPage = w.masterPage;
        var createFlyer = masterPage.createFlyer;
        var wizardStep = createFlyer.wizardStep;

        var uploadFlyerConstructor = function () {
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

                var handleValidationResult = function () {
                    if (!form.validate().checkForm()) {
                        inputNext.attr("disabled", "disabled");
                    }
                    else {
                        inputNext.removeAttr("disabled");
                    }
                };

                form.find("input[data-clientname='DeliveryDate']").removeClass("hasDatepicker");

                form.find("input[data-clientname='DeliveryDate']").datepicker({
                    onSelect: function () {
                        $(this).valid();
                        handleValidationResult();
                    }
                });

                form.find("a.date").off().on("click", function (e) {
                    e.preventDefault();

                    var input = form.find("input[data-clientname='DeliveryDate']");

                    if (input.datepicker("widget").is(":visible")) {
                        input.datepicker("hide");
                    }
                    else {
                        input.datepicker("show");
                    }
                });

                form.find(".upload input").off().on("change", function () {
                    var fileName = $(this).val().replace(/^.*(\\|\/|\:)/, '');

                    $(this).parent(".upload").find(".file-text").text(fileName);
                    $(this).trigger("keyup");
                });

                form.find("input").each(function () {
                    var element = $(this);
                    var clientName = element.data("clientname") ? element.data("clientname") : element.attr("name");
                    var name = element.attr("name");

                    switch (clientName) {
                        case "File":
                            if (element.attr("value") && element.attr("value").length == 0) {
                                rules[name] = {
                                    required: true,
                                    extension: "jpe?g|png|gif|tif"
                                };
                                messages[name] = {
                                    required: "File is required",
                                    extension: "Allowed file extensions are jpg (jpeg), png, gif or tif."
                                };
                            }
                            break;
                        case "DeliveryDate":
                            rules[name] = {
                                required: true,
                                date: true,
                                dateCurrentOrFuture: true
                            };
                            messages[name] = {
                                required: "Delivery date is required",
                                date: "Please enter a valid current or future date.",
                                dateCurrentOrFuture: "Please enter a valid current or future date."
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
                        case "Link":
                            rules[name] = {
                                maxlength: 300
                            };
                            messages[name] = {
                                maxlength: "Link (e-mail or ULR) max length is 300 characters."
                            };
                            break;
                    }
                }).on("keyup", handleValidationResult);

                masterPage.assignValidation(validationOptions);

                $("a.preview").off().on("click", function (e) {
                    if (!form.valid()) {
                        e.preventDefault();
                        masterPage.setPopupControlHeaderHtml("genericprogress", "Please provide valid values to all required fields to see full preview.");
                        masterPage.setPopupControlIcon("genericprogress", "");
                        masterPage.handleClickOnPopupControls(null, "genericprogress");
                    }
                    else {
                        masterPage.createFlyer.flyerMenu.previewHandler(e);
                    }
                });
            };

            self.init = init;
            init();
        };

        var uf = masterPage.createFlyer.wizardSteps.uploadFlyer;

        if (!uf) {
            masterPage.createFlyer.wizardSteps.uploadFlyer = new uploadFlyerConstructor();
        }
        else {
            uf.init();
        }
    };

    if (shouldSubscribeToEvents) {
        $(func);
        $(document).on("setContent", func);
    }
})(jQuery, window);