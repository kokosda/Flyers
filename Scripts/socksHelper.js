(function ($, w) {
    $(function () {
        var socksHelperConstructor = function () {
            var self = this;
            var init = function () {
                var options = w.socksHelper_Options;

                if (!options) {
                    return;
                }

                var getUserModel = function (firstName, middleName, lastName, email, avatarUrl) {
                    var result = {
                        firstName: firstName ? encodeURIComponent(firstName) : undefined,
                        middleName: middleName ? encodeURIComponent(middleName) : undefined,
                        lastName: lastName ? encodeURIComponent(lastName) : undefined,
                        email: email ? encodeURIComponent(email) : undefined,
                        avatarUrl: avatarUrl ? encodeURIComponent(avatarUrl) : undefined
                    };

                    for (var f in result) {
                        if (result[f] === undefined) {
                            delete result[f];
                        }
                    }

                    return result;
                };

                var getAuthenticationModel = function (userId, userName, token, authenticationProvider) {
                    var result = {
                        userId: userId ? encodeURIComponent(userId) : undefined,
                        userName: userName ? encodeURIComponent(userName) : undefined,
                        token: token ? encodeURIComponent(token) : undefined,
                        authenticationProvider: authenticationProvider ? encodeURIComponent(authenticationProvider) : undefined
                    };

                    for (var f in result) {
                        if (result[f] === undefined) {
                            delete result[f];
                        }
                    }

                    return result;
                };

                var serializeModel = function (model) {
                    var result = $.param(model);

                    return result;
                };

                var appendReturnUrl = function (url) {
                    var result = url;

                    if (typeof result == "string") {
                        try {
                            var returnUrl = masterPage.getQueryValue(w.location.href, "returnurl");

                            if (typeof returnUrl == "string" && returnUrl.length > 0) {
                                result += "&returnurl=" + returnUrl;
                            }
                        }
                        catch (ex) {
                            console.log(ex);
                        }
                    }

                    return result;
                };

                var appendRememberMe = function (url, eventInvokerElement) {
                    var result = url;

                    if (typeof result == "string" && eventInvokerElement) {
                        var checkboxRemember = eventInvokerElement.closest(".social").parent().find("form:first").find("input[type='checkbox'][name='Remember']");

                        if (checkboxRemember.size() && checkboxRemember.is(":checked")) {
                            result += "&" + checkboxRemember.attr("name") + "=on";
                        }
                    }

                    return result;
                };

                var getFacebookUserAvatar = function (authResponse, callback) {
                    FB.api("/" + authResponse.userID + "/picture?redirect=false&type=large&access_token=" + authResponse.accessToken, function (pictureResponse) {
                        var url;
                        
                        if (pictureResponse && pictureResponse.data && typeof pictureResponse.data.url == "string" && pictureResponse.data.url.length > 0) {
                            url = pictureResponse.data.url;
                        }

                        callback(url);
                    });
                };

                var handleAfterSocialAuthorization = function (userModel, authenticationModel) {
                    var url = options.SignUpUrl + "?mode=mini";

                    url += "&" + serializeModel(userModel) + "&" + serializeModel(authenticationModel);
                    appendReturnUrl(url);
                    window.location.href = url;
                };

                var facebookStatusChangeCallback = function (response, element) {
                    if (response.status === "connected") {
                        var authResponse = response.authResponse;

                        FB.api("/" + authResponse.userID + "?fields=first_name,middle_name,last_name,email", function (userResponse) {
                            if (userResponse) {
                                if (userResponse.error) {
                                    alert(JSON.stringify(userResponse.error));
                                }
                                else {
                                    getFacebookUserAvatar(authResponse, function (avatarUrl) {
                                        var userModel = getUserModel(userResponse.first_name, userResponse.middle_name, userResponse.last_name, userResponse.email, avatarUrl);
                                        var authenticationModel = getAuthenticationModel(authResponse.userID, userResponse.email, authResponse.accessToken, "facebook");
                                        var loginData = serializeModel(userModel) + "&" + serializeModel(authenticationModel);

                                        loginData = appendReturnUrl(loginData);
                                        loginData = appendRememberMe(loginData, element);

                                        if (typeof userResponse.email == "string") {
                                            $.ajax({
                                                url: options.LoginUrl,
                                                type: "POST",
                                                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                                                data: loginData,
                                                success: function (r) {
                                                    if (r.Result) {
                                                        w.location.href = r.RedirectionUrl;
                                                    }
                                                    else {
                                                        if (typeof r.Message == "string") {
                                                            alert(r.Message);
                                                        }

                                                        if (r.CanContinueRegistration) {
                                                            handleAfterSocialAuthorization(userModel, authenticationModel);
                                                        }
                                                    }
                                                },
                                                error: masterPage.ajaxError
                                            });
                                        }
                                        else {
                                            handleAfterSocialAuthorization(userModel, authenticationModel);
                                        }
                                    });
                                }
                            }
                            else {
                                alert("Facebook api/{userID} request has returned empty response.");
                            }
                        });
                    } else if (response.status === "not_authorized") {
                        alert("Not authorized.");
                    } else {
                        alert("Not logged in to Facebook.");
                    }
                };

                var linkedInApiErrorCallback = function (errorResponse) {
                    if (errorResponse && typeof errorResponse.message) {
                        alert(errorResponse.message);
                    }
                    else {
                        alert("Unknown error while connecting to LinkedIn.");
                    }
                };

                var linkedInStatusChangeCallback = function (element) {
                    IN.API.Raw("/people/" + IN.User.getMemberId() + ":(first-name,last-name,email-address,picture-url)")
                          .method("GET")
                          .result(function (userResponse) {
                              var handleAuthentication = function (avatarUrl) {
                                  var userModel = getUserModel(userResponse.firstName, undefined, userResponse.lastName, userResponse.emailAddress, avatarUrl);
                                  var authenticationModel = getAuthenticationModel(IN.User.getMemberId(), userResponse.emailAddress, "linkedin_oauth", "linkedin");
                                  var loginData = serializeModel(userModel) + "&" + serializeModel(authenticationModel);

                                  loginData = appendReturnUrl(loginData);
                                  loginData = appendRememberMe(loginData, element);

                                  if (typeof userResponse.emailAddress == "string" && userResponse.emailAddress.length > 0) {
                                      $.ajax({
                                          url: options.LoginUrl,
                                          type: "POST",
                                          contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                                          data: loginData,
                                          success: function (r) {
                                              if (r.Result) {
                                                  w.location.href = r.RedirectionUrl;
                                              }
                                              else {
                                                  if (typeof r.Message == "string") {
                                                      alert(r.Message);
                                                  }

                                                  if (r.CanContinueRegistration) {
                                                      handleAfterSocialAuthorization(userModel, authenticationModel);
                                                  }
                                              }
                                          },
                                          error: function (r) {
                                              masterPage.ajaxError(r);
                                          }
                                      });
                                  }
                                  else {
                                      handleAfterSocialAuthorization(userModel, authenticationModel);
                                  }
                              };

                              IN.API.Raw("/people/" + IN.User.getMemberId() + "/picture-urls::(original)")
                                    .method("GET")
                                    .result(function (pictureUrlsResponse) {
                                        if (pictureUrlsResponse.values.length > 0) {
                                            handleAuthentication(pictureUrlsResponse.values[0]);
                                        }
                                        else {
                                            handleAuthentication(userResponse.pictureUrl);
                                        }
                                    })
                                    .error(function () {
                                        handleAuthentication(userResponse.pictureUrl);
                                    });
                          })
                         .error(linkedInApiErrorCallback);
                };

                self.onFacebookLoginClick = function (e) {
                    e.preventDefault();

                    var element = $(e.target);
                    var options = {
                        scope: "public_profile,email",
                        return_scopes: true
                    };

                    FB.login(function(response) {
                        facebookStatusChangeCallback(response, element);
                    }, options);
                };
                self.onLinkedInLoginClick = function (e) {
                    e.preventDefault();

                    if (options.LinkedInUseRedirect) {
                        var url = options.LinkedInAuthorizationUrl + "?response_type=code&client_id=" + options.LinkedInAppId + "&redirect_uri=" + encodeURIComponent(options.LinkedInRedirectUri) + "&state=" + encodeURIComponent(options.LinkedInAuthenticationState);

                        window.location.href = url;
                        return;
                    }

                    var element = $(e.target);

                    IN.User.authorize(function() {
                        linkedInStatusChangeCallback(element);
                    });
                };
                w.fbAsyncInit = function () {
                    FB.init({
                        appId: options.FacebookAppId,
                        cookie: true,
                        xfbml: false,
                        version: "v2.5"
                    });
                };

                (function (d, s, id) {
                    var js, fjs = d.getElementsByTagName(s)[0];
                    if (d.getElementById(id)) return;
                    js = d.createElement(s); js.id = id;
                    js.src = "//connect.facebook.net/en_US/sdk.js";
                    fjs.parentNode.insertBefore(js, fjs);
                }(document, "script", "facebook-jssdk"));
            };

            init();
        };

        masterPage.socksHelper = masterPage.socksHelper || new socksHelperConstructor();
	});
})(jQuery, window);