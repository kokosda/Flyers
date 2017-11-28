(function ($, w) {
    var masterPage = w.masterPage;
    var shouldSubscribeToEvents = (!masterPage) || (!masterPage.createFlyer.wizardSteps.selectMarketArea);

    var func = function (e, step) {
        var name = "selectMarketArea";

        if (step && step.toLowerCase() != name.toLowerCase()) {
            return;
        }
        
        masterPage = w.masterPage;

        var createFlyer = masterPage.createFlyer;
        var wizardStep = createFlyer.wizardStep;
        var history = masterPage.history;

        var selectMarketAreaConstructor = function () {
            var self = this;
            var init = function () {
                var getForm = function () {
                    return $("#content form");
                };
                var inputSubmit = getForm().find("input[type='submit']");
                var validate = function () {
                    return getForm().find("#hiddenPrices").val().length > 0
                };
                var handleValidationResult = function () {
                    if (!(getForm().validate().checkForm() && validate())) {
                        inputSubmit.attr("disabled", "disabled");
                    }
                    else {
                        inputSubmit.removeAttr("disabled");
                    }
                };

                getForm().find("input[data-clientname='DeliveryDate']").removeClass("hasDatepicker");

                getForm().find("input[data-clientname='DeliveryDate']").datepicker({
                    onSelect: function () {
                        $(this).valid();
                        handleValidationResult();
                    }
                });

                getForm().find("a.date").off().on("click", function (e) {
                    e.preventDefault();

                    var input = getForm().find("input[data-clientname='DeliveryDate']");

                    if (input.datepicker("widget").is(":visible")) {
                        input.datepicker("hide");
                    }
                    else {
                        input.datepicker("show");
                    }
                });

                var assignValidation = function () {
                    var validationOptions = {
                        form: getForm(),
                        rules: {},
                        messages: {},
                        submitHandler: function (form, success, error) {
                            masterPage.setPopupControlHeaderHtml("genericprogress", "Saving your flyer.");
                            masterPage.handleClickOnPopupControls(null, "genericprogress");

                            $("input,select").each(function () {
                                if (typeof $(this).data("clientname") == "string") {
                                    $(this).attr("name", $(this).data("clientname"));
                                }
                            });

                            var data = $(form).serializeArray();

                            data.push({ name: "backgroundCheck", value: true });
                            $.ajax({
                                url: $(form).attr("action"),
                                type: $(form).attr("method"),
                                data: data,
                                success: success || function (r) {
                                    if (r.Result) {
                                        masterPage.setPopupControlHeaderHtml("genericprogress", "Flyer saved. Preparing your cart.");
                                        form.submit();
                                    }
                                    else if (typeof r.Message == "string") {
                                        masterPage.hidePopupControl();
                                        alert(r.Message);
                                    }
                                },
                                error: error || function (r) {
                                    masterPage.hidePopupControl();
                                    masterPage.ajaxError(r);
                                },
                            });
                        },
                        keepInputsDisabledAfterSubmit: true
                    };

                    wizardStep.submitHandler = validationOptions.submitHandler;

                    var rules = validationOptions.rules;
                    var messages = validationOptions.messages;

                    getForm().find("input").each(function () {
                        var element = $(this);
                        var clientName = element.data("clientname");
                        var name = element.attr("name");

                        switch (clientName) {
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
                        }
                    }).on("keyup", handleValidationResult);

                    masterPage.assignValidation(validationOptions);

                    getForm().valid();
                };

                assignValidation();
                handleValidationResult();

                var getPageNumber = function () {
                    return parseInt(getForm().find("#hiddenPageNumber").val());
                };

                var handleFilterChange = function (e) {
                    var query = "";

                    if (e.data && e.data["query"]) {
                        query = e.data["query"];
                    }
                    else {
                        var element = $(this);

                        query = w.location.pathname + "?step=" + name.toLowerCase() + "&";

                        if (element.attr("id") != "hiddenPrices") {
                            getForm().find("#hiddenPrices").val("");
                        }
                        else {
                            if (query.indexOf("page=") < 0) {
                                if (getPageNumber() > 1) {
                                    query += "page=" + getPageNumber().toString() + "&";
                                }
                            }
                        }

                        getForm().find("select,input[type='radio']:checked,input[type='hidden'].data").each(function () {
                            var val = $(this).val();
                            var name = $(this).data("clientname") ? $(this).data("clientname") : $(this).attr("name");

                            if (typeof val == "string" && val.length > 0) {
                                query += name + "=" + val + "&";
                            }
                        });

                        if (query.lastIndexOf("&") == query.length - 1) {
                            query = query.substr(0, query.length - 1);
                        }
                    }

                    $.ajax({
                        url: query,
                        type: "GET",
                        cache: false,
                        success: function (r) {
                            var html = $("#content form .flyers-content .price", r);

                            getForm().find(".flyers-content .price").replaceWith(html);
                            html = $("#content form .create.price", r);
                            getForm().find(".create.price").replaceWith(html);
                            html = $("#content form .radioset", r);
                            getForm().find(".radioset").replaceWith(html);
                            html = $("#content form select[data-clientname='state']", r);
                            getForm().find("select[data-clientname='state']").replaceWith(html);
                            self.init();
                            new history.pushState(query, self.init);
                        },
                        error: masterPage.ajaxError
                    });
                };

                var handlePricesChange = function () {
                    var id = $(this).attr("id");
                    var index = parseInt(id.split("_")[1], 10);
                    var pageSize = parseInt(getForm().find("#hiddenPageSize").val());
                    var absoluteIndex = (getPageNumber() - 1) * pageSize + index;
                    var prices = getForm().find("#hiddenPrices").val();

                    if (typeof prices == "string") {
                        var splittedPrices = (prices.length > 0 ? prices.split(",") : []);
                        splittedPrices.forEach(function (item, i) {
                            splittedPrices[i] = parseInt(item);
                        });

                        if ($(this).is(":checked")) {
                            splittedPrices.push(absoluteIndex);
                            splittedPrices = splittedPrices.sort(function (a, b) { return a - b; });
                        }
                        else {
                            var ioAbsoluteIndex = splittedPrices.indexOf(absoluteIndex);
                            if (ioAbsoluteIndex >= 0) {
                                splittedPrices.splice(ioAbsoluteIndex, 1);
                            }
                        }

                        prices = splittedPrices.join();
                    }

                    getForm().find("#hiddenPrices").val(prices);
                    getForm().find("#hiddenPrices").trigger("change");
                };

                var handlePageClick = function (e) {
                    e.preventDefault();
                    e.data = { query: $(e.target).attr("href") };
                    handleFilterChange(e);
                };

                var initHandlers = function () {
                    getForm().find("select,input[type='radio'],input[type='hidden'].data").off().on("change", handleFilterChange);
                    getForm().find(".price .tables table:first input[type='checkbox']").off().on("change", handlePricesChange);
                    getForm().find(".pager li a").off().on("click", handlePageClick);
                };

                initHandlers();
            };

            self.init = init;
            init();
        };

        var sma = masterPage.createFlyer.wizardSteps.selectMarketArea;

        if (!sma) {
            masterPage.createFlyer.wizardSteps.selectMarketArea = new selectMarketAreaConstructor();
        }
        else {
            sma.init();
        }
    };

    if (shouldSubscribeToEvents) {
        $(func);
        $(document).on("setContent", func);
    }
})(jQuery, window);