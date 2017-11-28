(function ($, w) {
    var masterPage = w.masterPage;
    var shouldSubscribeToEvents = (!masterPage) || (!masterPage.createFlyer.wizardSteps.location);

    var func = function (e, step) {
        var name = "location";

        if (step && step.toLowerCase() != name.toLowerCase()) {
            return;
        }
        
        masterPage = w.masterPage;
        var createFlyer = masterPage.createFlyer;
        var wizardStep = createFlyer.wizardStep;

        var locationConstructor = function () {
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
                        case "ZipCode":
                            rules[name] = {
                                zipcodeUS: true
                            };
                            messages[name] = {
                                zipcodeUS: "Please enter valid zip code!"
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
                
                var getMapElement = function () {
                    return form.find("#map")[0];
                };

                var getMapLinkInput = function () {
                    return form.find("input[data-clientname='MapLink']");
                };

                var getMapControls = function () {
                    return form.find("hr, .item.small-bs.chekbox, #map");
                };

                var getStateInput = function () {
                    return form.find("select[data-clientname='State']");
                };

                var getCityInput = function () {
                    return form.find("input[data-clientname='City']");
                };

                var getZipCodeInput = function () {
                    return form.find("input[data-clientname='ZipCode']");
                };

                var getAddress = function () {
                    var result = "";
                    var canGetAddress = form.find("input[data-clientname='StreetAddress']").val().trim().length > 0;

                    if (canGetAddress) {
                        getAddressInputs().each(function () {
                            if ($(this).data("clientname") == "State") {
                                result += $(this).find(":selected").text().trim() + " ";
                            }
                            else {
                                if ($(this).val().length) {
                                    result += $(this).val() + " ";
                                }
                            }
                        });
                        result = result.trim();
                    }

                    return result;
                };

                var getAddressInputs = function () {
                    var result = form.find("input[data-clientname='StreetAddress'],input[data-clientname='City'],select[data-clientname='State'],input[data-clientname='ZipCode']");

                    return result;
                };

                getAddressInputs().on("change", function () {
                    var element = $(this);

                    if (element.data("clientname") == "State") {
                        if (canOverrideUserInput()) {
                            getCityInput().val("");
                            getZipCodeInput().val("");
                        }
                    }

                    setMap();
                });

                var getAddMapInput = function () {
                    return form.find("input[data-clientname='AddMap']");
                };

                var canOverrideUserInput = function () {
                    return getAddMapInput().parent().is(":visible") && getAddMapInput().is(":checked");
                };

                getAddMapInput().on("change", function () {
                    var isChecked = $(this).is(":checked");

                    if (!isChecked) {
                        getMapLinkInput().val("");
                        $(getMapElement()).hide();
                    }
                    else {
                        $(getMapElement()).show();
                        setMap();
                    }
                });

                var setMap = function (preferMapLink) {
                    var mapLink = getMapLinkInput().val();
                    var gmParams = masterPage.parseGoogleMapLink(preferMapLink ? mapLink : null);
                    var address = getAddress();
                    var updateInputs = function (geodata, buildMapLink) {
                        var gmParams = geodata ? geodata.gmParams : undefined;

                        if (gmParams) {
                            var ml = mapLink;

                            if (buildMapLink) {
                                ml = masterPage.buildGoogleMapLink(gmParams);
                            }

                            getMapLinkInput().val(ml || "");
                        }
                        else {
                            getMapLinkInput().val("");
                        }

                        if (getMapLinkInput().val().length) {
                            $(getMapControls()[0]).show();
                            getAddMapInput().parent().show();

                            var canShowMapElement = getAddMapInput().is(":checked");

                            if (canShowMapElement) {
                                $(getMapElement()).show();
                            }
                        }
                        else {
                            getMapControls().hide();
                        }

                        if (geodata && canOverrideUserInput()) {
                            if (geodata.state) {
                                var option = getStateInput().find(":contains(" + geodata.state + ")");

                                if (option.size()) {
                                    getStateInput().val(option.val());
                                }
                            }
                            if (geodata.city) {
                                getCityInput().val(geodata.city);
                            }
                            if (geodata.zipCode) {
                                getZipCodeInput().val(geodata.zipCode);
                            }
                        }
                    };

                    masterPage.initGoogleMap(getMapElement());

                    if (!gmParams) {
                        var options = {
                            address: address,
                            callback: function (result) {
                                updateInputs(result, true);
                                masterPage.setGoogleMap(result ? result.gmParams : undefined, getAddress());
                            }
                        };

                        masterPage.googleGeocodeAddress(options);
                    }
                    else {
                        updateInputs({ gmParams: gmParams });
                        masterPage.setGoogleMap(gmParams, address);
                    }
                };

                setMap(true);
            };

            self.init = init;
            init();
        };

        var l = masterPage.createFlyer.wizardSteps.location;

        if (!l) {
            masterPage.createFlyer.wizardSteps.location = new locationConstructor();
        }
        else {
            l.init();
        }
    };

    if (shouldSubscribeToEvents) {
        $(func);
        $(document).on("setContent", func);
    }
})(jQuery, window);