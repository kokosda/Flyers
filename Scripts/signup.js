(function ($, w) {
    $(function () {
        var signUpConstructor = function () {
            var self = this;
            var init = function () {
                var masterPage = w.masterPage;
                var formLogin = $(".content.signup .small-content form")
                var validationOptions = masterPage.getLoginValidationOptions(formLogin);

                masterPage.assignValidation(validationOptions);

                var formSignUp = $("#aspnetForm");
                var getForm = function () {
                    return formSignUp;
                };
                var getAssociationsSelectEl = function (stateSelectEl) {
                    return getForm().find("select[name='association']");
                };
                var fillSelectEl = function (selectEl, firstOptionText, values) {
                    var innerFirstOptionText = typeof firstOptionText == "string" ? firstOptionText : "[Select]";
                    var options = "<option value=''>" + innerFirstOptionText + "</option>";

                    for (var v in values) {
                        options += "<option value='" + values[v].Item1 + "'>" + values[v].Item2 + "</option>";
                    }

                    selectEl.html(options);
                };

                formSignUp.find("[id*='ddlBusinessState']").on("change", function () {                    
	                var targetEl = $(this);
	                var data = { state: "\"" + targetEl.val() + "\"" };

	                if (targetEl.val().length == 0) {
	                    getAssociationsSelectEl().empty();
	                    fillSelectEl(getAssociationsSelectEl(), "Select Association...", []);
	                }
	                else {
	                    $.ajax({
	                        url: getForm().data("marketsurl"),
	                        type: "GET",
	                        data: data,
                            contentType: "application/json; charset=UTF-8",
	                        success: function (r) {
	                            var model = r.d;

	                            if (model) {
	                                if (typeof model.Message == "string") {
	                                    alert(model.Message);
	                                    if (w["console"] && typeof w.console.log == "function") {
	                                        w.console.log(model.Message);
	                                    }
	                                }
	                                else {
	                                    var selectEl = getAssociationsSelectEl(targetEl);

	                                    fillSelectEl(selectEl, "Select Association...", model.Associations);
	                                }
	                            }
	                        },
	                        error: masterPage.ajaxError
	                    });
	                }
                });

                var rules = {};
                var messages = {};

                validationOptions = {};
                validationOptions.form = formSignUp;
                validationOptions.rules = rules;
                validationOptions.messages = messages;
                validationOptions.validateInvisibleInputs = true;
                validationOptions.keepInputsDisabledAfterSubmit = true;
                validationOptions.submitHandler = function (form) {
                    var inputSubmit = $(form).find("input[type='submit']");

                    __doPostBack(inputSubmit.attr("name"), "");
                };

                var emailId = "";
                var passwordId = "";

                formSignUp.find("select,input").each(function () {
                    var element = $(this);
                    var name = element.attr("name");
                    var clientName = element.data("clientname");

                    if (typeof clientName == "string") {
                        switch (clientName) {
                            case "Email":
                                rules[name] = {
                                    required: true,
                                    pattern: /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/
                                };
                                messages[name] = {
                                    required: "Email is required.",
                                    pattern: "Email is not valid.",
                                    email: "Email is not valid."
                                };
                                emailId = element.attr("id");
                                break;
                            case "EmailConfirm":
                                rules[name] = {
                                    required: true,
                                    pattern: /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/,
                                    equalTo: "#" + emailId
                                };
                                messages[name] = {
                                    required: "Confirm Email is required.",
                                    pattern: "Confirm Email is not valid.",
                                    email: "Confirm Email is not valid.",
                                    equalTo: "Emails do not match."
                                };
                                break;
                            case "Password":
                                rules[name] = {
                                    required: true
                                };
                                messages[name] = {
                                    required: "Password is required."
                                };
                                passwordId = element.attr("id");
                                break;
                            case "ConfirmPassword":
                                rules[name] = {
                                    required: true,
                                    equalTo: "#" + passwordId
                                };
                                messages[name] = {
                                    required: "Confirm Password is required.",
                                    equalTo: "Passwords do not match."
                                };
                                break;
                            case "FirstName":
                                rules[name] = {
                                    required: true
                                };
                                messages[name] = {
                                    required: "First Name is required."
                                };
                                break;
                            case "LastName":
                                rules[name] = {
                                    required: true
                                };
                                messages[name] = {
                                    required: "Last Name is required."
                                };
                                break;
                            case "Title":
                                rules[name] = {
                                    required: true
                                };
                                messages[name] = {
                                    required: "Select your title."
                                };
                                break;
                            case "BusinessAddress1":
                                rules[name] = {
                                    required: true
                                };
                                messages[name] = {
                                    required: "Business Address is required."
                                };
                                break;
                            case "BusinessCity":
                                rules[name] = {
                                    required: true
                                };
                                messages[name] = {
                                    required: "Business City is required."
                                };
                                break;
                            case "BusinessState":
                                rules[name] = {
                                    required: true
                                };
                                messages[name] = {
                                    required: "Select business state."
                                };
                                break;
                            case "BusinessZipCode":
                                rules[name] = {
                                    required: true,
                                    zipcodeUS: true
                                };
                                messages[name] = {
                                    required: "Business Zip Code is required.",
                                    zipcodeUS: "Zip Code is not valid."
                                };
                                break;
                            case "BusinessPhone":
                                rules[name] = {
                                    required: true,
                                    pattern: /((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}/
                                };
                                messages[name] = {
                                    required: "Business Phone is required.",
                                    pattern: "Invalid Business Phone. Use ###-###-#### format."
                                };
                                element.mask("999-999-9999", { autoclear: false });
                                break;
                            case "HomePhone":
                            case "CellPhone":
                            case "BusinessFax":
                                var labelText = element.closest(".item").find("label").text();
                                rules[name] = {
                                    phoneNumber: true
                                };
                                messages[name] = {
                                    phoneNumber: "Invalid " + labelText + ". Use ###-###-#### format."
                                };
                                element.mask("999-999-9999", { autoclear: false });
                                break;
                            case "Website":
                                rules[name] = {
                                    pattern: /http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- ./?%&=]*)?/
                                };
                                messages[name] = {
                                    pattern: "Invalid website URL. Use http:// prefix."
                                };
                                break;
                            case "BrokerageName":
                                rules[name] = {
                                    required: true
                                };
                                messages[name] = {
                                    required: "Brokerage Name is required."
                                };
                                break;
                            case "BrokerageAddress1":
                                rules[name] = {
                                    required: true
                                };
                                messages[name] = {
                                    required: "Brokerage Address is required."
                                };
                                break;
                            case "BrokerageCity":
                                rules[name] = {
                                    required: true
                                };
                                messages[name] = {
                                    required: "Brokerage City is required."
                                };
                                break;
                            case "BrokerageState":
                                rules[name] = {
                                    required: true
                                };
                                messages[name] = {
                                    required: "Select brokerage state."
                                };
                                break;
                            case "BrokerageZipCode":
                                rules[name] = {
                                    required: true,
                                    zipcodeUS: true
                                };
                                messages[name] = {
                                    required: "Brokerage Zip Code is required.",
                                    zicodeUS: "Zip Code is not valid."
                                };
                                break;
                            case "SecondaryEmail":
                                rules[name] = {
                                    pattern: /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/
                                };
                                messages[name] = {
                                    pattern: "Secondary Email is not valid.",
                                    email: "Secondary Email is not valid."
                                };
                                break;
                            case "TermsAndConditions":
                                rules[name] = {
                                    isChecked: true
                                };
                                messages[name] = {
                                    isChecked: "Please accept our Terms & Conditions to proceed."
                                };
                                break;
                        };
                    }
                });
                masterPage.assignValidation(validationOptions);
            };

            init();
        };

        masterPage.signUp = masterPage.signUp || new signUpConstructor();
	});
})(jQuery, window);