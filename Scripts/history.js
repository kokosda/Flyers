(function ($, w) {
	$(function () {
		var historyConstructor = function () {
			var self = this;
			var states = {};
			var stateCounter = 0;

			var stateConstructor = function (query, setStateCallback) {
				var self = this;
				var html = $("#content").html();
				
				stateCounter++;

				self.setState = function () {
				    $("#content").html(html);
				    if (typeof self.setStateCallback == "function") {
				        self.setStateCallback();
				    }
					else if (typeof setStateCallback == "function") {
						setStateCallback();
					}
				};

				if (query) {
				    var key = stateCounter.toString() + "|" + query;
				    states[key] = self;
					history.pushState(key, null, query);
				}
			};

			self.pushState = function (query, setStateCallback) {
				new stateConstructor(query, setStateCallback);
			};

			var initialState = new stateConstructor();

			self.setInitialSetStateCallback = function (callback) {
			    if (typeof callback == "function") {
			        initialState.setStateCallback = callback;
			    }
			};

			window.onpopstate = function (e) {
				var key = e.state;
				var state = states[key] || initialState;

				state.setState();
			};
		};

		var h = w.masterPage.history;

		w.masterPage.history = h || new historyConstructor();
	});
})(jQuery, window);