(function ($, w) {
    var masterPage = w.masterPage;
    var shouldSubscribeToEvents = (!masterPage) || (!masterPage.createFlyer.wizardSteps.photo);

    var func = function (e, step) {
        var name = "photo";

        if (step && step.toLowerCase() != name.toLowerCase()) {
            return;
        }
        
        masterPage = w.masterPage;
        var createFlyer = masterPage.createFlyer;
        var wizardStep = createFlyer.wizardStep;

        var photoConstructor = function () {
            var self = this;
            var init = function () {
                var form = $("#content form");
                
                form.find("input.delete").on("click", function () {
                    var element = $(this);

                    element.parent(".file-one").find("input[type='hidden']").val("true");
                });

                form.find("input[type='file']").on("change", function () {
                    var element = $(this);

                    if (element.val()) {
                        element.parent(".file-one").find("input[type='hidden']").val("false");
                    }
                });
            };

            self.init = init;
            init();
        };

        var p = masterPage.createFlyer.wizardSteps.photo;

        if (!p) {
            masterPage.createFlyer.wizardSteps.photo = new photoConstructor();
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