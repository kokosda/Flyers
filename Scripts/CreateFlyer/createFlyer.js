(function ($, w) {
	$(function () {
		var createFlyerConstructor = function () {
			var self = this;
			var masterPage = w.masterPage;
			var history = masterPage.history;

			var leftMenuConstructor = function () {
				var leftMenu = this;
				var init = function () {
				    $(".left-menu a").off().on("click", function (e) {
				        e.preventDefault();

				        var target = $(this);
				        var url = target.attr("href");

				        if (!target.hasClass("active")) {
				            $.ajax({
				                url: url,
				                type: "GET",
				                cache: false,
				                success: function (r) {
				                    setContent(r, url);
				                },
				                error: masterPage.ajaxError
				            });
				        }
				    });
				};
                
				leftMenu.getActive = function () {
				    return $(".left-menu a.active");
				};

				leftMenu.init = init;
				leftMenu.init();
			};

			var flyerMenuConstructor = function () {
			    var flyerMenu = this;

			    var init = function () {
			        var shouldRemoveAttributeDisabled;
			        var onSubmitWizardStepError = function (r, inputNext) {
			            if (shouldRemoveAttributeDisabled) {
			                inputNext.removeAttr("disabled");
			            }

			            masterPage.ajaxError(r);
			        };
			        var save = function (inputNext, redirectOnSuccess) {
			            var autoHideModal = true;

			            inputNext.attr("disabled", "disabled");
			            $.ajax({
			                url: $(".flyer-menu .save").attr("href"),
			                type: "POST",
			                contentType: "application/json; charset=UTF-8",
			                dataType: "json",
			                success: function (r) {
			                    var model = r.d;

			                    if (model.Result && typeof model.RedirectToUrl == "string") {
			                        if (redirectOnSuccess === true) {
			                            w.location.href = model.RedirectToUrl;
			                        }
			                    }
			                    else if (model.Result === false && typeof model.Message == "string") {
			                        masterPage.setPopupControlHeaderHtml("genericprogress", model.Message);
			                        masterPage.setPopupControlIcon("genericprogress", "ok");
			                        autoHideModal = false;
			                    }

			                    if (shouldRemoveAttributeDisabled) {
			                        inputNext.removeAttr("disabled");
			                    }
			                },
			                error: function (r) {
			                    if (shouldRemoveAttributeDisabled) {
			                        inputNext.removeAttr("disabled");
			                    }

			                    masterPage.ajaxError(r);
			                },
			                complete: function () {
			                    if (autoHideModal) {
			                        masterPage.hidePopupControl();
			                    }
			                }
			            });
			        };

			        flyerMenu.previewHandler = function (e) {
			            e.preventDefault();

			            var target = $(e.target);
			            var form = $("#content form[method='post']");
			            var inputNext = form.find("input[type='submit']");
			            var shouldSubmit = !inputNext.attr("disabled");

			            shouldRemoveAttributeDisabled = !inputNext.attr("disabled");

			            if (shouldSubmit) {
			                self.wizardStep.submitHandler(form, function () {
			                    if (masterPage.shouldShowFlyerPreview()) {
			                        masterPage.showFlyerPreview(e, target.attr("href"));
			                    }
			                    else {
			                        masterPage.synthesizeAnchorClick(target[0]);
			                    }

			                    save(inputNext);
			                },
                            function (r) { onSubmitWizardStepError(r, inputNext); });
			            }
			            else {
			                masterPage.showFlyerPreview(e, target.attr("href"));
			            }
			        };

			        $("a.preview").off().on("click", flyerMenu.previewHandler);

			        $("a.save").off().on("click", function (e) {
			            e.preventDefault();

			            var form = $("#content form[method='post']");
			            var inputNext = form.find("input[type='submit']");
			            var target = $(this);
			            var shouldSubmit = !inputNext.attr("disabled");

			            shouldRemoveAttributeDisabled = !inputNext.attr("disabled");

			            masterPage.setPopupControlHeaderHtml("genericprogress", "Saving your flyer. Please wait.");
			            masterPage.handleClickOnPopupControls(null, "genericprogress");

			            if (shouldSubmit) {
			                self.wizardStep.submitHandler(form, function () { save(inputNext, true); }, function (r) { onSubmitWizardStepError(r, inputNext); });
			            }
			            else {
			                save(inputNext, true);
			            }
			        });
			    };

			    flyerMenu.init = init;
			    init();
			};
			
			self.leftMenu = new leftMenuConstructor();
			self.flyerMenu = new flyerMenuConstructor();
			self.wizardSteps = {};
			self.buyer = {};
			self.custom = {};

			var submitHandlerDefault = function (formParam, callback, errorCallback) {
			    var form = $(formParam);
			    var url = form.attr("action");
			    var httpMethod = form.attr("method");
			    var data = {};
			    var isFormData = false;

			    if (form.attr("enctype").toLowerCase() == "multipart/form-data") {
			        data = new FormData(form[0]);
			        isFormData = true;
			    }
			    else {
			        form.find("input,select,textarea").each(function () {
			            var element = $(this);
			            var excludeFromData = (element.attr("type") == "radio" && (!element.is(":checked"))) ||
                                              (element.attr("type") == "checkbox" && (!element.parent().is(":visible")));

			            if (excludeFromData) {
			                return;
			            }

			            var name = element.data("clientname") || element.attr("name");

			            if (name) {
			                if (element.attr("type") == "checkbox") {
			                    data[name] = element.is(":checked");
			                }
			                else {
			                    data[name] = element.val();
			                }
			            }
			        });
			    }

			    var ajaxSettings = {
			        url: url,
			        type: httpMethod,
			        data: data,
			        success: function (r) {
			            if (typeof callback == "function") {
			                callback(r);
			            }
			            else {
			                setContent(r, url);
			            }
			        },
			        error: errorCallback || masterPage.ajaxError,
			        complete: function () {
			            form.find("input[type='submit']").removeClass("load");
			        }
			    };

			    if (isFormData) {
			        ajaxSettings.processData = false;
			        ajaxSettings.contentType = false;
			    }

			    form.find("input[type='submit']").addClass("load");
			    $.ajax(ajaxSettings);
			};

			self.wizardStep = {
			    init: function () {
			        self.wizardStep.form = $("#content form[method='post']");
			        self.wizardStep.submitHandler = submitHandlerDefault;

			        var validationOptions = {
                        form: self.wizardStep.form,
                        submitHandler: self.wizardStep.submitHandler,
                        keepInputsDisabledAfterSubmit: true
			        };

			        masterPage.assignValidation(validationOptions);
			    },
			    form: null,
			    submitHandler: submitHandlerDefault
			};
			self.wizardStep.init();

			var scriptsMap = {};

			var getStep = function () {
			    return $("#content input[type='hidden'][name$='Step']").val();
			};

			var initComponents = function () {
			    masterPage.elements.init();
			    self.leftMenu.init();
			    self.flyerMenu.init();
			    self.wizardStep.init();
			    $(document).trigger("setContent", getStep());
			};

			history.setInitialSetStateCallback(initComponents);

			var setContent = function (htmlPage, url, target) {
			    var html = $(htmlPage).find("#content").html();
			    var scripts = $(htmlPage).find("script");

			    $("#content").html(html);

			    masterPage.loadScripts(scripts, scriptsMap);
			    initComponents();
			    new history.pushState(url, initComponents);
			};

			$("header a,footer a,footer [type='submit']").on("click", function (e) {
			    e.preventDefault();

			    var element = $(this);

			    var getUrl = function () {
			        var url = element.attr("href");

			        if (typeof url !== "string" || url.length == 0) {
			            if (element.attr("type") == "submit") {
			                var form = element.closest("form");

			                url = form.attr("action");

			                var formData = form.serialize();

			                if (typeof formData == "string" && formData.length > 0) {
			                    if (url.indexOf("?") >= 0) {
			                        url += "&" + formData;
			                    }
			                    else {
			                        url += "?" + formData;
			                    }
			                }
			            }
			        }

			        var result = url;
			        return result;
			    }

			    masterPage.setPopupControlHeaderHtml("genericconfirmation", "Are you sure you want<br />to leave this page?");
			    masterPage.handleClickOnPopupControls(null, "genericconfirmation");
			    $("#popup [data-yes]").attr("href", getUrl());
			    $("#popup [data-yes]").off().on("click", function (e2) {
			        masterPage.hidePopupControl();
			    });
			    $("#popup [data-no]").off().on("click", function (e2) {
			        e2.preventDefault();
			        masterPage.hidePopupControl();
			    });
			});
		};
		
		var cf = w.masterPage.createFlyer;

		w.masterPage.createFlyer = cf || new createFlyerConstructor();
	});
})(jQuery, window);