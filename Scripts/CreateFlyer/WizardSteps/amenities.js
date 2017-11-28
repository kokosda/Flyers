(function ($, w) {
    var masterPage = w.masterPage;
    var shouldSubscribeToEvents = (!masterPage) || (!masterPage.createFlyer.wizardSteps.amenities);

    var func = function (e, step) {
        var name = "amenities";

        if (step && step.toLowerCase() != name.toLowerCase()) {
            return;
        }
        
        masterPage = w.masterPage;
        var createFlyer = masterPage.createFlyer;
        var wizardStep = createFlyer.wizardStep;

        var amenitiesConstructor = function () {
            var self = this;
            var init = function () {
                var form = $("#content form[method='post']");
                
                form.find("#checkboxCheckAll").on("click", function (e) {
                    var element = $(e.target);

                    if (element.is(":checked")) {
                        form.find("[data-checkall] input[type='checkbox']").attr("checked", "checked");
                        form.find("[data-checkall] input[type='checkbox']").prop("checked", true);
                    }
                    else {
                        form.find("[data-checkall] input[type='checkbox']").removeAttr("checked");
                        form.find("[data-checkall] input[type='checkbox']").prop("checked", false);
                    }
                });
            };

            self.init = init;
            init();
        };

        var a = masterPage.createFlyer.wizardSteps.amenities;

        if (!a) {
            masterPage.createFlyer.wizardSteps.amenities = new amenitiesConstructor();
        }
        else {
            a.init();
        }
    };

    if (shouldSubscribeToEvents) {
        $(func);
        $(document).on("setContent", func);
    }
})(jQuery, window);