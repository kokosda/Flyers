(function ($, w) {
    $(function () {
        var form = $(".account form");
        var tableTransactionHistory = form.find(".history");
        var tablePurchaseSummary = form.find(".sumary");
        var divTotal = tablePurchaseSummary.parent("fieldset").find(".total:first");
        var divCoupon = tablePurchaseSummary.parent("fieldset").find(".coupon");
        var subTotal = 0;
        var total = 0;
        var taxCost = 0;
        var discount = 0;
        var discountRate = 0;
        var discountPercentage = false;
        var totalStrong = divTotal.find(".total strong");
        var inputTotalPrice = divTotal.find("input[name='TotalPrice']");
        var subTotalStrong = divTotal.find("[id$='divSubTotal'] strong");
        var taxRate = parseFloat(divTotal.find("[id$='divTaxRate'] label").text().replace("%", ""));
        var strongTaxCost = divTotal.find("[id$='divTaxRate'] strong");
        var strongDiscount = divTotal.find("[id$='divDiscount'] strong");
        var divDiscount = divTotal.find("[id$='divDiscount']");
        var labelCouponMessage = divCoupon.find("[id$='labelCouponMessage']");
        var inputPromoCodeIsApplied = form.find("input[name$='PromoCodeIsApplied']");
        var setTotals = function () {
            subTotal = 0;

            var itemsCount = 0;

            tablePurchaseSummary.find("tbody tr td:nth-child(3)").each(function () {
                subTotal += parseFloat($(this).text().replace("$", ""));
                itemsCount++;
            });

            if (discountRate) {
                if (discountPercentage) {
                    discount = subTotal * discountRate / 100;

                    if (subTotal - discount < 0) {
                        discount = subTotal;
                    }
                }
                else {
                    discount = subTotal - discountRate;

                    if (discount < 0) {
                        discount = subTotal;
                    }
                }
            }
            else {
                discount = 0;
            }

            subTotal -= discount;
            taxCost = subTotal * taxRate / 100;
            total = subTotal + taxCost;

            var checkTotalDiff = parseFloat(total.toFixed(2)) - (parseFloat(subTotal.toFixed(2)) + parseFloat(taxCost.toFixed(2)));
            var tempTotalDiff = parseFloat(checkTotalDiff.toFixed(2));

            if (tempTotalDiff > 0) {
                total -= 0.01;
            }
            else if (tempTotalDiff < 0) {
                total += 0.01;
            }

            strongTaxCost.text("$" + taxCost.toFixed(2));
            strongDiscount.text("$" + discount.toFixed(2));
            subTotalStrong.text("$" + subTotal.toFixed(2));
            totalStrong.text("$" + total.toFixed(2));
            inputTotalPrice.val(total.toFixed(2));

            updatePaymentButton(itemsCount, inputTotalPrice.val());
        };
        var getOrdersCount = function () {
            return tablePurchaseSummary.find("tbody tr").size();
        }
        var getInputOrderIds = function () {
            return tableTransactionHistory.parent().find("input[name='OrderIds']");
        };

        tableTransactionHistory.find("tbody input[type='checkbox'][id$='checkbox']").on("change", function () {
            var checkbox = $(this);
            var tr = checkbox.closest("tr");
            var orderId = parseInt(tr.find("td:nth-child(2)").text(), 10);
            var trPs = tablePurchaseSummary.find("tbody tr");
            var completed = false;

            if (checkbox.is(":checked")) {
                var html = checkbox.closest("tr")[0].outerHTML;

                if (trPs.size()) {
                    trPs.each(function (i) {
                        if (!completed) {
                            if (orderId > parseInt($(this).find("td:nth-child(1)").text())) {
                                $(this).before(html);
                                $(this).prev().find("td:nth-child(1)").remove();
                                completed = true;
                            }
                            else if (tr.size() - 1 == i) {
                                $(this).after(html);
                                $(this).next().find("td:nth-child(1)").remove();
                                completed = true;
                            }
                        }
                    });
                }
                else {
                    tablePurchaseSummary.find("tbody").append(html);
                    tablePurchaseSummary.find("tbody tr td:nth-child(1)").remove();
                }
            }
            else {
                trPs.each(function () {
                    if (orderId == parseInt($(this).find("td:nth-child(1)").text())) {
                        $(this).remove();
                    }
                });
            }

            if (discountRate > 0) {
                labelCouponMessage.show();
                labelCouponMessage.text("Please reapply coupon number or promotional claim code.");
            }

            discountRate = 0;
            divDiscount.hide();
            inputPromoCodeIsApplied.val("false");

            if (!getOrdersCount()) {
                getInputOrderIds().val("");
            }
            else {
                var orderIds = "";

                tablePurchaseSummary.find("tbody tr td:nth-child(1)").each(function () {
                    orderIds += $(this).text() + ",";
                });

                orderIds = orderIds.substr(0, orderIds.length - 1);
                getInputOrderIds().val(orderIds);
            }

            getInputOrderIds().trigger("keyup");

            setTotals();
        });

	    form.find("a[href='#fulltransactionhistory']").on("click", function (e) {
	        e.preventDefault();

	        var table = $(this).parent("fieldset").find("table tr").show();

	        $(this).hide();
	    });

	    divCoupon.find("input[type='submit']").on("click", function (e) {
	        e.preventDefault();

	        var promoCode = divCoupon.find("input[type='text']").val();

	        if (promoCode.length == 0) {
	            labelCouponMessage.show();
	            labelCouponMessage.text("Please enter a coupon number or promotion code.");
	            return;
	        }

	        var ordersCount = getOrdersCount();

	        if (ordersCount == 0)
	        {
	            labelCouponMessage.show();
	            labelCouponMessage.text("Please select some flyers.")
	            return;
	        }

	        var data = {
	            promoCode: "\"" + promoCode + "\"",
                ordersCount: "\"" + ordersCount + "\""
	        };

	        $.ajax({
	            url: $(this).data("getdiscountrateurl"),
	            type: "GET",
	            cache: false,
                data: data,
	            contentType: "application/json; charset=UTF-8",
	            dataType: "json",
	            success: function (r) {
	                var model = r.d;

	                if (model.DiscountRate) {
	                    discountRate = model.DiscountRate;
	                    discountPercentage = model.Percentage;
	                    labelCouponMessage.hide();
	                    divDiscount.show();
	                    inputPromoCodeIsApplied.val("true");
	                    setTotals();
	                }
	                else if (typeof model.Message == "string") {
	                    labelCouponMessage.show();
	                    labelCouponMessage.text(model.Message);
	                    divDiscount.hide();
	                    inputPromoCodeIsApplied.val("false");
	                }
	            },
	            error: masterPage.ajaxError
	        });
	    });

	    divCoupon.find("input[type='text']").on("keyup", function () {
	        if ($(this).val().length == 0) {
	            labelCouponMessage.text("");
	            labelCouponMessage.hide();
	        }
	    });

	    var initFacebook = function () {
	        var d = document;
	        var s = "script";
	        var id = "facebook-jssdk";

	        var js, fjs = d.getElementsByTagName(s)[0];
	        if (d.getElementById(id)) return;
	        js = d.createElement(s); js.id = id;
	        js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
	        fjs.parentNode.insertBefore(js, fjs);
	    };

	    var submitHandler = function () {
	        masterPage.setPopupControlHeaderHtml("genericprogress", "Processing your payment.");
	        masterPage.handleClickOnPopupControls(null, "genericprogress");
	        form.off("submit");
	        form.submit();
	    };

	    var validationOptions = {
	        form: form,
	        rules: {},
	        messages: {},
	        submitHandler: submitHandler,
	        keepInputsDisabledAfterSubmit: true,
	        validateInvisibleInputs: true
	    };
	    var rules = validationOptions.rules;
	    var messages = validationOptions.messages;

	    form.find("input,select").each(function () {
	        var element = $(this);
	        var clientName = element.data("clientname") ? element.data("clientname") : element.attr("name");
	        var name = element.attr("name");

	        switch (clientName) {
	            case "OrderIds":
	                rules[name] = {
	                    required: true
	                };
	                messages[name] = {
	                    required: "Please select some flyers."
	                };
	                break;
	        }
	    }).on("keyup", function () {
	        getInputOrderIds().valid();
	    });

	    var updatePaymentButton = function (itemsCount, payAmount) {
	        form.find("#stripeCom").remove();
	        form.find("button.stripe-button-el").remove();

	        var inputMakePayment = form.find("#inputMakePayment");	        
	        var cents = typeof payAmount == "string" ? parseFloat(payAmount) * 100 : 0;

	        if (itemsCount > 0) {
	            inputMakePayment.show();
	            inputMakePayment.prop("disabled", true);
	            inputMakePayment.val("Wait");
	            inputMakePayment.addClass("load");
                
	            var a = form.serializeArray();
	            var data = { d: a };

	            $.ajax({
	                url: form.data("canpayurl"),
	                type: "POST",
	                cache: false,
                    data: JSON.stringify(data),
	                contentType: "application/json; charset=UTF-8",
	                dataType: "json",
	                success: function (r) {
	                    var model = r.d;

	                    if (model.Result === true) {
	                        if (cents > 0) {
	                            var script = "<script id='stripeCom' type='text/javascript'";
	                            var options = w.stripeCom;

	                            options['data-description'] = itemsCount == 1 ? '1 flyer' : itemsCount + " flyers";
	                            options['data-amount'] = cents;

	                            script += " src='" + options.src + "'";
	                            script += " class='" + options.cssClass + "'";

	                            for (var p in options) {
	                                if (p.indexOf('data') == 0) {
	                                    if (options[p] !== null) {
	                                        script += " " + p + "='" + options[p] + "' ";
	                                    }
	                                }
	                            }

	                            script += "></script>";

	                            form.append(script);
	                            inputMakePayment.hide();
	                            form.on("submit", function () {
	                                masterPage.setPopupControlHeaderHtml("genericprogress", "Processing your payment.");
	                                masterPage.handleClickOnPopupControls(null, "genericprogress");
	                            });
	                        }
	                        else if (cents == 0) {
	                            inputMakePayment.val("Free of Charge!");
	                            form.find("#inputMakePayment").show();
	                            form.off("submit");

	                            inputMakePayment.off("click").on("click", function (e) {
	                                e.preventDefault();
	                                masterPage.setPopupControlHeaderHtml("genericprogress", "Processing your payment.");
	                                masterPage.handleClickOnPopupControls(null, "genericprogress");
	                                form.submit();
	                            });
	                        }
	                    }
	                    else if (model.Message.length > 0) {
	                        alert(model.Message);
	                    }
	                },
	                error: masterPage.ajaxError,
	                complete: function () {
	                    inputMakePayment.prop("disabled", false);
	                    inputMakePayment.removeClass("load");
	                }
	            });
	        }
	        else {
	            inputMakePayment.hide();
	        }
	    };

	    masterPage.assignValidation(validationOptions);

	    if (tableTransactionHistory.find("tbody input[type='checkbox'][id$='checkbox']").size() == 1) {
	        tableTransactionHistory.find("tbody input[type='checkbox'][id$='checkbox']:first").prop("checked", true);
	    }

	    if (tableTransactionHistory.find("tbody input[type='checkbox'][id$='checkbox']:checked:first").size() > 0) {
	        tableTransactionHistory.find("tbody input[type='checkbox'][id$='checkbox']:checked:first").trigger("change");
	    }
	    else {
	        tableTransactionHistory.find("tbody input[type='checkbox'][id$='checkbox']:first").trigger("change");
	    }

	    var thanks = masterPage.getQueryValue(w.location.href, "thanks");

	    if (typeof thanks == "string") {
	        initFacebook();
	        masterPage.handleClickOnPopupControls(null, "thanks");
	    }

	    setTimeout(function () {
	        $(".message").slideUp("slow");
	    }, 5000);
	});
})(jQuery, window);