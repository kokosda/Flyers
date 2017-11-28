(function ($, w) {
	$(function () {
		var masterPageConstructor = function () {
		    var self = this;

			self.assignValidation = function (options) {
				var form = $(options.form);

				form.data("validator", null);
				form.off("validate").off("submit");

				var validator = form.validate({
					rules: options.rules,
					messages: options.messages,
					submitHandler: function (form) {
					    disableAllButtons(form, true);

					    if (typeof options.submitHandler == "function") {
					        options.submitHandler(form);
					    }
					    else {
					        form.submit();
					    }

					    if (!options.keepInputsDisabledAfterSubmit) {
					        disableAllButtons(form, false);
					    }
					},
					errorPlacement: function (error, element) {
						if (element.attr("type") == "checkbox") {
							if (element.parent().hasClass("chekbox")) {
								error.insertAfter(element.next());
							}
						} else {
							error.insertAfter(element);
						}
					},
					success: options.success,
					invalidHandler: options.invalidHandler,
					showErrors: options.showErrors
				});

				if (options.disableOnFocusout) {
					validator.settings.onfocusout = false;
				}
				if (options.validateInvisibleInputs) {
					validator.settings.ignore = "";
				}
			};

			self.getLoginValidationOptions = function (form) {
				var result = {
					form: form,
					rules: {
						"UserName": {
							required: true,
							email: true
						},
						"Password": {
							required: true
						}
					},
					messages: {
						"UserName": {
							required: "This field is required.",
							email: "Please enter a valid email address."
						},
						"Password": {
							required: "This field is required."
						}
					}
				};

				return result;
			};

			self.getRecoverPasswordValidationOptions = function (form) {
				var result = {
					form: form,
					rules: {
						"Email": {
							required: true,
							email: true
						}
					},
					messages: {
						"Email": {
							required: "This field is required.",
							email: "Please enter a valid email address."
						}
					}
				};

				return result;
			};

			self.ajaxError = function (r) {
				alert(r.status + " " + r.statusText);
			};

			self.queryToMap = function (url) {
				var result;

				if (typeof url == "string") {
					var split = url.split("?");

					if (split.length > 1) {
						var query = split[1].split("&");

						result = {};
						
						for (var i in query) {
							split = query[i].split("=");

							if (split.length > 0) {
								result[split[0]] = "";

								if (split.length > 1) {
									result[split[0]] = split[1];
								}
							}
						}
					}
				}

				return result;
			}

			var googleMap;
			var googleMapMarker;
			var googleMapParametersConstructor = function (latitude, longitude, zoom) {
				var self = this;

				self.latitude = latitude || 0;
				self.longitude = longitude || 0;
				self.zoom = zoom || 17;

				if (typeof latitude == "number" && typeof longitude == "number") {
					self.marker = latitude.toString() + "," + longitude.toString();
				}

				self.toLatLng = function () {
					return new google.maps.LatLng(self.latitude, self.longitude);
				}
			};

			self.initGoogleMap = function (mapElement) {
			    var p = new googleMapParametersConstructor();
				var mapOptions = {
					center: p.toLatLng(),
					zoom: p.zoom
				};

				if (!googleMap) {
					googleMap = new google.maps.Map(mapElement, mapOptions);
				}
				else {
				    if (!($(mapElement).find("*").size())) {
				        $(mapElement).replaceWith(googleMap.getDiv());
				    }

					googleMap.setCenter(p.toLatLng());
					googleMap.setZoom(p.zoom);
				}
			};
			self.setGoogleMap = function (googleMapParameters, markerTitle) {
				if (!googleMapParameters) {
					return;
				}

				var latitude = googleMapParameters.latitude;
				var longitude = googleMapParameters.longitude;

				if (!isNaN(latitude) && !isNaN(longitude)) {
					var latLng = googleMapParameters.toLatLng();

					google.maps.event.trigger(googleMap, "resize");
					googleMap.setCenter(latLng);
					googleMap.setZoom(googleMapParameters.zoom);

					if (googleMapMarker) {
						googleMapMarker.setMap(null);
					}

					googleMapMarker = new google.maps.Marker({
						position: latLng,
						map: googleMap,
						title: markerTitle
					});
				}
			};
			self.parseGoogleMapLink = function (mapLink) {
				var mappedQuery = self.queryToMap(mapLink);
				var result;

				if (mappedQuery) {
					result = new googleMapParametersConstructor();

					if (mappedQuery["q"]) {
						result.marker = mappedQuery["q"];
					}
					if (mappedQuery["ll"] && mappedQuery["ll"].indexOf(",") >= 0) {
						split = mappedQuery["ll"].split(",");
						result.latitude = parseFloat(split[0]);
						result.longitude = parseFloat(split[1]);
					}
					if (mappedQuery["z"]) {
						result.zoom = parseInt(mappedQuery["z"]);
					}
				}

				return result;
			};
			self.buildGoogleMapLink = function (googleMapParameters) {
				//format: "https://google.com/maps?q=35.128061,-106.535561&ll=35.126517,-106.535131&z=17"
				var result = "https://google.com/maps?";

				if (googleMapParameters.marker) {
					result += "q=" + googleMapParameters.marker + "&";
				}
				if (!isNaN(googleMapParameters.latitude) && !isNaN(googleMapParameters.longitude)) {
					result += "ll=" + googleMapParameters.latitude + "," + googleMapParameters.longitude + "&";
				}
				if (!isNaN(googleMapParameters.zoom)) {
					result += "z=" + googleMapParameters.zoom + "&";
				}

				if (result[result.length - 1] == "&") {
					result = result.substr(0, result.length - 1);
				}
				else {
					result == undefined;
				}

				return result;
			};

			var googleGeocodeAddressTypes = {
			    "state": {
			        "types": ["administrative_area_level_1", "political"]
			    },
			    "city": {
			        "types": ["locality", "political"]
			    },
			    "zipCode": {
			        "types": ["postal_code"]
			    },
			    "street": {
			        "types": ["route"]
			    }
			};

			var parseGeocodeResponse = function (response) {
			    var result;

			    if (!response || !response.length) {
			        return result;
			    }

			    var compareTypes = function (responseTypes, checkingTypes) {
			        var result;

			        if (responseTypes.length >= checkingTypes.length) {
			            for (var ct in checkingTypes) {
			                result = false;

			                for (var rt in responseTypes) {
			                    if (checkingTypes[ct] == responseTypes[rt]) {
			                        result = true;
			                        break;
			                    }
			                }

			                if (!result) {
			                    break;
			                }
			            }
			        }

			        return result;
			    };
			    var streetFound;

			    for (var ac in response[0].address_components) {
			        var addressComponent = response[0].address_components[ac];

			        if (compareTypes(addressComponent.types, googleGeocodeAddressTypes["street"].types)) {
			            streetFound = true;
			            break;
			        }
			    }

			    if (!streetFound) {
			        return result;
			    }

			    result = {
			        geocoding: response[0]
			    };

			    var geocoding = result.geocoding;

			    for (var ac in geocoding.address_components) {
			        var addressComponent = geocoding.address_components[ac];
			        var types = addressComponent.types;

			        if ((!result.state) && compareTypes(addressComponent.types, googleGeocodeAddressTypes["state"].types)) {
			            result.state = addressComponent.long_name;
			        }
			        if ((!result.city) && compareTypes(addressComponent.types, googleGeocodeAddressTypes["city"].types)) {
			            result.city = addressComponent.long_name;
			        }
			        else if ((!result.zipCode) && compareTypes(addressComponent.types, googleGeocodeAddressTypes["zipCode"].types)) {
			            result.zipCode = addressComponent.long_name;
			        }
			    }

			    return result;
			};

			var googleGeocoder;

			self.googleGeocodeAddress = function (options) {
				var address = options.address;
				var callback = options.callback;

				if (typeof address != "string" || address.length == 0) {
					callback();
					return;
				}

				if (!googleGeocoder) {
				    googleGeocoder = new google.maps.Geocoder();
				}

				googleGeocoder.geocode({ 'address': address }, function (results, status) {
				    var result;

				    if (status == google.maps.GeocoderStatus.OK) {
				        result = parseGeocodeResponse(results);
				        var geocoding = result ? result.geocoding : undefined;

						if (geocoding) {
						    var location = geocoding.geometry.location;
						    result.gmParams = new googleMapParametersConstructor(location.lat(), location.lng());
						}

						callback(result);
					}
					else if (status == google.maps.GeocoderStatus.UNKNOWN_ERROR) {
						setTimeout(self.googleGeocodeAddress, 1000, options);
					}
				});
			};

			self.loadScripts = function (scripts, scriptsMap) {
			    if (scripts.size()) {
			        scripts.each(function () {
			            var src = $(this).attr("src");

					    if ((!$("script[src='" + src + "']").size()) && (!scriptsMap[src])) {
					        $.getScript(src);
					        scriptsMap[src] = src;
					    }
					});
				}
			};

			self.synthesizeAnchorClick = function (anchorElement) {
			    if (anchorElement) {
			        var a = document.createElement("a");

			        a.setAttribute("href", anchorElement.getAttribute("href"));
			        a.setAttribute("target", "_blank");

			        var dispatch = document.createEvent("HTMLEvents");

			        dispatch.initEvent("click", true, true);
			        a.dispatchEvent(dispatch);
			    }
			};

			var init = function () {
			    var popupContent = $("#popup").find("#popup_content").detach();
			    var popupBigContent = $("#popup_big").find("#popup_content").detach();

				var handleClickOnPopupControls = function (e, popupName) {
				    if (e) {
				        e.preventDefault();
				    }

					var target = $(this);
					var selector = "#";
					var validationOptions;

					if (target.hasClass("login")) {
						selector += "login";
						validationOptions = self.getLoginValidationOptions();
					}
					else if (target.hasClass("recoverpassword")) {
					    selector += "recoverpassword";
						validationOptions = self.getRecoverPasswordValidationOptions();
					}
					else if (typeof popupName == "string") {
					    selector += popupName;
					}

					selector += "_popup_content";

					var content = popupContent.find(selector);
					var foundInBig = false;

					if (!content.size()) {
					    content = popupBigContent.find(selector);
					    foundInBig = content.size() > 0;
					}

					if (content.size()) {
					    if (foundInBig) {
					        showPopupBig(content.html());
					    }
                        else {
					        $("#popup").html(content.html());

					        if (validationOptions) {
					            validationOptions.form = $("#popup form");
					            self.assignValidation(validationOptions);
					        }

					        initPopup();

					        $(".recoverpassword").off().on("click", handleClickOnPopupControls);
					        $("#popup_bg").show();
					        $("#popup").show();
					    }
					}
				};
                
				self.handleClickOnPopupControls = handleClickOnPopupControls;

				self.setPopupControlIcon = function (popupName, iconClass) {
				    if (typeof iconClass == "string") {
				        var iconElement = popupContent.find("#" + popupName + "_popup_content .icon");

				        iconElement.removeAttr("class");

				        if (iconClass.length > 0) {
				            iconElement.addClass("icon " + iconClass);
				        }

				        iconElement = $("#popup .icon");
				        iconElement.removeAttr("class");

				        if (iconClass.length > 0) {
				            iconElement.addClass("icon " + iconClass);
				        }
				    }
				};

				self.setPopupControlHeaderHtml = function (popupName, html) {
				    popupContent.find("#" + popupName + "_popup_content [data-header]").html(html);
				    $("#popup [data-header]").html(html);
				};

				self.hidePopupControl = function () {
				    if ($("#popup").is(":visible") || $("#popup_big").is(":visible")) {
				        $("#popup").hide();
				        $("#popup_big").hide();
				        $("#popup_bg").hide();
				    }
				};

				var showPopupBig = function (markup) {
				    if ($("#popup_big .conteiner").size()) {
				        $("#popup_big .conteiner").mCustomScrollbar("destroy");
				    }

				    $("#popup_big").empty();
				    $("#popup_big").html(markup);
				    $("#popup_bg").show();
				    $("#popup_big").show();
				    $("#popup_big .conteiner").mCustomScrollbar();
				    $("#popup_big .close").off().on("click", function (e) {
				        e.preventDefault();
				        $("#popup_big .conteiner").mCustomScrollbar("destroy");
				        $("#popup_big").hide();
				        $("#popup_bg").hide();
				    });
				};

				var handleFormClear = function (e) {
					$(e.target).closest("form").find("input[type='text']").val("");
				};

				$(".login").off().on("click", handleClickOnPopupControls);
				$(".search-form form .clear").off().on("click", handleFormClear);
				$(".plaxo-invoker").on("click", function (e) {
				    e.preventDefault();
				    showPlaxoABChooser($(".plaxo-target").attr("id"), "AddressWidget.htm");
				});
				$("#popup_preview .close").off().on("click", function (e) {
				    e.preventDefault();
				    $("#popup_preview .content").mCustomScrollbar("destroy");
				    $("#popup_preview").hide();
				    $("#popup_bg").hide();
				});
				self.checkCookies();

				setTimeout(function () {
				    $("div.message.ok,div.message.error,div.message.system").slideUp();
				}, 10000);
			};

			var popupPos = function () {
				var w = $(window).height(),
					p = $('#popup').height() + 144,
					pm = (w - p) / 2;
				if ((w - 40) > p) {
					$('#popup').css({ 'top': pm, 'position': 'fixed' });
				} else {
					$('#popup').css({ 'top': '20px', 'position': 'absolute' });
				}
			}

			var initPopup = function () {
				$('#popup').each(function () {
					$('.add_card').click(function () {
						$('#popup').show();
						$('#popup_bg').show();
						return false;
					});
					$('#popup .cancel, #popup .close').click(function () {
						$('#popup').hide();
						$('#popup_bg').hide();
						return false;
					});
					popupPos();
					$(window).resize(function () {
						popupPos();
					});
				});
			};

			var disableAllButtons = function (container, disable, callback) {
				var innerCont = $(container);
				if (innerCont.size()) {
				    if (disable) {
				        innerCont.find("input[type=button],input[type=submit],button,a").attr("disabled", "disabled");
						setTimeout(function () {
						    if (typeof callback == "function") {
						        callback();
						    }
						}, 0);
					}
					else {
						setTimeout(function () {
							innerCont.find("input[type=button],input[type=submit],button,a").removeAttr("disabled");
						}, 0);
					}
				}
			};

			if ($.validator) {
				$.validator.addMethod("phoneNumber", function (value, element) {
					var result = true;

					if (typeof value == "string" && value.length > 0) {
						if (value != "___-___-____") {
							result = /((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}/.test(value);
						}
					}

					return result;
				});
				$.validator.addMethod("isChecked", function (value, element) {
					var result = $(element).is(":checked")

					return result;
				});
				$.validator.addMethod("dateCurrentOrFuture", function (value, element) {
				    var result = false;

				    if (typeof value == "string") {
				        result = new Date(value) >= new Date(new Date().toDateString());
				    }

				    return result;
				});
			}

			var fetchingFlyerMarkup = false;
			var getFlyerMarkup = function (url, callback) {
			    if (fetchingFlyerMarkup === false && typeof url == "string" && typeof callback == "function") {
			        innerUrl = url;

			        if (innerUrl.indexOf("?") < 0) {
			            innerUrl += "?";
			        }
			        else {
			            innerUrl += "&";
			        }

			        innerUrl += "markuponly=true";

			        fetchingFlyerMarkup = true;
			        $.ajax({
			            url: innerUrl,
			            type: "GET",
			            cache: false,
			            success: function (r) {
			                callback(r);
			            },
			            error: masterPage.ajaxError,
			            complete: function () {
			                fetchingFlyerMarkup = false;
			            }
			        });
			    }
			};

			self.shouldShowFlyerPreview = function () {
			    return $(window).width() >= 700;
			};

			self.showFlyerPreview = function (e, url) {
			    if (self.shouldShowFlyerPreview()) {
			        if (e && (typeof e.preventDefault == "function")) {
			            e.preventDefault();
			        }

			        getFlyerMarkup(url, function (markup) {
			            if (typeof markup == "string") {
			                $("#popup_preview .content").mCustomScrollbar("destroy");
			                $("#popup_preview .content").remove();
			                $("#popup_preview").prepend("<div class='content' data-mcs-theme='minimal-dark'></div>");
			                $("#popup_preview .content").html(markup);
			                $("#popup_bg").show();
			                $("#popup_preview").show();
			                $("#popup_preview .content").mCustomScrollbar();
			            }
			        });
			    }
			};
			self.toUri = function (url) {
			    var result;

			    if (typeof url == "string") {
			        var a = document.createElement("a");

			        a.href = url;
			        result = {
			            rawUrl: url,
			            protocol: a.protocol,
			            hostName: a.hostname,
			            port: a.port,
			            path: a.pathname,
			            query: a.search,
			            hash: a.hash
			        };

			        $(a).remove();
			    }

			    return result;
			};
			self.getQueryValue = function (url, key) {
			    var result;
			    var uri = self.toUri(url);

			    if (uri && typeof uri.query == "string" && typeof key == "string") {
			        var query = {};

			        uri.query.replace(
                        new RegExp("([^?=&]+)(=([^&#]*))?", "g"),
                        function (p1, p2, p3, p4) {
                            query[p2.toLowerCase()] = p4;
                        }
                    );
			        result = query[key];
			    }

			    return result;
			};
			self.checkCookies = function () {
			    var cookieName = "test4Cookie" + (new Date().getTime()) + "=cookieValue";

			    document.cookie = cookieName;

			    var cookiesEnabled = document.cookie.indexOf(cookieName) != -1;

			    if (!cookiesEnabled) {
			        alert("Your browser cookies are not enabled. Please enable cookies for better performance and login. For any assistance please mail us at customerservice@flyerme.com.");
			    }
			    else {
			        document.cookie = cookieName + "=;expires=Thu, 01 Jan 1970 00:00:01 GMT";
			    }
			};

			init();
		};

		w.masterPage = new masterPageConstructor();
	});
})(jQuery, window);