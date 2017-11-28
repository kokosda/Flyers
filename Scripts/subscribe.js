(function ($, w) {
	$(function () {
	    var form = $("#content form");
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

	    form.find("select,input,textarea").each(function () {
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
	                case "State":
	                    rules[name] = {
	                        required: true
	                    };
	                    messages[name] = {
	                        required: "Select your state."
	                    };
	                    break;
	            };
	        }
	    });

	    if (form.size()) {
	        masterPage.assignValidation(validationOptions);
	    }
	});
})(jQuery, window);