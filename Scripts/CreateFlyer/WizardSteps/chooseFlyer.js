(function ($, w) {
    var masterPage = w.masterPage;
    var shouldSubscribeToEvents = (!masterPage) || (!masterPage.createFlyer.wizardSteps.chooseFlyer);

    var func = function (e, step) {
        var name = "chooseFlyer";

        if (step && step.toLowerCase() != name.toLowerCase()) {
            return;
        }
        
        masterPage = w.masterPage;
        var createFlyer = masterPage.createFlyer;
        var wizardStep = createFlyer.wizardStep;

        var chooseFlyerConstructor = function () {
            var self = this;
            var init = function () {
                var form = $("#content form");
                
                form.find(".block .buttons a").on("click", function (e) {
                    var element = $(this);

                    if (element.data("clientname") == "SelectMe") {
                        e.preventDefault();
                        form.find("input[data-clientname='Layout']").val(element.data("value"));
                        form.find(".block").removeClass("selected");
                        element.parents(".block").addClass("selected");
                    }
                    else if (element.data("clientname") == "Preview") {
                        masterPage.showFlyerPreview(e, element.attr("href"));
                    }
                });
            };

            self.init = init;
            init();
        };

        var cf = masterPage.createFlyer.wizardSteps.chooseFlyer;

        if (!cf) {
            masterPage.createFlyer.wizardSteps.chooseFlyer = new chooseFlyerConstructor();
        }
        else {
            cf.init();
        }
    };

    if (shouldSubscribeToEvents) {
        $(func);
        $(document).on("setContent", func);
    }
})(jQuery, window);