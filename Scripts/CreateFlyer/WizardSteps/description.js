(function ($, w) {
    var masterPage = w.masterPage;
    var shouldSubscribeToEvents = (!masterPage) || (!masterPage.createFlyer.wizardSteps.description);

    var func = function (e, step) {
        var name = "description";

        if (step && step.toLowerCase() != name.toLowerCase()) {
            return;
        }
        
        masterPage = w.masterPage;
        var createFlyer = masterPage.createFlyer;
        var wizardStep = createFlyer.wizardStep;

        var descriptionConstructor = function () {
            var self = this;
            var init = function () {
                var form = $("#content form[method='post']");
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

                form.find("input,textarea").each(function () {
                    var element = $(this);
                    var clientName = element.data("clientname");
                    var name = element.attr("name");

                    switch (clientName) {
                        case "OpenHouse":
                            rules[name] = {
                                maxlength: 255
                            };
                            break;
                        case "Description":
                            rules[name] = {
                                maxlength: 350
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

                var zillowForm = $("#content form[method='get']");

                zillowForm.find("input[type='submit']").on("click", function (e) {
                    e.preventDefault();

                    var data = {
                        address: "\"" + zillowForm.find("input[name='address']").val() + "\"",
                        city: "\"" + zillowForm.find("input[name='city']").val() + "\"",
                        state: "\"" + zillowForm.find("input[name='state']").val() + "\"",
                        zipCode: "\"" + zillowForm.find("input[name='zipCode']").val() + "\""
                    };

                    for (var p in data) {
                        if (data[p].replace(/"/g, "").trim().length == 0) {
                            alert("Address components are required to proceed. Please, fill in your property location in the step Location.");
                            return;
                        }
                    }

                    $.ajax({
                        url: zillowForm.attr("action"),
                        type: "GET",
                        data: data,
                        cache: false,
                        contentType: "application/json; charset=UTF-8",
                        dataType: "json",
                        success: function (r) {
                            var result = r.d;

                            if (!result) {
                                var addressString = (data.address + " " + data.city + " " + data.state + " " + data.zipCode).replace(/"/g, "");

                                alert("Property information was not found for " + addressString + ". Please check your spelling.");
                            }

                            for (var p in result) {
                                var element = form.find("[data-clientname='" + p + "']");

                                if (element.size() && typeof result[p] == "string" && result[p].length > 0) {
                                    element.val(result[p]);
                                }
                            }
                        },
                        error: masterPage.ajaxError
                    });
                });
            };

            self.init = init;
            init();
        };

        var dc = masterPage.createFlyer.wizardSteps.description;

        if (!dc) {
            masterPage.createFlyer.wizardSteps.description = new descriptionConstructor();
        }
        else {
            dc.init();
        }
    };

    if (shouldSubscribeToEvents) {
        $(func);
        $(document).on("setContent", func);
    }
})(jQuery, window);