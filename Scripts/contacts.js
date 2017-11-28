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
					case "Name":
						rules[name] = {
							required: true
						};
						messages[name] = {
							required: "Name is required."
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
			        case "Message":
			            rules[name] = {
			                required: true,
                            maxlength: 3000
			            };
			            messages[name] = {
			                required: "Enter your question or comment."
			            };
			            break;
				};
			}
		});
		masterPage.assignValidation(validationOptions);

		var center = new google.maps.LatLng(26.324837, -80.286216);
		var factory = new google.maps.LatLng(26.324225, -80.283692);
		var initialize = function () {
		    var mapOptions = {
		        center: center,
		        scrollwheel: false,
		        zoom: 17,
		        mapTypeId: google.maps.MapTypeId.ROADMAP
		    };
		    var map = new google.maps.Map(document.getElementById("map"), mapOptions);
		    var content = '<div id="iw-container">' +
                              '<div class="iw-content">' +
                              '<p>7669 NW 117th Lane Parkland,<br>' +
                                  'Florida 3 3076, USA</p>' +
                                  '<p>customerservice@flyerme.com</p>' +
                              '</div>' +
                            '</div>';
		    var infowindow = new google.maps.InfoWindow({
		        content: content,
		        maxWidth: 350
		    });
		    var image = new google.maps.MarkerImage(
                 'images/non.gif',
                 new google.maps.Size(0, 0),
                 new google.maps.Point(0, 0),
                 new google.maps.Point(0, 0)
               );
		    var marker = new google.maps.Marker({
		        position: factory,
		        icon: image,
		        map: map,
		        title: "7669 NW 117th Lane Parkland, Florida 3 3076, USA"
		    });

		    google.maps.event.addListener(marker, 'click', function () {
		        infowindow.open(map, marker);
		    });
		    google.maps.event.addListener(infowindow, 'domready', function () {
		        var iwOuter = $('.gm-style-iw');
		        var iwBackground = iwOuter.prev();
		        iwBackground.children().css({ 'display': 'none' });
		        iwOuter.parent().parent().css({ left: '7px', top: '35px' });
		        var iwCloseBtn = iwOuter.next();
		        iwCloseBtn.css({ opacity: '0' });
		        if ($('.iw-content').height() < 140) {
		            $('.iw-bottom-gradient').css({ display: 'none' });
		        }
		        iwCloseBtn.mouseout(function () {
		            $(this).css({ opacity: '1' });
		        });

		    });

		    infowindow.open(map, marker);
		};

		initialize();
	});
})(jQuery, window);