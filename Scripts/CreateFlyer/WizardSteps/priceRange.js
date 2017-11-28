(function ($, w) {
    var masterPage = w.masterPage;
    var shouldSubscribeToEvents = (!masterPage) || (!masterPage.createFlyer.wizardSteps.priceRange);

    var func = function (e, step) {
        var name = "priceRange";

        if (step && step.toLowerCase() != name.toLowerCase()) {
            return;
        }
        
        masterPage = w.masterPage;
        var createFlyer = masterPage.createFlyer;
        var wizardStep = createFlyer.wizardStep;

        var priceRangeConstructor = function () {
            var self = this;
            var init = function () {
                var form = $("#content form");             
                var inputPriceMin = form.find("input[data-clientname='PriceMin']");
                var inputPriceMax = form.find("input[data-clientname='PriceMax']");
                var priceMin = parseInt(inputPriceMin.val());
                var priceMax = parseInt(inputPriceMax.val());

                form.find("#slider-range").each(function () {
                    function addSpaces(nStr) {
                        nStr += '';
                        x = nStr.split('.');
                        x1 = x[0];
                        x2 = x.length > 1 ? '.' + x[1] : '';
                        var rgx = /(\d+)(\d{3})/;
                        while (rgx.test(x1)) {
                            x1 = x1.replace(rgx, '$1' + ',' + '$2');
                        }
                        return x1 + x2;
                    }
                    $("#slider-range").slider({
                        range: true,
                        min: 0,
                        max: 1000000,
                        step: 1000,
                        values: [priceMin, priceMax],
                        slide: function (event, ui) {
                            inputPriceMin.val(ui.values[0]);
                            inputPriceMax.val(ui.values[1]);

                            var slidemar = ui.values[1] - ui.values[0];
                            var marg = 0;

                            if (slidemar < 225000) {
                                marg = (42 - (slidemar / 5000));

                                if (marg < 25) {
                                    marg = marg + 2;
                                }
                            }

                            $('.ui-slider-handle:first .ui-slider-tooltip').css({ 'margin-right': marg, 'margin-left': '-' + marg + 'px' }).text('$' + addSpaces(ui.values[0]));
                            $('.ui-slider-handle:last .ui-slider-tooltip').css({ 'margin-left': marg }).text('$' + addSpaces(ui.values[1]));
                        },
                        create: function (event, ui) {
                            var tooltip = $("<div class='ui-slider-tooltip' />").css({
                                position: "absolute",
                                bottom: -48,
                                left: -38
                            });
                            $(event.target).find(".ui-slider-handle").append(tooltip);
                        },
                    });
                    $(".ui-slider-handle:first .ui-slider-tooltip").text("$" + addSpaces(inputPriceMin.val()));
                    $(".ui-slider-handle:last .ui-slider-tooltip").text("$" + addSpaces(inputPriceMax.val()));
                });
            };

            self.init = init;
            init();
        };

        var pr = masterPage.createFlyer.wizardSteps.priceRangeConstructor;

        if (!pr) {
            masterPage.createFlyer.wizardSteps.priceRange = new priceRangeConstructor();
        }
        else {
            pr.init();
        }
    };

    if (shouldSubscribeToEvents) {
        $(func);
        $(document).on("setContent", func);
    }
})(jQuery, window);