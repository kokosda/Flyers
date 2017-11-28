(function ($, w) {
	$(function () {
	    var getPageNumber = function () {
	        return parseInt($("#hiddenPageNumber").val());
	    };

		var states = {};

		var stateConstructor = function (query) {
			var self = this;
			var html = $("#content").html();

			self.setState = function () {
			    $("#content").html(html);
			    initHandlers();
			};

			if (query) {
				states[query] = self;
				history.pushState(query, null, query);
			}
		};

		var initialState = new stateConstructor();

		var handleFilterChange = function (e) {
		    var query = "";

		    if (e.data && e.data["query"]) {
		        query = e.data["query"];
		    }
		    else {
		        var form = $(e.target).closest("form");

		        query = form.attr("action") + "?";

		        if ($(e.target).attr("id") != "hiddenPrices") {
		            $("#hiddenPrices").val("");
		        }
		        else {
		            if (query.indexOf("page=") < 0) {
		                if (getPageNumber() > 1) {
		                    query += "page=" + getPageNumber().toString() + "&";
		                }
		            }
		        }

		        form.find("select,input[type='radio']:checked,input[type='hidden'].data").each(function () {
		            var val = $(this).val();
		            var name = $(this).data("clientname") ? $(this).data("clientname") : $(this).attr("name");

		            if (typeof val == "string" && val.length > 0) {
		                query += name + "=" + val + "&";
		            }
		        });

		        if (query.lastIndexOf("&") == query.length - 1) {
		            query = query.substr(0, query.length - 1);
		        }
		    }

			$.ajax({
				url: query,
				type: "GET",
				success: function (r) {
				    var html = $("#content", r).html();

					$("#content").html(html);
					initHandlers();

					new stateConstructor(query);
				},
				error: masterPage.ajaxError
			});
		};
		
		var handlePricesChange = function () {
			var id = $(this).attr("id");
			var index = parseInt(id.split("_")[1], 10);
			var pageSize = parseInt($("#hiddenPageSize").val());
			var absoluteIndex = (getPageNumber() - 1) * pageSize + index;
			var prices = $("#hiddenPrices").val();

			if (typeof prices == "string") {
				var splittedPrices = (prices.length > 0 ? prices.split(",") : []);
				splittedPrices.forEach(function (item, i) {
					splittedPrices[i] = parseInt(item);
				});

				if ($(this).is(":checked")) {
					splittedPrices.push(absoluteIndex);
					splittedPrices = splittedPrices.sort(function (a, b) { return a - b; });
				}
				else {
					var ioAbsoluteIndex = splittedPrices.indexOf(absoluteIndex);
					if (ioAbsoluteIndex >= 0) {
						splittedPrices.splice(ioAbsoluteIndex, 1);
					}
				}

				prices = splittedPrices.join();
			}

			$("#hiddenPrices").val(prices);
			$("#hiddenPrices").trigger("change");
		};

		var handlePageClick = function (e) {
		    e.preventDefault();
		    e.data = { query: $(e.target).attr("href") };
		    handleFilterChange(e);
		};

		var initHandlers = function () {
		    $(".select-state form").find("select,input[type='radio'],input[type='hidden'].data").off().on("change", handleFilterChange);
		    $(".price .tables table:first input[type='checkbox']").off().on("change", handlePricesChange);
		    $(".pager li a").off().on("click", handlePageClick);
		    $(".price form input[type='submit']").off().on("click", function (e) {
		        e.preventDefault();
		        w.location.href = $(this).closest("form").attr("action");
		    });
		};

		initHandlers();

		window.onpopstate = function (e) {
			var query = e.state;
			var state = states[query] || initialState;

			state.setState();
		};
	});
})(jQuery, window);