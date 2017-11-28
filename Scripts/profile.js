(function ($, w) {
	$(function () {
		var form = $("#content form");
		var getHiddenScrollY = function () {
			return form.find("[id*='hiddenScrollY']");
		};
		var handleDdlBusinessStateChange = function (e) {
			getHiddenScrollY().val(w.scrollY);
		};

		form.find("[id*='ddlBusinessState']").on("change", handleDdlBusinessStateChange);

		var setScroll = function () {
			var hiddenScrollY = getHiddenScrollY();
			if (hiddenScrollY.val().length > 0) {
				var scrollY = parseInt(hiddenScrollY.val());
				if (!isNaN(scrollY)) {
					w.scrollTo(0, scrollY);
				}
			}
		};

		setScroll();

		var inputSubmit;

		form.find("input[type='submit']").on("click", function () {
		    inputSubmit = $(this);
		});

		var rules = {};
		var messages = {};
		var validationOptions = {
		    form: form,
	        rules: rules,
	        messages: messages,
	        validateInvisibleInputs: true,
	        keepInputsDisabledAfterSubmit: true,
	        submitHandler: function (form) {
	            __doPostBack(inputSubmit.attr("name"), "");
	        }
		};

		form.find("select,input").each(function () {
			var element = $(this);
			var name = element.attr("name");
			var clientName = element.data("clientname");

			if (typeof clientName == "string") {
			    switch (clientName) {
			        case "CurrentPassword":
			            rules[name] = {
			                required: {
			                    depends: function () {
			                        return $("[id*='inputNewPassword']").val().length > 0;
			                    }
			                }
			            };
			            messages[name] = {
			                required: "Current Password is required."
			            };
			            break;
			        case "ConfirmNewPassword":
			            rules[name] = {
			                equalTo: "#" + form.find("[id*='inputNewPassword']").attr("id")
			            };
			            messages[name] = {
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
				};
			}
		}).on("keyup", function () {
		    form.valid();
		});
		masterPage.assignValidation(validationOptions);
		form.find("input.delete").on("click", function () {
		    var element = $(this);

		    element.parents(".image").find("input[type='hidden']").val("true");

		    var control = element.parents(".block").find(".choice input[type='file']");

		    control.replaceWith(control.val("").clone(true));
		});
		form.find("input[type='file']").on("change", function () {
		    var element = $(this);

		    if (element.val()) {
		        element.parent(".block").find(".image").find("input[type='hidden']").val("false");
		    }
		});
		form.find(".choice input[type='file']").on("change", function () {
		    var container = $(this).parents(".block").find(".picture .image .table-cell");

		    container.addClass("change");

		    var inputFiles = this.files;

		    if (inputFiles == undefined || inputFiles.length == 0) {
		        return;
		    }

		    var inputFile = inputFiles[0];
		    var reader = new FileReader();

		    reader.onload = function (event) {
		        container.find("img").remove();

		        var img = $("<img/>", { "class": "preview" });
		        img.appendTo(container);
		        img.attr("src", event.target.result);
		    };
		    reader.onerror = function (event) {
		        alert("ERROR: " + event.target.error.code);
		    };
		    reader.readAsDataURL(inputFile);
		});
	});
})(jQuery, window);